using System;
using TechTalk.SpecFlow;
using TTCBDD.Configuration;
using OpenQA.Selenium;
using TTCBDD.PageObject;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;
using TTCBDD.Helpers.UI;
using NUnit.Framework;
using FluentAssertions;
using TTCBDD.BaseClass.PageObject;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class DemoWebShopFeatureSteps
    {
        public PageBase pageBase;
        public HomePage hPage;
        public LoginPage lPage;
        public DetailPage detailPage;
        public BooksPage booksPage;
        public BookDetailsPage bookDetailPage;
        public ComputersPage computerPage;
     

        [Given(@"I navigated to the DemoWebShop website")]
        public void GivenINavigatedToTheDemoWebShopWebsite()
        {          
            PageBase initHomePage = new PageBase(ObjectRepository.Driver);
            pageBase = initHomePage;
            hPage = initHomePage.SetHomePageObject();
        }

        [When(@"I click the Log in link")]
        public void WhenIClickTheLogInLink()
        {
            lPage = hPage.NavigateToLogin();
        }

        [When(@"I submit username and password")]
        public void WhenISubmitUsernameAndPassword()
        {
            detailPage = lPage.Login();
        }

        [Then(@"the DetailPage displays")]
        public void ThenTheDetailPageDisplays()
        {
            lPage.IsDetailPageDisplayed();
        }

        [Then(@"the LoginPage displays with title ""(.*)""")]
        public void ThenTheLoginPageDisplaysWithTitle(string title)
        {
            GenericHelper.ActualAndExpectedAreEqual(title, lPage.GetWelcomeMessage());
        }


        //Scenario: Confirm Price of chosen item

        [Given(@"I navigated to the Books page")]
        public void GivenINavigatedToTheBooksPage()
        {
            booksPage = detailPage.NavigateToBooks();
        }

        [When(@"I click the ""(.*)"" link")]
        public void WhenIClickTheLink(string bookTitle)
        {
            bookDetailPage = booksPage.SelectBook(bookTitle);
        }

        [Then(@"I confirm price is ""(.*)""")]
        public void ThenIConfirmPriceIs(string price)
        {
            GenericHelper.ActualAndExpectedAreEqual(bookDetailPage.BookPricing(), price);
         //   bookDetailPage.BookPricing().Should().Be(price);
        }



        // Scenario: Add product to cart failed

        [Given(@"I navigated to Computers > ""(.*)""")]
        public void GivenINavigatedToComputers(string title)
        {
            detailPage.FetchComputerSubMenu();
            detailPage.ClickComputerSubMenu(title);
            ComputersAccessoriesDetailsPage computersAccessories = (ComputersAccessoriesDetailsPage)detailPage.CreateAppropriateComputerSubMenuObject(title);
        }


        [When(@"I add ""(.*)"" to my cart")]
        public void WhenIAddToMyCart(string itemTitle)
        {
            pageBase.AddAnyItemToCart(itemTitle);
            pageBase.AddToCart();
        }

        [Then(@"the notification bar displays ""(.*)""")]
        public void ThenTheNotificationBarDisplays(string message)
        {
            pageBase.MessageInNotificationBar().Should().Be(message);
        }

        [Then(@"(.*) item is visible in the cart")]
        public void ThenItemIsVisibleInTheCart(int numberOfItemInCart)
        {
            
        }

    }
}
