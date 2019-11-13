using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ComponentHelper
{
    public class LinkHelper
    {
        private static IWebElement element;

        public static void ClinkLink(By Locator)
        {
            element = GenericHelper.GetWebElement(Locator);
            element.Click();
        }
    }
}
