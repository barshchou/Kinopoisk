using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinopoisk.Pages
{
    public class MediaContentPage
    {
        private readonly IBrowser _browser;
        private const string TRAILERWINDOWFRAME = "discovery-trailers-iframe";

        public MediaContentPage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.XPath("//div[contains(@class, 'styles_posterContainer')]"));
        }

        private IWebElement favouritesButton => _browser.Page.FindElement(By.XPath("//div/button[contains(@class, 'styles_listToWatchButton')]"));
        private IWebElement watchedButton => _browser.Page.FindElement(By.XPath("//div[contains(@class, 'styles_basicMediaSection')]//div/button[contains(@class, 'styles_watchedButton')]"));
        private IWebElement watchTrailerButton => _browser.Page.FindElement(By.XPath("//div/div[contains(@class, 'film-trailer')]"));
        private IWebElement favoritesFolder => _browser.Page.FindElement(By.XPath("//div/a[contains(text(), 'Буду смотреть')]"));
        private IWebElement trailerIFrame => _browser.Page.FindElement(By.ClassName(TRAILERWINDOWFRAME));
        private IWebElement playerFramer => _browser.Page.FindElement(By.Id("player"));
        private IWebElement trailerIFrameCloser => _browser.Page.FindElement(By.ClassName("discovery-trailers-closer"));

        public void AddContentToFavourites()
        {
            if (!IsContentAddedToFavorites())
            _browser.Page.Click(favouritesButton);
        }

        public bool WasContentAddedToFavorites() => _browser.Page.IsElementPresent(By.XPath("//div[contains(text(), 'Фильм добавлен в папку «Буду смотреть»')]"));

        public void AddContentToWatched()
        {
            if (!IsContentAddedToWatched())
                _browser.Page.Click(watchedButton);
        }

        public bool WasContentRemovedFromFavorites() =>_browser.Page.IsElementPresent(By.XPath("//div[contains(text(), 'Фильм удалён из папки «Буду смотреть»')]"));

        public bool IsContentAddedToFavorites() => _browser.Page.IsElementPresent(By.XPath(
            "//div[contains(@class, 'styles_userControlsContainer')]//div/button[contains(@class, 'styles_rootActive')][text() = 'Буду смотреть']"));
        public bool IsContentAddedToWatched() => _browser.Page.IsElementPresent(By.XPath(
            "//div[contains(@class, 'styles_userControlsContainer')]//div/button/span[contains(text(), 'Отметить просмотренным')]"));

        public void OpenTrailer()
        {
            _browser.Page.Click(watchTrailerButton);
            _browser.Page.SwitchTo().Frame(trailerIFrame);
        }

        public bool IsTrailerPlayerDisplayed() => _browser.Page.IsElementPresent(By.Id("player"));

        public void CloseTrailerFrame() 
        {
            _browser.Page.SwitchTo().DefaultContent();
            _browser.Page.Click(trailerIFrameCloser);
        } 

        public Profile_MoviesPage OpenFavoritesFolder()
        {
            _browser.Page.Click(favoritesFolder);
            return new Profile_MoviesPage(_browser);
        }

        public HomePage OpenHomePage()
        {
            _browser.Page.GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_browser);
        }
    }
}
