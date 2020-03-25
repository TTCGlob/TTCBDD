using OpenQA.Selenium;

namespace TTCBDD.Helpers.UI
{
    public class TextBoxHelper
    {
        private static IWebElement element;
        public static void TypeInTextBox(By Locator, string Text)
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
