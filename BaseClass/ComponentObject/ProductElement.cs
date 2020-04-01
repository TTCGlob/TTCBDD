using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.UtilityClasses;

namespace TTCBDD.BaseClass.ComponentObject
{
	public class ProductElement
	{
		private readonly IWebElement element;

		private readonly By ProductNameBy = By.ClassName("product-title");
		public string ProductName { get => element.FindElement(ProductNameBy).Text; }

		private readonly By BuyNowButtonBy = By.XPath("//input[contains(@class, 'product-box-add-to-cart-button')]");

		public ProductElement(IWebElement productElement)
		{
			element = productElement;
		}

		#region Action
		public void AddProductToCart()
		{
			element.FindElement(BuyNowButtonBy).Click();
		}
		#endregion
	}
}
