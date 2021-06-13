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
            var homePageLoggedIn = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);



            Assert.IsTrue(homePageLoggedIn.IsUserLoggedIn());
        }
    }
}
