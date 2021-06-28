using Kinopoisk.Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class MediaContentPage : BasePage
    {
        private const string TRAILERWINDOWFRAME = "discovery-trailers-iframe";
        private const string TRAILERCHILDFRAME = "//*[@id='player']/div/iframe";

        public MediaContentPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.XPath("//div[contains(@class, 'styles_posterContainer')]"));
        }

        public MediaContentPage()
        {

        }

        private IWebElement FavouritesButton => _driver.FindElement(By.XPath("//div/button[contains(@class, 'styles_listToWatchButton')]"));
        private IWebElement WatchedButton => _driver.FindElement(By.XPath("//div[contains(@class, 'styles_basicMediaSection')]//div/button[contains(@class, 'styles_watchedButton')]"));
        private IWebElement WatchTrailerButton => _driver.FindElement(By.XPath("//div/div[contains(@class, 'film-trailer')]"));
        private IWebElement FavoritesFolder => _driver.FindElement(By.XPath("//div/a[contains(text(), 'Буду смотреть')]"));
        private IWebElement TrailerIFrame => _driver.FindElement(By.ClassName(TRAILERWINDOWFRAME));
        private IWebElement ChildTrailerFrame => _driver.FindElement(By.XPath(TRAILERCHILDFRAME));
        private IWebElement PlayerFrame => _driver.FindElement(By.Id("player"));
        private IWebElement TrailerIFrameCloser => _driver.FindElement(By.ClassName("discovery-trailers-closer"));
        private IWebElement PlayButton => _driver.FindElement(By.XPath("//div/button[@data-control-name = 'play']")); 
        private IWebElement SkipAdsButton => _driver.FindElement(By.XPath("//div[contains(text(), 'Пропустить')]"));
        private IWebElement StreamPlayer => _driver.FindElement(By.XPath("//div[@id = 'q']/div[@tabindex = '-1']"));

        public void AddContentToFavourites()
        {
            if (!IsContentAddedToFavorites())
            Click(FavouritesButton);
        }

        public bool WasContentAddedToFavorites() => IsElementPresent(By.XPath("//div[contains(text(), 'Фильм добавлен в папку «Буду смотреть»')]"));

        public void AddContentToWatched()
        {
            if (!IsContentAddedToWatched())
                Click(WatchedButton);
        }

        public bool WasContentRemovedFromFavorites() => IsElementPresent(By.XPath("//div[contains(text(), 'Фильм удалён из папки «Буду смотреть»')]"));

        public bool IsContentAddedToFavorites() => IsElementPresent(By.XPath(
            "//div[contains(@class, 'styles_userControlsContainer')]//div/button[contains(@class, 'styles_rootActive')][text() = 'Буду смотреть']"));
        public bool IsContentAddedToWatched() => IsElementPresent(By.XPath(
            "//div[contains(@class, 'styles_userControlsContainer')]//div/button/span[contains(text(), 'Отметить просмотренным')]"));

        /// <summary>
        /// Open trailer and switch to a new iframes
        /// </summary>
        public void OpenTrailer()
        {
            Click(WatchTrailerButton);
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(TrailerIFrame);
        }

        public void PlayPauseTrailer()
        {
            MoveToElement(PlayButton);
            Click(PlayButton);
        }

        public void WaitTrailerAds() => WaitElementIsPresent(By.XPath("//div/button[@data-control-name = 'play']"), 60000);

        public bool IsTrailerPlayerDisplayed() => IsElementPresent(By.Id("player"));

        public void CloseTrailerFrame() 
        {
            _driver.SwitchTo().DefaultContent();
            Click(TrailerIFrameCloser);
        }

        public Profile_MoviesPage OpenFavoritesFolder()
        {
            Click(FavoritesFolder);
            return new Profile_MoviesPage(_driver, _wait);
        }

        public HomePage OpenHomePage()
        {
            _driver.Navigate().GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_driver, _wait);
        }
    }
}
