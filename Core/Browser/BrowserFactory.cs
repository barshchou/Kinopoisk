using Kinopoisk.Core.Enums;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace Kinopoisk.Core.Browser
{
    public sealed class BrowserFactory : 
        AbstractFactory,
        IBrowserWebDriver<FirefoxDriver>,
        IBrowserWebDriver<ChromeDriver>
        //,
        //IBrowserWebDriver<RemoteWebDriver>
    {
        IBrowser<ChromeDriver> IBrowserWebDriver<ChromeDriver>.Create()
        {
            var dirName = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin", StringComparison.Ordinal));
            var fileInfo = new FileInfo(dirName);
            var parentDirName = fileInfo?.FullName;
            return new BrowserAdapter<ChromeDriver>(new ChromeDriver(parentDirName + @"Drivers"), BrowserType.Chrome);
        }

        IBrowser<FirefoxDriver> IBrowserWebDriver<FirefoxDriver>.Create()
        {
            var dirName = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin", StringComparison.Ordinal));
            var fileInfo = new FileInfo(dirName);
            var parentDirName = fileInfo?.FullName;
            var service = FirefoxDriverService.CreateDefaultService(parentDirName + @"\Drivers");
            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            return new BrowserAdapter<FirefoxDriver>(new FirefoxDriver(service), BrowserType.Firefox);
        }
    }
}
