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
        public void AddHill(Map map, Point hillCenter)
        {
            if (!map.PointIsInAMap(hillCenter))
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            else
            {
                if (hillCenter.Height > 0)
                {
                    if (hillCenter.Height > map.GetHeight(hillCenter.X, hillCenter.Y))
                    {
                        map.Update(hillCenter.X, hillCenter.Y, hillCenter);
                    }
                    GenerateRadius(map, hillCenter, 1); // Положительное направление
                }
                else
                {
                    if (map.IsPointAreHigher(hillCenter) || map.GetHeight(hillCenter.X, hillCenter.Y) == 0)
                    {
                        map.Update(hillCenter.X, hillCenter.Y, hillCenter);
                        GenerateRadius(map, hillCenter, -1); // Отрицательное направление
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
        public void GenerateRadius(Map map, Point hillCenter, int direction)
        {
            int shiftY = 0;
            int heightPoint = direction;

            for (int shiftX = Math.Abs(hillCenter.Height) - 1; shiftX >= 0; shiftX--)
            {
                DrawLineDown(
                   map: map,
                   point: hillCenter,
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
        private void DrawLineDown(Map map, Point point, int shiftX, int shiftY, int heightPoint, int direction)
        {
            if (shiftY == 0)
            {
                if (map.IsXInAMap(point.X - shiftX))
                {
                    map.Update(point.X - shiftX, point.Y, new Point(shiftX, point.Y, heightPoint));
                }
                if (map.IsXInAMap(point.X + shiftX))
                {
                    map.Update(point.X + shiftX, point.Y, new Point(shiftX, point.Y, heightPoint));
                }
            }
            else
            {
                int currentHeight = direction; // The initial height depends on the direction
                int plus = 1; // Height change step
                int minPosition = 0 - shiftY;

                for (int topPosition = shiftY; topPosition >= minPosition; topPosition--)
                {
                    if (currentHeight == heightPoint)
                    {
                        plus = -plus;
                    }

                    if (map.IsInAMap(point.X - shiftX, point.Y + topPosition))
                    {
                        Point tempPoint = new Point(point.X - shiftX, point.Y + topPosition, currentHeight);
                        if ((direction > 0 && map.IsPointAreHigher(tempPoint)) ||
                            (direction < 0 && map.IsPointAreBelow(tempPoint)))
                        {
                            map.Update(
                                x: point.X - shiftX,
                                y: point.Y + topPosition,
                                point: tempPoint);
                        }
                    }
                    if (map.IsInAMap(point.X + shiftX, point.Y + topPosition))
                    {
                        Point tempPoint = new Point(point.X + shiftX, point.Y + topPosition, currentHeight);
                        if ((direction > 0 && map.IsPointAreHigher(tempPoint)) ||
                            (direction < 0 && map.IsPointAreBelow(tempPoint)))
                        {
                            map.Update(
                                x: point.X + shiftX,
                                y: point.Y + topPosition,
                                point: tempPoint);
                        }
                    }

                    currentHeight += plus * direction; // Changing the height depending on the direction
                }
            }
        }
    }
}
