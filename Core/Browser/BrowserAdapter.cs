using Kinopoisk.Core.Enums;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Core.Browser
{
    public sealed class BrowserAdapter<T> : IBrowser<T> where T : IWebDriver
    {
        private readonly PageAdapter<T> _page;
        private readonly JavaScriptAdapter<T> _javaScript;

        public BrowserAdapter(T driver, BrowserType type)
        {
            Type = type;
            Driver = driver;
            _page = new PageAdapter<T>(this);
            _javaScript = new JavaScriptAdapter<T>(this);
        }

        public BrowserType Type { get; }
        public T Driver { get; }
        public IPage Page => _page;
        public IWait<IWebDriver> _wait { get; }
        public IJavaScript JavaScript => _javaScript;
    }
}
