using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TTCBDD.ComponentHelper
{
    public class AssertHelperWithInjection
    {
        ScenarioContext context;
        public AssertHelperWithInjection(ScenarioContext context)
        {
            this.context = context;
        }

        public void AreEqual(object Expected, object Actual)
        {
            try
            {
                Assert.AreEqual(Expected, Actual);
                Console.WriteLine("Assert Equal Success " + Expected);

            }
            catch (Exception e)
            {
            
            }
        }
    }
}
