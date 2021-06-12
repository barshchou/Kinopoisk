using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace Kinopoisk.Pages
{
    public class HomePage
    {
        private readonly IBrowser _browser;

        public HomePage(IBrowser browser)
        {
            _browser = browser;
        }

        private IWebElement loginButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Войти')]"));
        private IWebElement logoutButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Выйти')]"));
        private IWebElement userProfile => _browser.Page.FindElement(By.XPath("//div[contains(@class, 'header-v4__user-bar')]/div/button"));

        public LoginPage OpenLoginPage()
        {
            ClickLogin();
            return new LoginPage(_browser);
        }

        public void ClickLogin() => loginButton.Click();
        public void Logout()
        {
            Actions ac = new Actions(_browser.Page);
            ac.MoveToElement(userProfile).Build().Perform();
            logoutButton.Click();
        }

        public bool IsUserLoggedIn() 
        {
            return _browser.Page.FindElements(By.XPath("//button[contains(text(), 'Войти')]")).Count <= 0;
        }

    }
}
