using System;
using TechTalk.SpecFlow;
using TTCBDD.Configuration;
using OpenQA.Selenium;
using TTCBDD.PageObject;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;
using TTCBDD.Helpers.UI;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class DemoWebShopFeatureSteps
    {
        IWebDriver driver;

        [Given(@"I navigated to the DemoWebShop website")]
        public void GivenINavigatedToTheDemoWebShopWebsite()
        {
            var navigation = new AppConfigReader();
            PageBase initHomePage = new PageBase(driver);
            navigation.GetBrowser();
            navigation.GetWebsiteUrl();
            initHomePage.SetHomePageObject();
        }

        [When(@"I click the Log in link")]
        public void WhenIClickTheLogInLink()
        {      
            var hPage = new HomePage(driver);
            hPage.NavigateToLogin();
        }

        [Then(@"the login page displays")]
        public void ThenTheLoginPageDisplays()
        {
            if (GenericHelper.IsElementPresent(By.XPath("//h1")))
            {
                Console.WriteLine("Welcome to your training session");
            }
            else WhenIClickTheLogInLink();         
        }
    }
}
