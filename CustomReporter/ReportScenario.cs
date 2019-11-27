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
        public string name { get; }
        public string description { get; }
        public string[] Tags { get; }
        private DateTime start;
        private DateTime end;
        public TimeSpan duration { get => end.Subtract(start); }
        public bool Pass => steps.All(s => s.pass);
        public int stepsPassed => steps.Where(s => s.pass).Count();
        public int stepsFailed => steps.Where(s => !s.pass).Count();
        public List<ReportStep> steps { get; } = new List<ReportStep>();
        private Exception testException;
        public int key { get; set; }
        public ReportScenario(ScenarioContext scenarioContext)
        {
            name = scenarioContext.ScenarioInfo.Title;
            description = scenarioContext.ScenarioInfo.Description;
            Tags = scenarioContext.ScenarioInfo.Tags;
            start = scenarioContext.Get<DateTime>("scenarioStart");
        }

        public ReportScenario AddStep(ReportStep reportStep)
        {
            reportStep.key = steps.Count();
            steps.Add(reportStep);
            return this;
        }
        public ReportScenario Fail(ReportStepError stepError)
        {
            steps.Last().Fail(stepError);
            return this;
        }
        public ReportScenario End()
        {
            end = DateTime.Now;
            return this;
        }
    }
}
