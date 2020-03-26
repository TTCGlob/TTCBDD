using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Settings;

namespace TTCBDD.PageObject
{
    public class DetailPage
    {
        //Region WebElement

        private By emailLink = By.XPath("//div[@class='header-links']/ul/li/a[@href='/customer/info' and @class='account']");
        private By booksLink = By.LinkText("Books");
        private By computersLink = By.LinkText("Computers");

        //Region Action


        //Region Navigation

        public void NavigateToBooks()
        {
            ObjectRepository.Driver.FindElement(booksLink).Click();
        }







    }
}
