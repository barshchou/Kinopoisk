using Kinopoisk.Core.Helpers;
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
            _browser.Page.WaitUntilElementIsPresent(By.XPath("//div[@class = 'desktop-layout-with-sidebar__content']"));
        }

        private IWebElement loginButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Войти')]"));
        private IWebElement logoutButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Выйти')]"));
        private IWebElement userProfile => _browser.Page.FindElement(By.XPath("//div[contains(@class, 'header-v4__user-bar')]/div/button"));
        private IWebElement searchContentField => _browser.Page.FindElement(By.CssSelector("input[name='kp_query']"));
        private IWebElement searchButton => _browser.Page.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement searchResultsSuggested(string contentName) => _browser.Page.FindElement(
            By.XPath($"//div/h4[text() = '{contentName}']"));

        public LoginPage OpenLoginPage()
        {
            if (IsUserLoggedIn())
                Logout();

            ClickLogin();
            return new LoginPage(_browser);
        }

        public SearchResultsPage SearchContent(string contentName)
        {
            Search(contentName);
            return new SearchResultsPage(_browser);
        }

        public void ClickLogin() => _browser.Page.Click(loginButton);
        public void Logout()
        {
            if (IsUserLoggedIn())
            {
                _browser.Page.MoveToElement(userProfile);
                _browser.Page.Click(logoutButton);
            }
        }

        public bool IsUserLoggedIn() 
        {
            return _browser.Page.IsElementPresent(By.XPath("//button[contains(text(), 'Выйти')]"));
        }

        public void Search(string contentName)
        {
            _browser.Page.Type(contentName, searchContentField);
            _browser.Page.Click(searchButton);
        }

        public void SearchSuggestedContent(string contentName)
        {
            _browser.Page.Type(contentName, searchContentField);
            _browser.Page.Click(searchResultsSuggested(contentName));
        }
    }
}
