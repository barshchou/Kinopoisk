using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Helpers;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Kinopoisk.Pages
{
    public class ProfilePage 
    {
        private readonly IBrowser _browser;

        public ProfilePage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.XPath($"//a[contains(text(), '{BrowserConfig.Username}')]"));
        }

        private IWebElement firstNameTextfield => _browser.Page.FindElement(By.Id("edit[main][first_name]"));
        private IWebElement lastNameTextfield => _browser.Page.FindElement(By.Id("edit[main][last_name]"));
        private IWebElement genderDropdown => _browser.Page.FindElement(By.Name("edit[main][sex]"));
        private IWebElement editBirthDayDropdown => _browser.Page.FindElement(By.Name("edit[birth][day]"));
        private IWebElement editBirthMonthDropdown => _browser.Page.FindElement(By.Name("edit[birth][month]"));
        private IWebElement editBirthYearDropdown => _browser.Page.FindElement(By.Name("edit[birth][year]"));
        private IWebElement saveChangesButton => _browser.Page.FindElement(By.Id("js-save-edit-form"));

        public void UpdateBio(string fisrtName, string lastName, string gender, string day, string month, string year)
        {
            _browser.Page.Type(fisrtName, firstNameTextfield, clear: true);
            _browser.Page.Type(lastName, lastNameTextfield, clear: true);
            _browser.Page.Select(genderDropdown, gender);
            _browser.Page.Select(editBirthDayDropdown, day);
            _browser.Page.Select(editBirthMonthDropdown, month);
            _browser.Page.Select(editBirthYearDropdown, year);
            _browser.Page.Click(saveChangesButton);
        }

        public bool IsFirstNameValueSaved(string value) => firstNameTextfield.GetAttribute("value").Contains(value);
        public bool IsLastNameValueSaved(string value) => lastNameTextfield.GetAttribute("value").Contains(value);
        public bool IsGenderValueSaved(string value) => GetDropDownOptionName(genderDropdown).Contains(value);

        public bool AreDOBValuesSaved(string[] values)
        {
            return GetDropDownOptionName(editBirthDayDropdown).Contains(values[0]) &
            GetDropDownOptionName(editBirthMonthDropdown).Contains(values[1]) &
            GetDropDownOptionName(editBirthYearDropdown).Contains(values[2]);
        }

        /// <summary>
        /// Get Text for drop down option selected
        /// </summary>
        /// <returns>Selected text</returns>
        public string GetDropDownOptionName(IWebElement dropdown)
        {
            var selectElement = new SelectElement(dropdown);
            return selectElement.SelectedOption.Text;
        }

        public HomePage OpenHomePage()
        {
            _browser.Page.GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_browser);
        }
    }
}
