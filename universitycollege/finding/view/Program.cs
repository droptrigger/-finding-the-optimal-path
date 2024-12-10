using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using universitycollege.finding.controller;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long memoryBefore = GC.GetTotalMemory(true);

            Controller controller = new Controller();
            Map map = controller.GenerateMap(10, 10, 5);

            PathFinder pathfinder = new PathFinder(map);
            List<Map.Coords> path = pathfinder.FindPath(new Map.Coords(0, 0), new Map.Coords(map.mapSizeX - 1, map.mapSizeY - 1));

            DrawMapToConsole(map, path);

            stopwatch.Stop();
            long memoryAfter = GC.GetTotalMemory(true);

            Colorful.Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс", System.Drawing.Color.Cyan);
            Colorful.Console.WriteLine($"Использование памяти: " +
                $"{(memoryAfter - memoryBefore)} Байт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024} Килобайт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024 / 1024} Мегабайт",
                Color.Cyan);
        }


        public static void DrawMapToConsole(Map map, List<Map.Coords> path = null)
        {
            for (int x = 0; x < map.mapSizeY; x++)
            {
                for (int y = 0; y < map.mapSizeX; y++)
                {
                    int height = map.MapArr[x, y];

                    Color color = ColorTranslator.FromHtml(InMemory.Colors[height]);
                    if (path != null && path.Contains(new Map.Coords(x, y)))
                    {
                        Colorful.Console.Write($"{Math.Abs(height)} ", Color.Black);
                    }
                    else
                    {
                        Colorful.Console.Write($"{Math.Abs(height)} ", color);
                    }
                }

                Colorful.Console.WriteLine();
            }
        }
    }
}
