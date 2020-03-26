using OpenQA.Selenium;

namespace TTCBDD.Helpers.UI
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
