using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.Reporter
{
    public class Reporter
    {
        private string HtmlPath;
        private string OutputPath;
        public Reporter(string HtmlPath, string OutputPath)
        {
            this.HtmlPath = HtmlPath;
            this.OutputPath = OutputPath;
        }

    }
}
