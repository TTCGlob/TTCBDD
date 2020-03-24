using System;

namespace TTCBDD.CustomException
{
    public class NoKeyWordFoundException : Exception
    {
        public NoKeyWordFoundException(string msg) : base(msg)
        {

        }
    }
}
