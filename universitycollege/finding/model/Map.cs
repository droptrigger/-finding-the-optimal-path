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
        public int MapSizeX => _mapSizeX;
        public int MapSizeY => _mapSizeY;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dimensionX">Ширина</param>
        /// <param name="dimensionY">Высота</param>
        /// <exception cref="IndexOutOfMapException">Ошибка, если превышены допустимые значения высоты и/или ширины</exception>
        public Map(int dimensionX, int dimensionY)
        {
            if (dimensionX > (int)InMemory.Constants.MAX_X_MAP || dimensionY > (int)InMemory.Constants.MAX_Y_MAP)
            {
                string message = "";

                if (dimensionX > (int)InMemory.Constants.MAX_X_MAP)
                {
                    message += $"Максимальное значение ширины карты: {InMemory.Constants.MAX_X_MAP}";
                }
                if (dimensionY > (int)InMemory.Constants.MAX_Y_MAP)
                {
                    message += $"\nМаксимальное значение высоты карты: {InMemory.Constants.MAX_Y_MAP}";
                }

                throw new IndexOutOfRangeException(message);
            }
            else
            {
                _mapSizeX = dimensionX;
                _mapSizeY = dimensionY;

                Random rnd = new Random();
                _mapArr = new sbyte[dimensionX, dimensionY];
            }
        }

        public sbyte GetHeight(Coords coords)
        {
            return _mapArr[coords.x, coords.y];
        }

        /// <summary>
        /// Updating a point by coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="height">heigh</param>
        public void Update(Coords coords, sbyte height)
        {
            _mapArr[coords.x, coords.y] = height;
        }

        public bool PointIsInAMap(Coords coords)
        {
            return (coords.x < _mapSizeX && coords.y < _mapSizeY);
        }

        public bool IsInAMap(Coords coords)
        {
            return ((coords.x < MapSizeX && coords.y < MapSizeY) && (coords.x >= 0 && coords.y >= 0));
        }

        public bool IsXInAMap(int x)
        {
            return ((x < MapSizeX) && (x >= 0));
        }

        public bool IsYInAMap(int y)
        {
            return ((y < MapSizeY) && (y >= 0));
        }

        public bool IsPointAreHigher(Coords coords, sbyte height)
        {
            return (_mapArr[coords.x, coords.y] < height);
        }

        public bool IsPointAreBelow(Coords coords, sbyte height)
        {
            return (_mapArr[coords.x, coords.y] > height && _mapArr[coords.x, coords.y] < 1);
        }

        public void AddMap(Map addedMap, Coords centerCoords)
        {
            
        }

        /// <summary>
        /// Метод получения стоимости перемещения
        /// </summary>
        public int GetAmount(Path path)
        {
            int amount = (int)InMemory.AmountRoad.DEFAULT_ROAD;

            sbyte tempHeight = GetHeight(path.PathCoords[0]);

            for (int i = 1; i < path.PathCoords.Count; i++)
            {
                sbyte height = GetHeight(path.PathCoords[i]);
                if (height < 0)
                {
                    amount += (short)InMemory.AmountRoad.BRIDGE + (short)InMemory.AmountRoad.BRIDGE_SUPPORT * -height;
                }
                else
                {
                    sbyte differenceHeight = Math.Abs((sbyte)(tempHeight - GetHeight(path.PathCoords[i])));

                    if (differenceHeight > 0)
                    {
                        amount += (short)InMemory.AmountRoad.UPPER_ROAD * differenceHeight;
                    }
                    else
                    {
                        amount += (short)InMemory.AmountRoad.DEFAULT_ROAD;
                    }
                }
                tempHeight = height;
            }

            return amount;
        }
    }
}
 