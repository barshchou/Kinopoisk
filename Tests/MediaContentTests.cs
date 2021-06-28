using Kinopoisk.Core;
using Kinopoisk.Core.Driver;
using NUnit.Framework;

namespace Kinopoisk.Tests
{
    public class MediaContentTests : TestBase
    {
        const string CONTENTNAME = "Троя";
        [Test]
        public void AddToFavoritesTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(CONTENTNAME)
                .OpenContentItem(CONTENTNAME);
            mediaContentPage.AddContentToFavourites();
            
            Assert.IsTrue(mediaContentPage.IsContentAddedToFavorites());
            var moviesPages = mediaContentPage.OpenFavoritesFolder();
            Assert.IsTrue(moviesPages.IsContentAddedToFavorites(CONTENTNAME));
            moviesPages.RemoveAllFavourites();
            Assert.IsTrue(moviesPages.IsFavoritesPurged());
        }

        [Test]
        public void AddToWatchedTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(CONTENTNAME)
                .OpenContentItem(CONTENTNAME);
            mediaContentPage.AddContentToWatched();
            Assert.IsTrue(mediaContentPage.IsContentAddedToWatched());
            Assert.IsTrue(mediaContentPage.OpenHomePage().SearchContent(CONTENTNAME).OpenContentItem(CONTENTNAME).IsContentAddedToWatched());
        }

        [Test]
        public void WatchTrailerTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var mediaContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password)
                .SearchContent(CONTENTNAME)
                .OpenContentItem(CONTENTNAME);
            mediaContentPage.OpenTrailer();
            mediaContentPage.WaitTrailerAds();
            mediaContentPage.PlayPauseTrailer();
            Assert.IsTrue(mediaContentPage.IsTrailerPlayerDisplayed());
            mediaContentPage.CloseTrailerFrame();
        }
    }
}
