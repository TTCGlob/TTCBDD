using OpenQA.Selenium;

namespace TTCBDD.Helpers.UI
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
