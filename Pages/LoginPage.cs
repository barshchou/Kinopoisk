using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {

        }

        public LoginPage()
        {

        }

        private IWebElement LoginTextField => _driver.FindElement(By.Id("passp-field-login"));
        private IWebElement LoginSignInButton => _driver.FindElement(By.XPath("//button[@type = 'submit']"));
        private IWebElement PasswordTextField => _driver.FindElement(By.Id("passp-field-passwd"));

        /// <summary>
        /// Fill in login fields and log in
        /// </summary>
        public void Login(string login, string password)
        {
            Type(login, LoginTextField);
            Click(LoginSignInButton);
            WaitUntilElementIsPresent(By.Id("passp-field-passwd"));
            Type(password, PasswordTextField);
            Click(LoginSignInButton);
        }

        public HomePage OpenHomePage(string login, string password)
        {
            Login(login, password);
            return new HomePage(_driver, _wait);
        }

        public bool AreCredentialsInvalid() => WaitElementIsPresent(By.XPath("//div[contains(@class, 'Textinput-Hint_state_error')]"));

    }
}
