using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TTCBDD.PageObject
{
    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
        public string age { get; set; }
        public string profile_image { get; set; }
        public Employee() { }
        [JsonConstructor]
        public Employee(string employee_name, string employee_salary, string employee_age, string id = "")
        {
            this.id = id;
            name = employee_name;
            salary = employee_salary;
            age = employee_age;
        }
        public bool Equals(Employee other)
        {
            return this.id.Equals(other.id)
                && this.name.Equals(other.name)
                && this.salary.Equals(other.salary)
                && this.age.Equals(other.age);
        }
        public override string ToString()
        {
            return $"ID: {id} Name: {name} Age: {age} Salary: {salary}";
        }
    }
}