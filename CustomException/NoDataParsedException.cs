using System;

namespace TTCBDD.CustomException
{
    public class NoDataParsedException : Exception
    {
        public string message;
        public NoDataParsedException(string message) : base(message)
        {
        }
    }
}
