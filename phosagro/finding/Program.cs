using universitycollege.finding.model;
using universitycollege.finding.data;
using System;

namespace universitycollege.finding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 25;
            int m = 25;
            Map map = new Map(n, m);

            Random rnd = new Random();
            TopologyGenerator generator = new TopologyGenerator();

            for (int i = 0; i < 25; i++)
            {
                generator.AddHill(map, new Point(rnd.Next(n - 1), rnd.Next(m - 1), 5));
            }


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int height = map.MapArr[i, j].Height;
                    
                    if (height > 0)
                    {
                        var color = System.Drawing.ColorTranslator.FromHtml(InMemory.Colors[height]);
                        Colorful.Console.Write(map.MapArr[i, j] + "  ", color);
                    }
                    else
                    {
                        Colorful.Console.Write(map.MapArr[i, j] + "  ");
                    }
                }
                Colorful.Console.WriteLine();
            }
        }
    }
}
