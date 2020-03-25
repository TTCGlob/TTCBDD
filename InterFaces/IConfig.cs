using TTCBDD.Constants;

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
