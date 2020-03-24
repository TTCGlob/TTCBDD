using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using System;

namespace TTCBDD.BaseClasses
{
    public class PageBase
    {
        private IWebDriver driver;

        [Obsolete]
        public PageBase (IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        protected void Login()
        {
            NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetWebsiteUrl());
            LinkHelper.ClinkLink(By.PartialLinkText("Log"));
        } 

        public string Title()
        {
            return driver.Title; 
        }

    }
}
