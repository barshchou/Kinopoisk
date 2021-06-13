using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Helpers;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

        public PageAdapter(BrowserAdapter<T> browser, int waitFor = 15000, int pollingInterval = 200)
        {
            _browser = browser;
            _driver = browser.Driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(waitFor))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
            };
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

        /// <summary>
        /// Clicks on an element
        /// </summary>
        /// <param name="element">Element to be clicked</param>
        public void Click(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            ExplicitWait(
                Wait.ExpectedConditions.ElementToBeClickable(element),
                $"Button ID={element.GetAttribute("id")} to be clickable");

            element.Click();
        }

        /// <summary>
        /// Type into text field
        /// </summary>
        /// <param name="value">String text to type</param>
        /// <param name="element">Webelement to type in</param>
        public void Type(string value, IWebElement element)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Cannot type a null value.");
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Cannot type on a null element.");
            }

            ExplicitWait(Wait.ExpectedConditions.ElementToBeClickable(element),
                $"Textbox ID={element.GetAttribute("id")} to be click-able");
            WaitHelper.PollingWait(() => element.Enabled, failureMessage: "Text box to be enabled");

            element.SendKeys(value.Trim());
        }

        /// <summary>
        /// Move mouse to webelement
        /// </summary>
        /// <param name="element">Webelement to hover over</param>
        public void MoveToElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Cannot move to a null element.");
            }

            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Build().Perform();
        }

        //public void ExplicitWait(Func<IWebDriver, bool> expectedCondition, string failureMessage)
        //{
        //    var implicitWait = _wait.Timeout;
        //    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        //    try
        //    {
        //        _wait.Until(condition: expectedCondition);
        //    }
        //    catch (WebDriverTimeoutException e)
        //    {
        //        throw new TimeoutException(
        //            message: $"Waited for {_wait.Timeout} seconds for: {failureMessage}",
        //            innerException: e);
        //    }

        //    _driver.Manage().Timeouts().ImplicitWait = implicitWait;
        //}

        /// <summary>
        /// Check if element is present on the page
        /// </summary>
        /// <param name="condition">Locator type and path</param>
        /// <returns></returns>
        public bool WaitUntilElementIsPresent(By condition)
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
                _wait.Timeout = implicitWait;
                _driver.Manage().Timeouts().ImplicitWait = implicitWait;
            }
        }

        public bool IsElementPresent(By condition)
        {
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            try
            {
                return _driver.FindElements(condition).Count > 0;
            }
            catch (WebDriverException)
            {
                return false;
            }
            finally
            {
                _wait.Timeout = implicitWait;
                _driver.Manage().Timeouts().ImplicitWait = implicitWait;
            }
        }

        public bool IsElementPresent(By condition, out bool isPresent)
        {
            isPresent = false;
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            try
            {
                return isPresent =_driver.FindElements(condition).Count > 0;
            }
            catch (WebDriverException)
            {
                return isPresent;
            }
            finally
            {
                _wait.Timeout = implicitWait;
                _driver.Manage().Timeouts().ImplicitWait = implicitWait;
            }
        }

        public bool WaitElementIsPresent(By condition)
        {
            bool isPresent = false;
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WaitHelper.PollingWait(() => IsElementPresent(condition, out isPresent),
                    timeoutMilliseconds: 10 * 1000
                    );

            _driver.Manage().Timeouts().ImplicitWait = implicitWait;
            return isPresent;
        }

        public string Url { get; set; }

        public void Dispose()
        {
            _driver.SwitchTo();
        }
    }
}
