using System;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TTCBDD.UtilityClasses;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class TricentisWebShopSteps
    {
        private readonly ScenarioContext context;
        private readonly IWebDriver driver;
        public TricentisWebShopSteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.GetDriver();
        }

        [Given(@"I navigate to to the webshop")]
        public void GivenINavigateToToTheWebshop()
        {
            driver.NavigateToWebsite();
        }
        
        [Given(@"I click the log in link")]
        public void GivenIClickTheLogInLink()
        {
            driver.FindElement(By.PartialLinkText("Log in")).Click();
        }
        
        [When(@"I login as ""(.*)"", ""(.*)""")]
        public void WhenILoginAs(string username, string password)
        {
            driver.FindElement(By.Id("Email")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            context.Add("username", username);
            driver.FindElement(By.XPath(".//input[@value='Log in']")).Click();
        }
        
        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            var username = context.Get<string>("username");
            driver.FindElement(By.ClassName("account")).Text.Should().Be(username);
        }
    }
}
