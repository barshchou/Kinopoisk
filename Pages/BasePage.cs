using Kinopoisk.Core.Driver;
using Kinopoisk.Core.Enums;
using Kinopoisk.Core.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using Wait = SeleniumExtras.WaitHelpers;

namespace Kinopoisk.Pages
{
    public class BasePage
    {
        public IWebDriver _driver;
        public IWait<IWebDriver> _wait;
        public BasePage(IWebDriver driver, IWait<IWebDriver> wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public BasePage()
        {

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
        public void Type(string value, IWebElement element, bool clear = false)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Cannot type a null value.");
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Cannot type on a null element.");
            }

            if (clear)
                Clear(element);

            ExplicitWait(Wait.ExpectedConditions.ElementToBeClickable(element),
                $"Textbox ID={element.GetAttribute("id")} to be click-able");
            WaitHelper.PollingWait(() => element.Enabled, failureMessage: "Text box to be enabled");

            element.SendKeys(value.Trim());
        }

        /// <summary>
        /// Select element by text from dropdown
        /// </summary>
        /// <param name="element">Webelement dropdown element</param>
        /// <param name="value">Value t obe selected from dropdown</param>
        /// <param name="matchCondition">Match condition to select element</param>
        public void Select(IWebElement element, string value, MatchCondition matchCondition = MatchCondition.Text)
        {
            ExplicitWait(Wait.ExpectedConditions.ElementToBeClickable(element),
                $"Elements {element.GetAttribute("id")} to be clickable");
            var selectElement = new SelectElement(element);
            switch (matchCondition)
            {
                case MatchCondition.Text:
                    selectElement.SelectByText(value);
                    break;
                case MatchCondition.Value:
                    selectElement.SelectByValue(value);
                    break;
                default:
                    selectElement.SelectByText(value);
                    break;
            }
        }

        public void Clear(IWebElement element) => element.Clear();

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

        /// <summary>
        /// Checks if element is present
        /// </summary>
        /// <param name="condition">Webelement locator</param>
        /// <returns>True if element is present</returns>
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

        /// <summary>
        /// Checks if element is present
        /// </summary>
        /// <param name="condition">Webelement locator</param>
        /// <param name="isPresent">Bool out parametr</param>
        /// <returns>Return bool out parameter if element is present</returns>
        public bool IsElementPresent(By condition, out bool isPresent)
        {
            isPresent = false;
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            try
            {
                return isPresent = _driver.FindElements(condition).Count > 0;
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

        /// <summary>
        /// Wait till element is present
        /// </summary>
        /// <param name="condition">Webelement locator</param>
        /// <param name="timeout">Timeout amount</param>
        /// <returns>True if element is present</returns>
        public bool WaitElementIsPresent(By condition, int timeout = 10000)
        {
            bool isPresent = false;
            var implicitWait = _wait.Timeout;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WaitHelper.PollingWait(() => IsElementPresent(condition, out isPresent),
                    timeoutMilliseconds: timeout
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
