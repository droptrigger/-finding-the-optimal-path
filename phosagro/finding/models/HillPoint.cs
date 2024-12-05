using phosagro.finding.data;

namespace phosagro.finding.model
{
    public class HillPoint : Point
    {
        private int _topСoefficient;


        public int TopCoefficient
        {
            get { return _topСoefficient; }
            set { _topСoefficient = value; } 
        }

        /// <summary>
        /// Сonstructor
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="coefficient">The height of the hill</param>
        public HillPoint(int x, int y, int coefficient) : base(x, y)
        {
            if (coefficient > (int)InMemory.Constants.MAX_COEFF_VALUE)
            {
                throw new System.Exception($"Max value = {InMemory.Constants.MAX_COEFF_VALUE}");
            }
            else
            {
                TopCoefficient = coefficient;
            }
        }
    }
}
