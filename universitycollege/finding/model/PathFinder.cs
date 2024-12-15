using System;
using System.Collections.Generic;
using universitycollege.finding.view;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class PathFinder
    {
        private Map _map;
        private Dictionary<sbyte, double> _costDict;
        private static readonly double DIAONAL = Math.Sqrt(2);

        public PathFinder(Map map)
        {
            _map = map;
        }

        public List<Coords> FindPath(Coords start, Coords end)
        {
            _costDict = InMemory.BicicleCost;
            HashSet<Coords> openSet = new HashSet<Coords>(); // Координаты, доступные для исследования
            HashSet<Coords> closedSet = new HashSet<Coords>(); // Координаты, которые уже исследованы

            Dictionary<Coords, Coords> pathDictionary = new Dictionary<Coords, Coords>();

            openSet.Add(start);

            Dictionary<Coords, double> gScore = new Dictionary<Coords, double>(); // Словарь для хранения стоимости пути от стартовой до текущей клетки
            Dictionary<Coords, double> fScore = new Dictionary<Coords, double>(); // Словарь для хранения общей стоимости от стартовой до конечной клетки

            gScore[start] = 0;
            fScore[start] = GetDistance(start, end);

            while (openSet.Count > 0)
            {
                double minFScore = double.MaxValue;
                Coords current = new Coords();

                // Поиск координаты с минимальным значением fScore
                foreach (Coords coords in openSet)
                {
                    if (fScore.ContainsKey(coords) && fScore[coords] < minFScore)
                    {
                        minFScore = fScore[coords];
                        current = coords;
                    }
                }

                if (GetDistance(current, end) == 0)
                {
                    return ConstructPath(pathDictionary, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                // Исследуем соседние координаты
                foreach (Coords neighbor in GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    double tentativeGScore = gScore[current] + GetGScore(neighbor);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);

                    double neighborGScore = double.MaxValue;

                    if (gScore.ContainsKey(neighbor))
                    {
                        neighborGScore = gScore[neighbor];
                    }

                    if (tentativeGScore >= neighborGScore)
                    {
                        continue;
                    }

                    pathDictionary[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetDistance(neighbor, end);
                }
            }

            return new List<Coords>();
        }

        /// <summary>
        /// Метод получения соседей клетки
        /// </summary>
        /// <param name="current">Текущие координаты клетки</param>
        /// <returns></returns>
        private List<Coords> GetNeighbors(Coords current)
        {
            var neighbors = new List<Coords>();

            int[,] directions = new int[,]
            {
                { -1, 0 },  // Лево
                { 1, 0 },   // Право
                { 0, -1 },  // Низ
                { 0, 1 },   // Верх
                { -1, -1 }, // Левый нижний
                { -1, 1 },  // Левый верхний
                { 1, -1 },  // Правый нижний
                { 1, 1 }    // Правый верхний
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = current.x + directions[i, 0];
                int newY = current.y + directions[i, 1];

                if (_map.IsInAMap(new Coords(newX, newY)))
                {
                    neighbors.Add(new Coords(newX, newY));
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Метод получения стоимости перемещения в зависимости от высоты
        /// </summary>
        /// <param name="neighbor">Координата клетки</param>
        /// <returns>Возвращает стоимость перемещения</returns>
        private double GetGScore(Coords neighbor)
        {
            double baseCost = 0;

            sbyte height = _map.GetHeight(new Coords(neighbor.x, neighbor.y));
            baseCost = _costDict[height];

            if ((neighbor.x != 0) && (neighbor.y != 0))
            {
                baseCost *= DIAONAL;
            }

            return baseCost;
        }

        /// <summary>
        /// Метод, вычисляющий расстояние между двумя клетками
        /// </summary>
        /// <param name="a">Координата от которой идти</param>
        /// <param name="b">Координата к которой идти</param>
        /// <returns></returns>
        private double GetDistance(Coords a, Coords b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        /// <summary>
        /// Восстановление пути
        /// </summary>
        /// <param name="pathDictionary">Словарь с координатами</param>
        /// <param name="current">Конечная координата</param>
        /// <returns></returns>
        private List<Coords> ConstructPath(Dictionary<Coords, Coords> pathDictionary, Coords current)
        {
            List<Coords> totalPath = new List<Coords> { current };

            while (pathDictionary.ContainsKey(current))
            {
                current = pathDictionary[current];
                totalPath.Add(current);
            }

            totalPath.Reverse();
            return totalPath;
        }
    }
}
