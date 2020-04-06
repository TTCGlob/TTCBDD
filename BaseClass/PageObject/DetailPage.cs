using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;

namespace TTCBDD.PageObject
{
    public class DetailPage : PageBase
    {
        public DetailPage(IWebDriver _driver) : base(_driver)
        {

        }
        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        #region WebElement

        private By emailLink = By.XPath("//div[@class='header-links']/ul/li/a[@href='/customer/info' and @class='account']");
        private By booksLink = By.LinkText("Books");
        private By computersLink = By.LinkText("Computers");

        #endregion

        #region Action

        #endregion

        #region Navigation

        public BooksPage NavigateToBooks()
        {
            driver.FindElement(booksLink).Click();
            return new BooksPage(driver);
        }

        public ComputersPage NavigateToComputers()
        {
            driver.FindElement(computersLink).Click();
            return new ComputersPage(driver);
        }

        #endregion

    }
}
