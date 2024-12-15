using System.Collections.Generic;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class Pattern
    {
        private sbyte[,] _patternArr;
        private Coords _coordsCenter;
        private string _name;

        public string Name => _name;
        public sbyte[,] PatternArr => _patternArr;

        /// <summary>
        /// Constructor for reading a pattern from a folder
        /// </summary>
        /// <param name="fileName">Name of the pattern</param>
        public Pattern(string fileName)
        {
            _patternArr = ReaderPatternFile.GetPatternArr(fileName);
            _name = fileName;
            GetCenterCoords();
        }

        /// <summary>
        /// Constructor for creating a new pattern in a folder
        /// </summary>
        /// <param name="patternArr">Two-dimensional array of numbers</param>
        /// <param name="fileName">Name of the pattern</param>
        public Pattern(sbyte[,] patternArr, string fileName)
        {
            _patternArr = patternArr;
            _name = fileName;
            ReaderPatternFile.CreatePattern(patternArr, fileName);
            GetCenterCoords();
        }

        private void GetCenterCoords()
        {
            _coordsCenter.x = _patternArr.GetLength(0) / 2;
            _coordsCenter.y = _patternArr.GetLength(1) / 2;
        }

        /// <summary>
        /// Method to get the coordinates where the pattern will be drawn
        /// </summary>
        /// <param name="map">Reference to an object of the Map class</param>
        /// <param name="centerCoords">Coordinates of the point where the center of the pattern will be</param>
        /// <returns></returns>
        public Dictionary<Coords, sbyte> GetPatternCoords(Map map, Coords centerCoords)
        {
            Dictionary<Coords, sbyte> tempDict = new Dictionary<Coords, sbyte>();

            for (int collums = 0; collums < _patternArr.GetLength(0); collums++)
            {
                for (int row = 0; row < _patternArr.GetLength(1); row++)
                {
                    Coords mapCoords = new Coords(centerCoords.x + collums - (_patternArr.GetLength(0) / 2),
                                           centerCoords.y + row - (_patternArr.GetLength(1) / 2));

                    if (_patternArr[collums, row] == 6)
                        tempDict[mapCoords] = 0;
                    
                    else if (map.IsInAMap(mapCoords) &&
                        (map.IsPointAreBelow(mapCoords, _patternArr[collums, row]) || map.IsPointAreHigher(mapCoords, _patternArr[collums, row])))
                        tempDict[mapCoords] = _patternArr[collums, row];
                }
            }

            return tempDict;
        }

        /// <summary>
        /// Returns a string representation of the pattern
        /// </summary>
        /// <returns>A string containing the name and the pattern array</returns>
        public override string ToString()
        {
            string pattern = _name + "\n";

            for (int i = 0; i < _patternArr.GetLength(0); i++)
            {
                for (int j = 0; j < _patternArr.GetLength(1); j++)
                {
                    pattern += _patternArr[i, j] + " ";
                }
                pattern += "\n";
            }

            return pattern;
        }
    }
}
