using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TTCBDD.CustomReporter
{
    public class ReportScenario
    {
        public string Name { get; }
        public string Description { get; }
        public string[] Tags { get; }
        public List<ReportStep> Steps { get; }
        private Exception TestException;
        public bool Pass => Steps.All(s => s.Pass);

        public ReportScenario(string Name, string Description, string[] Tags)
        {
            this.Name = Name;
            this.Description = Description;
            this.Tags = Tags;
        }
        public ReportScenario(ScenarioInfo scenarioInfo)
        {
            Name = scenarioInfo.Title;
            Description = scenarioInfo.Description;
            Tags = scenarioInfo.Tags;
            Steps = new List<ReportStep>();
        }

        public ReportScenario AddStep(ReportStep step)
        {
            Steps.Add(step);
            return this;
        }
        public ReportScenario Fail(ReportStepError stepError)
        {
            Steps.Last().Fail(stepError);
            return this;
        }
    }
}
