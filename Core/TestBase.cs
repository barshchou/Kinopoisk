using Kinopoisk.Core.Driver;
using Kinopoisk.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Kinopoisk.Core
{
    [TestFixture]
    public class TestBase
    {
        protected HomePage _homePage { get; set; }
        private IWebDriver Driver { get; set; }

        [SetUp]
        public void SetUp()
        {
            Driver = Core.Driver.Driver.GetDriver();
            var _wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(10000));
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(BrowserConfig.BaseUrl);
            _homePage = new HomePage(Driver, _wait);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
