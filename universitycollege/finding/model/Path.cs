using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace universitycollege.finding.model
{
    public class Path
    {
        private sbyte[,] _startPoint;
        private sbyte[,] _endPoint;
        private List<sbyte[,]> _pathList = new List<sbyte[,]>();

        public List<sbyte[,]> PathList => _pathList;
        public sbyte[,] StartPoint => _startPoint;
        public sbyte[,] EndPoint => _endPoint;

        public Path(sbyte[,] startPoint, sbyte[,] endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;

            _pathList.Add(startPoint);
        }

        public void AddPoint(sbyte[,] point)
        {
            _pathList.Add(point);
        }

        public void RemovePoint(sbyte[,] point)
        {
            _pathList.Remove(point);
        }

        public void StopPath()
        {
            _pathList.Add(_endPoint);
        }
    }
}
