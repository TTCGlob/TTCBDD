using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace TTCBDD.UtilityClasses
{
    public static class ScenarioContextExtensions
    {
        private static readonly string WebDriverKey = "--web-driver";
        public static bool HasTag(this ScenarioContext context, string tag)
        {
            return Array.Exists(context.ScenarioInfo.Tags, t => t.Equals(tag));
        }

        public static void SetDriver(this ScenarioContext context, IWebDriver driver)
        {
            context[WebDriverKey] = driver;
        }

        public static IWebDriver GetDriver(this ScenarioContext context)
        {
            return context[WebDriverKey] as IWebDriver;
        }
    }
}
