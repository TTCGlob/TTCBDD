using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TTCBDD.StepDefinition;
using TTCBDD.Settings;
using TTCBDD.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TTCBDD.Hooks.GeneralHook
{
    [Binding]
    public sealed class HooksSG
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            //Set the Config property to an instance of AppConfigReader to avoid a NullReferenceException on run.
            ObjectRepository.Config = new AppConfigReader();
            ObjectRepository.Driver = new ChromeDriver();

            ObjectRepository.Driver.Navigate().GoToUrl(ObjectRepository.Config.GetWebsiteUrl());

/*            var variable = new DemoWebShopFeatureSteps();
            variable.GivenINavigatedToTheDemoWebShopWebsite();*/
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ObjectRepository.Driver.Close();
        }
    }
}
