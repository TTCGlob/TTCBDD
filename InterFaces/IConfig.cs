using TTCBDD.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.Interfaces
{
    public interface IConfig
    {
        string GetUsername();
        string GetPassword();
        BrowserType GetBrowser();

        string GetWebsiteUrl();

        int GetPageLoadTimeout();

        int GetElementLoadTimeout();
        
    }
}
