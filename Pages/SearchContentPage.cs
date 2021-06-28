using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class SearchContentPage : BasePage
    {
        public SearchContentPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.CssSelector(".moviename-big"));
        }

        public SearchContentPage()
        {

        }

        private IWebElement YearFilter => _driver.FindElement(By.CssSelector(".__yearSB__"));
        private IWebElement YearFromFilter => _driver.FindElement(By.CssSelector(".__yearSB1__"));
        private IWebElement YearToFilter => _driver.FindElement(By.CssSelector(".__yearSB2__"));
        private IWebElement CountryFilter => _driver.FindElement(By.CssSelector(".__countrySB__"));
        private IWebElement SearchButton => _driver.FindElement(By.CssSelector(".nice_button"));

        public SearchResultsPage SearchContentByFilter(string country, string yearFrom, string yearTo)
        {
            Select(CountryFilter, country);
            Select(YearFromFilter, yearFrom);
            Select(YearToFilter, yearTo);
            Click(SearchButton);
            return new SearchResultsPage(_driver, _wait);
        }
    }
}
