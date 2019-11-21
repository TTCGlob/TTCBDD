using FluentAssertions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;
using TTCBDD.PageObject;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public sealed class CompaniesDB
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public CompaniesDB(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }
        [Given(@"User creates a new company record")]
        public void GivenUserCreatesANewCompanyRecord()
        {
            Company company = new Company()
            {
                id = new Random().Next(999),
                companyName = "Sodor Transport",
                netWorth = 1444444,
                employees = new List<Employee>()
                {
                    new Employee()
                    {
                        id = "1",
                        name = "Thomas",
                        salary = "180",
                        age = "111"
                    },
                    new Employee()
                    {
                        id = "3",
                        name = "Henry",
                        salary = "13340405",
                        age = "33"
                    },
                    new Employee()
                    {
                        id = "5",
                        name = "Gordon",
                        salary = "13131",
                        age = "12"
                    },
                },
                shareholders = new List<Shareholder>()
                {
                    new Shareholder()
                    {
                        id = "565",
                        name = "Sir Tomham Hat",
                        stake = 100
                    }
                }
            };
            context["company"] = company;
        }


        [When(@"user accesses endpoint ""(.*)""")]
        public void WhenUserAccessesEndpoint(string resource)
        {
            var companies = new RestCall<List<Company>>(Method.GET, context.Get<string>("url"), resource)
                .Data();
            context["company"] = companies[0];
        }

        [When(@"User submits this to the endpoint ""(.*)""")]
        public void WhenUserSubmitsThisToTheEndpoint(string resource)
        {
            var company = context.Get<Company>("company");
            var response = new RestCall<Company>(Method.POST, context.Get<string>("url"), resource)
                .AddPayload(company)
                .Execute();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Then(@"a list of companies is returned")]
        public void ThenAListOfCompaniesIsReturned()
        {
            var company = context.Get<Company>("company");
            company.Should().NotBeNull();
        }

        [Then(@"the company is displayed in the database at ""(.*)""")]
        public void ThenTheCompanyIsDisplayedInTheDatabaseAt(string resource)
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), resource)
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            retrievedCompany.companyName.Should().Be(storedCompany.companyName);
        }


    }
}
