using OpenQA.Selenium;

namespace Kinopoisk.Core.Interfaces
{
    public interface IBrowserWebDriver<out T> where T : IWebDriver
    {
        IBrowser<T> Create();
    }
}
