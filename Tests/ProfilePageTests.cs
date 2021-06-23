using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Helpers;
using NUnit.Framework;
using System;
using System.Globalization;

namespace Kinopoisk.Tests
{
    public class ProfilePageTests : TestBase
    {
        [Test]
        public void UpdateProfileTest()
        {
            var time = DateTime.UtcNow;
            DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
            var day = time.Day.ToString();
            var month = info.MonthNames[DateTime.Now.Month - 1];
            var year = time.AddYears(-20).Year.ToString();
            var firstName = RandomNameGenerator.RandomName();
            var lastName = RandomNameGenerator.RandomName();
            var gender = "мужской";


            var loginPage = _homePage.OpenLoginPage();
            var homePage = loginPage.OpenHomePage(BrowserConfig.Login, BrowserConfig.Password);
            var profilePage = homePage.OpenProfilePage();
            profilePage.UpdateBio(firstName, lastName, gender, day, month, year);
            profilePage = profilePage.OpenHomePage().OpenProfilePage();
            Assert.IsTrue(profilePage.IsFirstNameValueSaved(firstName));
            Assert.IsTrue(profilePage.IsLastNameValueSaved(lastName));
            Assert.IsTrue(profilePage.IsGenderValueSaved(gender));
            Assert.IsTrue(profilePage.AreDOBValuesSaved(new string[]{ day, month, year}));
        }
    }
}
