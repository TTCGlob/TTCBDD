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
        public string Name { get; }
        public string Description { get; }
        public List<ReportScenario> Scenarios { get; } = new List<ReportScenario>();
        public bool Pass
        {
            get => Scenarios.All(s => s.Pass);
        }

        public ReportFeature(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
        public ReportFeature(FeatureInfo featureInfo)
        {
            Name = featureInfo.Title;
            Description = featureInfo.Description;
        }

        public ReportFeature AddScenario(ReportScenario scenario)
        {
            Scenarios.Add(scenario);
            return this;
        }
        public ReportFeature AddScenario(ScenarioInfo scenarioInfo)
        {
            Scenarios.Add(new ReportScenario(scenarioInfo));
            return this;
        }
        public ReportFeature AddStep(ReportStep step)
        {
            Scenarios.Last().AddStep(step);
            return this;
        }
        public ReportFeature Fail(ReportStepError stepError)
        {
            Scenarios.Last().Fail(stepError);
            return this;
        }
    }
}
