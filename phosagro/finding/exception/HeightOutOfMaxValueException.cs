using System;

namespace universitycollege.finding.exception
{
    class HeightOutOfMaxValueException : Exception
    {
        public HeightOutOfMaxValueException(string message)
            : base(message) { }
    }
}
