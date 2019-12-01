using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public sealed class NorthwindSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public NorthwindSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [When(@"User accesses orders going to ""(.*)""")]
        public void WhenUserAccessesOrdersGoingTo(string country)
        {
            var data = new RestCall<JArray>(Method.GET, context.Get<string>("url"), "Orders")
                .Equals("ShipCountry", country)
                .Select("ShipCountry", "OrderID", "CustomerID", "EmployeeID")
                .Traverse("value")
                .Data()
                .Select(o => new
                {
                    ShipCountry = (string)o["ShipCountry"],
                    OrderID = (int)o["OrderID"],
                    CustomerID = (string)o["CustomerID"],
                    EmployeeID = (int)o["EmployeeID"]
                });
            context["data"] = data;
        }

        [Then(@"Orders are displayed")]
        public void ThenOrdersAreDisplayed()
        {
            var data = context.Get<IEnumerable<Object>>("data");
        }

    }
}
