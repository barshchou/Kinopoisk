using OpenQA.Selenium;

namespace Kinopoisk.Core.Interfaces
{
    public interface IBrowserFactory
    {
        IBrowser Create<T>() where T : IWebDriver;
    }
}
