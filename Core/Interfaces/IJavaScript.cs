using Kinopoisk.Core.Adapters;
using OpenQA.Selenium;

namespace Kinopoisk.Core.Interfaces
{
    public interface IJavaScript
    {
        object Execute(string javaScript, params object[] args);
        void WaitAjax();
        void WaitReadyState();
        void FireJQueryEvent(IWebElement element, JavaScriptEvent javaScriptEvent);
    }
}
