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
            HashSet<Coords> openSet = new HashSet<Coords>(); // Coordinates available for exploration
            HashSet<Coords> closedSet = new HashSet<Coords>(); // Coordinates that have already been explored

            Dictionary<Coords, Coords> pathDictionary = new Dictionary<Coords, Coords>();

            openSet.Add(start);

            Dictionary<Coords, double> gScore = new Dictionary<Coords, double>(); // Dictionary to store the cost of the path from start to current cell
            Dictionary<Coords, double> fScore = new Dictionary<Coords, double>(); // Dictionary to store the total cost from start to end cell

            gScore[start] = 0;
            fScore[start] = GetDistance(start, end);

            while (openSet.Count > 0)
            {
                double minFScore = double.MaxValue;
                Coords current = new Coords();

                // Find the coordinate with the minimum fScore value
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

                // Explore neighboring coordinates
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
        /// Method to get the neighbors of a cell
        /// </summary>
        /// <param name="current">Current cell coordinates</param>
        /// <returns></returns>
        private List<Coords> GetNeighbors(Coords current)
        {
            var neighbors = new List<Coords>();

            int[,] directions = new int[,]
            {
                { -1, 0 },  // Left
                { 1, 0 },   // Right
                { 0, -1 },  // Down
                { 0, 1 },   // Up
                { -1, -1 }, // Bottom left
                { -1, 1 },  // Top left
                { 1, -1 },  // Bottom right
                { 1, 1 }    // Top right
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
        /// Method to get the cost of moving based on height
        /// </summary>
        /// <param name="neighbor">Cell coordinates</param>
        /// <returns>Returns the cost of movement</returns>
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
        /// Method to calculate the distance between two cells
        /// </summary>
        /// <param name="a">Coordinate from which to go</param>
        /// <param name="b">Coordinate to which to go</param>
        /// <returns></returns>
        private double GetDistance(Coords a, Coords b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        /// <summary>
        /// Path reconstruction
        /// </summary>
        /// <param name="pathDictionary">Dictionary with coordinates</param>
        /// <param name="current">Ending coordinate</param>
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
