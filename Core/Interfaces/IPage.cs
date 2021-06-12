﻿using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace Kinopoisk.Core.Interfaces
{
    public interface IPage : IWebDriver
    {

        new string Title { get; }
        new string PageSource { get; }
        new string CurrentWindowHandle { get; }
        new ReadOnlyCollection<string> WindowHandles { get; }
        void GoToUrl(string url);
        new IWebElement FindElement(By by);
        new ReadOnlyCollection<IWebElement> FindElements(By selector);
        void NavigateBack();
        void Refresh();
        new void Close();
        new void Quit();
        new IOptions Manage();
        new INavigation Navigate();
        new ITargetLocator SwitchTo();
        //void ExplicitWait(Func<IWebDriver, IWebElement> expectedCondition, string failureMessage);
    }
}