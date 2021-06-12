using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using Wait = SeleniumExtras.WaitHelpers;

namespace Kinopoisk.Pages
{
    public class LoginPage
    {
        private readonly IBrowser _browser;

        public LoginPage(IBrowser browser)
        {
            _browser = browser;
        }

        private IWebElement loginTextField => _browser.Page.FindElement(By.Id("passp-field-login"));
        private IWebElement loginSignInButton => _browser.Page.FindElement(By.XPath("//button[@type = 'submit']"));
        private IWebElement passwordTextField => _browser.Page.FindElement(By.Id("passp-field-passwd"));

        /// <summary>
        /// Fill in login fields and log in
        /// </summary>
        public HomePage Login()
        {
            loginTextField.SendKeys(BrowserConfig.Login);
            loginSignInButton.Click();
            _browser.Page.ExplicitWait();
            passwordTextField.SendKeys(BrowserConfig.Password);
            loginSignInButton.Click();
            return new HomePage(_browser);
        }
    }
}
