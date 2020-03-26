using FluentAssertions;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.RestObjects;
using TTCBDD.Helpers.Rest;

namespace TTCBDD.StepDefinition.Rest
{
    [Binding]
    public sealed class CompaniesDB_Reed
    {
        private ScenarioContext context;

        public CompaniesDB_Reed(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        #region Given
        [Given(@"User accesses companies API at ""(.*)""")]
        public void GivenUserAccessesCompaniesAPIAt(string url)
        {
            context["baseUrl"] = url;
        }

        [Given(@"User accesses employees of company with id: ""(.*)""")]
        public void GivenUserAccessesEmployeesOfCompanyWithId(string companyId)
        {
            context["company2Id"] = companyId;
            new RestCall<Company>(Method.GET, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", companyId)
                .Execute(res => context[$"company2Employees"] = res.Data.employees);
        }

        [Given(@"User accesses company with id: ""(.*)""")]
        public void GivenUserAccessesCompanyWithId(string companyId)
        {
            context["company1Id"] = companyId;
            new RestCall<Company>(Method.GET, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", companyId)
                .Execute(res => context["resCompany"] = res.Data);
        }

        [Given(@"User accesses all companies")]
        public void GivenUserAccessesAllCompanies()
        {
            new RestCall<List<Company>>(Method.GET, (string)context["baseUrl"], "")
                .Execute(res => context["companies"] = res.Data);
        }

        [Given(@"User accesses employee with id ""(.*)""")]
        public void GivenUserAccessesEmployeeWithId(string employeeId)
        {
            context["employee"] = context.Get<Company>("resCompany").employees
                .Find(e => e.id.Equals(employeeId));
        }

        [Given(@"User accesses shareholder with id ""(.*)""")]
        public void GivenUserAccessesShareholderWithId(string shareholderId)
        {
            context["shareholder"] = context.Get<Company>("resCompany").shareholders
                            .Find(e => e.id.Equals(shareholderId));
        }
        #endregion

        #region When
        [When(@"The User replaces company1 employees with company2 employees")]
        public void WhenTheUserReplacesCompanyEmployeesWithCompanyEmployees()
        {
            context.Get<Company>("resCompany").employees = context.Get<List<Employee>>("company2Employees");

            context["resCompany"] = new RestCall<Company>(Method.PUT, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", context.Get<Company>("resCompany").id.ToString())
                .AddPayload(context.Get<Company>("resCompany"))
                .Execute()
                .Data;
        }

        [When(@"User accesses company employees and increases each salary by ""(.*)""%")]
        public void WhenUserAccessesCompanyEmployeesAndIncreasesEachSalaryBy(int raise)
        {
            foreach (var contextCompany in (List<Company>)context["companies"])
            {
                if (contextCompany.employees != null)
                {
                    foreach (var emp in contextCompany.employees)
                    {
                        if (emp.salary != null)
                            emp.salary = BasicHelperMethods.RaiseSalary(emp.salary, raise);
                    }
                    new RestCall<Company>(Method.PUT, (string)context["baseUrl"], "/{id}")
                        .AddUrlParameter("id", contextCompany.id.ToString())
                        .AddPayload(contextCompany)
                        .Execute();
                }
            }
        }

        [When(@"User converts the employee to a shareholder")]
        public void WhenUserConvertsTheEmployeeToAShareholder()
        {
            // remove the employee specified from the employee list            
            context.Get<Company>("resCompany").employees = context.Get<Company>("resCompany")
                .employees
                .Where(employee => employee.id != context.Get<Employee>("employee").id.ToString())
                .ToList();

            if (context.Get<Company>("resCompany").shareholders == null)
                context.Get<Company>("resCompany").shareholders = new List<Shareholder>();
            // add the employee to the shareholder list
            context.Get<Company>("resCompany").shareholders.Add(new Shareholder()
            {
                id = context.Get<Employee>("employee").id,
                name = ((Employee)context["employee"]).name,
                stake = 1
            });

            new RestCall<Company>(Method.PUT, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", context.Get<Company>("resCompany").id.ToString())
                .AddPayload(context.Get<Company>("resCompany"))
                .Execute();
        }

        [When(@"User converts the shareholder to an employee")]
        public void WhenUserConvertsTheShareholderToAnEmployee()
        {
            // remove the shareholder specified from the shareholder list            
            context.Get<Company>("resCompany").shareholders = context.Get<Company>("resCompany")
                .shareholders
                .Where(shareholder => shareholder.id != context.Get<Shareholder>("shareholder").id.ToString())
                .ToList();

            if (context.Get<Company>("resCompany").employees == null)
                context.Get<Company>("resCompany").employees = new List<Employee>();
            // add the employee to the shareholder list
            context.Get<Company>("resCompany").employees.Add(new Employee()
            {
                id = context.Get<Shareholder>("shareholder").id,
                name = context.Get<Shareholder>("shareholder").name,
            });

            new RestCall<Company>(Method.PUT, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", context.Get<Company>("resCompany").id.ToString())
                .AddPayload(context.Get<Company>("resCompany"))
                .Execute();
        }

        [When(@"User creates a new employee and shareholder")]
        public void WhenUserCreatesANewEmployeeAndShareholder()
        {
            string name = "Boo Merr", salary = BasicHelperMethods.RandomNumber(1000000, 50000),
                age = "68", id = BasicHelperMethods.RandomNumber(10000, 100);
            int stake = int.Parse(BasicHelperMethods.RandomNumber(100));

            context["employee"] = new Employee()
            {
                id = id,
                name = name,
                salary = salary,
                age = age
            };
            context["shareholder"] = new Shareholder()
            {
                id = id,
                name = name,
                stake = stake
            };

            if (context.Get<Company>("resCompany").employees == null)
                context.Get<Company>("resCompany").employees = new List<Employee>();
            if (context.Get<Company>("resCompany").shareholders == null)
                context.Get<Company>("resCompany").shareholders = new List<Shareholder>();

            context.Get<Company>("resCompany").employees.Add(context.Get<Employee>("employee"));
            context.Get<Company>("resCompany").shareholders.Add(context.Get<Shareholder>("shareholder"));

            context["resCompany"] = new RestCall<Company>(Method.PUT, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", (string)context["company1Id"])
                .AddPayload(context.Get<Company>("resCompany"))
                .Execute()
                .Data;
        }

        [When(@"User deletes all companies with net worth less than ""(.*)""")]
        public void WhenUserDeletesAllCompaniesWithNetWorthLessThan(int minNetWorth)
        {
            foreach (var cntxcompany in (List<Company>)context["companies"])
            {
                if (cntxcompany.netWorth <= minNetWorth)
                    new RestCall<Company>(Method.DELETE, (string)context["baseUrl"], "/{id}")
                        .AddUrlParameter("id", cntxcompany.id.ToString())
                        .Execute();
            }
        }
        #endregion

        #region Then
        [Then(@"Company1 employees should match company2 employees")]
        public void ThenCompanyEmployeesShouldMatchCompanyEmployees()
        {
            context.Get<Company>("resCompany")
                .employees
                .Should()
                .BeEquivalentTo(context.Get<List<Employee>>($"company2Employees"));
        }

        [Then(@"The salary increase will be reflected in the database")]
        public void ThenTheSalaryIncreaseWillBeReflectedInTheDatabase()
        {
            var resCompanies = new RestCall<List<Company>>(Method.GET, (string)context["baseUrl"], "")
                .Execute()
                .Data;

            //using the companies in context from the first step
            foreach (var contextCompany in (List<Company>)context["companies"])
            {
                var resCompany = resCompanies.Find(e => e.companyName.Equals(contextCompany.companyName));
                contextCompany.employees.Should().BeEquivalentTo(resCompany.employees);
            }
        }

        [Then(@"The employee should be present as a shareholder")]
        public void ThenTheEmployeeShouldBePresentAsAShareholder()
        {
            // Request the database for the company with id
            var company = new RestCall<Company>(Method.GET, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", (string)context["company1Id"])
                .Execute()
                .Data;

            company.employees
                .Exists(employee => employee.name.Contains(((Employee)context["employee"]).name))
                .Should()
                .BeFalse();

            company.shareholders
                .Exists(shareholder => shareholder.name.Contains(((Employee)context["employee"]).name))
                .Should()
                .BeTrue();
        }

        [Then(@"The shareholder should be present as an employee")]
        public void ThenTheShareholderShouldBePresentAsAnEmployee()
        {
            // Request the database for the company with id
            var company = new RestCall<Company>(Method.GET, (string)context["baseUrl"], "/{id}")
                .AddUrlParameter("id", (string)context["company1Id"])
                .Execute()
                .Data;

            company.shareholders
                .Exists(shareholder => shareholder.name.Contains(context.Get<Shareholder>("shareholder").name))
                .Should()
                .BeFalse();

            company.employees
                .Exists(shareholder => shareholder.name.Contains(context.Get<Shareholder>("shareholder").name))
                .Should()
                .BeTrue();
        }


        [Then(@"The employee is present in the employee list")]
        public void ThenTheEmployeeIsPresentInTheEmployeeList()
        {
            context.Get<Company>("resCompany").employees
                .Exists(employee => employee.name.Contains(context.Get<Employee>("employee").name))
                .Should()
                .BeTrue();
        }

        [Then(@"The employee is present in the shareholder list")]
        public void ThenTheEmployeeIsPresentInTheShareholderList()
        {
            context.Get<Company>("resCompany").shareholders
                .Exists(shareholder => shareholder.name.Contains(context.Get<Shareholder>("shareholder").name))
                .Should()
                .BeTrue();
        }

        [Then(@"All companies in database networth will be greater than ""(.*)""")]
        public void ThenAllCompaniesInDatabaseNetworthWillBeGreaterThan(int minNetWorth)
        {
            context["companies"] = new RestCall<List<Company>>(Method.GET, (string)context["baseUrl"], "")
                .Execute()
                .Data;

            //((List<Company>)context["companies"])
            //    .All(company => company.netWorth > minNetWorth).Should().BeTrue();       
            context.Get<List<Company>>("companies")
                .All(company => company.netWorth > minNetWorth)
                .Should()
                .BeTrue();
        }
        #endregion
    }
}
