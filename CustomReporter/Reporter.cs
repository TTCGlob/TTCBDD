using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;

namespace TTCBDD.CustomReporter
{
    public class Reporter
    {
        private string OutputPath;
        public int featuresPassed => features.Where(f => f.passed).Count();
        public int featuresFailed => features.Where(f => !f.passed).Count();
        public int scenariosPassed => features.Sum(f => f.scenariosPassed);
        public int scenariosFailed => features.Sum(f => f.scenariosFailed);
        public int stepsPassed => features.Sum(f => f.stepsPassed);
        public int stepsFailed => features.Sum(f => f.stepsFailed);

        public List<ReportFeature> features { get; } = new List<ReportFeature>();
        public Reporter(string OutputPath)
        {
            this.OutputPath = OutputPath;
        }
        public Reporter AddFeature(ReportFeature reportFeature)
        {
            features.Add(reportFeature);
            return this;
        }

        public Reporter AddScenario(ReportScenario reportScenario)
        {
            features.Last().AddScenario(reportScenario);
            return this;
        }
        public Reporter AddStep(ScenarioContext scenarioContext)
        {
            features.Last().AddStep(new ReportStep(scenarioContext));
            return this;
        }
        public Reporter Fail(ReportStepError stepError)
        {
            features.Last().Fail(stepError);
            return this;
        }

        public Reporter EndFeature()
        {
            features.Last().End();
            return this;
        }

        public Reporter EndScenario()
        {
            features.Last().scenarios.Last().End();
            return this;
        }

        public void Serialize()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(OutputPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }
    }
}
