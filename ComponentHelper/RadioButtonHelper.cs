using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TTCBDD.ComponentHelper
{
    public class RadioButtonHelper
    {
        private static IWebElement element;
        public static void ClickRadioButton(By Locator)
        {
            element = GenericHelper.GetWebElement(Locator);
            element.Click();
        }

    }
}
