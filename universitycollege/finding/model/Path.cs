using System;
using System.Collections.Generic;
using universitycollege.finding.view;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class Path
    {
        private List<Map.Coords> _pathCoords;
        private Map _map;
        private double _amount;

        public List<Map.Coords> PathCoords => _pathCoords;
        public double Amount => _amount;

        public Path(Map map, InMemory.TypesOfPath type)
        {
            if (type == InMemory.TypesOfPath.HARD)
            {
                GetHardPath(map);
            }
            else
            {
                _map = map;
                PathFinder pathfinder = new PathFinder(map);
                _pathCoords = pathfinder.FindPath(new Map.Coords(0, 0), new Map.Coords(map.MapSizeX - 1, map.MapSizeY - 1));
                GetAmount();
            }
        }

        public Path(List<Map.Coords> pathCoords, Map map) 
        { 
            _pathCoords = pathCoords; 
            _map = map;
            GetAmount();
        }

        public Path(Map map, Map.Coords start, Map.Coords end, InMemory.TypesOfPath type)
        {
            if (type == InMemory.TypesOfPath.HARD)
            {
                GetHardPath(map, start, end);
            }
            else
            {
                _map = map;
                PathFinder pathfinder = new PathFinder(map);
                _pathCoords = pathfinder.FindPath(start, end);
            }
        }
        
        /// <summary>
        /// Метод получения строгого пути по диагонали от левого верхнего угла до правого нижнего
        /// </summary>
        /// <param name="map">Ссылка на объект класса Map</param>
        public void GetHardPath(Map map)
        {
            _map = map;

            Map.Coords end = new Map.Coords(_map.MapSizeX - 1, _map.MapSizeY - 1);

            int x = 0;
            int y = 0;

            List<Map.Coords> returnList = new List<Map.Coords>();

            while (true)
            {
                returnList.Add(new Map.Coords(x, y));

                if (x == end.x && y == end.y)
                {
                    _pathCoords = returnList;
                    GetAmount();
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
                    if (y > end.x)
                        y--;

                    else
                        y++;
                }
            }
        }

        /// <summary>
        /// Метод получения строгого пути по диагонали от точки start до точки end
        /// </summary>
        /// <param name="map">Ссылка на объект класса Map</param>
        /// <param name="start">Координата, от которой идти</param>
        /// <param name="end">Координата, к которой идти</param>
        public void GetHardPath(Map map, Map.Coords start, Map.Coords end)
        {
            _map = map;

            int x = start.x;
            int y = start.y;

            List<Map.Coords> returnList = new List<Map.Coords>();

            while (true)
            {
                returnList.Add(new Map.Coords(x, y));

                if (x == end.x && y == end.y)
                {
                    _pathCoords = returnList;
                    GetAmount();
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
                    if (y > end.x)
                        y--;

                    else
                        y++;
                }
            }
        }

        /// <summary>
        /// Метод получения стоимости перемещения
        /// </summary>
        private void GetAmount() // TODO: Доделать
        {
            double amount = (int)InMemory.AmountRoad.DEFAULT_ROAD;

            sbyte tempHeight = _map.GetHeight(_pathCoords[0]);

            for (int i = 1; i < _pathCoords.Count; i++)
            {
                int height = _map.GetHeight(_pathCoords[i]);
                if (height < 0)
                {
                    amount += (short)InMemory.AmountRoad.BRIDGE + (short)InMemory.AmountRoad.BRIDGE_SUPPORT * -height;
                }
                else
                {
                    sbyte differenceHeight = Math.Abs((sbyte)(tempHeight - _map.GetHeight(_pathCoords[i]))); 

                    if (differenceHeight > 0)
                    {
                        amount += (short)InMemory.AmountRoad.UPPER_ROAD * differenceHeight;
                    }
                    else
                    {
                        amount += (short)InMemory.AmountRoad.DEFAULT_ROAD;
                    }
                }
            }

            _amount = amount;
        }

        public static bool operator > (Path path1, Path path2)
        {
            return path1.Amount > path2.Amount;
        }

        public static bool operator <(Path path1, Path path2)
        {
            return path1.Amount < path2.Amount;
        }
    }
}
