using Kinopoisk.Core.Browser;
using OpenQA.Selenium;

namespace Kinopoisk.Core
{
    internal sealed class JavaScriptAdapter<T> : IJavaScript where T : IWebDriver
    {
        private readonly BrowserAdapter<T> _browser;
        private readonly T _driver;
        private readonly IJavaScriptExecutor _js;

        public JavaScriptAdapter(BrowserAdapter<T> browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _js = (IJavaScriptExecutor)_driver;
        }

        public object Execute(string javaScript, params object[] args)
        {
            return _js.ExecuteScript(javaScript, args);
        }
    }

    public interface IJavaScript
    {
        object Execute(string javaScript, params object[] args);
    }
}
