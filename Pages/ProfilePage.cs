using Kinopoisk.Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class ProfilePage : BasePage
    {
        public ProfilePage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.XPath($"//a[contains(text(), '{BrowserConfig.Username}')]"));
        }

        public ProfilePage()
        {

        }

        private IWebElement FirstNameTextfield => _driver.FindElement(By.Id("edit[main][first_name]"));
        private IWebElement LastNameTextfield => _driver.FindElement(By.Id("edit[main][last_name]"));
        private IWebElement GenderDropdown => _driver.FindElement(By.Name("edit[main][sex]"));
        private IWebElement EditBirthDayDropdown => _driver.FindElement(By.Name("edit[birth][day]"));
        private IWebElement EditBirthMonthDropdown => _driver.FindElement(By.Name("edit[birth][month]"));
        private IWebElement EditBirthYearDropdown => _driver.FindElement(By.Name("edit[birth][year]"));
        private IWebElement SaveChangesButton => _driver.FindElement(By.Id("js-save-edit-form"));

        public void UpdateBio(string fisrtName, string lastName, string gender, string day, string month, string year)
        {
            Type(fisrtName, FirstNameTextfield, clear: true);
            Type(lastName, LastNameTextfield, clear: true);
            Select(GenderDropdown, gender);
            Select(EditBirthDayDropdown, day);
            Select(EditBirthMonthDropdown, month);
            Select(EditBirthYearDropdown, year);
            Click(SaveChangesButton);
        }

        public bool IsFirstNameValueSaved(string value) => FirstNameTextfield.GetAttribute("value").Contains(value);
        public bool IsLastNameValueSaved(string value) => LastNameTextfield.GetAttribute("value").Contains(value);
        public bool IsGenderValueSaved(string value) => GetDropDownOptionName(GenderDropdown).Contains(value);

        public bool AreDOBValuesSaved(string[] values)
        {
            return GetDropDownOptionName(EditBirthDayDropdown).Contains(values[0]) &
            GetDropDownOptionName(EditBirthMonthDropdown).Contains(values[1]) &
            GetDropDownOptionName(EditBirthYearDropdown).Contains(values[2]);
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
            _driver.Navigate().GoToUrl(BrowserConfig.BaseUrl);
            return new HomePage(_driver, _wait);
        }
    }
}
