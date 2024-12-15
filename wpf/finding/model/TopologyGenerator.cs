using System.Collections.Generic;
using System.Windows;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class TopologyGenerator
    {
        private Map _map;

        public TopologyGenerator(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Method for drawing based on a pattern
        /// </summary>
        /// <param name="pattern">Reference to an object of the Pattern class</param>
        /// <param name="coords">Coordinates of the center</param>
        public void AddPatternTopology(Pattern pattern, Coords coords)
        {
            Dictionary<Coords, sbyte> PatternCoords = pattern.GetPatternCoords(_map, coords);

            foreach (KeyValuePair<Coords, sbyte> coordsPattern in PatternCoords)
            {
                _map.Update(coordsPattern.Key, coordsPattern.Value);
            }
        }
    }
}