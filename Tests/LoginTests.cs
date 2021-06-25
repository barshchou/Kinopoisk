using Kinopoisk.Core.Browser;
using NUnit.Framework;

namespace Kinopoisk
{
    [TestFixture]
    [Parallelizable]
    public class LoginTests : TestBase
    {
        [Test]
        public void SuccessfullLoginTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePageLoggedIn = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            
            Assert.IsTrue(homePageLoggedIn.IsUserLoggedIn());
        }

        [Test]
        public void UnsuccessfullLoginTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            loginPage.Login(BrowserConfig.Login, "InvalidPassword");
            
            Assert.IsTrue(loginPage.AreCredentialsInvalid());
        }

        [Test]
        public void LogoutTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePageLoggedIn = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            homePageLoggedIn.Logout();
            Assert.IsFalse(homePageLoggedIn.IsUserLoggedIn());

        }
    }
}