using log4net;
using System;
using TechTalk.SpecFlow;
using TTCBDD.ComponentHelper;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class LoggingExampleSteps
    {
        // The configuration for logging is taken from app.config
        // The 'typeof' is always the name of 'this' class
        // Declare this as private for the class and use it in any step def in this class
        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(LoggingExampleSteps));

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            // Demonstrating logging with log4net
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {            
            // This logger outputs to both the console and the file log
            // But each has an independent logging level as defined in 
            // the app.config xml
            Logger.Debug("Logger: Debug info usually only goes to the console");
            Logger.Info("Logger: Info goes to both the console and the file");
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            // Demonstrating logging with log4net
            Logger.Info("Logger: The result should be " + p0 + " on the screen");
        }
    }
}
