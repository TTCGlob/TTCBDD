using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.RestObjects;
using TTCBDD.Extension;
using TTCBDD.Helpers.Rest;

namespace TTCBDD.StepDefinition.Rest
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

        [Given(@"User creates new product")]
        public void GivenUserCreatesNewProduct()
        {
            var product = new Product()
            {
                product_name = "Eggs Size 8",
                stock_level = 3500,
            };
            context.Add("product", product);
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

        [When(@"User posts this to ""(.*)""")]
        public void WhenUserPostsThisTo(string resource)
        {
            var product = context.Get<Product>("product");
            context["resource"] = resource;
            var response = new RestCall<Product>(Method.POST, context.GetUrl(), resource)
                .AddPayload(product)
                .Execute(res => product.id = res.Data.id)
                .StatusCode.Should().Be(HttpStatusCode.Created);
            context["product"] = product;
        }


        [Then(@"All the fresh products are displayed")]
        public void ThenAllTheFreshProductsAreDisplayed()
        {
            var names = context.Get<IEnumerable<string>>("names");
            names.ForEach(n => Console.WriteLine(n));
            names.Should().NotBeEmpty();
        }

        [Then(@"The product is visible in the database")]
        public void ThenTheProductIsVisibleInTheDatabase()
        {
            var storedProduct = context.Get<Product>("product");
            var retrievedProduct = new RestCall<Product>(Method.GET, context.GetUrl(), "products/{id}")
                .AddUrlParameter("id", storedProduct.id.ToString())
                .Data().Equals(storedProduct).Should().BeTrue();
        }

    }
}