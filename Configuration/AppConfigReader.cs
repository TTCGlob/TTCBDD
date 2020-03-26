using TTCBDD.Interfaces;
using TTCBDD.Settings;
using System;
using System.Configuration;
using TTCBDD.Constants;

namespace TTCBDD.Configuration
{
    public class AppConfigReader : IConfig
    {
        public BrowserType GetBrowser()
        {
            string browser =  ConfigurationManager.AppSettings.Get(AppConfigKeys.Browser);
            return (BrowserType)Enum.Parse(typeof(BrowserType), browser);
        }

        public int GetElementLoadTimeout()
        {
            string timeout = ConfigurationManager.AppSettings.Get(AppConfigKeys.ElementLoadTimeOut);
            if (timeout == null)
                return 60;
            else
                return Convert.ToInt32(timeout);
        }

        public int GetPageLoadTimeout()
        {
            string timeout = ConfigurationManager.AppSettings.Get(AppConfigKeys.PageLoadTimeOut);
            if (timeout == null)
                return 30;
            else
                return Convert.ToInt32(timeout);
        }

        public string GetPassword()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Password);
        }

        public string GetUsername()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Username);
        }

        public string GetWebsiteUrl()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.WebSite);
        }

        public bool GetHeadless()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get(AppConfigKeys.Headless));
        }
    }
}
