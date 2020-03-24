
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TTCBDD.Settings;
using TTCBDD.Configuration;
using TTCBDD.CustomException;
using NUnit.Framework;

namespace TTCBDD.BaseClasses
{
    [TestFixture]
    public class BaseClass
    {
        
        private static IWebDriver GetFirefoxDriver()
        {
           IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        private static IWebDriver GetChromeDriver()
        {

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        private static IWebDriver GetIEDriver()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        //private static PhantomJSDriver GetPhantomJSDriver()
        //{
        //    PhantomJSDriver driver = new PhantomJSDriver();
        //    return driver;
        //}

        //[AssemblyInitialize]
        public static void InitWebdriver(TestContext tc)
        {
            ObjectRepository.Config = new AppConfigReader();
            switch (ObjectRepository.Config.GetBrowser())
            {
                case BrowserType.Firefox:
                    ObjectRepository.Driver = GetFirefoxDriver();
                    break;

                case BrowserType.Chrome:
                    ObjectRepository.Driver = GetChromeDriver();
                    break;

                case BrowserType.IExplorer:
                    ObjectRepository.Driver = GetIEDriver();
                    break;
                default:
                    throw new NoSuitableDriverFound("Driver not found : " + ObjectRepository.Config.GetBrowser().ToString());
            }
            ObjectRepository.Driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(ObjectRepository.Config.GetPageLoadTimeout()));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeout());
        }

       // [AssemblyCleanup]

        public static void TearDown()
        {
            if (ObjectRepository.Driver != null)
            {
                ObjectRepository.Driver.Close();
                ObjectRepository.Driver.Quit();
            }
        }
    }
}
