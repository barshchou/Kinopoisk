using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace Kinopoisk.Core.Helpers
{
    public class WaitHelper
    {
        #region PollingWait Methods

        /// <summary>
        /// Use this method to wait for a condition to be true.
        /// If the condition does not evaluate to true before the timeout, a TimeoutException is thrown.
        /// </summary>
        /// <param name="condition">
        /// The condition you want be true.
        /// You must make any calls necessary to update the value within the function.
        /// </param>
        /// <param name="sleepMilliseconds">
        /// The number of milliseconds to sleep between attempts to evaluate the condition.
        /// </param>
        /// <param name="timeoutMilliseconds">
        /// The maximum number of milliseconds to wait for the condition to be true before giving up.
        /// </param>
        /// <param name="failureMessage">
        /// Special message to include in the case of the timeout.
        /// </param>
        public static void PollingWait(
            Func<bool> condition,
            int sleepMilliseconds = 100,
            int timeoutMilliseconds = 1000,
            string failureMessage = null)
        {
            var watch = new Stopwatch();
            watch.Start();

            while (true)
            {

                if (condition()) { return; }

                Thread.Sleep(sleepMilliseconds);

                if (watch.ElapsedMilliseconds > timeoutMilliseconds)
                {
                    if (failureMessage == null)
                    {
                        throw new TimeoutException(
                            message: $"Waited for {timeoutMilliseconds} milliseconds without success");
                    }

                    throw new TimeoutException(
                        message: $"Waited for {failureMessage} for {timeoutMilliseconds} without success");
                }
            }
        }

        #endregion

        #region Wait Methods

        /// <summary>
        /// Wait until element will be displayed.
        /// If the condition does not evaluate to true before the timeout, a TimeoutException is thrown.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <param name="pollingInterval">The number of milliseconds to sleep between attempts to evaluate the condition.</param>
        /// <param name="timeout">The maximum number of milliseconds to wait for the condition to be true before giving up.</param>
        /// <param name="failureMessage">
        /// The special message which will be included in Timeout Exception message in case of Timeout Exception is thrown.
        /// </param>
        public static void WaitForElementToBeDisplayed(
            IWebElement element,
            int pollingInterval = 100,
            int timeout = 20000,
            string failureMessage = null) =>
            PollingWait(
                condition: () => IsElementDisplayed(element),
                sleepMilliseconds: pollingInterval,
                timeoutMilliseconds: timeout,
                failureMessage: failureMessage);

        /// <summary>
        /// Wait for element disappear.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <param name="msg">failure message</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        public static void WaitForElementToBeDisappear(IWebElement element, string msg = null, int timeout = 5000) =>
            PollingWait(condition: () => !IsElementDisplayed(element: element), timeoutMilliseconds: timeout, failureMessage: msg);

        #endregion

        #region Verify Methods

        /// <summary>
        /// Verify visibility of an element.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <returns>True if element displayed.</returns>
        public static bool IsElementDisplayed(IWebElement element)
        {
            if (element == null) throw new ArgumentNullException(paramName: nameof(element));

            try
            {
                return element.Displayed;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        #endregion
    }
}
