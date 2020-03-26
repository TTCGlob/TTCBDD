using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;

namespace TTCBDD.PageObject
{
    public class HomePage : PageBase
    {
        //Region WebElement: WebElement definition for that page - Search text box, Log In link, etc...
        //Region Action: The methods definition aka the action that can be performed on the Webpage e.g. Search, Vote, Subscribe
        //Region Navigation: Navigation from current Webpage to other pages e.g. Navigate to Book page, to Login Page, etc...

        //Region WebElement:
        private IWebDriver driver;
        
        private By newsLetterEmail = By.Id("newsletter-email");
        private By subscribe = By.Id("newsletter-subscribe-button");
        private By newsLetterResult = By.Id("newsletter-result-block");

        private By login = By.XPath("//a[@href= ' / login']");

        //constructor
        [Obsolete]
        public HomePage(IWebDriver _driver) : base(_driver)
        {
            this.driver = _driver;
        }

        //Region Action:

        public void Subscribe(string email)
        {
            ObjectRepository.Driver.FindElement(newsLetterEmail).SendKeys(email);
            ObjectRepository.Driver.FindElement(subscribe).Click();           
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

        //Region Navigation:
        [Obsolete]
        public LoginPage NavigateToLogin()
        {
            ObjectRepository.Driver.FindElement(login).Click();
            return new LoginPage(driver);
        }

    }
}
