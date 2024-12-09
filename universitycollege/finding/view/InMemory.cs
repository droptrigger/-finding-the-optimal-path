using System.Collections.Generic;

namespace universitycollege.finding.view
{
    public struct InMemory
    {
        public static readonly Dictionary<int, string> Colors = new Dictionary<int, string>
        {
            { -3, "#1E90FF" },
            { -2, "#00BFFF" },
            { -1, "#87CEEB" },
            { 0, "#FFFFFF" },
            { 1, "#00FF00" },
            { 2, "#006400" },
            { 3, "#FFD700" },
            { 4, "#FF8C00" },
            { 5, "#B22222" }
        };

        // TODO
        public enum Constants
        {
            MAX_COEFF_VALUE = 5,
            MAX_X_MAP = 1000,
            MAX_Y_MAP = 1000
        }
    }
}
