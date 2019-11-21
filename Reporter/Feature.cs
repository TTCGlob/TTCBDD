using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.Reporter
{
    public class Feature
    {
        private string Name;
        private string Description;
        private List<Scenario> Scenarios;
        private bool Pass
        {
            get => Scenarios.All(s => s.Pass);
        }

        public Feature(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

        public Feature AddScenario(Scenario scenario)
        {
            Scenarios.Add(scenario);
        }

    }
}
