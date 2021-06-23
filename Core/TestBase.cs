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

        [SetUp]
        public void SetUp()
        {
            Driver = _factory.Value.GetBrowser(BrowserConfig.Browser);
            Driver.Page.Manage().Window.Maximize();
            Driver.Page.GoToUrl(BrowserConfig.BaseUrl);
            _homePage = new HomePage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Page.Quit();
        }
    }
}
