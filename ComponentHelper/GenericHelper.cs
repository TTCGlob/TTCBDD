using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using TTCBDD.Settings;
using System;

namespace TTCBDD.ComponentHelper
{
    public class GenericHelper
    {
        public static bool IsElementPresent(By Locator)
        {
            try
            {
                return ObjectRepository.Driver.FindElements(Locator).Count == 1;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static IWebElement GetWebElement(By Locator)
        {
            if (IsElementPresent(Locator))
                return ObjectRepository.Driver.FindElement(Locator);
            else
                throw new NoSuchElementException("Element Not Found : " + Locator.ToString());
        }

        public static void TakeScreenshot(string FileName, ScreenshotImageFormat FormatType)
        {
            //FileName = FileName + DateTime.Now();
            ObjectRepository.Driver.TakeScreenshot().SaveAsFile(FileName, FormatType);
        }
    }
}
