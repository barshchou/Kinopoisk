using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinopoisk.Pages
{
    public class Profile_MoviesPage
    {
        private readonly IBrowser _browser;

        public Profile_MoviesPage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.XPath("//li/span[contains(text(), 'Фильмы')]"));
        }

        private IWebElement contentItem(string contentName) => _browser.Page.FindElement(By.XPath($"//div/a[@class = 'name'][text() = '{contentName}']"));
        private IWebElement selectAllTitlesCheckbox => _browser.Page.FindElement(By.CssSelector("#selectAllbox"));
        private IWebElement removeSelectedButton => _browser.Page.FindElement(By.CssSelector("#delete_selected[value='удалить отмеченные фильмы']"));

        public bool IsContentAddedToFavorites(string contentName) => _browser.Page.IsElementPresent(By.XPath($"//div/a[@class = 'name'][text() = '{contentName}']"));

        public void RemoveAllFavourites()
        {
            _browser.Page.MoveToElement(selectAllTitlesCheckbox);
            _browser.Page.Click(selectAllTitlesCheckbox);
            _browser.Page.Click(removeSelectedButton);
            _browser.Page.SwitchTo().Alert().Accept();
        }

        public bool IsFavoritesPurged() => _browser.Page.WaitUntilElementIsPresent(By.XPath("//p[@class = 'emptyMessage']"));

        public MediaContentPage OpenContenItem(string contentName)
        {
            _browser.Page.Click(contentItem(contentName));
            return new MediaContentPage(_browser);
        }

        public HomePage OpenHomePage()
        {
            _browser.Page.GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_browser);
        }
    }
}
