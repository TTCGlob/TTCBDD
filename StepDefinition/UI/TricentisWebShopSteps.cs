using System;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.PageObject;
using TTCBDD.UtilityClasses;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class TricentisWebShopSteps
    {
        private readonly ScenarioContext context;
        private readonly IWebDriver driver;
        public TricentisWebShopSteps(ScenarioContext context, IWebDriver driver)
        {
            this.context = context;
            this.driver = driver;
        }

        [Given(@"I navigate to to the webshop")]
        public void GivenINavigateToToTheWebshop()
        {
            driver.NavigateToWebsite();
            context.Set(new HomePage(driver));
        }
        
        [Given(@"I click the log in link")]
        public void GivenIClickTheLogInLink()
        {
            var homePage = context.Get<HomePage>();
            var loginPage = homePage.NavigateToLoginPage();
            context.Set(loginPage);
        }
        
        [When(@"I login as ""(.*)"", ""(.*)""")]
        public void WhenILoginAs(string username, string password)
        {
            context.Set(username, "username");
            var loginPage = context.Get<LoginPage>();
            var homePage = loginPage.Login(username, password);
            context.Set(homePage);
        }
        
        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            var username = context.Get<string>("username");
            var homePage = context.Get<HomePage>();
            homePage.LoggedInUser().Should().Be(username);
        }
    }
}
