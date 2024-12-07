using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace universitycollege.finding.model
{
    public class Path
    {
        private Point _startPoint;
        private Point _endPoint;
        private List<Point> _pathList = new List<Point>();

        public List<Point> PathList => _pathList;
        public Point StartPoint => _startPoint;
        public Point EndPoint => _endPoint;

        public Path(Point startPoint, Point endPoint) 
        { 
            _startPoint = startPoint;
            _endPoint = endPoint;

            _pathList.Add(startPoint);
        }

        public void AddPoint(Point point)
        {
            _pathList.Add(point);
        }

        public void RemovePoint(Point point) 
        { 
            _pathList.Remove(point);
        }

        public void StopPath()
        {
            _pathList.Add(_endPoint);
        }
    }
}
