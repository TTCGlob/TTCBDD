using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.Reporter
{
    public class Scenario
    {
        public string Name { get; }
        public string Description { get; }
        public string[] Tags { get; }
        private List<Step> Steps;
        private Exception TestException;
        public bool Pass => Steps.All(s => s.Pass);

        public Scenario(string Name, string Description, string[] Tags)
        {
            this.Name = Name;
            this.Description = Description;
            this.Tags = Tags;
        }

        public Scenario AddStep(Step step)
        {
            Steps.Add(step);
            return this;
        }
    }
}
