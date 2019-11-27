using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TTCBDD.CustomReporter
{
    public class ReportStep
    {
        public string stepType { get; }
        public string name { get; }
        public bool pass { get => _stepError == null;  }
        private DateTime start;
        private DateTime end;
        public TimeSpan duration { get => end.Subtract(start); }

        private ReportStepError _stepError;

        public int key { get; set; }

        public ReportStepError StepError { get => _stepError; }

        public ReportStep(ScenarioContext scenarioContext)
        {
            stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            name = scenarioContext.StepContext.StepInfo.Text;
            start = scenarioContext.Get<DateTime>("stepStart");
            end = scenarioContext.Get<DateTime>("stepEnd");
        }

        public ReportStep Fail(ReportStepError stepError)
        {
            _stepError = stepError;
            return this;
        }
    }
}
