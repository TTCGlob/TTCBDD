using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using System;

namespace TTCBDD.BaseClasses
{
    public class PageBase
    {
        //Region WebElement:

        private IWebDriver driver;
        private By homePageIcon = By.XPath("//img[@src='/Themes/DefaultClean/Content/images/logo.png']");
        private IWebElement homePageIcon1 =  By.XPath("//img[@src='/Themes/DefaultClean/Content/images/logo.png']");
        private By cartLink = By.XPath("//a[@href='/cart']/span[@class='cart-label']");
        private By wishlistLink = By.XPath("//a[@href='/wishlist']/span[@class='cart-label']");

        //Constructor
        [Obsolete]
        public PageBase(IWebDriver _driver)
        {
            PageFactory.InitElements(_driver, this);
        }

        //Region Action:
        protected void Logout()
        {
            if (GenericHelper.IsElementPresent(By.XPath("//a[contains(normalize-space(),'Log out')]")))
            {
                ButtonHelper.ClickButton(By.XPath("//a[contains(normalize-space(),'Log out')]"));
            }
        }

        public string Title()
        {
            return driver.Title;
        }

        //Region Navigation:
        protected void NavigateToHomePage()
        {
            ObjectRepository.Driver.FindElement(homePageIcon).Click();
        }

        public void NavigateToCart()
        {
            ObjectRepository.Driver.FindElement(cartLink).Click();
        }

        public void NavigateToWishlist()
        {
            ObjectRepository.Driver.FindElement(wishlistLink).Click();
        }

        /*       


         protected void Login()
            {
                NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetWebsiteUrl());
                LinkHelper.ClinkLink(By.PartialLinkText("Log"));
            } */

    }

}
