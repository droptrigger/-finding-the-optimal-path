using System;

namespace universitycollege.finding.model
{
    public class HardPath
    {
        private Map _map;
        private double _amount;

        public double Amount => _amount;

        public HardPath(Map map)
        {
            _map = map;
            GetHardPathAmount();

        }

        public HardPath(Map map, Map.Coords start, Map.Coords end)
        {
            _map = map;
            GetHardPathAmount(start, end);

        }

        private void GetHardPathAmount()
        {
            double amount = 0;
            Map.Coords start = new Map.Coords(0 ,0);
            Map.Coords end = new Map.Coords(_map.MapSizeX - 1, _map.MapSizeY - 1);

            int x = start.x;
            int y = start.y;

            while(true)
            {
                amount += Math.Abs(_map.MapArr[x, y]) + 1;

                if (x == end.x && y == end.y)
                {
                    _amount = amount;
                    break;
                }

                if (x != end.x)
                {
                    x++;
                }

                if (y != end.y)
                {
                    y++;
                }
            }
        }

        private void GetHardPathAmount(Map.Coords start, Map.Coords end)
        {
            double amount = 0;

            int x = start.x;
            int y = start.y;

            while (true)
            {
                amount += Math.Abs(_map.MapArr[x, y]) + 1;

                if (x == end.x && y == end.y)
                {
                    _amount = amount;
                    break;
                }

                if (x != end.x)
                {
                    x++;
                }

                if (y != end.y)
                {
                    y++;
                }
            }
        }

    }
}
