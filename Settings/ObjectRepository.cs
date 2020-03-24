using OpenQA.Selenium;
using TTCBDD.Interfaces;
using System.Collections.Generic;

namespace TTCBDD.Settings
{
    public class ObjectRepository
    {
        public static IConfig Config { get; set; }
        public static IWebDriver Driver { get; set; }

        public static List<string> ErrMsg = new List<string>();
    }
}
