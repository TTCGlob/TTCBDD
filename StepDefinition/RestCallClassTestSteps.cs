using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;
using TTCBDD.Public_Var;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public class RestCallClassTestSteps
    {
        [Given(@"User accesses employees API at ""(.*)""")]
        public void GivenUserAccessesEmployeesAPIAt(string url)
        {
            PublicVar.BaseUrl = url;
        }

        [When(@"User accesses employee ""(.*)""")]
        public void WhenUserAccessesEmployee(string id)
        {
            var get = new RestCall<Employee>(Method.GET, PublicVar.BaseUrl, "/employee/{id}")
                .AddUrlParameter("id", id)
                .AddHeader("Accept", "application/json")
                .Execute();
            var employee = get.Data;
            PublicVar.employee = employee;
        }

        [When(@"User creates new employee with name: ""(.*)"", age: ""(.*)"", and salary ""(.*)""")]
        public void WhenUserCreatesNewEmployeeWithNameAgeAndSalary(string name, string age, string salary)
        {
            PublicVar.employee = new Employee(name, salary, age);
            var employee = new RestCall<Employee>(Method.POST, PublicVar.BaseUrl, "/create")
                .AddHeader("Content-Type", "application/json")
                .AddHeader("Accept", "application/json")
                .AddPayload(PublicVar.employee)
                .Execute()
                .Data;
            PublicVar.employee = employee;
        }
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
            var delete = new RestCall<object>(Method.DELETE, PublicVar.BaseUrl, "/delete/{id}")
                .AddUrlParameter("id", PublicVar.employee.id)
                .AddSuccessCondition((res) => res.Content.Contains("success"));
            delete.Execute();
            AssertHelper.IsTrue(delete.Success);
        }

    }
}
