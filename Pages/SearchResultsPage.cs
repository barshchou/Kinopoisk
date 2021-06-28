using Kinopoisk.Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.CssSelector("div.search_results_top"));
        }

        public SearchResultsPage()
        {

        }

        private IWebElement SearchResultsHeader => _driver.FindElement(By.CssSelector("div.search_results_top"));
        private IWebElement SearchResultItem(string contentName) => _driver.FindElement(By.XPath($"//p[@class = 'name']/a[text() = '{contentName}']"));
        private IWebElement ContentCountry => _driver.FindElement(By.XPath("//div[@class = 'info']/span[2]"));
        private IWebElement ContentYear => _driver.FindElement(By.XPath("//div[@class = 'info']/p/span[1]"));

        public bool AreSearchResultsDisplayed(string contentName) => IsElementPresent(By.XPath($"//p[@class = 'name']/a[text() = '{contentName}']"));
        public bool IsSearchResultsFilteredByCountry(string country) => IsElementPresent(By.XPath($"//div[@class = 'info']/span[2][contains(text(), '{country}')]"));
        public bool IsSearchResultsFilteredByYear(string yearFrom, string yearTo) 
        {
            var result = false;
            if (int.Parse(GetText(ContentYear).Substring(0, 4)) > int.Parse(yearFrom) 
                & int.Parse(GetText(ContentYear).Substring(0, 4)) < int.Parse(yearTo)) 
                result = true;
            return result;
        }

        public string GetText(IWebElement element) => element.Text;

        public MediaContentPage OpenContentItem(string contentName)
        {
            Click(SearchResultItem(contentName));
            return new MediaContentPage(_driver, _wait);
        }

        public HomePage OpenHomePage()
        {
            _driver.Navigate().GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_driver, _wait);
        }
    }
}
