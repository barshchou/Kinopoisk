using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
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
            _browser.Page.Type(BrowserConfig.Login, loginTextField);
            _browser.Page.Click(loginSignInButton);
            _browser.Page.IsElementPresent(By.Id("passp-field-passwd"));
            _browser.Page.Type(BrowserConfig.Password, passwordTextField);
            _browser.Page.Click(loginSignInButton);
            return new HomePage(_browser);
        }
    }
}
