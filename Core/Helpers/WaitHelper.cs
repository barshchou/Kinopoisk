using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kinopoisk.Core.Helpers
{
    public class WaitHelper
    {
        #region PollingWait Methods

        /// <summary>
        /// Use this method to wait for an asynchronous condition to be true.
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
            Func<Task<bool>> condition,
            int sleepMilliseconds = 100,
            int timeoutMilliseconds = 1000,
            string failureMessage = null)
        {
            var watch = new Stopwatch();
            watch.Start();

            while (true)
            {
                if (condition().Result) { return; }

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
        /// Wait until element will be enabled.
        /// If the condition does not evaluate to true before the timeout, a TimeoutException is thrown.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <param name="pollingInterval">The number of milliseconds to sleep between attempts to evaluate the condition.</param>
        /// <param name="timeout">The maximum number of milliseconds to wait for the condition to be true before giving up.</param>
        /// <param name="failureMessage">
        /// The special message which will be included in Timeout Exception message in case of Timeout Exception is thrown.
        /// </param>
        public static void WaitForElementToBeClickable(
            IWebElement element,
            int pollingInterval = 100,
            int timeout = 20000,
            string failureMessage = null) =>
            PollingWait(
                condition: () => IsElementClickable(element),
                sleepMilliseconds: pollingInterval,
                timeoutMilliseconds: timeout,
                failureMessage: failureMessage);

        /// <summary>
        /// Wait until element state will be stable in period of time.
        /// If the condition does not evaluate to true before the timeout, a TimeoutException is thrown.
        /// !NOTE! => PollingInterval must be greater then PeriodMilliseconds.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <param name="periodMilliseconds">Period of time when element state must be stable.</param>
        /// <param name="delayTimeoutMilliseconds">
        /// Delay of time before call method which check that element state stable at that moment.
        /// </param>
        /// <param name="pollingInterval">The number of milliseconds to sleep between attempts to evaluate the condition.</param>
        /// <param name="timeout">The maximum number of milliseconds to wait for the condition to be true before giving up.</param>
        /// <param name="failureMessage">
        /// The special message which will be included in Timeout Exception message in case of Timeout Exception is thrown.
        /// </param>
        public static void WaitForElementStateToBeStable(
            IWebElement element,
            int periodMilliseconds,
            int delayTimeoutMilliseconds,
            int pollingInterval = 2000,
            int timeout = 20000,
            string failureMessage = null) =>
                PollingWait(
                    condition: () =>
                        IsElementStateStable(
                            element: element,
                            periodMilliseconds: periodMilliseconds,
                            delayTimeoutMilliseconds: delayTimeoutMilliseconds),
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

        /// <summary>
        /// Wait for correct element quantity appear.
        /// </summary>
        /// <param name="elements">Instances of IWebElement.</param>
        /// <param name="quantity">Quantity IWebElement which will be on page.</param>
        /// <param name="periodMilliseconds">Period of time when element state must be stable.</param>
        /// <param name="delayTimeoutMilliseconds">
        /// Delay of time before call method which check that element state stable at that moment.
        /// </param>
        /// <param name="pollingInterval">The number of milliseconds to sleep between attempts to evaluate the condition.</param>
        /// <param name="timeout">The maximum number of milliseconds to wait for the condition to be true before giving up.</param>
        /// <param name="failureMessage">
        /// The special message which will be included in Timeout Exception message in case of Timeout Exception is thrown.
        /// </param>
        public static void WaitUntilCorrectElementsQuantityAppear(
            List<IWebElement> elements,
            int quantity,
            int periodMilliseconds,
            int delayTimeoutMilliseconds,
            int pollingInterval = 2000,
            int timeout = 4000,
            string failureMessage = null) =>
            PollingWait(
                condition: () =>
                    AreElementsQuantityCorrectDuringTimePeriod(
                        elements: elements,
                        quantity: quantity,
                        periodMilliseconds: periodMilliseconds,
                        delayTimeoutMilliseconds: delayTimeoutMilliseconds),
                sleepMilliseconds: pollingInterval,
                timeoutMilliseconds: timeout,
                failureMessage: failureMessage);

        #endregion

        #region Verify Methods

        /// <summary>
        /// Verify visibility of an element.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <returns>True if element displayed.</returns>
        private static bool IsElementDisplayed(IWebElement element)
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

        /// <summary>
        /// Verify clickability of an element.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <returns>True if element clickable.</returns>
        private static bool IsElementClickable(IWebElement element)
        {
            if (element == null) throw new ArgumentNullException(paramName: nameof(element));

            try
            {
                return element.Enabled;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verify that element state will be stable in during specified period of time.
        /// How it works:
        /// When we set 'periodMillisecond' ->
        /// it's specified period of time during we will checking that element have stable state.
        /// When we set 'delayTimeoutMilliseconds' ->
        /// it's specified delay before calling 'IsElementClickable' method.
        /// For instance:
        /// 'periodMillisecond' - 10000 milliseconds, 'delayTimeoutMilliseconds' - 2000;
        /// During 10000 milliseconds will be calling 'IsElementClickable' method
        /// with 2000 milliseconds delay before each call.
        /// Result of this verification will be added in List of results
        /// if List of result do not contain 'false' => method return 'true'.
        /// </summary>
        /// <param name="element">Instance of IWebElement.</param>
        /// <param name="periodMilliseconds">Period of time when element state must be stable.</param>
        /// <param name="delayTimeoutMilliseconds">
        /// Delay of time before call method which check that element state stable at that moment.
        /// </param>
        /// <returns>True if element state was stable during all period of time.</returns>
        private static bool IsElementStateStable(IWebElement element, int periodMilliseconds, int delayTimeoutMilliseconds)
        {
            if (element == null) throw new ArgumentNullException(paramName: nameof(element));

            var results = new List<bool>();
            var task = Task.Run(function: async () =>
            {
                await Task.Delay(delayTimeoutMilliseconds);

                try
                {
                    return IsElementClickable(element: element);
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            var watch = new Stopwatch();
            watch.Start();

            while (watch.ElapsedMilliseconds < periodMilliseconds)
            {
                results.Add(item: task.Result);
            }

            return !results.Contains(item: false);
        }

        /// <summary>
        /// Verify that elements quantity correct during time period.
        /// </summary>
        /// <param name="elements">Instances of IWebElement.</param>
        /// <param name="quantity">Quantity IWebElement which will be on page.</param>
        /// <param name="periodMilliseconds">Period of time when element state must be stable.</param>
        /// <param name="delayTimeoutMilliseconds">
        /// Delay of time before call method which check that element state stable at that moment.
        /// </param>
        /// <returns>True if elements quantity correct during time period.</returns>
        private static bool AreElementsQuantityCorrectDuringTimePeriod(
            List<IWebElement> elements,
            int quantity,
            int periodMilliseconds,
            int delayTimeoutMilliseconds)
        {
            if (elements == null) throw new ArgumentNullException(paramName: nameof(elements));

            var results = new List<bool>();
            var task = Task.Run(function: async () =>
            {
                await Task.Delay(delayTimeoutMilliseconds);

                try
                {
                    if (elements.Count == quantity)
                    {
                        return true;
                    }

                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            var watch = new Stopwatch();
            watch.Start();

            while (watch.ElapsedMilliseconds < periodMilliseconds)
            {
                results.Add(item: task.Result);
            }

            return !results.Contains(item: false);
        }

        #endregion
    }
}
