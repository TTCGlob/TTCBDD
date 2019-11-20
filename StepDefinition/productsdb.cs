using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public sealed class productsdb
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public productsdb(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"User accesses API at ""(.*)""")]
        public void GivenUserAccessesAPIAt(string url)
        {
            context.Add("url", url);
        }

        [When(@"User accesses products labeled fresh")]
        public void WhenUserAccessesProductsLabeledFresh()
        {
            var response = new RestCall<List<Product>>(Method.GET, context.Get<string>("url"), "/products")
                .Where("product_name like Fresh")
                .Execute();
            response.IsSuccessful.Should().BeTrue();
            context.Add("data", response.Data);
        }

        [When(@"Selects product names")]
        public void WhenSelectsProductNames()
        {
            var results = context.Get<List<Product>>("data");
            var names = results.Select(p => p.product_name);
            context.Add("names", names);
        }

        [Then(@"All the fresh products are displayed")]
        public void ThenAllTheFreshProductsAreDisplayed()
        {
            var names = context.Get<IEnumerable<string>>("names");
            names.ForEach(n => Console.WriteLine(n));
            names.Should().NotBeEmpty();
        }

    }
}