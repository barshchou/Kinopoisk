using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
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
        private IWebElement contentCountry => _browser.Page.FindElement(By.XPath("//div[@class = 'info']/span[2]"));
        private IWebElement contentYear => _browser.Page.FindElement(By.XPath("//div[@class = 'info']/p/span[1]"));

        public bool AreSearchResultsDisplayed(string contentName) => _browser.Page.IsElementPresent(By.XPath($"//p[@class = 'name']/a[text() = '{contentName}']"));
        public bool IsSearchResultsFilteredByCountry(string country) => _browser.Page.IsElementPresent(By.XPath($"//div[@class = 'info']/span[2][contains(text(), '{country}')]"));
        public bool IsSearchResultsFilteredByYear(string yearFrom, string yearTo) 
        {
            var result = false;
            if (int.Parse(GetText(contentYear).Substring(0, 4)) > int.Parse(yearFrom) 
                & int.Parse(GetText(contentYear).Substring(0, 4)) < int.Parse(yearTo)) 
                result = true;
            return result;
        }

        public string GetText(IWebElement element) => element.Text;

        public MediaContentPage OpenContentItem(string contentName)
        {
            _browser.Page.Click(searchResultItem(contentName));
            return new MediaContentPage(_browser);
        }

        public HomePage OpenHomePage()
        {
            _browser.Page.GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_browser);
        }
    }
}
