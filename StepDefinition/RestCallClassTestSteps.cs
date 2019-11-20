using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;
using TTCBDD.Public_Var;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class RestCallClassTestSteps
    {
        #region Given
        [Given(@"User accesses employees API at ""(.*)""")]
        public void GivenUserAccessesEmployeesAPIAt(string url)
        {
            PublicVar.BaseUrl = url;
        }
        [Given(@"User creates a new employee")]
        public void GivenUserCreatesANewEmployee()
        {
            var rand = new Random();
            PublicVar.employee = new Employee()
            {
                name = BasicHelperMethods.RandomString(5, 12),
                salary = rand.Next(500, 500000).ToString(),
                age = rand.Next(20, 100).ToString()
            };
        }

        #endregion
        #region When

        [When(@"User accesses employee ""(.*)""")]
        public void WhenUserAccessesEmployee(string id)
        {
            PublicVar.employee = new RestCall<Employee>(Method.GET, PublicVar.BaseUrl, "/employee/{id}")
                .AddUrlParameter("id", id)
                .Execute()
                .Data;
        }

        [When(@"User creates new employee with name: ""(.*)"", age: ""(.*)"", and salary ""(.*)""")]
        public void WhenUserCreatesNewEmployeeWithNameAgeAndSalary(string name, string age, string salary)
        {
            PublicVar.employee = new Employee(name, salary, age);
            new RestCall<Employee>(Method.POST, PublicVar.BaseUrl, "/create")
                .AddPayload(PublicVar.employee)
                .Execute(res => PublicVar.employee.id = res.Data.id);
        }
        [When(@"User updates employee ""(.*)"" with new salary ""(.*)""")]
        public void WhenUserUpdatesEmployeeWithNewSalary(string id, string salary)
        {
            PublicVar.employee.salary = salary;
            PublicVar.employee = new RestCall<Employee>(Method.PUT, PublicVar.BaseUrl, "update/{id}")
                .AddUrlParameter("id", id)
                .AddPayload(PublicVar.employee)
                .Execute()
                .Data;
        }
        [When(@"User retrieves (.*) employees")]
        public void WhenUserRetrievesEmployees(int numEmployees)
        {
            PublicVar.employees = new RestCall<List<Employee>>(Method.GET, PublicVar.BaseUrl, "/employees")
                .Execute()
                .Data
                .Take(numEmployees);
        }
        [When(@"User raises all their salaries by (.*)%")]
        public void WhenUserRaisesAllTheirSalariesBy(int raise)
        {
            PublicVar.employees = PublicVar.employees.Select(e =>
            {
                var salary = int.Parse(e.salary);
                var newSalary = (int)(salary * (1 + raise / 100));
                e.salary = newSalary.ToString();
                Console.WriteLine($"Old salary: {salary} New salary: {e.salary}");
                return e;
            });

            PublicVar.employees
                .ForEach(employee =>
            {
                _ = new RestCall<Employee>(Method.PUT, PublicVar.BaseUrl, "/update/{id}")
                    .AddUrlParameter("id", employee.id)
                    .AddPayload(employee)
                    .Execute();
            });
        }
        [When(@"User adds the employee to the database")]
        public void WhenUserAddsTheEmployeeToTheDatabase()
        {
            new RestCall<Employee>(Method.POST, PublicVar.BaseUrl, "/create")
                .AddPayload(PublicVar.employee)
                .Execute(res => PublicVar.employee.id = res.Data.id);
        }

        #endregion
        #region Then

        [Then(@"The employee record is displayed")]
        public void ThenTheEmployeeRecordIsDisplayed()
        {
            Console.WriteLine(PublicVar.employee.ToString());
            AssertHelper.IsTrue(PublicVar.employee != null);
        }


        [Then(@"The employee is present in the employees list")]
        public void ThenTheEmployeeIsPresentInTheEmployeesList()
        {
            var employee = new RestCall<List<Employee>>(Method.GET, PublicVar.BaseUrl, "/employees")
                .AddHeader("Accept", "application/json")
                .Execute()
                .Data
                .Where(e => e.id == PublicVar.employee.id)
                .First();
            Console.WriteLine($"Created employee: {PublicVar.employee.ToString()}");
            Console.WriteLine($"Retrieved employee: {employee.ToString()}");
            AssertHelper.IsTrue(PublicVar.employee.Equals(employee));
        }
        [Then(@"The new employee is successfully deleted from the database")]
        public void ThenTheNewEmployeeIsSuccessfullyDeletedFromTheDatabase()
        {
            new RestCall<object>(Method.DELETE, PublicVar.BaseUrl, "/delete/{id}")
                .AddUrlParameter("id", PublicVar.employee.id)
                .Execute()
                .Content.Should().Contain("success");
        }
        [Then(@"The new salary ""(.*)"" is reflected in the database")]
        public void ThenTheNewSalaryIsReflectedInTheDatabase(string salary)
        {
            AssertHelper.Equals(PublicVar.employee.salary, salary);
        }
        [Then(@"This change is reflected in the database")]
        public void ThenThisChangeIsReflectedInTheDatabase()
        {
            var success = PublicVar.employees.All(e =>
                {
                    return new RestCall<Employee>(Method.GET, PublicVar.BaseUrl, "/employee/{id}")
                        .AddUrlParameter("id", e.id)
                        .Check(res => res.Data.salary.Equals(e.salary));
                });
            AssertHelper.IsTrue(success);
        }

        #endregion
    }
}