namespace phosagro.finding.model
{
    public class Point
    {
        private int _x;
        private int _y;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
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
        }

        public override string ToString() 
        {
            return $"{X} {Y}";
        }
    }
}
