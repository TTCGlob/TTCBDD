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
        public List<ReportFeature> Features { get; }
        public Reporter(string OutputPath)
        {
            this.OutputPath = OutputPath;
            Features = new List<ReportFeature>();
        }
        public Reporter AddFeature(FeatureInfo featureInfo)
        {
            Features.Add(new ReportFeature(featureInfo));
            return this;
        }

        public Reporter AddScenario(ScenarioInfo scenarioInfo)
        {
            Features.Last().AddScenario(scenarioInfo);
            return this;
        }
        public Reporter AddStep(StepInfo stepInfo, ScenarioExecutionStatus status)
        {
            var step = new ReportStep(stepInfo);
            Features.Last().AddStep(step);
            return this;
        }
        public Reporter Fail(ReportStepError stepError)
        {
            Features.Last().Fail(stepError);
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
