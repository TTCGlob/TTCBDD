using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TTCBDD.ComponentHelper;
using TTCBDD.CustomReporter;

namespace TTCBDD.GeneralHook
{
    [Binding]
    public sealed class GeneralHooks
    {

        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks));
        private static ILog StaticLogger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks));
        private static Reporter Reporter;
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
        public static void ConfigureCustomReporter()
        {
            var SolutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var TemplatePath = Path.Combine(SolutionDirectory, @"CustomReporter");
            ReportPath = Path.Combine(SolutionDirectory, @"Report");
            Reporter = new Reporter(TemplatePath, ReportPath);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "Report\\index.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            StaticLogger.Info(string.Format("Extent Report written to {0}", path));
            var templatePath = Path.Combine(path1, "/CustomReporter/index.html");
            var ReportDir = Path.GetDirectoryName(ReportPath);

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
            featureContext["featureStart"] = DateTime.Now;
            Reporter.AddFeature(new ReportFeature(featureContext));
            StaticLogger.Info($"Feature: {featureContext.FeatureInfo.Title}");
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            scenarioContext["scenarioStart"] = DateTime.Now;
            Reporter.AddScenario(new ReportScenario(scenarioContext));
            StaticLogger.Info($"Scenario: {scenarioContext.ScenarioInfo.Title}");
        }
        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            Logger.Info($"Step: {scenarioContext.StepContext.StepInfo.StepDefinitionType}: {scenarioContext.StepContext.StepInfo.Text}");
            scenarioContext["stepStart"] = DateTime.Now;
        }
        [AfterStep]
        public static void InsertReportingSteps(ScenarioContext scenarioContext)
        {

            var stepInfo = scenarioContext.StepContext.StepInfo;
            var stepStatus = scenarioContext.ScenarioExecutionStatus;
            scenarioContext["stepEnd"] = DateTime.Now;
            Reporter.AddStep(scenarioContext);
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
                Reporter.Fail(new ReportStepError(scenarioContext.TestError.Source, scenarioContext.TestError.Message, scenarioContext.TestError.StackTrace));
                test.Fail(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
                StaticLogger.Error(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
            }
        }

        [AfterScenario]
        public static void AfterScenario(ScenarioContext scenarioContext)
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
            Reporter.EndScenario();
            if (scenarioContext.TestError != null)
            {
                StaticLogger.Error($"The scenario {scenarioContext.ScenarioInfo.Title} has finished with test error(s): {scenarioContext.TestError}");
            }
        }
        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Reporter.EndFeature();
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
            Reporter.WriteReport();
            //Reporter.Serialize();
            var ScriptPath = Path.Combine(ReportPath, "script.js");
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
