using FluentAssertions;
using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // Get the last company created, sorted by creation date
            IEnumerable<Company> items1 = new RestCall<List<Company>>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource")).Data();
            Company latest = items1.Aggregate((i1, i2) => i1.id > i2.id ? i1 : i2);

            context["company"] = latest;
            Logger.Debug($"Managing Company: {latest.companyName} [id: {latest.id}]");
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
            var storedCompany = context.Get<Company>("company");
            storedCompany.employees.Count.Should().BeGreaterThan(0, "The target company should have at least one employee to delete");
            var targetEmployee = storedCompany.employees[0];
            
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            int startCount = retrievedCompany.employees.Count;
            startCount.Should().BeGreaterThan(0, "The target company should have at least one employee to delete");

            // Delete the first employee added (he died) from the db company
            bool itemFound = false;
            foreach(Employee employee in retrievedCompany.employees)
            {
                if (employee.Equals(targetEmployee))
                {
                    storedCompany.employees.RemoveAll(r => r.id == targetEmployee.id);
                    retrievedCompany.employees.RemoveAll(r => r.id == targetEmployee.id);
                    itemFound = true;
                    break;
                }
            }
            itemFound.Should().BeTrue("expected to find a matching company");

            // Update the company in the DB
            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", retrievedCompany.id.ToString())
                .AddPayload(retrievedCompany)
                .Execute();

            int endCount = retrievedCompany.employees.Count;

            context["company"] = storedCompany;
            context["deletedEmployee"] = targetEmployee;
            Logger.Debug($"Removed employee: {targetEmployee.name} [{targetEmployee.id}] from company {storedCompany.companyName} [{storedCompany.id}]");
            Logger.Debug($"Company {storedCompany.companyName} [{storedCompany.id}] had {startCount} employees, now has {endCount}");
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

            Logger.Debug($"Check new employee for {retrievedCompany.companyName} [{retrievedCompany.id}]. Expected '{storedEmployee.name} [{storedEmployee.id}], "
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

        [When(@"I add a shareholder to the company database")]
        public void WhenIAddAShareholderToTheCompanyDatabase()
        {
            var company = context.Get<Company>("company");

            var shareholder = new Shareholder()
            {
                id = "S" + new Random().Next(99999).ToString(),
                name = $"{StringUtils.GetRandomNameStringTitleCase()} {StringUtils.GetRandomNameStringTitleCase()}",
                stake = new Random().Next(1, 100)
            };

            company.shareholders.Add(shareholder);
            context["company"] = company;

            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", company.id.ToString())
                .AddPayload(company)
                .Execute();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Logger.Debug($"Added shareholder: {shareholder.name} [{shareholder.id}], stake: {shareholder.stake}, Creation Date: {shareholder.creationDate}");
        }
        
        [Then(@"the company has the new shareholder")]
        public void ThenTheCompanyHasTheNewShareholder()
        {
            var storedCompany = context.Get<Company>("company");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            retrievedCompany.shareholders.Count.Should().Be(storedCompany.shareholders.Count);
            var storedShareholder = storedCompany.shareholders[storedCompany.shareholders.Count - 1];

            Shareholder retrievedShareholder = null;
            foreach (Shareholder shareholder in retrievedCompany.shareholders)
            {
                if (shareholder.id == storedShareholder.id)
                {
                    retrievedShareholder = shareholder;
                    break;
                }
            }

            retrievedShareholder.Should().NotBeNull();
            retrievedShareholder.Should().BeEquivalentTo(storedShareholder);

            Logger.Debug($"Check new shareholder for {retrievedCompany.companyName} [{retrievedCompany.id}]. Expected '{storedShareholder.name} [{storedShareholder.id}], "
                + $"Found '{retrievedShareholder.name} [{retrievedShareholder.id}]"
                + $" ({retrievedCompany.shareholders.Count} shareholder(s) in total)");
        }

        [When(@"I delete a shareholder from the company database")]
        public void WhenIDeleteAShareholderFromTheCompanyDatabase()
        {
            var storedCompany = context.Get<Company>("company");
            storedCompany.shareholders.Count.Should().BeGreaterThan(0, "The target company should have at least one shareholder to delete");
            var targetShareholder = storedCompany.shareholders[0];

            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();
            int startCount = retrievedCompany.shareholders.Count;
            startCount.Should().BeGreaterThan(0, "The target company should have at least one shareholder to delete");

            // Delete the first shareholder added (he sold his shares to buy a yacht) from the db company
            bool itemFound = false;
            foreach (Shareholder shareholder in retrievedCompany.shareholders)
            {
                if (shareholder.Equals(targetShareholder))
                {
                    storedCompany.shareholders.RemoveAll(r => r.id == targetShareholder.id);
                    retrievedCompany.shareholders.RemoveAll(r => r.id == targetShareholder.id);
                    itemFound = true;
                    break;
                }
            }
            itemFound.Should().BeTrue("expected to find a matching company");

            // Update the company in the DB
            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", retrievedCompany.id.ToString())
                .AddPayload(retrievedCompany)
                .Execute();

            int endCount = retrievedCompany.shareholders.Count;

            context["company"] = storedCompany;
            context["deletedShareholder"] = targetShareholder;
            Logger.Debug($"Removed shareholder: {targetShareholder.name} [{targetShareholder.id}] from company {storedCompany.companyName} [{storedCompany.id}]");
            Logger.Debug($"Company {storedCompany.companyName} [{storedCompany.id}] had {startCount} shareholder, now has {endCount}");
        }

        [Then(@"the company no longer has the shareholder")]
        public void ThenTheCompanyNoLongerHasTheShareholder()
        {
            var storedCompany = context.Get<Company>("company");
            var deletedShareholder = context.Get<Shareholder>("deletedShareholder");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();

            retrievedCompany.shareholders.Should().NotContain(deletedShareholder,
                $"The company {retrievedCompany.companyName} [{retrievedCompany.id}] should not have the shareholder {deletedShareholder.name} [{deletedShareholder.id}]");
            Logger.Info($"The company {retrievedCompany.companyName} [{retrievedCompany.id}] no longer has the shareholder {deletedShareholder.name} [{deletedShareholder.id}]");
        }

        [When(@"I rename the company in the database")]
        public void WhenIRenameTheCompanyInTheDatabase()
        {
            // Update the company details
            var storedCompany = context.Get<Company>("company");
            string oldName = storedCompany.companyName;
            storedCompany.companyName = $"{StringUtils.GetRandomNameStringTitleCase()} {StringUtils.GetRandomBusinessType()}";
            context["company"] = storedCompany;
            context["companyOldName"] = oldName;

            // Update the company in the DB
            var response = new RestCall<Company>(Method.PUT, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .AddPayload(storedCompany)
                .Execute();

            Logger.Info($"Changed old name '{oldName}' to '{storedCompany.companyName}' for company '{storedCompany.id}'");
        }

        [Then(@"the company has the new name")]
        public void ThenTheCompanyHasTheNewName()
        {
            var storedCompany = context.Get<Company>("company");
            string oldName = context.Get<string>("companyOldName");
            var retrievedCompany = new RestCall<Company>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource") + "/{id}")
                .AddUrlParameter("id", storedCompany.id.ToString())
                .Data();

            retrievedCompany.companyName.Should().Be(storedCompany.companyName, "The new name '{retrievedCompany.companyName}' was expected to be '{storedCompany.companyName}'");
            Logger.Info($"The company {retrievedCompany.id} now has the new name '{retrievedCompany.companyName}'");
        }

        [Given(@"I have a company with at least one employee")]
        public void GivenIHaveACompanyWithAtLeastOneEmployee()
        {
            // Get the last company created, sorted by creation date
            IEnumerable<Company> items1 = new RestCall<List<Company>>(Method.GET, context.Get<string>("url"), context.Get<string>("companyResource")).Data();
            Company company = null;
            
            // Iterate through the company from most recent to least recent and find one with at least one employee
            foreach(Company companyItem in items1.OrderByDescending(companyItem => companyItem.creationDate).ToList())
            {
                if (companyItem.employees.Count > 0)
                {
                    company = companyItem;
                    break;
                }
            }
            company.Should().NotBeNull("No company was found to delete employees from");

            context["company"] = company;
            Logger.Debug($"Managing Company: {company.companyName} [id: {company.id}] ({company.employees.Count} employee(s)");
        }

    }
}
