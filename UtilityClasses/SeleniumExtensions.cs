using OpenQA.Selenium;
using TTCBDD.Configuration;

namespace TTCBDD.UtilityClasses
{
    public static class SeleniumExtensions
    {
        public static void NavigateToWebsite(this IWebDriver driver)
        {
            var config = new AppConfigReader();
            var url = config.GetWebsiteUrl();
            driver.Navigate().GoToUrl(url);
        }

        public static bool TryFindElement(this ISearchContext searchContext, By locator, out IWebElement element)
        {
            try
            {
                element = searchContext.FindElement(locator);
            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;
            }
            return true;
        }
    }
}
