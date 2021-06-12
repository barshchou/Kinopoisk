using OpenQA.Selenium;

namespace Kinopoisk.Core.Interfaces
{
    public interface IBrowser
    {
        IPage Page { get; }
    }

    public interface IBrowser<out T> : IBrowser where T : IWebDriver
    {
        BrowserType Type { get; }
        T Driver { get; }
    }
}
