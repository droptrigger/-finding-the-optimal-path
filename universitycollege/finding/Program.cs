using System;
using System.Diagnostics;
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

            const int SIZE_X = (int)InMemory.Constants.MAX_X_MAP;
            const int SIZE_Y = (int)InMemory.Constants.MAX_Y_MAP;

            const int COUNT_GENERATE = 1000;

            Map map = new Map(SIZE_X, SIZE_Y);

            Random rnd = new Random();
            TopologyGenerator generator = new TopologyGenerator();

            int timestart = (int)DateTime.Now.Ticks;

            //Генерация рандомных холмов
            for (int i = 0; i < COUNT_GENERATE; i++)
            {
                generator.AddHill(
                    map: map,
                    x: rnd.Next(SIZE_X - 1),
                    y: rnd.Next(SIZE_Y - 1),
                    height: (sbyte)(rnd.Next(InMemory.Colors.Count) - 3));
            }

            //ReadMap(map, SIZE_X, SIZE_Y);

            stopwatch.Stop();
            Colorful.Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс", System.Drawing.Color.Cyan);
            long memoryAfter = GC.GetTotalMemory(true);
            Colorful.Console.WriteLine($"Использование памяти: " +
                $"{(memoryAfter - memoryBefore)} Байт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024} Килобайт | " +
                $"{(double)(memoryAfter - memoryBefore) / 1024 / 1024} Мегабайт",
                System.Drawing.Color.Cyan);
        }

        public static void ReadMap(Map map, int sizeX, int sizeY)
        {
            for (int x = 0; x < sizeY; x++)
            {
                for (int y = 0; y < sizeX; y++)
                {
                    int height = map.MapArr[x, y];

                    var color = System.Drawing.ColorTranslator.FromHtml(InMemory.Colors[height]);
                    Colorful.Console.Write($"{map.MapArr[x, y]} ", color);
                }

                Colorful.Console.WriteLine();
            }
        }
    }
}
