using OpenQA.Selenium;
using TTCBDD.Configuration;

namespace TTCBDD.UtilityClasses
{
    public static class IWebDriverExtensions
    {
        public static void NavigateToWebsite(this IWebDriver driver)
        {
            var config = new AppConfigReader();
            var url = config.GetWebsiteUrl();
            driver.Navigate().GoToUrl(url);
        }
    }
}
