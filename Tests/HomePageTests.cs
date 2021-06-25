using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Helpers;
using NUnit.Framework;

namespace Kinopoisk.Tests
{
    [TestFixture]
    [Parallelizable]
    public class HomePageTests : TestBase
    {
        const string CONTENTNAME = "Троя";

        [TestCase]
        public void SearchContentByNameTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent(CONTENTNAME);

            Assert.IsTrue(searchResults.AreSearchResultsDisplayed(CONTENTNAME));
        }

        [TestCase]
        public void SearchContentByName_NoResultsTest()
        {
            var contentName = RandomNameGenerator.RandomString();
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent(contentName);

            Assert.False(searchResults.AreSearchResultsDisplayed(contentName));
        }

        [TestCase]
        public void SearchContentByName_TranslitTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent("Troya");

            Assert.True(searchResults.AreSearchResultsDisplayed(CONTENTNAME));
        }

        [TestCase]
        public void SearchContentByName_WrongLanguageTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var searchResults = homePage.SearchContent("Nhjz");

            Assert.True(searchResults.AreSearchResultsDisplayed(CONTENTNAME));
        }

        [TestCase]
        public void SearchByFilterTest()
        {
            var country = "Россия";
            var yearTo = "2020";
            var yearFrom = "2010";
            var loginPage = _homePage.OpenLoginPage();
            var searchContentPage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password).OpenSearchContentPage();
            var searchResultsPage = searchContentPage.SearchContentByFilter(country, yearFrom, yearTo);

            Assert.IsTrue(searchResultsPage.IsSearchResultsFilteredByCountry(country));
            Assert.IsTrue(searchResultsPage.IsSearchResultsFilteredByYear(yearFrom, yearTo));
        }
    }
}
