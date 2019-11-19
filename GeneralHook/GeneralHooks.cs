using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
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
        private static ILog staticLogger = Log4NetHelper.GetXmlLogger(typeof(GeneralHooks)); // For static methods

        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Set the target folders
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "Report\\index.html";
            
            // Create the HTML Reporter
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            
            //These don't actually seem to work
            htmlReporter.Configuration().Theme = Theme.Dark;
            htmlReporter.Configuration().DocumentTitle = "TTC Automation Report";
            htmlReporter.Configuration().ReportName = "TTC Automation Report";
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;

            // Create the Extent Report
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title, 
                FeatureContext.Current.FeatureInfo.Description.Replace("\t", ""));
            staticLogger.Info("Starting Feature: " + FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            PublicVar.StepStatus = "Pass";
            PublicVar.StepMsg = "";
            scenario = featureName.
                CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title, 
                ScenarioContext.Current.ScenarioInfo.Description.Replace("\t", ""));

            Logger.Info("Starting scenario: " + ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        [Obsolete]
        public void InsertReportingSteps()
        {
            // Colin's alternative method
            // TODO: Discuss: IGherkinFormatterModel does not have the concept of 'And'
            ScenarioBlock scenarioBlock = ScenarioContext.Current.CurrentScenarioBlock;
            if (ScenarioContext.Current.TestError == null && PublicVar.StepStatus == "Pass")
            {
                switch (scenarioBlock)
                {
                    case ScenarioBlock.Given:
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass("Passed");
                        break;
                    case ScenarioBlock.When:
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass("Passed");
                        break;
                    case ScenarioBlock.Then:
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass("Passed");
                        break;
                    default:
                        break;
                }
            }
            else if(ScenarioContext.Current.TestError != null)
            {
                switch (scenarioBlock)
                {
                    case ScenarioBlock.Given:
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).
                            Fail(string.Format("Error from: {0}\nMessage: {1}\nStack Trace: {2}",
                            ScenarioContext.Current.TestError.Source,
                            ScenarioContext.Current.TestError.Message,
                            ScenarioContext.Current.TestError.StackTrace));
                        break;
                    case ScenarioBlock.When:
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).
                            Fail(string.Format("Error from: {0}\nMessage: {1}\nStack Trace: {2}",
                            ScenarioContext.Current.TestError.Source,
                            ScenarioContext.Current.TestError.Message,
                            ScenarioContext.Current.TestError.StackTrace));
                        break;
                    case ScenarioBlock.Then:
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).
                            Fail(string.Format("Error from: {0}\nMessage: {1}\nStack Trace: {2}",
                            ScenarioContext.Current.TestError.Source, 
                            ScenarioContext.Current.TestError.Message,
                            ScenarioContext.Current.TestError.StackTrace));
                        break;
                    default:
                        break;
                }
            }
            else if (PublicVar.StepStatus == "Fail")
            {
                switch (scenarioBlock)
                {
                    case ScenarioBlock.Given:
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                        break;
                    case ScenarioBlock.When:
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                        break;
                    case ScenarioBlock.Then:
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
                        break;
                    default:
                        break;
                }
            }
            
            // CJ[20/11/2019]: ketan, delete this if you thihnk the above refactor is acceptable

            //var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            //if (ScenarioContext.Current.TestError == null && PublicVar.StepStatus== "Pass")
            //{
            //    if (stepType == "Given")
            //        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
            //    else if(stepType == "When")
            //                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
            //    else if(stepType == "Then")
            //                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
            //    else if(stepType == "And")
            //                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            //}
            //else if(ScenarioContext.Current.TestError != null)
            //{
            //    if (stepType == "Given")
            //    {
            //        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    }
            //    else if(stepType == "When")
            //    {
            //        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    }
            //    else if(stepType == "Then") {
            //        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    }
            //    else if(stepType == "And")
            //    {
            //        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    }
            //    goto done;
            //}
            //else if (PublicVar.StepStatus == "Fail")
            //{
            //    if (stepType == "Given")
            //    {
            //        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
            //    }
            //    else if (stepType == "When")
            //    {
            //        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
            //    }
            //    else if (stepType == "Then")
            //    {
            //        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
            //    }
            //    else if (stepType == "And")
            //    {
            //        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(PublicVar.StepMsg);
            //    }
            //}

            UnHandleError();
        }

        [AfterScenario]
        [Obsolete]
        public void AfterScenario()
        {
            var scenario = ScenarioContext.Current;
            string name = scenario.ScenarioInfo.Title;
            Logger.Info(string.Format("Finished scenario: {0}", name));

            if (scenario.TestError != null)
            {
                var error = scenario.TestError;
                Logger.Error(string.Format("An error ocurred: {0}", error.Message));
                Logger.Error(string.Format("It was of type: {0}", error.GetType().Name));
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
