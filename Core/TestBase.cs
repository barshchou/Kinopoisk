using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using Kinopoisk.Pages;
using NUnit.Framework;
using System;

namespace Kinopoisk
{
    [TestFixture]
    public class TestBase
    {
        private readonly Lazy<BrowserFactory> _factory = new Lazy<BrowserFactory>();
        protected IBrowser Driver;
        protected HomePage _homePage { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver = _factory.Value.GetBrowser(BrowserConfig.Browser);
            Driver.Page.Manage().Window.Maximize();
            Driver.Page.GoToUrl(BrowserConfig.BaseUrl);
            _homePage = new HomePage(Driver);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Driver.Page.Quit();
        }
    }
}
