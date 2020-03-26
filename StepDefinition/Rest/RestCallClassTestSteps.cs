using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using log4net;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.RestObjects;
using TTCBDD.Helpers.Generic;
using TTCBDD.Helpers.Rest;

namespace TTCBDD.StepDefinition.Rest
{
    [Binding]
    public class RestCallClassTestSteps
    {
        ScenarioContext context;
        private ILog Logger = Log4NetHelper.GetXmlLogger(typeof(RestCallClassTestSteps));
        public RestCallClassTestSteps(ScenarioContext _context)
        {
            context = _context;
        }

        #region Given
        [Given(@"User accesses employees API at ""(.*)""")]
        public void GivenUserAccessesEmployeesAPIAt(string url)
        {
            context.Add("url", url);
        }
        [Given(@"User creates a new employee")]
        public void GivenUserCreatesANewEmployee()
        {
            var employee = Employee.Random();
            context.Add("employee", employee);
            Logger.Debug($"Created employee {employee.name} [id: {employee.id}]");
        }

        #endregion
        #region When

        [When(@"User accesses employee ""(.*)""")]
        public void WhenUserAccessesEmployee(string id)
        {
            var employee = new RestCall<Employee>(Method.GET, context.Get<string>("url"), "/employee/{id}")
                .AddUrlParameter("id", id)
                .Data();
            context.Add("employee", employee);
            Logger.Debug($"Accessed employee {employee.name} [id: {employee.id}]");
        }

        [When(@"User creates new employee with name: ""(.*)"", age: ""(.*)"", and salary ""(.*)""")]
        public void WhenUserCreatesNewEmployeeWithNameAgeAndSalary(string name, string age, string salary)
        {
            var employee = new Employee()
            {
                name = name,
                age = age,
                salary = salary,
            };
            employee.id = new RestCall<Employee>(Method.POST, context.Get<string>("url"), "/create")
                .AddPayload(employee)
                .Data().id;
            context.Add("employee", employee);
            Logger.Debug($"Created employee {employee.name} [id: {employee.id}]");
        }
        [When(@"User updates employee ""(.*)"" with new salary ""(.*)""")]
        public void WhenUserUpdatesEmployeeWithNewSalary(string id, string salary)
        {
            var employee = context.Get<Employee>("employee");
            employee.salary = salary;
            _ = new RestCall<Employee>(Method.PUT, context.Get<string>("url"), "update/{id}")
                .AddUrlParameter("id", id)
                .AddPayload(employee)
                .Data();
            Logger.Debug($"Updated employee {employee.name} [id: {employee.id}] with new salary {salary}");
        }
        [When(@"User retrieves (.*) employees")]
        public void WhenUserRetrievesEmployees(int numEmployees)
        {
            var employees = new RestCall<List<Employee>>(Method.GET, context.Get<string>("url"), "/employees")
                .Execute()
                .Data
                .Take(numEmployees);
            context.Add("employees", employees);
            Logger.Debug($"Retrieved {numEmployees} employees");
        }
        [When(@"User raises all their salaries by (.*)%")]
        public void WhenUserRaisesAllTheirSalariesBy(int raiseBy)
        {
            var employees = context.Get<IEnumerable<Employee>>("employees");
            var raiseBy15Percent = BasicHelperMethods.RaiseBy(15);
            employees = employees.Select(e => new Employee()
            {
                id = e.id,
                name = e.name,
                salary = raiseBy15Percent(e.salary)
            });
            employees.ForEach(employee =>
            {
                new RestCall<Employee>(Method.PUT, context.Get<string>("url"), "/update/{id}")
                    .AddUrlParameter("id", employee.id)
                    .AddPayload(employee)
                    .Execute();
            });
        }
        [When(@"User adds the employee to the database")]
        public void WhenUserAddsTheEmployeeToTheDatabase()
        {
            var employee = context.Get<Employee>("employee");
            employee.id = new RestCall<Employee>(Method.POST, context.Get<string>("url"), "/create")
                .AddPayload(employee)
                .Data()
                .id;
        }

        #endregion
        #region Then

        [Then(@"The employee record is displayed")]
        public void ThenTheEmployeeRecordIsDisplayed()
        {
            var employee = context.Get<Employee>("employee");
            employee.Should().NotBeNull();
        }


        [Then(@"The employee is present in the employees list")]
        public void ThenTheEmployeeIsPresentInTheEmployeesList()
        {
            var storedEmployee = context.Get<Employee>("employee");
            var retrievedEmployee = new RestCall<List<Employee>>(Method.GET, context.Get<string>("url"), "/employees")
                .AddHeader("Accept", "application/json")
                .Data()
                .Where(e => e.id == context.Get<Employee>("employee").id)
                .First();
            Console.WriteLine($"Created employee: {storedEmployee.ToString()}");
            Console.WriteLine($"Retrieved employee: {retrievedEmployee.ToString()}");
            retrievedEmployee.Should().BeEquivalentTo(storedEmployee);
        }
        [Then(@"The new employee is successfully deleted from the database")]
        public void ThenTheNewEmployeeIsSuccessfullyDeletedFromTheDatabase()
        {
            new RestCall<object>(Method.DELETE, context.Get<string>("url"), "/delete/{id}")
                .AddUrlParameter("id", context.Get<Employee>("employee").id)
                .Execute()
                .Content.Should().Contain("success");
        }
        [Then(@"The new salary ""(.*)"" is reflected in the database")]
        public void ThenTheNewSalaryIsReflectedInTheDatabase(string salary)
        {
            context.Get<Employee>("employee").salary.Should().Be(salary);
        }
        [Then(@"This change is reflected in the database")]
        public void ThenThisChangeIsReflectedInTheDatabase()
        {
            var employees = context.Get<IEnumerable<Employee>>("employees");
            var raiseBy15Percent = BasicHelperMethods.RaiseBy(15);
            var raiseReflected = employees.Select(employee =>
            {
                var retrievedEmployee = new RestCall<Employee>(Method.GET, context.Get<string>("url"), "/employee/{id}")
                    .AddUrlParameter("id", employee.id)
                    .Data();
                return (employee.id, raiseBy15Percent(employee.salary) == retrievedEmployee.salary);
            });
            raiseReflected.ForEach(e => e.Item2.Should().BeTrue($"employee ID: {e.Item1} salary has not been raised")); ;
        }

        #endregion
    }
}