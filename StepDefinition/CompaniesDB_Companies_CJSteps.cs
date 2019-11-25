using FluentAssertions;
using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;
using TTCBDD.UtilityClasses;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class CompaniesDB_Companies_CJSteps
    {

        private readonly ScenarioContext context;
        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(CompaniesDB_Companies_CJSteps));

        public CompaniesDB_Companies_CJSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
            context.Add("companyResource", "/companies");
            context.Add("employeeResource", "/employees");
        }

        [Given(@"User accesses API at url ""(.*)""")]
        public void GivenUserAccessesAPIAtUrl(string url)
        {
            context.Add("url", url);
        }
        
        [Given(@"I have a company I wish to create")]
        public void GivenIHaveACompanyIWishToCreate()
        {
            Company company = new Company()
            {
                id = new Random().Next(11111, 99999),
                companyName = $"{StringUtils.GetRandomNameStringTitleCase()} {StringUtils.GetRandomBusinessType()}"
            };
            context["company"] = company;
            Logger.Debug($"Created Company: {company.companyName} [id: {company.id}]");
        }

        [When(@"I submit this company to the company database")]
        public void WhenISubmitThisCompanyToTheCompanyDatabase()
        {
            var company = context.Get<Company>("company");
            var response = new RestCall<Company>(Method.POST, context.Get<string>("url"), context.Get<string>("companyResource"))
                .AddUrlParameter("id", company.id.ToString())
                .AddPayload(company)
                .Execute();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            Logger.Debug($"Added Company: {company.companyName} [id: {company.id}]");
        }

        [Given(@"I have a company I wish to manage")]
        public void GivenIHaveACompanyIWishToManage()
        {
            // Get the last company created
            var companies = new RestCall<List<Company>>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource")).Data();
            var company = companies[companies.Count - 1];
            context["company"] = company;
            Logger.Debug($"Managing Company: {company.companyName} [id: {company.id}]");
        }
                
        [When(@"I add an address to the company database")]
        public void WhenIAddAnAddressToTheCompanyDatabase()
        {
            var company = context.Get<Company>("company");
            company.address = new Address()
            {
                number = new Random().Next(1, 999),
                street = $"{StringUtils.GetRandomNameStringTitleCase()} {StringUtils.GetRandomNameStringTitleCase()}",
                city = StringUtils.GetRandomNameStringTitleCase()
            };

            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
               .AddUrlParameter("id", company.id.ToString())
               .AddPayload(company)
               .Execute();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Logger.Debug($"Updated Company Address: {company.address.number} {company.address.street}, {company.address.city}");
        }
        
        [When(@"I add an employee to the company database")]
        public void WhenIAddAnEmployeeToTheCompanyDatabase()
        {
            var company = context.Get<Company>("company");

            var employee = new Employee()
            {
                id = "E" + new Random().Next(99999).ToString(),
                name = $"{StringUtils.GetRandomNameStringTitleCase()} {StringUtils.GetRandomNameStringTitleCase()}",
                salary = new Random().Next(50000, 500000).ToString(),
                age = new Random().Next(18, 64).ToString(),
                profile_image = $"{StringUtils.GetRandomNameStringTitleCase()}{StringUtils.GetRandomNameStringTitleCase()}"
            };

            company.employees.Add(employee);
            context["company"] = company;

            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", company.id.ToString())
                .AddPayload(company)
                .Execute();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Logger.Debug($"Added employee: {employee.name} [{employee.id}], age: {employee.age}, salary: {employee.salary}");
        }
        
        [When(@"I delete an employee from the company database")]
        public void WhenIDeleteAnEmployeeFromTheCompanyDatabase()
        {
            var company = context.Get<Company>("company");
            var employee = company.employees[0];
            employee.Should().NotBeNull("The company should have an employee to delete, none found");

            // Delete the first employee added (he died) from the db company
            var response = new RestCall<Company>(Method.DELETE, context.Get<string>("url"), context.Get<string>("employeeResource") + "/{id}")
                .AddUrlParameter("id", employee.id.ToString())
                .Execute();

            // Delete the same employee from the context company
            company.employees.RemoveAll(r => r.id == employee.id);
            context["company"] = company;
            context["deletedEmployee"] = employee;
            Logger.Debug($"Removed employee: {employee.name} [{employee.id}] from company {company.companyName} [{company.id}]");
        }
        
        [When(@"I add a shareholder to the company database")]
        public void WhenIAddAShareholderToTheCompanyDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I delete a shareholder from the company database")]
        public void WhenIDeleteAShareholderFromTheCompanyDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I rename the company in the database")]
        public void WhenIRenameTheCompanyInTheDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the company is now displayed in the database")]
        public void ThenTheCompanyIsNowDisplayedInTheDatabase()
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            retrievedCompany.companyName.Should().Be(storedCompany.companyName);
            Logger.Debug($"Company found: {retrievedCompany.companyName} [id: {retrievedCompany.id}]");
        }
        
        [Then(@"the company has the new address")]
        public void ThenTheCompanyHasTheNewAddress()
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();

            retrievedCompany.address.ToString().Should().Be(storedCompany.address.ToString());
            Logger.Debug($"Check updated address. Expected " +
                $"'{storedCompany.address.number} {storedCompany.address.street}, " +
                $" {storedCompany.address.city}', got '{retrievedCompany.address.number} " +
                $"{retrievedCompany.address.street}, {retrievedCompany.address.city}'");
        }
        
        [Then(@"the company has the new employee")]
        public void ThenTheCompanyHasTheNewEmployee()
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            retrievedCompany.employees.Count.Should().Be(storedCompany.employees.Count);
            var storedEmployee = storedCompany.employees[storedCompany.employees.Count - 1];
                        
            Employee retrievedEmployee = null;
            foreach (Employee employee in retrievedCompany.employees)
            {
                if (employee.id == storedEmployee.id)
                {
                    retrievedEmployee = employee;
                    break;
                }
            }

            retrievedEmployee.Should().NotBeNull();
            retrievedEmployee.Should().BeEquivalentTo(storedEmployee);

            Logger.Debug($"Check new employee. Expected '{storedEmployee.name} [{storedEmployee.id}], "
                + $"Found '{retrievedEmployee.name} [{retrievedEmployee.id}]");
        }
        
        [Then(@"the company no longer has the employee")]
        public void ThenTheCompanyNoLongerHasTheEmployee()
        {
            var storedCompany = context.Get<Company>("company");
            var deletedEmployee = context.Get<Employee>("deletedEmployee");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();

            retrievedCompany.employees.Should().NotContain(deletedEmployee, 
                $"The company {retrievedCompany.companyName} [{retrievedCompany.id}] should not have the employee {deletedEmployee.name} [{deletedEmployee.id}]");
           Logger.Info($"The company {retrievedCompany.companyName} [{retrievedCompany.id}] no longer has the employee {deletedEmployee.name} [{deletedEmployee.id}]");
        }
        
        [Then(@"the company has the new shareholder")]
        public void ThenTheCompanyHasTheNewShareholder()
        {

        }
        
        [Then(@"the company no longer has the shareholder")]
        public void ThenTheCompanyNoLongerHasTheShareholder()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the company has the new name")]
        public void ThenTheCompanyHasTheNewName()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
