using System;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding.controller
{
    /// <summary>
    /// The controller for generating the landscape
    /// </summary>
    public class Controller
    {
        public Map GenerateMap(int x, int y, int numberOfGenerations)
        {
            Map map = new Map(x, y);

            Random rnd = new Random();
            TopologyGenerator generator = new TopologyGenerator(map);

            // Generating random hills
            for (int i = 0; i < numberOfGenerations; i++)
            {
                generator.AddSymmetricalHill(
                    coords: new Map.Coords(rnd.Next(x), rnd.Next(y)),
                    height: (sbyte)(rnd.Next(InMemory.ColorsForWrite.Count) - 3));
            }

            return map;
        }
    }
}
