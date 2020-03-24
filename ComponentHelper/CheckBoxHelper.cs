using OpenQA.Selenium;

namespace TTCBDD.ComponentHelper
{
    public class CheckBoxHelper
    {
        private static IWebElement element;
        public static void CheckedCheckBox(By Locator)
        {
            element = GenericHelper.GetWebElement(Locator);
            element.Click();
        }

        public static bool IsCheckedBoxCheck(By Locator)
        {
            element = GenericHelper.GetWebElement(Locator);
            string flag = element.GetAttribute("value");

            if (flag == null)
                return false;
            else
                return true;
        }
    }
}
