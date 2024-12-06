using universitycollege.finding.data;
using universitycollege.finding.exception;

namespace universitycollege.finding.model
{
    public class Point
    {
        private int _x;
        private int _y;
        private int _height = 0;

        public int Height => _height;
        public int X => _x;
        public int Y => _y;

        /// <summary>
        /// Сonstructor
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="height">The height of the hill</param>
        /// <exception cref="HeightOutOfMaxValueException"></exception>
        public Point(int x, int y, int height)
        {
            if (height > (int)InMemory.Constants.MAX_COEFF_VALUE || height < -(int)InMemory.Constants.MAX_COEFF_VALUE)
            {
                throw new HeightOutOfMaxValueException($"Max value = {InMemory.Constants.MAX_COEFF_VALUE}");
            }
            else
            {
                this._x = x;
                this._y = y;
                _height = height;
            }
        }

        /// <summary>
        /// Сonstructor
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
            _height = 0;
        }

        public override string ToString() 
        {
            return $"{_height}";
        }
    }
}
