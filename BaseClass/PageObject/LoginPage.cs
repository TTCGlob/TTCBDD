using OpenQA.Selenium;

namespace TTCBDD.BaseClass.PageObject
{
    public class LoginPage : PageBase
    {

        private By EmailFieldBy = By.Id("Email");
        private IWebElement EmailField;

        private By PasswordFieldBy = By.Id("Password");
        private IWebElement PasswordField;

        private By RememberMeBy = By.Id("RememberMe");
        private IWebElement RememberMe;

        private By LoginButtonBy = By.XPath(".//input[@value='Log in']");
        private IWebElement LoginButton;

        public LoginPage(IWebDriver driver) : base(driver)
        {
            EmailField = driver.FindElement(EmailFieldBy);
            PasswordField = driver.FindElement(PasswordFieldBy);
            RememberMe = driver.FindElement(RememberMeBy);
            LoginButton = driver.FindElement(LoginButtonBy);
        }

        #region Action
        public HomePage Login(string email, string password, bool rememberMe = false)
        {
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            if (rememberMe != RememberMeChecked())
                RememberMe.Click();
            LoginButton.Click();
            return new HomePage(driver);
        }
        #endregion

        #region Data
        public bool RememberMeChecked() => RememberMe.Selected;
        #endregion
    }
}