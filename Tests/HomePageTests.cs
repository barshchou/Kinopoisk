using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Helpers;
using NUnit.Framework;

namespace Kinopoisk.Tests
{
    [TestFixture]
    [Parallelizable]
    public class HomePageTests : TestBase
    {
        [Test]
        public void SearchContentByNameTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent("Троя");

            Assert.IsTrue(searchResults.AreSearchResultsDisplayed("Троя"));
        }

        [Test]
        public void SearchContentByName_NoResultsTest()
        {
            var contentName = RandomNameGenerator.RandomString();
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent(contentName);

            Assert.False(searchResults.AreSearchResultsDisplayed(contentName));
        }

        [Test]
        public void SearchContentByName_TranslitTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent("Troya");

            Assert.True(searchResults.AreSearchResultsDisplayed("Троя"));
        }

        [Test]
        public void SearchContentByName_WrongLanguageTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent("Nhjz");

            Assert.True(searchResults.AreSearchResultsDisplayed("Троя"));
        }
    }
}
