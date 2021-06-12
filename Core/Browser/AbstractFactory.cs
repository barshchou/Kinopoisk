using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Kinopoisk.Core.Browser
{
    public abstract class AbstractFactory : IBrowserFactory
    {
        private readonly Dictionary<BrowserType, Func<IBrowser>> _browsers =
            new Dictionary<BrowserType, Func<IBrowser>>();

        protected AbstractFactory()
        {
            _browsers.Add(BrowserType.Chrome, Chrome);
            _browsers.Add(BrowserType.Firefox, Firefox);
            _browsers.Add(BrowserType.Remote, Remote);
        }

        public IBrowser Create<T>() where T : IWebDriver
        {
            var factoryMethod = this as IBrowserWebDriver<T>;
            return factoryMethod?.Create();
        }

        private IBrowser Remote()
        {
            return Create<RemoteWebDriver>();
        }

        private IBrowser Chrome()
        {
            return Create<ChromeDriver>();
        }

        private IBrowser Firefox()
        {
            return Create<FirefoxDriver>();
        }

        public IBrowser GetBrowser(BrowserType type)
        {
            return BrowserConfig.RemoteBrowser && BrowserConfig.UseSauceLabs ||
                   BrowserConfig.RemoteBrowser && BrowserConfig.UseBrowserstack ||
                   BrowserConfig.RemoteBrowser && BrowserConfig.UseSeleniumGrid
                ? _browsers[BrowserType.Remote].Invoke()
                : (_browsers.ContainsKey(type)
                    ? _browsers[type].Invoke()
                    : _browsers[BrowserType.Firefox].Invoke());
        }
    }
}
