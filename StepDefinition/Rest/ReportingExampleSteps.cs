using log4net;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TTCBDD.Helpers.Generic;

namespace TTCBDD.StepDefinition.Rest
{
    [Binding]
    public class ReportingExampleSteps
    {
        private ILog Log = Log4NetHelper.GetXmlLogger(typeof(ReportingExampleSteps));
        private int firstNumber;
        private int secondNumber;
        private int result;

        [Given(@"I have entered (.*) into my calculator")]
        public void GivenIHaveEnteredIntoMyCalculator(int p0)
        {
            firstNumber = p0;
            Log.Info(string.Format("Added first number: {0}", firstNumber));

        }

        [Given(@"I have also entered (.*) into my calculator")]
        public void GivenIHaveAlsoEnteredIntoMyCalculator(int p0)
        {
            secondNumber = p0;
            Log.Info(string.Format("Added second number: {0}", secondNumber));
        }

        [When(@"I press the add key")]
        public void WhenIPressTheAddKey()
        {
            result = firstNumber + secondNumber;
            Log.Info("Pressed the 'Add' key");
        }

        [When(@"I press the subtract key")]
        public void WhenIPressTheSubtractKey()
        {
            result = firstNumber - secondNumber;
            Log.Info("Pressed the 'Subtract' key");
        }

        [When(@"I press the multiply key")]
        public void WhenIPressTheMultiplyKey()
        {
            result = firstNumber * secondNumber;
            Log.Info("Pressed the 'Multiply' key");
        }

        [When(@"I press the divide key")]
        public void WhenIPressTheDivideKey()
        {
            result = firstNumber / secondNumber;
            Log.Info("Pressed the 'Divide' key");
        }

        [Then(@"the displayed result should be (.*)")]
        public void ThenTheDisplayedResultShouldBe(int p0)
        {
            if (p0 != result)
            {
                Log.Info(string.Format("Actual result using {0} and {1} ({2}) did not match expected value {3}",
                    firstNumber, secondNumber, result, p0));
            }
            Assert.AreEqual(p0, result);
        }
    }
}
