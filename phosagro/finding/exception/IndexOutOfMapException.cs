using System;

namespace universitycollege.finding.exception
{
    class IndexOutOfMapException : Exception
    {
        public IndexOutOfMapException(string message)
            : base(message) { }
    }
}
