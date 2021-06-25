using Kinopoisk.Core.Enums;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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
        }

        public IBrowser Create<T>() where T : IWebDriver
        {
            var factoryMethod = this as IBrowserWebDriver<T>;
            return factoryMethod?.Create();
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
            switch (type)
            {
                case BrowserType.Firefox:
                    return _browsers[BrowserType.Firefox].Invoke();
                case BrowserType.Chrome:
                    return _browsers[BrowserType.Chrome].Invoke();
                default:
                    return _browsers[BrowserType.Chrome].Invoke();
            }
        }
    }
}
