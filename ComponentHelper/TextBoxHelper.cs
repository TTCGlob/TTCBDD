using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ComponentHelper
{
    public class TextBoxHelper
    {
        private static IWebElement element;
        public static void TypeInTextBox(By Locator,string Text)
        {
            element = GenericHelper.GetWebElement(Locator);
            element.SendKeys(Text);
        }

        public static void ClearTextBox(By Locator)
        {
            element = GenericHelper.GetWebElement(Locator);
            element.Clear();
        }
    }
}
