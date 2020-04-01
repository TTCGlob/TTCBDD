using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace TTCBDD.BaseClass.PageObject
{
    public class PageBase
    {
        protected IWebDriver driver;

        private By LoginBy = By.PartialLinkText("Log in");
        private IWebElement LoginLink { get => driver.FindElement(LoginBy); }

        private By AccountLinkBy = By.ClassName("account");
        private IWebElement AccountLink { get => driver.FindElement(AccountLinkBy); }

        private By HeaderMenuBy = By.ClassName("header-menu");
        private IWebElement HeaderMenu { get => driver.FindElement(HeaderMenuBy); }

        private By ShoppingCartLinkBy = By.PartialLinkText("Shopping cart");
        private IWebElement ShoppingCartLink { get => driver.FindElement(ShoppingCartLinkBy); }

        private By HeaderCategoryBy(string category) => By.PartialLinkText(category);

        #region Data
        public string LoggedInUser { get => AccountLink.Text; }
        public int ProductsInCart
        {
            get
            {
                var cartLinkText = ShoppingCartLink.Text;
                string match = Regex.Match(cartLinkText, @"\d+").Value;
                return int.Parse(match);
            }
        }
        #endregion

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        #region Action
        public LoginPage NavigateToLoginPage()
        {
            LoginLink.Click();
            return new LoginPage(driver);
        }

        public ShoppingCartPage NavigateToCart()
        {
            ShoppingCartLink.Click();
            return new ShoppingCartPage(driver);
        }

        public ProductPage ClickHeaderProductCategory(string category)
        {
            HeaderMenu.FindElement(HeaderCategoryBy(category)).Click();
            return new ProductPage(driver);
        }
        #endregion

        
    }
}
