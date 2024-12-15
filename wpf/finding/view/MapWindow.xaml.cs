using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using universitycollege.finding.controller;
using universitycollege.finding.model;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.view
{
    public partial class MapWindow : Window
    {
        private Map _map;
        private TopologyGenerator _generator;
        private Dictionary<int, Brush> _colors;
        private PatternController _patternController;

        private Pattern _selectedPattern = null;

        private Point _lastMousePosition;
        private bool _isDragging = false;
        private bool _isDrawing = false;
        private double _scale = 1.0;
        private double _offsetX = 0;
        private double _offsetY = 0;

        private const int CellSize = 20;
        private int MapWidth;
        private int MapHeight;

        public MapWindow(int x, int y)
        {
            InitializeComponent();
            InitializeColors();
            DrawMap(x, y);
            DrawGrid();
            LoadPatterns();
        }

        private void InitializeColors()
        {
            _colors = new Dictionary<int, Brush>
            {
                { -3, Brushes.DodgerBlue },
                { -2, Brushes.DeepSkyBlue },
                { -1, Brushes.SkyBlue },
                { 0, Brushes.White },
                { 1, Brushes.LimeGreen },
                { 2, Brushes.DarkGreen },
                { 3, Brushes.Gold },
                { 4, Brushes.DarkOrange },
                { 5, Brushes.Firebrick },
                { 6, Brushes.White }
            };
        }

        private void LoadPatterns()
        {
            _patternController = new PatternController();
            var patterns = _patternController.AllPatternsList;

            List<PatternPreview> patternPreviews = new List<PatternPreview>();

            foreach (var pattern in patterns)
            {
                var canvas = CreatePatternPreviewCanvas(pattern);
                patternPreviews.Add(new PatternPreview { Name = pattern.Name, PreviewCanvas = canvas, Pattern = pattern });
            }

            PatternListBox.ItemsSource = patternPreviews;
            PatternListBox.SelectedIndex = 0;
        }

        private Canvas CreatePatternPreviewCanvas(Pattern pattern)
        {
            Canvas previewCanvas = new Canvas();
            sbyte[,] patternArr = pattern.PatternArr;

            Map tempMap = new Map(patternArr.GetLength(0), patternArr.GetLength(1));
            TopologyGenerator tempGenerator = new TopologyGenerator(tempMap);

            tempGenerator.AddPatternTopology(pattern, new Coords(tempMap.MapSizeX / 2, tempMap.MapSizeY / 2));

            int margin = 10;

            for (int y = 0; y < tempMap.MapSizeY; y++)
            {
                for (int x = 0; x < tempMap.MapSizeX; x++)
                {
                    int height = tempMap.MapArr[x, y];

                    Rectangle rect = null;

                    if (height != 0)
                    {
                        rect = new Rectangle
                        {
                            Width = margin,
                            Height = margin,
                            Fill = _colors[height],
                        };
                    }

                    if (rect != null)
                    {
                        Canvas.SetLeft(rect, x * margin);
                        Canvas.SetTop(rect, y * margin);
                        previewCanvas.Children.Add(rect);
                    }
                }
            }

            return previewCanvas;
        }

        private void UpdateMap()
        {
            MapCanvas.Children.Clear();
            PathFinder pathFinder = new PathFinder(_map);
            List<Map.Coords> path = pathFinder.FindPath(new Map.Coords(0, 0), new Map.Coords(_map.MapSizeX - 1, _map.MapSizeY - 1));

            for (int y = 0; y < _map.MapSizeY; y++)
            {
                for (int x = 0; x < _map.MapSizeX; x++)
                {
                    int height = _map.MapArr[x, y];
                    Rectangle rect = CreateRectangle(x, y, height, path);
                    if (rect != null)
                    {
                        MapCanvas.Children.Add(rect);
                    }
                }
            }
        }

        private Rectangle CreateRectangle(int x, int y, int height, List<Map.Coords> path)
        {
            Rectangle rect = null;

            if (path != null && path.Contains(new Map.Coords(x, y)))
            {
                rect = new Rectangle
                {
                    Width = CellSize,
                    Height = CellSize,
                    Fill = Brushes.Black,
                };
            }
            else if (height != 0)
            {
                rect = new Rectangle
                {
                    Width = CellSize,
                    Height = CellSize,
                    Fill = _colors[height],
                };
            }

            if (rect != null)
            {
                Canvas.SetLeft(rect, x * CellSize);
                Canvas.SetTop(rect, y * CellSize);
            }

            return rect;
        }

        private void DrawMap(int x, int y)
        {
            MapHeight = y;
            MapWidth = x;

            _map = new Map(MapWidth, MapHeight);
            _generator = new TopologyGenerator(_map);

            UpdateMap();
        }

        private void MapCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double zoomFactor = 0.1;
            double oldScale = _scale;

            if (e.Delta > 0)
            {
                _scale += zoomFactor;
            }
            else if (e.Delta < 0)
            {
                double minScaleX = ActualWidth / (MapWidth * CellSize);
                double minScaleY = ActualHeight / (MapHeight * CellSize);
                double minScale = Math.Max(minScaleX, minScaleY);

                if (_scale > minScale)
                {
                    _scale -= zoomFactor;
                    if (_scale < minScale) _scale = minScale;
                }
            }

            Point mousePosition = e.GetPosition(MapCanvas);
            _offsetX = (_offsetX - mousePosition.X) * (_scale / oldScale) + mousePosition.X;
            _offsetY = (_offsetY - mousePosition.Y) * (_scale / oldScale) + mousePosition.Y;

            UpdateTransform();
        }

        private void MapCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                _isDragging = true;
                _lastMousePosition = e.GetPosition(MapCanvas);
                MapCanvas.CaptureMouse();
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                _isDrawing = true;
                //if (PatternListBox.SelectedItem is Pattern selectedPattern)
                //{
                    DrawPatternAtMousePosition(e);
                //}
            }
        }

        private void MapCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           if (_isDragging)
            {
                _isDragging = false;
                MapCanvas.ReleaseMouseCapture();
            }
            else if (_isDrawing)
            {
                _isDrawing = false;
                MapCanvas.ReleaseMouseCapture();
            }
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (MapCanvas.IsMouseOver) // Проверка, находится ли мышь над канвасом
            {
                if (_isDragging)
                {
                    Point currentMousePosition = e.GetPosition(MapCanvas);
                    Vector delta = currentMousePosition - _lastMousePosition;
                    _offsetX += delta.X;
                    _offsetY += delta.Y;
                    UpdateTransform();
                    _lastMousePosition = currentMousePosition;
                }
            }
        }

        private void DrawPatternAtMousePosition(MouseEventArgs e)
        {
            if (_isDrawing && MapCanvas.IsMouseOver) // Дополнительная проверка
            {
                Point mousePosition = e.GetPosition(MapCanvas); // Измените на MapCanvas
                int cellX = (int)(mousePosition.X / CellSize);
                int cellY = (int)(mousePosition.Y / CellSize);

                if (cellX >= 0 && cellX < _map.MapSizeX && cellY >= 0 && cellY < _map.MapSizeY) // Проверка границ
                {
                    _generator.AddPatternTopology(_selectedPattern, new Coords(cellX, cellY));
                    UpdateMap();
                }
            }
        }

        private void UpdateTransform()
        {
            MapCanvas.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(_scale, _scale),
                    new TranslateTransform(_offsetX, _offsetY)
                }
            };
        }

        private void DrawGrid()
        {
            for (int row = 0; row < MapHeight; row++)
            {
                for (int col = 0; col < MapWidth; col++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,
                        Fill = Brushes.Transparent,
                        StrokeThickness = 0
                    };

                    Canvas.SetLeft(rect, col * CellSize);
                    Canvas.SetTop(rect, row * CellSize);

                    MapCanvas.Children.Add(rect);
                }
            }
        }

        private void PatternListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatternListBox.SelectedItem is PatternPreview selectedPreview)
            {
                _selectedPattern = selectedPreview.Pattern;
            }
        }

    }
}
