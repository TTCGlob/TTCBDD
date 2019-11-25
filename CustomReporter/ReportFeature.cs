using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TTCBDD.CustomReporter
{
    public class ReportFeature
    {
        public string name { get; }
        public string description { get; }
        
        private DateTime start;
        private DateTime end;
        public TimeSpan duration { get => end.Subtract(start); }

        public int scenariosPassed => scenarios.Where(s => s.Pass).Count();
        public int scenariosFailed => scenarios.Where(s => !s.Pass).Count();
        public int stepsPassed => scenarios.Sum(s => s.stepsPassed);
        public int stepsFailed => scenarios.Sum(s => s.stepsFailed);
        public bool passed => scenarios.All(s => s.Pass);

        public List<ReportScenario> scenarios { get; } = new List<ReportScenario>();

        public ReportFeature(FeatureContext featureContext)
        {
            name = featureContext.FeatureInfo.Title;
            start = featureContext.Get<DateTime>("featureStart");
            description = featureContext.FeatureInfo.Description;
        }

        public ReportFeature AddScenario(ReportScenario scenario)
        {
            scenarios.Add(scenario);
            return this;
        }

        public ReportFeature AddStep(ReportStep reportStep)
        {
            scenarios.Last().AddStep(reportStep);
            return this;
        }
        public ReportFeature Fail(ReportStepError stepError)
        {
            scenarios.Last().Fail(stepError);
            return this;
        }
        public ReportFeature End()
        {
            end = DateTime.Now;
            return this;
        }
    }
}
