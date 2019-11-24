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
        public string StepType { get; }
        public string Name { get; }
        public bool Pass { get => _stepError == null;  }

        private ReportStepError _stepError;

        public ReportStepError StepError { get => _stepError; }
        public ReportStep(StepInfo stepInfo)
        {
            StepType = stepInfo.StepDefinitionType.ToString();
            Name = stepInfo.Text;
        }
        public ReportStep Fail(ReportStepError stepError)
        {
            _stepError = stepError;
            return this;
        }
    }
}
