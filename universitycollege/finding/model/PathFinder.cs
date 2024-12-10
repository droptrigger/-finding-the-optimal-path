using System;
using System.Collections.Generic;

namespace universitycollege.finding.model
{
    public class PathFinder
    {
        private Map _map;

        public PathFinder(Map map)
        {
            _map = map;
        }

        public List<Map.Coords> FindPath(Map.Coords start, Map.Coords end)
        {
            HashSet<Map.Coords> usableCoords = new HashSet<Map.Coords>();   // Координаты, на которые уже сходили
            HashSet<Map.Coords> unusableCoords = new HashSet<Map.Coords>(); // Координаты, на которые еще не сходили

            Dictionary<Map.Coords, Map.Coords> dictOfThePath = new Dictionary<Map.Coords, Map.Coords>(); // TODO: 

            usableCoords.Add(start);

            Dictionary<Map.Coords, double> gScore = new Dictionary<Map.Coords, double>(); // TODO: Создать класс
            Dictionary<Map.Coords, double> fScore = new Dictionary<Map.Coords, double>(); // TODO: Создать класс

            gScore[start] = 0; // Вес клетки
            fScore[start] = GetDistance(start, end); // Дистанция от текущей клетки до конечной

            while (usableCoords.Count > 0)
            {
                double minFScore = double.MaxValue; // TODO: Diagonal
                Map.Coords current = new Map.Coords();

                foreach (Map.Coords coords in usableCoords)
                {
                    if (fScore[coords] < minFScore)
                    {
                        minFScore = fScore[coords];
                        current = coords;
                    }
                }

                if (GetDistance(current, end) == 0)
                {
                    return ConstructPath(dictOfThePath, current);
                }

                usableCoords.Remove(current); // Координаты, на которые уже сходили
                unusableCoords.Add(current);

                foreach (Map.Coords neighbor in GetNeighbors(current))
                {
                    if (unusableCoords.Contains(neighbor))
                        continue;

                    double tempGScore = gScore[current] + GetGScore(neighbor);

                    if (!usableCoords.Contains(neighbor))
                        usableCoords.Add(neighbor);

                    double neighborGScore = double.MaxValue;

                    if (gScore.ContainsKey(neighbor))
                    {
                        neighborGScore = gScore[neighbor];
                    }

                    if (tempGScore >= neighborGScore)
                    {
                        continue;
                    }

                    dictOfThePath[neighbor] = current;
                    gScore[neighbor] = tempGScore;
                    fScore[neighbor] = gScore[neighbor] + GetDistance(neighbor, end);
                }
            }

            return new List<Map.Coords>();
        }


        private List<Map.Coords> GetNeighbors(Map.Coords current)
        {
            var neighbors = new List<Map.Coords>();

            int[,] directions = new int[,]
            {
            { -1, 0 }, // Лево
            { 1, 0 },  // Право
            { 0, -1 }, // Низ
            { 0, 1 },  // Верх
            { -1, -1 }, // Юго-запад
            { -1, 1 },  // Северо-запад
            { 1, -1 },  // Юго-восток
            { 1, 1 }    // Юго-запад
            };

            for (int i = 0; i < directions.Length / 2; i++)
            {
                int newX = current.x + directions[i, 0];
                int newY = current.y + directions[i, 1];

                if (_map.IsInAMap(newX, newY))
                {
                    neighbors.Add(new Map.Coords(newX, newY));
                }
            }

            return neighbors;
        }

        private double GetGScore(Map.Coords neighbor)
        {
            double baseCost = 1;

            int height = _map.GetHeight(neighbor.x, neighbor.y);

            switch (height)
            {
                case -3:
                    baseCost = 4;
                    break;
                case -2:
                    baseCost = 3;
                    break;
                case -1:
                    baseCost = 2;
                    break;
                case 0:
                    baseCost = 1;
                    break;
                case 1:
                    baseCost = 2;
                    break;
                case 2:
                    baseCost = 3;
                    break;
                case 3:
                    baseCost = 4;
                    break;
                case 4:
                    baseCost = 5;
                    break;
                case 5:
                    baseCost = 7;
                    break;
            }

            if ((neighbor.x != 0) && (neighbor.y != 0))
            {
                baseCost *= 1.4;
            }

            return baseCost;
        }

        private double GetDistance(Map.Coords a, Map.Coords b)
        {
            return Math.Sqrt( Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) );
        }

        private List<Map.Coords> ConstructPath(Dictionary<Map.Coords, Map.Coords> dictOfThePath, Map.Coords current)
        {
            List<Map.Coords> totalPath = new List<Map.Coords> { current };

            while (dictOfThePath.ContainsKey(current))
            {
                current = dictOfThePath[current];
                totalPath.Add(current);
            }

            return totalPath;
        }
    }
}
