using System;
using System.Collections.Generic;

namespace universitycollege.finding.model
{
    public class Path
    {
        private List<Map.Coords> _pathCoords;
        private Map _map;
        private double _amount;

        public List<Map.Coords> PathCoords => _pathCoords;
        public double Amount => _amount;

        public Path(List<Map.Coords> pathCoords, Map map) 
        { 
            _pathCoords = pathCoords; 
            _map = map;
            GetAmount();
        }

        public Path(Map map, Map.Coords start, Map.Coords end)
        {
            _map = map;
            PathFinder pathfinder = new PathFinder(map);
            _pathCoords = pathfinder.FindPath(start, end);
        }

        public Path(Map map)
        {
            _map = map;
            PathFinder pathfinder = new PathFinder(map);
            _pathCoords = pathfinder.FindPath(new Map.Coords(0, 0), new Map.Coords(map.MapSizeX - 1, map.MapSizeY - 1));
            GetAmount();
        }

        private void GetAmount()
        {
            double amount = 0;

            foreach (Map.Coords coords in _pathCoords) 
            {
                amount += Math.Abs(_map.GetHeight(coords)) + 1;
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
