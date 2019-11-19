using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using log4net;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TTCBDD.ComponentHelper;
using TTCBDD.Public_Var;

namespace TTCBDD.GeneralHook
{
    [Binding]
    public sealed class GeneralHooks
    {

        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks));

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
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Configuration().DocumentTitle = "A new title";
            Console.WriteLine($"Before test run");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            Console.WriteLine($"Before Feature {featureContext.FeatureInfo.Title}");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            Console.WriteLine($"Currently running scenario {scenarioContext.ScenarioInfo.Title} which {scenarioContext.ScenarioInfo.Description}");
        }
        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine($"Before running test {scenarioContext.StepContext.StepInfo.Text}");
        }
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            Console.WriteLine($"This step has error: {scenarioContext.TestError}");
            //    //var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
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
                test.Fail(scenarioContext.TestError); 
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
            
            Console.WriteLine($"The scenario {scenarioContext.ScenarioInfo.Title} has finished with test error: {scenarioContext.TestError}");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

        public void UnHandleError()
        {

        }
    }
}
