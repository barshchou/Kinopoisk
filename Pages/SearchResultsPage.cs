﻿using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;

namespace Kinopoisk.Pages
{
    public class SearchResultsPage
    {
        private readonly IBrowser _browser;

        public SearchResultsPage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.CssSelector("div.search_results_top"));
        }

        private IWebElement searchResultsHeader => _browser.Page.FindElement(By.CssSelector("div.search_results_top"));
        private IWebElement searchResultItem(string contentName) => _browser.Page.FindElement(By.XPath($"//p[@class = 'name']/a[text() = '{contentName}']"));

        public bool AreSearchResultsDisplayed(string contentName)
        {
            return _browser.Page.IsElementPresent(By.XPath($"//p[@class = 'name']/a[text() = '{contentName}']"));
        }
    }
}