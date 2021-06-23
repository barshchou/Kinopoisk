using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kinopoisk.Pages
{
    public class MediaContentPage
    {
        private readonly IBrowser _browser;
        private const string TRAILERWINDOWFRAME = "discovery-trailers-iframe";
        private const string TRAILERCHILDFRAME = "//*[@id='player']/div/iframe";

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
        private IWebElement childTrailerFrame => _browser.Page.FindElement(By.XPath(TRAILERCHILDFRAME));
        private IWebElement playerFrame => _browser.Page.FindElement(By.Id("player"));
        private IWebElement trailerIFrameCloser => _browser.Page.FindElement(By.ClassName("discovery-trailers-closer"));
        private IWebElement playButton => _browser.Page.FindElement(By.XPath("//div/button[@data-control-name = 'play']")); 
        private IWebElement skipAdsButton => _browser.Page.FindElement(By.XPath("//div[contains(text(), 'Пропустить')]"));
        private IWebElement streamPlayer => _browser.Page.FindElement(By.XPath("//div[@id = 'q']/div[@tabindex = '-1']"));
        private IWebElement trailerSkipButton => GetTrailerShadowRoot(streamPlayer);
        private IWebElement shadowRoot => GetTrailerShadowRoot(streamPlayer);
        private IWebElement trailerSkipButtonShadow => shadowRoot.FindElement(By.XPath("//div[contains(text(), 'Пропустить')]"));


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

        /// <summary>
        /// Open trailer and switch to a new iframes
        /// </summary>
        public void OpenTrailer()
        {
            _browser.Page.Click(watchTrailerButton);
            _browser.Page.SwitchTo().DefaultContent();
            _browser.Page.SwitchTo().Frame(trailerIFrame);
        }

        /// <summary>
        /// Get shadow root DOM of trailer window
        /// NOTE: Not working properly
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Shadow root document</returns>
        public IWebElement GetTrailerShadowRoot(IWebElement element) 
        {
            var shadowRoot = _browser.JavaScript.Execute("return arguments[0].shadowRoot", element);
            var trailerSkip = (IWebElement)_browser.JavaScript.Execute("arguments[0].querySelector('.lPkK2Gz6Id')", shadowRoot);
            return trailerSkip;
        }

        /// <summary>
        /// Skip trailer ads twice
        /// </summary>
        public void SkipTrailerAds()
        {
            _browser.Page.Click(trailerSkipButtonShadow);
            _browser.Page.Click(trailerSkipButtonShadow);
        }

        public void PlayPauseTrailer()
        {
            _browser.Page.MoveToElement(playButton);
            _browser.Page.Click(playButton);
        }

        public void WaitTrailerAds() => _browser.Page.WaitElementIsPresent(By.XPath("//div/button[@data-control-name = 'play']"), 60000);

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
