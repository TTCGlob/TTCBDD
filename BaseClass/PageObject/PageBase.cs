using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TTCBDD.Helpers.UI;
using TTCBDD.Settings;
using System;
using TTCBDD.PageObject;

namespace TTCBDD.BaseClasses
{
    public class PageBase
    {
        public PageBase(IWebDriver _driver)
        {
            driver = _driver;
        //if using the field declaration, need to implicitely define it.Best place is in the constructor so that it will be defined everywhere in the Class
        //The option I have chosen here, is to declare a method "GetHomePageIcon()" so that it gets initiated only when it is called later on in the code.
        //No risk of declarinig and using something that is not defined.

        //  homePageIconField = driver.FindElement(homePageIconBy);
        }

        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        #region WebElement

        protected IWebDriver driver;

        private By homePageIconBy = By.PartialLinkText("Demo Web Shop");

        private IWebElement GetHomePageIcon()
        {
            return driver.FindElement(homePageIconBy);
        }

        //  private IWebElement homePageIconField;

        private By wishListLink = By.PartialLinkText("Wishlist");
        private By cartLink = By.PartialLinkText("Shopping cart");

/*        private By wishListLink = By.XPath("//a[@href='/wishlist']/span[@class='cart-label']");
        private By cartLink = By.XPath("//a[@href='/cart']/span[@class='cart-label']");*/


        #endregion


        #region Action

        protected void Logout()
        {
            if (GenericHelper.IsElementPresent(By.PartialLinkText("Log out")))
            {
                ButtonHelper.ClickButton(By.PartialLinkText("Log out"));
            }
        }

        public string Title()
        {
            return driver.Title;
        }

        public HomePage SetHomePageObject()
        {
            return new HomePage(driver);
        }
        #endregion

        #region Navigation

        protected HomePage NavigateToHomePage()
        {
            GetHomePageIcon().Click();
            return new HomePage(driver);
        }

        public void NavigateToCart()
        {
            driver.FindElement(cartLink).Click();
        }

        public void NavigateToWishlist()
        {
            driver.FindElement(wishListLink).Click();
        }

        #endregion

        /*       

         protected void Login()
            {
                NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetWebsiteUrl());
                LinkHelper.ClinkLink(By.PartialLinkText("Log"));
            } */

    }

}
