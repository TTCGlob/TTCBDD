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
                address = new Address()
                {
                    number = 12,
                    street = "Tompham Way",
                    city = "Sodor Island"
                    
                },
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

        [Given(@"User accesses company with ID (.*) at ""(.*)""")]
        public void GivenUserAccessesCompanyWithIDAt(int id, string resource)
        {
            var company = new RestCall<Company>(Method.GET, context.Get<string>("url"), resource)
                .AddUrlParameter("id", id.ToString())
                .Data();
            context["company"] = company;
        }

        [Given(@"User accesses a company at ""(.*)""")]
        public void GivenUserAccessesACompanyAt(string resource)
        {
            var company = new RestCall<List<Company>>(Method.GET, context.Get<string>("url"), resource)
                .Data()
                .First();
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
                .AddUrlParameter("id", company.id.ToString())
                .AddPayload(company)
                .Execute();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [When(@"User sets company value to (.*)")]
        public void WhenUserSetsCompanyValueTo(int newValue)
        {
            var company = context.Get<Company>("company");
            company.netWorth = newValue;
            context["company"] = company;
             var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), "companies/{id}")
                .AddUrlParameter("id", company.id.ToString())
                .AddPayload(company)
                .Execute();
        }

        [When(@"User changes the company (.*) address and submits it to ""(.*)""")]
        public void WhenUserChangesTheCompanyAddressAndSubmitsItTo(int id, string resource)
        {
            var company = context.Get<Company>("company");
            company.address = new Address()
            {
                number = 55,
                street = "Yeet Street",
                city = "Falcon City"
            };
            var response = new RestCall<object>(Method.PUT, context.Get<string>("url"), resource)
                .AddUrlParameter("id", id.ToString())
                .AddPayload(company)
                .Execute();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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

        [Then(@"The change is reflected at ""(.*)""")]
        public void ThenTheChangeIsReflectedAt(string resource)
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), resource)
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            storedCompany.netWorth.Should().Be(retrievedCompany.netWorth);
        }
        [Then(@"The company (.*) should have a new address")]
        public void ThenTheCompanyShouldHaveANewAddress(int id)
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), "/companies/{id}")
                .AddUrlParameter("id", id.ToString())
                .Data();
            (storedCompany.address.street == retrievedCompany.address.street && storedCompany.address.number == retrievedCompany.address.number && storedCompany.address.city == retrievedCompany.address.city)
                .Should().BeTrue();
        }


    }
}
