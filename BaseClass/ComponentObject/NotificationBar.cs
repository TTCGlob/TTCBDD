using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.BaseClass.ComponentObject
{
	public class NotificationBar
	{
		private readonly IWebDriver driver;
		private readonly By BarElementBy = By.Id("bar-notification");
		private IWebElement barElement { get => driver.FindElement(BarElementBy); }

		#region Data
		public string Classes { get => barElement.GetAttribute("class"); }
		public string Message { get => barElement.FindElement(By.ClassName("content")).Text; }
		#endregion

		public NotificationBar(IWebDriver driver)
		{
			this.driver = driver;
		}

		#region Waits
		public void WaitForNotification()
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
			wait.Until(_ => barElement.Displayed);
		}
		#endregion
	}
}
