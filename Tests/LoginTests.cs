using NUnit.Framework;

namespace Kinopoisk
{
    [TestFixture]
    [Parallelizable]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePageLoggedIn = loginPage.Login();
            
            Assert.IsTrue(homePageLoggedIn.IsUserLoggedIn());
        }

        [Test]
        public void LogoutTest()
        {
            var loginPage = _homePage.OpenLoginPage();
            var homePageLoggedIn = loginPage.Login();
            homePageLoggedIn.Logout();
            Assert.IsTrue(homePageLoggedIn.IsUserLoggedOut());

        }
    }
}