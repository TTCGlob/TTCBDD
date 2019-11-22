using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.CustomReporter
{
    public class ReportStepError
    {
        public string Source { get; }
        public string Error { get;  }
        public string StackTrace { get; }
        public ReportStepError(string source, string error, string stackTrace)
        {
            Source = source;
            Error = error;
            StackTrace = stackTrace;
        }
    }
}
