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
    public class HomePage : PageBase
    {

        public HomePage(IWebDriver _driver) : base(_driver)
        {

        }

        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        #region WebElement


        private By newsLetterEmail = By.Id("newsletter-email");
        private By subscribe = By.Id("newsletter-subscribe-button");
        private By newsLetterResult = By.Id("newsletter-result-block");

        private By login = By.XPath("//a[@href= ' / login']");

        #endregion

        #region Action

        public void Subscribe(string email)
        {
            driver.FindElement(newsLetterEmail).SendKeys(email);
            driver.FindElement(subscribe).Click();
        }

        public bool HasSubscribed()
        {
            try
            {
                return ObjectRepository.Driver.FindElement(newsLetterResult).
                    Text.Equals("Thank you for signing up! A verification email has been sent. We appreciate your interest.");
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Navigation

        public LoginPage NavigateToLogin()
        {
            driver.FindElement(login).Click();
            return new LoginPage(driver);
        }

        #endregion


    }
}
