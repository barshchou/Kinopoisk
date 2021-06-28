using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Kinopoisk.Core.Driver
{
    public class Driver
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                return SetupDriver();
            }
            return _driver;
        }

        private static IWebDriver SetupDriver()
        {
            return new ChromeDriver();
        }

        public static void QuitDriver()
        {
            _driver.Close();
            _driver.Quit();
            _driver = null;
        }
    }
}
