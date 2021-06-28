using Kinopoisk.Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class Profile_MoviesPage : BasePage
    {
        public Profile_MoviesPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.XPath("//li/span[contains(text(), 'Фильмы')]"));
        }

        public Profile_MoviesPage()
        {

        }

        private IWebElement ContentItem(string contentName) => _driver.FindElement(By.XPath($"//div/a[@class = 'name'][text() = '{contentName}']"));
        private IWebElement SelectAllTitlesCheckbox => _driver.FindElement(By.CssSelector("#selectAllbox"));
        private IWebElement RemoveSelectedButton => _driver.FindElement(By.CssSelector("#delete_selected[value='удалить отмеченные фильмы']"));

        public bool IsContentAddedToFavorites(string contentName) => IsElementPresent(By.XPath($"//div/a[@class = 'name'][text() = '{contentName}']"));

        public void RemoveAllFavourites()
        {
            MoveToElement(SelectAllTitlesCheckbox);
            Click(SelectAllTitlesCheckbox);
            Click(RemoveSelectedButton);
            _driver.SwitchTo().Alert().Accept();
        }

        public bool IsFavoritesPurged() => WaitUntilElementIsPresent(By.XPath("//p[@class = 'emptyMessage']"));

        public MediaContentPage OpenContenItem(string contentName)
        {
            Click(ContentItem(contentName));
            return new MediaContentPage(_driver, _wait);
        }

        public HomePage OpenHomePage()
        {
            _driver.Navigate().GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_driver, _wait);
        }
    }
}
