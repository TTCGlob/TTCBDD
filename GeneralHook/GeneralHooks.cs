using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using log4net;
using System;
using TechTalk.SpecFlow;
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

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "Report\\index.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            //Console.WriteLine("BeforeFeature");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            PublicVar.StepStatus = "Pass";
            PublicVar.StepMsg = "";
            //Console.WriteLine("BeforeScenario");
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        [Obsolete]
        public void InsertReportingSteps()
        {
            
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null && PublicVar.StepStatus== "Pass")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "When")
                                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "Then")
                                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "And")
                                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if(ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                goto done;
            }
            else if (PublicVar.StepStatus == "Fail")
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                }
            }
        done:
            UnHandleError();
        }

        [AfterScenario]
        [Obsolete]
        public void AfterScenario()
        {
            var scenario = ScenarioContext.Current;
            string name = scenario.ScenarioInfo.Title;
            Logger.Info("Finished scenario:" + name);

            if (scenario.TestError != null)
            {
                ScreenshotHelper.TakeScreenshot(name + "_after_scenario.jpg");
                var error = scenario.TestError;
                Logger.Error("An error ocurred:" + error.Message);
                Logger.Error("It was of type:" + error.GetType().Name);
            }
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
