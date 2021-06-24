using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium;

namespace Kinopoisk.Pages
{
    public class HomePage
    {
        private readonly IBrowser _browser;

        public HomePage(IBrowser browser)
        {
            _browser = browser;
            _browser.Page.WaitUntilElementIsPresent(By.XPath("//div[@class = 'desktop-layout-with-sidebar__content']"));
        }

        private IWebElement loginButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Войти')]"));
        private IWebElement logoutButton => _browser.Page.FindElement(By.XPath("//button[contains(text(), 'Выйти')]"));
        private IWebElement userProfileMenu => _browser.Page.FindElement(By.XPath("//div[contains(@class, 'header-v4__user-bar')]/div/button"));
        private IWebElement searchContentField => _browser.Page.FindElement(By.CssSelector("input[name='kp_query']"));
        private IWebElement searchButton => _browser.Page.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement profileSettingButton => _browser.Page.FindElement(By.XPath("//a[contains(text(), 'Настройки')]"));
        private IWebElement advancedSearchButton => _browser.Page.FindElement(By.XPath("//a[@aria-label = 'advanced-search']"));
        private IWebElement searchResultsSuggested(string contentName) => _browser.Page.FindElement(
            By.XPath($"//div/h4[text() = '{contentName}']"));

        public LoginPage OpenLoginPage()
        {
            if (IsUserLoggedIn())
                Logout();

            ClickLogin();
            return new LoginPage(_browser);
        }

        public SearchResultsPage SearchContent(string contentName)
        {
            Search(contentName);
            return new SearchResultsPage(_browser);
        }

        public void ClickLogin() => _browser.Page.Click(loginButton);

        /// <summary>
        /// Logout if already logged in
        /// </summary>
        public void Logout()
        {
            if (IsUserLoggedIn())
            {
                _browser.Page.MoveToElement(userProfileMenu);
                _browser.Page.Click(logoutButton);
            }
        }

        /// <summary>
        /// Navigate to Profile page
        /// </summary>
        /// <returns></returns>
        public ProfilePage OpenProfilePage()
        {
            _browser.Page.MoveToElement(userProfileMenu);
            _browser.Page.Click(profileSettingButton);
            return new ProfilePage(_browser);
        }

        public bool IsUserLoggedIn()  => _browser.Page.IsElementPresent(By.XPath("//button[contains(text(), 'Выйти')]"));

        /// <summary>
        /// Search content from search field box
        /// </summary>
        /// <param name="contentName"></param>
        public void Search(string contentName)
        {
            _browser.Page.Type(contentName, searchContentField);
            _browser.Page.Click(searchButton);
        }

        /// <summary>
        /// Search content and and open content page from suggested list
        /// </summary>
        /// <param name="contentName"></param>
        public void SearchSuggestedContent(string contentName)
        {
            _browser.Page.Type(contentName, searchContentField);
            _browser.Page.Click(searchResultsSuggested(contentName));
        }

        public SearchContentPage OpenSearchContentPage()
        {
            _browser.Page.Click(advancedSearchButton);
            return new SearchContentPage(_browser);
        }
    }
}
