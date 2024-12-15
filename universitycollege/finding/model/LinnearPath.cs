using System.Collections.Generic;

namespace universitycollege.finding.model
{
    public class LinnearPath : Path
    {
        public LinnearPath(Map map) : base(map)
        {
            GetLinnearPath(map);
        }

        public LinnearPath(Map map, Map.Coords start, Map.Coords end) : base(map, start, end)
        {
            GetLinnearPath(map, start, end);
        }

        /// <summary>
        /// Method to get a strict diagonal path from the top left corner to the bottom right
        /// </summary> 
        /// <param name="map">Reference to the Map class object</param>
        public void GetLinnearPath(Map map)
        {
            GetLinnearPath(map, new Map.Coords(0, 0), new Map.Coords(map.MapSizeX - 1, map.MapSizeY - 1));
        }

        /// <summary>
        /// Method to get a strict diagonal path from the start point to the end point
        /// </summary>
        /// <param name="map">Reference to the Map class object</param>
        /// <param name="start">Coordinate from which to start</param>
        /// <param name="end">Coordinate to which to go</param>
        public void GetLinnearPath(Map map, Map.Coords start, Map.Coords end)
        {
            int x = start.x;
            int y = start.y;

            List<Map.Coords> returnList = new List<Map.Coords>();

            while (true)
            {
                returnList.Add(new Map.Coords(x, y));

                if (x == end.x && y == end.y)
                {
                    PathCoords = returnList;
                    break;
                }

                if (x != end.x)
                {
                    if (x > end.x)
                        x--;
                    else
                        x++;
                }

                if (y != end.y)
                {
                    if (y > end.y)
                        y--;
                    else
                        y++;
                }
            }
        }
    }
}
