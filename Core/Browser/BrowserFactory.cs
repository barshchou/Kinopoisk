using Kinopoisk.Core.Browser;
using Kinopoisk.Core.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace Kinopoisk
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

        // TODO - Fix Remote WebDriver
        //IBrowser<RemoteWebDriver> IBrowserWebDriver<RemoteWebDriver>.Create()
        //{
        //    DesiredCapabilities capabilities;
        //    var gridUrl = BrowserConfig.GridHubUri;

        //    switch (BrowserConfig.Browser)
        //    {
        //        case BrowserType.Chrome:
        //            capabilities = new DesiredCapabilities();
        //            break;
        //        case BrowserType.Firefox:
        //            capabilities = new DesiredCapabilities();
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }

        //    if (BrowserConfig.RemoteBrowser && BrowserConfig.UseSauceLabs)
        //    {
        //        capabilities.SetCapability(CapabilityType.Version, "50");
        //        capabilities.SetCapability(CapabilityType.Platform, "Windows 10");
        //        capabilities.SetCapability("username", BrowserConfig.SauceLabsUsername);
        //        capabilities.SetCapability("accessKey", BrowserConfig.SauceLabsAccessKey);
        //        gridUrl = BrowserConfig.SauceLabsHubUri;
        //    }
        //    else if (BrowserConfig.RemoteBrowser && BrowserConfig.UseBrowserstack)
        //    {
        //        capabilities.SetCapability(CapabilityType.Version, "50");
        //        capabilities.SetCapability(CapabilityType.Platform, "Windows 10");
        //        capabilities.SetCapability("username", BrowserConfig.BrowserStackUsername);
        //        capabilities.SetCapability("accessKey", BrowserConfig.BrowserStackAccessKey);
        //        gridUrl = BrowserConfig.BrowserStackHubUrl;
        //    }

        //    return
        //        new BrowserAdapter<RemoteWebDriver>(
        //            new RemoteWebDriver(new Uri(gridUrl), capabilities), BrowserType.Remote);
        //}
    }
}
