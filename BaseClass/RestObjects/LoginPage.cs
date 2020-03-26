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

        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...


        //Region WebElement
        private IWebDriver driver;
        private By email = By.Id("Email");
        private By password = By.Id("Password");
        private By rememberMe = By.Id("RememberMe");
        private By logInBtn = By.XPath("//div/input[@value='Log in']");
        private By logOutLink = By.XPath("//a[@href='/logout']");


        //constructor
        [Obsolete]
        public LoginPage(IWebDriver _driver) : base(_driver)
        {
            this.driver = _driver;
        }

        //Region Action
        public DetailPage Login(string username, string pass)
        {
            ObjectRepository.Driver.FindElement(email).SendKeys(username);
            ObjectRepository.Driver.FindElement(password).SendKeys(pass);
            ObjectRepository.Driver.FindElement(logInBtn).Click();
            return new DetailPage();
        }

        //Region Navigation
        public void NavigateToHomePage()
        {
            ObjectRepository.Driver.FindElement(logOutLink).Click();
        }
    }
}
