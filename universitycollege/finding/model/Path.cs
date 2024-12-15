using System.Collections.Generic;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    /// <summary>
    /// Класс пути по карте
    /// </summary>
    public class Path
    {
        private List<Coords> _pathCoords;
        public List<Coords> PathCoords
        {
            get { return _pathCoords; }
            set { _pathCoords = value; }
        }

        public Path(Map map)
        {
            PathFinder pathfinder = new PathFinder(map);
            _pathCoords = pathfinder.FindPath(new Coords(0, 0), new Coords(map.MapSizeX - 1, map.MapSizeY - 1));
        }

        public Path(List<Coords> pathCoords) 
        { 
            _pathCoords = pathCoords; 
        }

        public Path(Map map, Coords start, Coords end)
        {
            PathFinder pathfinder = new PathFinder(map);
            _pathCoords = pathfinder.FindPath(start, end);    
        }
    }
}
