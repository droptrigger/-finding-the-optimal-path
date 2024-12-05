using phosagro.finding.data;

namespace phosagro.finding.model
{
    public class Map
    {
        private int[,] _mapArr;

        public int[,] MapArr
        {
            get { return _mapArr; }
            set { _mapArr = value; }
        }

        public Map(int dimensionX, int dimensionY)
        {
            if (dimensionX > (int)InMemory.Constants.MAX_X_MAP || dimensionY > (int)InMemory.Constants.MAX_Y_MAP)
            {
                throw new System.Exception("Error!");
            }
            else 
            {
                _mapArr = new int[dimensionX, dimensionY];
            }

        }
    }
}
