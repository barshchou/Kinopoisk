using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Kinopoisk.Core.Browser
{
    public class BrowserConfig
    {
        public static BrowserType Browser
            => (BrowserType)Enum.Parse(typeof(BrowserType), GetValue("Browser"));

        public static string BaseUrl => GetValue("BaseUrl");
        public static string Username => GetValue("Username");
        public static string Password => GetValue("Password");
        public static string Login => GetValue("Login");

        private static string GetValue(string value)
        {
            var dirName = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            var fileInfo = new FileInfo(dirName);
            var parentDirName = fileInfo?.FullName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(parentDirName)
                .AddJsonFile("appsettings.json");
            return builder.Build()[value];
        }
    }
}
