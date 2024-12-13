using System.Collections.Generic;

namespace universitycollege.finding.view
{
    public struct InMemory
    {
        public const string PathToPattern = @"..\..\..\..\resources\patterns\";

        public static readonly Dictionary<int, string> Colors = new Dictionary<int, string>
        {
            { -3, "#000080" },
            { -2, "#0000FF" },
            { -1, "#008080" },
            { 0, "#228B22" },
            { 1, "#FFA500" },
            { 2, "#FF8C00" },
            { 3, "#FF4500" },
            { 4, "#B22222" },
            { 5, "#8B0000" }
        };

        public enum Constants
        {
            MAX_X_MAP = 1000,
            MAX_Y_MAP = 1000
        }

        public enum AmountRoad
        {
            BRIDGE = 300,
            BRIDGE_SUPPORT = 100,
            DEFAULT_ROAD = 50,
            UPPER_ROAD = 250
        }

        public enum TypesOfPath
        {
            OPTIMAZE = 0,
            HARD = 1
        }

        public static readonly Dictionary<sbyte, double> Bicicle = new Dictionary<sbyte, double>
        {
            { -3, 4 },
            { -2, 3 },
            { -1, 2 },
            { 0, 1 },
            { 1, 2 },
            { 2, 3 },
            { 3, 4 },
            { 4, 5 },
            { 5, 6 },
        };
    }
}
