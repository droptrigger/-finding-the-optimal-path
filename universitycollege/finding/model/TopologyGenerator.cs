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
        /// Метод рисования по паттерну
        /// </summary>
        /// <param name="pattern">Ссылка на объект класса Pattern</param>
        /// <param name="coords">Координаты центра</param>
        public void AppPatternTopology(Pattern pattern, Coords coords)
        {
            Dictionary<Coords, sbyte> PatternCoords = pattern.GetPatternCoords(_map, coords);

            foreach (KeyValuePair<Coords, sbyte> coordsPattern in PatternCoords)
            {
                _map.Update(coordsPattern.Key, coordsPattern.Value);
            }
        }

        /// <summary>
        /// Метод для создания равномерного холма
        /// </summary>
        /// <param name="coords">Координаты центра</param>
        /// <param name="height">Масимальная высота</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void AddSymmetricalHill(Coords coords, sbyte height)
        {
            if (!_map.PointIsInAMap(coords))
            {
                throw new IndexOutOfRangeException("Координаты за пределами карты");
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
        /// Метод для контроля рисования горизонтальных линий
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="height"></param>
        /// <param name="direction"></param>
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
        /// Метод, который рисует зеркальные горизонтальные линии
        /// </summary>
        /// <param name="coords">Координаты центра</param>
        /// <param name="shift">Координаты сдвига</param>
        /// <param name="heightPoint">Максимальная высота в линии</param>
        /// <param name="direction">Направление (если -1, то рисует реку)</param>
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
                sbyte currentHeight = direction; // Начальная высота зависит от направления
                sbyte plus = 1; // Шаг изменения высоты
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

                    currentHeight += (sbyte)(plus * direction); // Изменение высоты в зависимости от направления
                }
            }
        }
    }
}