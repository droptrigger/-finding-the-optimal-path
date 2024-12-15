using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using universitycollege.finding.controller;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long memoryBefore = GC.GetTotalMemory(true);

            PatternController patternController = new PatternController();
            List<Pattern> patterns = patternController.AllPatternsList;

            Controller controller = new Controller();
            Map map = controller.GenerateMap(50, 50, 25);

            TopologyGenerator generator = new TopologyGenerator(map);
            // generator.AppPatternTopology(new Pattern("square_four.txt"), new Map.Coords(12, 12));

            foreach (Pattern pattern in patterns)
            {
                Console.WriteLine(pattern);
            }

            Path optimazePath = new Path(map);
            LinnearPath hardPath = new LinnearPath(map);

            if (map.GetAmount(optimazePath) < map.GetAmount(hardPath))
            {
                Console.WriteLine("Путь дешевле чем напрямую");
            }

            DrawMapToConsole(map, optimazePath);

            stopwatch.Stop();
            long memoryAfter = GC.GetTotalMemory(true);

            Colorful.Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс", Color.Cyan);
            Colorful.Console.WriteLine($"Использование памяти: " +
                $"{(memoryAfter - memoryBefore)} Байт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024} Килобайт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024 / 1024} Мегабайт",
                Color.Cyan);
        }

        public static void DrawMapToConsole(Map map, Path path)
        {
            for (int x = 0; x < map.MapSizeY; x++)
            {
                for (int y = 0; y < map.MapSizeX; y++)
                {
                    int height = map.MapArr[x, y];

                    Color color = ColorTranslator.FromHtml(InMemory.ColorsForWrite[height]);
                    if (path.PathCoords != null && path.PathCoords.Contains(new Map.Coords(x, y)))
                    {
                        Colorful.Console.Write($"{Math.Abs(height)} ", Color.Black);
                    }
                    else
                    {
                        Colorful.Console.Write($"{Math.Abs(height)} ", color);
                    }
                }
                Colorful.Console.ResetColor();
                Colorful.Console.WriteLine();
            }
        }
    }
}
