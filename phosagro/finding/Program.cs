using phosagro.finding.model;
using System;
using System.Collections.Generic;

namespace phosagro.finding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<HillPoint> points = new List<HillPoint>();

            Map map = new Map(5, 5);

            StartPoint start = new StartPoint(0, 0);
            EndPoint end = new EndPoint(5, 5);
            HillPoint hill = new HillPoint(2, 2, 2);

            points.Add(hill);

            foreach (HillPoint p in points) 
            {
                map.MapArr[p.X, p.Y] = p.TopCoefficient;
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(map.MapArr[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
