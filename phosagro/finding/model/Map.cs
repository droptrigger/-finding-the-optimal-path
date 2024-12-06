using universitycollege.finding.data;
using universitycollege.finding.exception;

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
                throw new IndexOutOfMapException("Error!");
            }
            else
            {
                _mapSizeX = dimensionX;
                _mapSizeY = dimensionY;

                _mapArr = new Point[dimensionX, dimensionY];
                for (int i = 0; i < dimensionX; i++)
                {
                    for (int j = 0; j < dimensionY; j++)
                    {
                        _mapArr[i, j] = new Point(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Updating a point by coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="point"></param>
        public void Update(int x, int y, Point point)
        {
            _mapArr[y, x] = point;
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
            if (_mapArr[point.Y, point.X].Height < point.Height)
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
