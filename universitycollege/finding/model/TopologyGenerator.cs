using System;

namespace universitycollege.finding.model
{
    public class TopologyGenerator
    {
        /// <summary>
        /// The method of creating a new hill
        /// </summary>
        /// <param name="map">A reference to an object of the map class</param>
        /// <param name="hillCenter">A reference to an object of the point class</param>
        /// <exception cref="IndexOutOfRangeException">If the point is outside the map</exception>
        public void AddHill(Map map, int x, int y, sbyte height)
        {
            if (!map.PointIsInAMap(x, y))
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            else
            {
                if (height > 0)
                {
                    if (height > map.GetHeight(x, y))
                    {
                        map.Update(x, y, height);
                    }
                    GenerateRadius(map, x, y, height, 1); // Положительное направление
                }
                else
                {
                    if (map.IsPointAreHigher(x, y, height) || map.GetHeight(x, y) == 0)
                    {
                        map.Update(x, y, height);
                        GenerateRadius(map, x, y, height, -1); // Отрицательное направление
                    }
                }
            }
        }

        /// <summary>
        /// A method for controlling the drawing of horizontal lines
        /// </summary>
        /// <param name="map">A reference to an object of the map class</param>
        /// <param name="hillCenter">A reference to an object of the point class</param>
        /// <param name="direction">The direction, if the mountain is higher than 0, is positive, otherwise it is negative</param>
        public void GenerateRadius(Map map, int x, int y, sbyte height, sbyte direction)
        {
            int shiftY = 0;
            sbyte heightPoint = direction;

            for (int shiftX = Math.Abs(height) - 1; shiftX >= 0; shiftX--)
            {
                DrawLineDown(
                   map: map,
                   x: x,
                   y: y,
                   shiftX: shiftX,
                   shiftY: shiftY,
                   heightPoint: heightPoint,
                   direction: direction);

                shiftY++;
                heightPoint += direction;
            }
        }

        /// <summary>
        /// A method that mirrors vertical lines
        /// </summary>
        /// <param name="map">A reference to an object of the map class</param>
        /// <param name="point">Reference to the point object</param>
        /// <param name="shiftX">Offset by x</param>
        /// <param name="shiftY">Offset by y</param>
        /// <param name="heightPoint">Maximum height in the line</param>
        private void DrawLineDown(Map map, int x, int y, int shiftX, int shiftY, sbyte heightPoint, sbyte direction)
        {
            if (shiftY == 0)
            {
                if (map.IsXInAMap(x - shiftX))
                {
                    if ((direction > 0 && map.IsPointAreHigher(x - shiftX, y, heightPoint)) ||
                            (direction < 0 && map.IsPointAreBelow(x - shiftX, y, heightPoint)))
                    {
                        map.Update(x - shiftX, y, heightPoint);
                    }
                }
                if (map.IsXInAMap(x + shiftX))
                {
                    if ((direction > 0 && map.IsPointAreHigher(shiftX + x, y, heightPoint)) ||
                            (direction < 0 && map.IsPointAreBelow(x + shiftX, y, heightPoint)))
                    {
                        map.Update(x + shiftX, y, heightPoint);
                    }

                }
            }
            else
            {
                sbyte currentHeight = direction; // The initial height depends on the direction
                sbyte plus = 1; // Height change step
                int minPosition = 0 - shiftY;

                for (int topPosition = shiftY; topPosition >= minPosition; topPosition--)
                {
                    if (currentHeight == heightPoint)
                    {
                        plus = (sbyte)-plus;
                    }

                    if (map.IsInAMap(x - shiftX, y + topPosition))
                    {
                        if ((direction > 0 && map.IsPointAreHigher(x - shiftX, y + topPosition, currentHeight)) ||
                            (direction < 0 && map.IsPointAreBelow(x - shiftX, y + topPosition, currentHeight)))
                        {
                            map.Update(
                                x: x - shiftX,
                                y: y + topPosition,
                                height: currentHeight);
                        }
                    }
                    if (map.IsInAMap(x + shiftX, y + topPosition))
                    {
                        if ((direction > 0 && map.IsPointAreHigher(x + shiftX, y + topPosition, currentHeight)) ||
                            (direction < 0 && map.IsPointAreBelow(x + shiftX, y + topPosition, currentHeight)))
                        {
                            map.Update(
                                x: x + shiftX,
                                y: y + topPosition,
                                height: currentHeight);
                        }
                    }

                    currentHeight += (sbyte)(plus * direction); // Changing the height depending on the direction
                }
            }
        }
    }
}
