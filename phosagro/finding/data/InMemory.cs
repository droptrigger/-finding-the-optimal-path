using System.Collections.Generic;

namespace universitycollege.finding.data
{
    public struct InMemory
    {
        public static readonly Dictionary<int, string> Colors = new Dictionary<int, string>
        {
            { 1, "#00FF00" },
            { 2, "#006400" },
            { 3, "#FFD700" },
            { 4, "#FF8C00" },
            { 5, "#B22222" }
        };

        public enum Constants
        {
            MAX_COEFF_VALUE = 5,
            MAX_X_MAP = 100,
            MAX_Y_MAP = 100
        }
    }
}
