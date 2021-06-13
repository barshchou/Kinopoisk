using Kinopoisk.Core.Browser;
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
    }
}
