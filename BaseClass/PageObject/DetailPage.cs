using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;
using TTCBDD.BaseClass.PageObject;
using System.Collections;


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
        private By computersAccessoriesLink = By.LinkText("Accessories");
        public Dictionary<string, IWebElement> computerSubMenu;

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


        public Dictionary<string, IWebElement> FetchComputerSubMenu()
        {
            NavigateToComputers();
            IList<IWebElement> subMenus = driver.FindElements(By.XPath("//div[@class='item-box']"));
            Dictionary<string, IWebElement> computerSubMenu = new Dictionary<string, IWebElement>();

            try
            {
                foreach (var item in subMenus)
                {
                    string title = item.FindElement(By.ClassName("title")).Text;
                    computerSubMenu.Add(title, item.FindElement(By.ClassName("title")));
                }
                return computerSubMenu;
            }
            catch (NoSuchElementException e)
            {                
                foreach (var item in subMenus)
                {
                    string title = item.FindElement(By.ClassName("title")).Text;
                    computerSubMenu.Add(title, item.FindElement(By.ClassName("title")));
                }
                return computerSubMenu;
            }   
        }

        public void ClickComputerSubMenu(string title)
        {
            try
            {
                FetchComputerSubMenu()[title].Click();
            }
            catch (NoSuchElementException e)
            {
                NavigateToComputers();
                FetchComputerSubMenu()[title].Click();
            }         
        }

        public Object CreateAppropriateComputerSubMenuObject(string title)
        {
            switch (title)
            {
                case "Desktops":
                    return new ComputersDesktopsDetailsPage(driver);
                case "Notebooks":
                    return new ComputersNotebooksDetailsPage(driver);
                case "Accessories":
                    return new ComputersAccessoriesDetailsPage(driver);
                default:
                    return false;
            }
        }

        #endregion

    }
}
