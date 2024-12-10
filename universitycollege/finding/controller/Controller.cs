using System;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding.controller
{
    public class Controller
    {
        public Map GenerateMap(int x, int y, int numberOfGenerations)
        {
            Map map = new Map(x, y);

            Random rnd = new Random();
            TopologyGenerator generator = new TopologyGenerator();

            int timestart = (int)DateTime.Now.Ticks;

            //Генерация рандомных холмов
            for (int i = 0; i < numberOfGenerations; i++)
            {
                generator.AddHill(
                    map: map,
                    x: rnd.Next(x),
                    y: rnd.Next(y),
                    height: (sbyte)(rnd.Next(InMemory.Colors.Count) - 3));
            }

            return map;
        }
    }
}
