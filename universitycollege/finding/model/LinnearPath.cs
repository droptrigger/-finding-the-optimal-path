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
        /// Метод получения строгого пути по диагонали от левого верхнего угла до правого нижнего
        /// </summary> 
        /// <param name="map">Ссылка на объект класса Map</param>
        public void GetLinnearPath(Map map)
        {
            GetLinnearPath(map, new Map.Coords(0, 0), new Map.Coords(map.MapSizeX - 1, map.MapSizeY - 1));
        }

        /// <summary>
        /// Метод получения строгого пути по диагонали от точки start до точки end
        /// </summary>
        /// <param name="map">Ссылка на объект класса Map</param>
        /// <param name="start">Координата, от которой идти</param>
        /// <param name="end">Координата, к которой идти</param>
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
