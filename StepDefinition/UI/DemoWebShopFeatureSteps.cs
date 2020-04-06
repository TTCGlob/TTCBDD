using System;
using TechTalk.SpecFlow;
using TTCBDD.Configuration;
using OpenQA.Selenium;
using TTCBDD.PageObject;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;
using TTCBDD.Helpers.UI;
using NUnit.Framework;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class DemoWebShopFeatureSteps
    {

        public HomePage hPage;
        public LoginPage lPage;
        public DetailPage detailPage;
        public BooksPage booksPage;
        public BookDetailsPage bookDetailPage;

        [Given(@"I navigated to the DemoWebShop website")]
        public void GivenINavigatedToTheDemoWebShopWebsite()
        {
            PageBase initHomePage = new PageBase(ObjectRepository.Driver);
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


        //Scenario: navigate to the books page and order a specific book at a specified price

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
            GenericHelper.ActualAndExpectedAreEqual(price, bookDetailPage.BookPricing());
        }
    }
}
