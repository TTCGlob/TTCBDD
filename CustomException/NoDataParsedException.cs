using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
