using universitycollege.finding.view;
using System;

namespace universitycollege.finding.model
{
    public class Map
    {
        private Point[,] _mapArr;

        private int _mapSizeX;
        private int _mapSizeY;

        public Point[,] MapArr => _mapArr;
        public int mapSizeX => _mapSizeX;
        public int mapSizeY => _mapSizeY;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dimensionX">Width</param>
        /// <param name="dimensionY">Height</param>
        /// <exception cref="IndexOutOfMapException"></exception>
        public Map(int dimensionX, int dimensionY)
        {
            if (dimensionX > (int)InMemory.Constants.MAX_X_MAP || dimensionY > (int)InMemory.Constants.MAX_Y_MAP)
            {
                string message = "";

                if (dimensionX > (int)InMemory.Constants.MAX_X_MAP)
                {
                    message += $"Max X value is: {InMemory.Constants.MAX_X_MAP}\n";
                }
                if (dimensionY > (int)InMemory.Constants.MAX_Y_MAP)
                {
                    message += $"Max Y value is: {InMemory.Constants.MAX_Y_MAP}";
                }

                throw new IndexOutOfRangeException(message);
            }
            else
            {
                _mapSizeX = dimensionX;
                _mapSizeY = dimensionY;

                _mapArr = new Point[dimensionX, dimensionY];
                for (int x = 0; x < dimensionX; x++)
                {
                    for (int y = 0; y < dimensionY; y++)
                    {
                        _mapArr[x, y] = new Point(x, y);
                    }
                }
            }
        }

        public int GetHeight(int x, int y)
        {
            return _mapArr[x, y].Height;
        }

        public string GetCoordsPoint(int x, int y)
        {
            return "" + _mapArr[x, y].X + " " + _mapArr[x, y].Y;
        }

        /// <summary>
        /// Updating a point by coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="point"></param>
        public void Update(int x, int y, Point point)
        {
            _mapArr[x, y] = point; // In the array, x and y are swapped
        }

        public bool PointIsInAMap(Point point)
        {
            if (point.X < _mapSizeX && point.Y < _mapSizeY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInAMap(int x, int y)
        {
            if ((x < mapSizeX && y < mapSizeY) && (x >= 0 && y >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsXInAMap(int x)
        {
            if ((x < mapSizeX) && (x >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPointAreHigher(Point point)
        {
            if (_mapArr[point.X, point.Y].Height < point.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPointAreBelow(Point point)
        {
            if (_mapArr[point.X, point.Y].Height > point.Height && _mapArr[point.X, point.Y].Height < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
