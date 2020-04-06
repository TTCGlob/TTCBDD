using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Helpers.UI;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;


namespace TTCBDD.PageObject
{
    public class BookDetailsPage : PageBase
    {
        public BookDetailsPage(IWebDriver _driver) : base(_driver)
        {

        }

        private By price = By.XPath("//span[@class='price-value-13']");

        public string BookPricing()
        {
            return driver.FindElement(price).Text;
        }
    }
}
