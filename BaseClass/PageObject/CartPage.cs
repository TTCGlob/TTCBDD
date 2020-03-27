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
    public class CartPage : PageBase
    {
        public CartPage(IWebDriver _driver) : base (_driver)
        {

        }

        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        #region WebElement

        private By pageTitle = By.PartialLinkText("Shopping cart");

        #endregion

        #region Action

        public bool IsTitled(string title)
        {
            //var clickCart = new PageBase();

            try
            {
                return driver.FindElement(pageTitle).Text.Contains(title);
            }
            catch (Exception)
            {
                clickCart.NavigateToCart();
                return driver.FindElement(pageTitle).Text.Contains(title);

            }
        }

        #endregion

        #region Navigation

        #endregion
    }
}
