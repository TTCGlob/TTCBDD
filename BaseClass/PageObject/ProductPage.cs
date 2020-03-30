using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using TTCBDD.BaseClass.ComponentObject;

namespace TTCBDD.BaseClass.PageObject
{
	public class ProductPage : PageBase
	{
		private readonly By ProductElementBy = By.ClassName("product-item");
		private Dictionary<string, ProductElement> products
		{
			get => driver.FindElements(ProductElementBy)
				.Select(e => new ProductElement(e))
				.ToDictionary(p => p.ProductName);
		}
		public ProductPage(IWebDriver driver) : base(driver) { }

		public void AddProductToCart(string productName)
		{
			try
			{
				var product = products[productName];
				product.AddProductToCart();
			}
			catch (KeyNotFoundException)
			{
				throw new NoSuchElementException($"Product {productName} not found");
			}
		}
	}
}