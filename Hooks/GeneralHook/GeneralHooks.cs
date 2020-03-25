using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using HtmlAgilityPack;
using log4net;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TTCBDD.Helpers.Generic;

namespace TTCBDD.Hooks.GeneralHook
{
    [Binding]
    public sealed class GeneralHooks
    {

        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks));
        private static ILog StaticLogger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks));

        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;

        public FeatureContext featureContext;

        public GeneralHooks(FeatureContext featureContext)
        {
            this.featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "Report\\index.html";
            ReportPath = path;
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            StaticLogger.Info(string.Format("Extent Report written to {0}", path));

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Configuration().DocumentTitle = "TTC Test Automation";
            htmlReporter.Configuration().ReportName = "TTC Test Automation";

            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            StaticLogger.Info($"Before test run");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            StaticLogger.Info($"Feature: {featureContext.FeatureInfo.Title}");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            Logger.Info($"Scenario: {scenarioContext.ScenarioInfo.Title}");
        }
        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            Logger.Info($"Step: {scenarioContext.StepContext.StepInfo.StepDefinitionType}: {scenarioContext.StepContext.StepInfo.Text}");
        }
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {

            var stepInfo = scenarioContext.StepContext.StepInfo;
            var stepStatus = scenarioContext.ScenarioExecutionStatus;
            ExtentTest test;
            switch (stepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    test = scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.When:
                    test = scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.Then:
                    test = scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    test = scenario.CreateNode("default");
                    break;
            }

            if (stepStatus != ScenarioExecutionStatus.OK)
            {
                test.Fail(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
                Logger.Error(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            //var scenario = ScenarioContext.Current;
            //string name = scenario.ScenarioInfo.Title;
            //Logger.Info("Finished scenario:" + name);

            //if (scenario.TestError != null)
            //{
            //    ScreenshotHelper.TakeScreenshot(name + "_after_scenario.jpg");
            //    var error = scenario.TestError;
            //    Logger.Error("An error ocurred:" + error.Message);
            //    Logger.Error("It was of type:" + error.GetType().Name);
            //}

            if (scenarioContext.TestError != null)
            {
                Logger.Error($"The scenario {scenarioContext.ScenarioInfo.Title} has finished with test error(s): {scenarioContext.TestError}");
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
            var ReportDir = Path.GetDirectoryName(ReportPath);
            var ScriptPath = Path.Combine(ReportDir, "script.js");
            var jsString = File.ReadAllText(ScriptPath);
            var doc = new HtmlDocument();
            doc.Load(ReportPath);
            var body = doc.DocumentNode.SelectSingleNode("//body");
            var script = doc.CreateElement("script");
            var js = doc.CreateTextNode(jsString);
            script.AppendChild(js);
            //script.SetAttributeValue("src", "./script.js");
            body.AppendChild(script);
            doc.Save(ReportPath);
        }
    }
}
