using universitycollege.finding.view;
using System;

namespace universitycollege.finding.model
{
    public class Map
    {
        public struct Coords
        {
            public int x, y;

            public Coords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private sbyte[,] _mapArr;

        private int _mapSizeX;
        private int _mapSizeY;

        public sbyte[,] MapArr => _mapArr;
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

                _mapArr = new sbyte[dimensionX, dimensionY];
            }
        }

        public int GetHeight(int x, int y)
        {
            return _mapArr[x, y];
        }

        /// <summary>
        /// Updating a point by coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="height">heigh</param>
        public void Update(int x, int y, sbyte height)
        {
            _mapArr[x, y] = height;
        }

        public bool PointIsInAMap(int x, int y)
        {
            return (x < _mapSizeX && y < _mapSizeY);
        }

        public bool IsInAMap(int x, int y)
        {
            return ((x < mapSizeX && y < mapSizeY) && (x >= 0 && y >= 0));
        }

        public bool IsXInAMap(int x)
        {
            return ((x < mapSizeX) && (x >= 0));
        }

        public bool IsPointAreHigher(int x, int y, int height)
        {
            return (_mapArr[x, y] < height);
        }

        public bool IsPointAreBelow(int x, int y, int height)
        {
            return (_mapArr[x, y] > height && _mapArr[x, y] < 1);
        }
    }
}
 