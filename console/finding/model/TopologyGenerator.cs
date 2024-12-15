using System;
using System.Collections.Generic;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class TopologyGenerator
    {
        private Map _map;

        public TopologyGenerator(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Method for drawing based on a pattern
        /// </summary>
        /// <param name="pattern">Reference to an object of the Pattern class</param>
        /// <param name="coords">Coordinates of the center</param>
        public void AddPatternTopology(Pattern pattern, Coords coords)
        {
            Dictionary<Coords, sbyte> PatternCoords = pattern.GetPatternCoords(_map, coords);

            foreach (KeyValuePair<Coords, sbyte> coordsPattern in PatternCoords)
            {
                _map.Update(coordsPattern.Key, coordsPattern.Value);
            }
        }

        /// <summary>
        /// Method for creating a symmetrical hill
        /// </summary>
        /// <param name="coords">Coordinates of the center</param>
        /// <param name="height">Maximum height</param>
        /// <exception cref="IndexOutOfRangeException">If coordinates outside the map</exception>
        public void AddSymmetricalHill(Coords coords, sbyte height)
        {
            if (!_map.PointIsInAMap(coords))
            {
                throw new IndexOutOfRangeException("Coordinates outside the map");
            }
            else
            {
                if (height > 0)
                {
                    if (height > _map.GetHeight(coords))
                    {
                        _map.Update(coords, height);
                    }
                    GenerateRadius(coords, height, 1);
                }
                else
                {
                    if (_map.IsPointAreHigher(coords, height) || _map.GetHeight(coords) == 0)
                    {
                        _map.Update(coords, height);
                        GenerateRadius(coords, height, -1);
                    }
                }
            }
        }

        /// <summary>
        /// Method for controlling the drawing of horizontal lines
        /// </summary>
        /// <param name="coords">Сoordinates of the drawing center</param>
        /// <param name="height">Maximum height</param>
        /// <param name="direction">Direction (-1 - river, 1 - hill)</param>
        private void GenerateRadius(Coords coords, sbyte height, sbyte direction)
        {
            int shiftY = 0;
            sbyte heightPoint = direction;

            for (int shiftX = Math.Abs(height) - 1; shiftX >= 0; shiftX--)
            {
                DrawLineDown(
                   coords: coords,
                   shift: new Coords(shiftX, shiftY),
                   heightPoint: heightPoint,
                   direction: direction);

                shiftY++;
                heightPoint += direction;
            }
        }

        /// <summary>
        /// Method that draws mirrored horizontal lines
        /// </summary>
        /// <param name="coords">Coordinates of the center</param>
        /// <param name="shift">Coordinates of the shift</param>
        /// <param name="heightPoint">Maximum height in the line</param>
        /// <param name="direction">Direction (-1 - river, 1 - hill)</param>
        private void DrawLineDown(Coords coords, Coords shift, sbyte heightPoint, sbyte direction)
        {
            if (shift.y == 0)
            {
                if (_map.IsXInAMap(coords.x - shift.x))
                {
                    if ((direction > 0 && _map.IsPointAreHigher(new Coords(coords.x - shift.x, coords.y), heightPoint)) ||
                            (direction < 0 && _map.IsPointAreBelow(new Coords(coords.x - shift.x, coords.y), heightPoint)))
                    {
                        _map.Update(new Coords(coords.x - shift.x, coords.y), heightPoint);
                    }
                }
                if (_map.IsXInAMap(coords.x + shift.x))
                {
                    if ((direction > 0 && _map.IsPointAreHigher(new Coords(coords.x + shift.x, coords.y), heightPoint)) ||
                            (direction < 0 && _map.IsPointAreBelow(new Coords(coords.x + shift.x, coords.y), heightPoint)))
                    {
                        _map.Update(new Coords(coords.x + shift.x, coords.y), heightPoint);
                    }

                }
            }
            else
            {
                sbyte currentHeight = direction; // The initial height depends on the direction
                sbyte plus = 1; // Height change step
                int minPosition = 0 - shift.y;

                for (int topPosition = shift.y; topPosition >= minPosition; topPosition--)
                {
                    if (currentHeight == heightPoint)
                    {
                        plus = (sbyte)-plus;
                    }

                    Coords tempCoords = new Coords(coords.x - shift.x, coords.y + topPosition);
                    if (_map.IsInAMap(tempCoords))
                    {
                        if ((direction > 0 && _map.IsPointAreHigher(tempCoords, currentHeight)) ||
                            (direction < 0 && _map.IsPointAreBelow(tempCoords, currentHeight)))
                        {
                            _map.Update(
                                coords: tempCoords,
                                height: currentHeight);
                        }
                    }
                    tempCoords = new Coords(coords.x + shift.x, coords.y + topPosition);
                    if (_map.IsInAMap(tempCoords))
                    {
                        if ((direction > 0 && _map.IsPointAreHigher(tempCoords, currentHeight)) ||
                            (direction < 0 && _map.IsPointAreBelow(tempCoords, currentHeight)))
                        {
                            _map.Update(
                                coords: tempCoords,
                                height: currentHeight);
                        }
                    }

                    currentHeight += (sbyte)(plus * direction); // Height change depending on the direction
                }
            }
        }
    }
}