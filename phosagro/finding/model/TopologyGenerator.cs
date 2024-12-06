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
                map.Update(hillCenter.X, hillCenter.Y, hillCenter);
                GenerateRadius(map, hillCenter);
            }
        }

        /// <summary>
        ///
        /// Если высота 2, то необходимо сделать переход из высоты ( 2 - 1 ).
        /// Переход - 4 клетки, которые вокруг центра холма.
        /// 
        /// Если высота 3, то радиус этого перехода становится в 2 раза больше,
        /// то есть закрасить нужно будет клетки вокруг перехода из высоты 3.
        /// В сумме это будет ( 4 + 4*2 ) клетки.
        /// 
        /// Зависимость, которая получилась:
        /// | Количество клеток = ( 4*1 + 4*2 + ... + 4*n )
        /// _______________________________________________ 
        /// ! Необходимо помнить:
        /// A. Точка может находится в углу или на краю карты.
        /// Б. Рядом с холмом может находится еще один холм,
        /// |  переходы нового холма не должны стереть другой 
        /// |  холм и уменьшать его переходы
        /// 
        /// </summary>
        public void GenerateRadius(Map map, Point hillCenter)
        {
            int shiftY = 0;
            int heightPoint = 1;

            for (int shihtX = hillCenter.Height - 1; shihtX >= 0; shihtX--)
            {
                DrawLineDown(
                   map: map,
                   point: hillCenter,
                   shiftX: shihtX,
                   shiftY: shiftY,
                   heightPoint: heightPoint);

                shiftY++;
                heightPoint++;
            }
        }

        /// <summary>
        /// A method that mirrors vertical links
        /// </summary>
        /// <param name="map">A reference to an object of the map class</param>
        /// <param name="point">Reference to the point object</param>
        /// <param name="shiftX">Offset by x</param>
        /// <param name="shiftY">Offset by y</param>
        /// <param name="heightPoint">Maximum height in the line</param>
        private void DrawLineDown(Map map, Point point, int shiftX, int shiftY, int heightPoint)
        {
            int centerX = point.X;
            int centerY = point.Y;

            int height = point.Height;

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
                int plus = 1;
                int currentHeight = 1;

                for (int i = shiftY; i >= 0 - shiftY; i--)
                {
                    if (currentHeight == heightPoint)
                    {
                        plus = -plus;
                    }

                    if (map.IsInAMap(point.X - shiftX, point.Y + i))
                    {
                        Point tempPoint = new Point(point.X - shiftX, point.Y + i, currentHeight);
                        if (map.IsPointAreHigher(tempPoint))
                        {
                            map.Update(
                                x: point.X - shiftX,
                                y: point.Y + i,
                                point: tempPoint);
                        }
                    }
                    if (map.IsInAMap(point.X + shiftX, point.Y + i))
                    {
                        Point tempPoint = new Point(point.X + shiftX, point.Y + i, currentHeight);
                        if (map.IsPointAreHigher(tempPoint))
                        {
                            map.Update(
                                x: point.X + shiftX,
                                y: point.Y + i,
                                point: tempPoint);
                        }
                    }

                    currentHeight += plus;
                }
            }

        }
    }
}
