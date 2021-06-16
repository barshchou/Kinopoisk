using Kinopoisk.Core.Browser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinopoisk.Tests
{
    public class MediaContentTests : TestBase
    {
        [Test]
        public void AddToFavoritesTest()
        {
            var contentName = "Троя";
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(contentName)
                .OpenContentItem(contentName);
            mediaContentPage.AddContentToFavourites();
            
            Assert.IsTrue(mediaContentPage.IsContentAddedToFavorites());
            var moviesPages = mediaContentPage.OpenFavoritesFolder();
            
            Assert.IsTrue(moviesPages.IsContentAddedToFavorites(contentName));

            moviesPages.RemoveAllFavourites();

            Assert.IsTrue(moviesPages.IsFavoritesPurged());
        }

        [Test]
        public void AddToWatchedTest()
        {
            var contentName = "Троя";
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(contentName)
                .OpenContentItem(contentName);
            mediaContentPage.AddContentToWatched();

            Assert.IsTrue(mediaContentPage.IsContentAddedToWatched());

            Assert.IsTrue(mediaContentPage.OpenHomePage().SearchContent(contentName).OpenContentItem(contentName).IsContentAddedToWatched());
        }

        [Test]
        public void WatchTrailerTest()
        {
            var contentName = "Троя";
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(contentName)
                .OpenContentItem(contentName);
            mediaContentPage.OpenTrailer();

            Assert.IsTrue(mediaContentPage.IsTrailerPlayerDisplayed());

            mediaContentPage.CloseTrailerFrame();
        }
    }
}
