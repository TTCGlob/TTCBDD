using System;
using TechTalk.SpecFlow;
using TTCBDD.Configuration;
using TTCBDD.ComponentHelper;
using OpenQA.Selenium;
using TTCBDD.PageObject;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class DemoWebShopFeatureSteps
    {
        
        private IWebDriver driver = ObjectRepository.Driver;
        [Obsolete]
        private HomePage hPage = new HomePage(driver);

        private LoginPage lPage = new LoginPage(driver);

        [Given(@"I have navigated to the DemoWebShop website")]
        public void GivenIHaveNavigatedToTheDemoWebShopWebsite()
        {
            var navigation = new AppConfigReader();
            navigation.GetBrowser();
            navigation.GetWebsiteUrl();
        }
        
        [When(@"I click the Log in link")]
        public void WhenIClickTheLogInLink()
        {
            hPage = new HomePage();
            lPage = hPage.NavigateToLogin();
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
