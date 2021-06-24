using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;

namespace Kinopoisk.Pages
{
    public class SearchContentPage
    {
        private readonly IBrowser _browser;

        public SearchContentPage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.CssSelector(".moviename-big"));
        }

        private IWebElement yearFilter => _browser.Page.FindElement(By.CssSelector(".__yearSB__"));
        private IWebElement yearFromFilter => _browser.Page.FindElement(By.CssSelector(".__yearSB1__"));
        private IWebElement yearToFilter => _browser.Page.FindElement(By.CssSelector(".__yearSB2__"));
        private IWebElement countryFilter => _browser.Page.FindElement(By.CssSelector(".__countrySB__"));
        private IWebElement searchButton => _browser.Page.FindElement(By.CssSelector(".nice_button"));

        public SearchResultsPage SearchContentByFilter(string country, string yearFrom, string yearTo)
        {
            _browser.Page.Select(countryFilter, country);
            _browser.Page.Select(yearFromFilter, yearFrom);
            _browser.Page.Select(yearToFilter, yearTo);
            _browser.Page.Click(searchButton);
            return new SearchResultsPage(_browser);
        }
    }
}
