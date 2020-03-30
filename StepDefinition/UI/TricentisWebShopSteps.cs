using System;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.ComponentObject;
using TTCBDD.BaseClass.PageObject;
using TTCBDD.UtilityClasses;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class TricentisWebShopSteps
    {
        private readonly ScenarioContext context;
        private readonly IWebDriver driver;
        private PageBase page;
        public TricentisWebShopSteps(ScenarioContext context, IWebDriver driver)
        {
            this.context = context;
            this.driver = driver;
        }

        [Given(@"I navigate to to the webshop")]
        public void GivenINavigateToToTheWebshop()
        {
            driver.NavigateToWebsite();
            page = new HomePage(driver);
        }
        
        [Given(@"I click the log in link")]
        public void GivenIClickTheLogInLink()
        {
            var homePage = page as HomePage;
            var loginPage = homePage.NavigateToLoginPage();
            page = loginPage;
        }

        [Given(@"I navigate to the ""(.*)"" category")]
        public void GivenINavigateToProductCategory(string productCategory)
        {
            page.ClickHeaderProductCategory(productCategory);
            page = new ProductPage(driver);
        }

        [Given(@"I login as ""(.*)"", ""(.*)""")]
        [When(@"I login as ""(.*)"", ""(.*)""")]
        public void WhenILoginAs(string username, string password)
        {
            context.Set(username, "username");
            var loginPage = page as LoginPage;
            var homePage = loginPage.Login(username, password);
            page = homePage;
        }

        [When(@"I add ""(.*)"" to my cart")]
        public void WhenIAddAProductToMyCart(string product)
        {
            var productPage = page as ProductPage;
            productPage.AddProductToCart(product);
        }

        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            var username = context.Get<string>("username");
            page.LoggedInUser.Should().Be(username);
        }

        [Then(@"A ""(.*)"" notification should appear")]
        public void ThenASuccessNotificationShouldAppear(string type)
        {
            var notificationBar = new NotificationBar(driver);
            notificationBar.WaitForNotification();
            notificationBar.Classes.Should().Contain(type);
        }

        [Then(@"It should say ""(.*)""")]
        public void ThenItShouldSay(string message)
        {
            var notificationBar = new NotificationBar(driver);
            notificationBar.WaitForNotification();
            notificationBar.Message.Should().Contain(message);
        }

        [Then(@"The shopping cart should indicate it has (.*) item in it")]
        public void ThenTheShoppingCartShouldIndicateItHasItemInIt(int items)
        {
            page.ProductsInCart.Should().Be(items);
        }

        [AfterScenario(Order = 0)]
        [Scope(Tag = "cart")]
        public void EmptyCart()
        {
            var cartPage = page.NavigateToCart();
            cartPage.RemoveAllItems();
        }
    }
}
