using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TTCBDD.BaseClass.PageObject
{
    public class PageBase
    {
        protected IWebDriver driver;

        private By LoginBy = By.PartialLinkText("Log in");
        private IWebElement LoginLink;

        private By AccountLinkBy = By.ClassName("account");
        private IWebElement AccountLink;

        protected PageBase(IWebDriver driver)
        {
            this.driver = driver;
            LoginLink = driver.FindElement(LoginBy);
            AccountLink = driver.FindElement(AccountLinkBy);
        }

        #region Action
        public LoginPage NavigateToLoginPage()
        {
            LoginLink.Click();
            return new LoginPage(driver);
        }
        #endregion

        #region Data
        public string LoggedInUser() => AccountLink.Text;
        #endregion
    }
}
