using HtmlAgilityPack;
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
        private string outputDirectory;
        private string templateDirectory;
        private string scriptId = "script-data";
        public int featuresPassed => features.Where(f => f.passed).Count();
        public int featuresFailed => features.Where(f => !f.passed).Count();
        public int scenariosPassed => features.Sum(f => f.scenariosPassed);
        public int scenariosFailed => features.Sum(f => f.scenariosFailed);
        public int stepsPassed => features.Sum(f => f.stepsPassed);
        public int stepsFailed => features.Sum(f => f.stepsFailed);

        public List<ReportFeature> features { get; } = new List<ReportFeature>();
        public Reporter(string templateDirectory, string outputDirectory)
        {
            this.templateDirectory = templateDirectory;
            this.outputDirectory = outputDirectory;
        }
        public Reporter(string OutputPath)
        {
            this.outputDirectory = OutputPath;
        }
        public Reporter AddFeature(ReportFeature reportFeature)
        {
            reportFeature.key = features.Count();
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
            using (StreamWriter sw = new StreamWriter(outputDirectory))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        public string MakeData()
        {
            var json = JsonConvert.SerializeObject(this);
            json = "const data = " + json;
            return json;
        }

        public void WriteReport()
        {
            
            var templateJs = Path.Combine(templateDirectory, "index.js");
            var outputJs = Path.Combine(outputDirectory, "index.js");
            File.Copy(templateJs, outputJs, true);

            var templateCss = Path.Combine(templateDirectory, "styles.css");
            var outputCss = Path.Combine(outputDirectory, "styles.css");
            File.Copy(templateCss, outputCss, true);

            var templateHtml = Path.Combine(templateDirectory, "template.html");
            var outputHtml = Path.Combine(outputDirectory, "vuereport.html");
            var doc = new HtmlDocument();
            doc.Load(templateHtml);

            var scriptNode = doc.DocumentNode.SelectSingleNode($"//script[@id='{scriptId}']");
            var textNode = doc.CreateTextNode(MakeData());
            scriptNode.AppendChild(textNode);

            File.Delete(outputHtml);     
            Stream output = File.Open(outputHtml, FileMode.OpenOrCreate);
            doc.Save(output);
            output.Close();

        }
    }
}
