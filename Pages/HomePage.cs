using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kinopoisk.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            WaitUntilElementIsPresent(By.XPath("//div[@class = 'desktop-layout-with-sidebar__content']"));
        }

        public HomePage()
        {

        }

        private IWebElement LoginButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Войти')]"));
        private IWebElement LogoutButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Выйти')]"));
        private IWebElement UserProfileMenu => _driver.FindElement(By.XPath("//div[contains(@class, 'header-v4__user-bar')]/div/button"));
        private IWebElement SearchContentField => _driver.FindElement(By.CssSelector("input[name='kp_query']"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement ProfileSettingButton => _driver.FindElement(By.XPath("//a[contains(text(), 'Настройки')]"));
        private IWebElement AdvancedSearchButton => _driver.FindElement(By.XPath("//a[@aria-label = 'advanced-search']"));
        private IWebElement searchResultsSuggested(string contentName) => _driver.FindElement(
            By.XPath($"//div/h4[text() = '{contentName}']"));

        public LoginPage OpenLoginPage()
        {
            if (IsUserLoggedIn())
                Logout();

            ClickLogin();
            return new LoginPage(_driver, _wait);
        }

        public SearchResultsPage SearchContent(string contentName)
        {
            Search(contentName);
            return new SearchResultsPage(_driver, _wait);
        }

        public void ClickLogin() => Click(LoginButton);

        /// <summary>
        /// Logout if already logged in
        /// </summary>
        public void Logout()
        {
            if (IsUserLoggedIn())
            {
                MoveToElement(UserProfileMenu);
                Click(LogoutButton);
            }
        }

        /// <summary>
        /// Navigate to Profile page
        /// </summary>
        /// <returns></returns>
        public ProfilePage OpenProfilePage()
        {
            MoveToElement(UserProfileMenu);
            Click(ProfileSettingButton);
            return new ProfilePage(_driver, _wait);
        }

        public bool IsUserLoggedIn()  => IsElementPresent(By.XPath("//button[contains(text(), 'Выйти')]"));

        /// <summary>
        /// Search content from search field box
        /// </summary>
        /// <param name="contentName"></param>
        public void Search(string contentName)
        {
            Type(contentName, SearchContentField);
            Click(SearchButton);
        }

        /// <summary>
        /// Search content and and open content page from suggested list
        /// </summary>
        /// <param name="contentName"></param>
        public void SearchSuggestedContent(string contentName)
        {
            Type(contentName, SearchContentField);
            Click(searchResultsSuggested(contentName));
        }

        public SearchContentPage OpenSearchContentPage()
        {
            Click(AdvancedSearchButton);
            return new SearchContentPage(_driver, _wait);
        }
    }
}
