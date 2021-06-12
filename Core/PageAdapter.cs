using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using Wait = SeleniumExtras.WaitHelpers;

namespace Kinopoisk
{
    public class PageAdapter<T> : IPage where T : IWebDriver
    {
        private readonly BrowserAdapter<T> _browser;
        private readonly T _driver;
        private readonly IWait<IWebDriver> _wait;

        public PageAdapter(BrowserAdapter<T> browser)
        {
            _browser = browser;
            _driver = browser.Driver;
        }

        public string Title => _driver.Title;
        public string PageSource => _driver.PageSource;
        public string CurrentWindowHandle => _driver.CurrentWindowHandle;
        public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        public void NavigateBack()
        {
            _driver.Navigate().Back();
        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        public void Close()
        {
            _driver.Close();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public IWebElement FindElement(By by)
        {
            return _driver.FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public void ExplicitWait(Func<IWebDriver, IWebElement> expectedCondition, string failureMessage)
        {
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            _wait.Until(expectedCondition);
            _driver.Manage().Timeouts().ImplicitWait = implicitWait;
        }

        public void ExplicitWait(Func<IWebDriver, bool> expectedCondition, string failureMessage)
        {
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            try
            {
                _wait.Until(condition: expectedCondition);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    message: $"Waited for {_wait.Timeout} seconds for: {failureMessage}",
                    innerException: e);
            }

            _driver.Manage().Timeouts().ImplicitWait = implicitWait;
        }

        public bool IsElementPresent(By condition)
        {
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            try
            {
                _wait.Until(Wait.ExpectedConditions.ElementIsVisible(condition));

                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
            finally
            {
                _driver.Manage().Timeouts().ImplicitWait = implicitWait;
            }
        }

        public string Url { get; set; }

        public void Dispose()
        {
            _driver.SwitchTo();
        }
    }
}
