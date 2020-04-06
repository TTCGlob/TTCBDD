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
    public class LoginPage : PageBase
    {

        public LoginPage(IWebDriver _driver) : base(_driver)
        {
            
        }

        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        #region WebElement

        private By email = By.Id("Email");
        private By password = By.Id("Password");
        private By rememberMe = By.Id("RememberMe");
        private By logInBtn = By.XPath("//div/input[@value='Log in']");
        private By logOutLink = By.XPath("//a[@href='/logout']");
        private By welcome = By.XPath("//h1");

        #endregion

        #region Action
        
        public string GetWelcomeMessage()
        {
            return driver.FindElement(welcome).Text;
        }
        
        public DetailPage Login()
        {
            var username = ObjectRepository.Config.GetUsername();
            var pass = ObjectRepository.Config.GetPassword();
            driver.FindElement(email).SendKeys(username);
            driver.FindElement(password).SendKeys(pass);
            driver.FindElement(logInBtn).Click();
            return new DetailPage(driver);
        }

        public bool IsDetailPageDisplayed()
        {
            return driver.FindElement(By.XPath("//div[@class='header-links']/ul/li/a[@href='/customer/info' and @class='account']")).
                Text.Equals(ObjectRepository.Config.GetUsername());
        }

        #endregion

        #region Navigation


        #endregion

    }
}
