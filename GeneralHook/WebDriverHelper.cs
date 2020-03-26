using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TTCBDD.Configuration;
using TTCBDD.CustomException;

namespace TTCBDD.GeneralHook
{
    public static class WebDriverHelper
    {
        private static AppConfigReader config = new AppConfigReader();
        public static IWebDriver InitialiseDriver()
        {
            IWebDriver driver;
            switch (config.GetBrowser())
            {
                case BrowserType.Chrome:
                    driver = new ChromeDriver(GetChromeOptions());
                    break;
                case BrowserType.Firefox:
                    driver = new FirefoxDriver(GetFirefoxOptions());
                    break;
                default:
                    throw new NoSuitableDriverFound("No driver of that type");
            }
            driver.Manage().Window.Maximize();
            return driver;
        }
        /// <summary>
        /// Gets the default options for the chrome web driver.
        /// </summary>
        /// <returns></returns>
        private static ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            if (config.GetHeadless())
                options.AddArgument("--headless");
            return options;
        }

        private static FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();
            if (config.GetHeadless())
                options.AddArgument("-headless");
            return options;
        }
    }
}
