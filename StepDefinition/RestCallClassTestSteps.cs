using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.APIObjects;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;
using TTCBDD.Public_Var;
using TTCBDD.APIObjects;

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

        #endregion
        #region When

        [When(@"User accesses employee ""(.*)""")]
        public void WhenUserAccessesEmployee(string id)
        {
            var get = new RestCall<Employee>(Method.GET, PublicVar.BaseUrl, "/employee/{id}")
                .AddUrlParameter("id", id)
                .Execute();
            var employee = get.Data;
            PublicVar.employee = employee;
        }

        [When(@"User creates new employee with name: ""(.*)"", age: ""(.*)"", and salary ""(.*)""")]
        public void WhenUserCreatesNewEmployeeWithNameAgeAndSalary(string name, string age, string salary)
        {
            PublicVar.employee = new Employee(name, salary, age);
            var employee = new RestCall<Employee>(Method.POST, PublicVar.BaseUrl, "/create")
                .AddHeader("Accept", "application/json")
                .AddPayload(PublicVar.employee)
                .Execute(res => PublicVar.employee.id = res.Data.id)
                .Data;
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
            var deleted = new RestCall<object>(Method.DELETE, PublicVar.BaseUrl, "/delete/{id}")
                .AddUrlParameter("id", PublicVar.employee.id)
                .Execute()
                .ContentContains("success");
            AssertHelper.IsTrue(deleted);
        }
        [Then(@"The new salary ""(.*)"" is reflected in the database")]
        public void ThenTheNewSalaryIsReflectedInTheDatabase(string salary)
        {
            AssertHelper.Equals(PublicVar.employee.salary, salary);
        }

        #endregion
    }
}
