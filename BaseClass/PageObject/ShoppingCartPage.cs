using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Helpers.Rest;

namespace TTCBDD.BaseClass.PageObject
{
	public class ShoppingCartPage : PageBase
	{
		private readonly By UpdateShoppingCartBy = By.Name("updatecart");
		private IWebElement UpdateShoppingCart { get => driver.FindElement(UpdateShoppingCartBy); }

		private readonly By CartItemRowBy = By.ClassName("cart-item-row");
		private IReadOnlyCollection<IWebElement> CartItems { get => driver.FindElements(CartItemRowBy); }

		private readonly By RemoveFromCartBy = By.Name("removefromcart");

		public ShoppingCartPage(IWebDriver driver) : base(driver)
		{

		}

		public void RemoveAllItems()
		{
			CartItems.ForEach(i => i.FindElement(RemoveFromCartBy).Click());
			UpdateShoppingCart.Click();
		}
	}
}
