using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TTCBDD.ComponentHelper
{
    public class ComboBoxHelper
    {
        private static SelectElement select;
        public static void SelectElement(By Locator, int index)
        {
            SelectElement select = new SelectElement(GenericHelper.GetWebElement(Locator));
            select.SelectByIndex(index);
        }
    }
}
