using System;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int SIZE_X = 40;
            const int SIZE_Y = 40;

            const int COUNT_GENERATE = 200;

            Map map = new Map(SIZE_X, SIZE_Y);

            Random rnd = new Random();
            TopologyGenerator generator = new TopologyGenerator();

            //Генерация рандомных холмов
            for (int i = 0; i < COUNT_GENERATE; i++)
            {
                generator.AddHill(
                    map,
                    new Point(rnd.Next(SIZE_X - 1), rnd.Next(SIZE_Y - 1), rnd.Next(InMemory.Colors.Count) - 3));
            }

            // TODO: отдельный метод
            for (int x = 0; x < SIZE_Y; x++)
            {
                for (int y = 0; y < SIZE_X; y++)
                {
                    int height = map.MapArr[x, y].Height;

                    var color = System.Drawing.ColorTranslator.FromHtml(InMemory.Colors[height]);
                    Colorful.Console.Write($"{map.MapArr[x, y]} ", color);
                    
                }

                Colorful.Console.WriteLine();
            }
        }
    }
}
