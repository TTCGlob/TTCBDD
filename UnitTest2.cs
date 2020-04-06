using NUnit.Framework;
using TTCBDD.StepDefinition;
using TTCBDD.Configuration;
using TechTalk.SpecFlow;

namespace TTCBDD
{

    [TestFixture]
    public class UnitTest2
    {
        [TestCase]
        public void Login()
        {
            var browserAndUrl = new AppConfigReader();
            browserAndUrl.GetBrowser();
            browserAndUrl.GetWebsiteUrl();

            var variable = new DemoWebShopFeatureSteps();
            variable.GivenINavigatedToTheDemoWebShopWebsite();
            System.Console.WriteLine("Hello Steffie");
        }
    }
}
