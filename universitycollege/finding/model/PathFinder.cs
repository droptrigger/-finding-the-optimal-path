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
        private double DIAONAL = Math.Sqrt(2);

        public PathFinder(Map map)
        {
            _map = map;
        }

        public List<Map.Coords> FindPath(Map.Coords start, Map.Coords end)
        {
            _costDict = InMemory.Bicicle;
            HashSet<Map.Coords> openSet = new HashSet<Map.Coords>(); // Координаты, доступные для исследования
            HashSet<Map.Coords> closedSet = new HashSet<Map.Coords>(); // Координаты, которые уже исследованы

            Dictionary<Map.Coords, Map.Coords> pathDictionary = new Dictionary<Map.Coords, Map.Coords>(); // Словарь для восстановления пути

            openSet.Add(start); // Добавляем начальную точку в открытые координаты

            Dictionary<Map.Coords, double> gScore = new Dictionary<Map.Coords, double>(); // Словарь для хранения стоимости пути от стартовой до текущей клетки
            Dictionary<Map.Coords, double> fScore = new Dictionary<Map.Coords, double>(); // Словарь для хранения общей стоимости от стартовой до конечной клетки

            gScore[start] = 0; // Начальная стоимость равна 0
            fScore[start] = GetDistance(start, end); // Оценка расстояния от текущей клетки до конечной

            while (openSet.Count > 0) // Пока есть доступные координаты
            {
                double minFScore = double.MaxValue; // Инициализация минимальной стоимости
                Map.Coords current = new Map.Coords(); // Текущая координата

                // Поиск координаты с минимальным значением fScore
                foreach (Map.Coords coords in openSet)
                {
                    if (fScore.ContainsKey(coords) && fScore[coords] < minFScore)
                    {
                        minFScore = fScore[coords];
                        current = coords;
                    }
                }

                // Если достигли конечной точки, восстанавливаем путь
                if (GetDistance(current, end) == 0)
                {
                    return ConstructPath(pathDictionary, current);
                }

                openSet.Remove(current); // Удаляем текущую координату из открытых
                closedSet.Add(current); // Добавляем в закрытые

                // Исследуем соседние координаты
                foreach (Map.Coords neighbor in GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor)) // Если сосед уже исследован, пропускаем
                        continue;

                    double tentativeGScore = gScore[current] + GetGScore(neighbor); // Временная стоимость пути

                    if (!openSet.Contains(neighbor)) // Если сосед не в открытых координатах, добавляем его
                        openSet.Add(neighbor);

                    double neighborGScore = double.MaxValue;

                    // Если сосед уже имеет стоимость, берем ее
                    if (gScore.ContainsKey(neighbor))
                    {
                        neighborGScore = gScore[neighbor];
                    }

                    // Если временная стоимость больше или равна стоимости соседа, пропускаем
                    if (tentativeGScore >= neighborGScore)
                    {
                        continue;
                    }

                    // Обновляем информацию о пути
                    pathDictionary[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetDistance(neighbor, end);
                }
            }

            return new List<Map.Coords>(); // Возвращаем пустой список, если путь не найден
        }

        // Получение соседних координат
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
                { 1, 1 }    // Северо-восток
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = current.x + directions[i, 0];
                int newY = current.y + directions[i, 1];

                if (_map.IsInAMap(new Map.Coords(newX, newY))) // Проверяем, находится ли новая координата в пределах карты
                {
                    neighbors.Add(new Map.Coords(newX, newY)); // Добавляем соседнюю координату
                }
            }

            return neighbors;
        }

        // Получение стоимости перемещения в зависимости от высоты
        private double GetGScore(Coords neighbor)
        {
            double baseCost = 0; // Базовая стоимость

            sbyte height = _map.GetHeight(new Coords(neighbor.x, neighbor.y)); // Получение высоты
            baseCost = _costDict[height];

            // Увеличиваем стоимость, если диагональ
            if ((neighbor.x != 0) && (neighbor.y != 0))
            {
                baseCost *= DIAONAL;
            }

            return baseCost; // Возвращаем стоимость
        }

        // Вычисление расстояния между двумя координатами
        private double GetDistance(Map.Coords a, Map.Coords b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        // Восстановление пути на основе словаря
        private List<Map.Coords> ConstructPath(Dictionary<Map.Coords, Map.Coords> pathDictionary, Map.Coords current)
        {
            List<Map.Coords> totalPath = new List<Map.Coords> { current };

            while (pathDictionary.ContainsKey(current))
            {
                current = pathDictionary[current]; // Переход к предыдущей координате
                totalPath.Add(current); // Добавление в список пути
            }

            totalPath.Reverse();
            return totalPath; // Возвращаем полный путь
        }
    }
}
