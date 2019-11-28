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
        public DateTime creationDate { get; set; }

        public Employee() {
            this.creationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Employee(string employee_name, string employee_salary, string employee_age, string id = "")
        {
            this.id = id;
            this.name = employee_name;
            this.salary = employee_salary;
            this.age = employee_age;
            this.creationDate = DateTime.UtcNow;
        }

        public bool Equals(Employee other)
        {
            return this.id.Equals(other.id)
                && this.name.Equals(other.name)
                && this.salary.Equals(other.salary)
                && this.age.Equals(other.age)
                && this.creationDate.Date.CompareTo(other.creationDate.Date) == 0;            
        }
        public override string ToString()
        {
            return $"ID: {id} Name: {name} Age: {age} Salary: {salary} Creation Date: {creationDate}";
        }

        public static Employee Random()
        {
            var rand = new Random();
            var employee = new Employee()
            {
                name = ComponentHelper.BasicHelperMethods.RandomString(8),
                salary = rand.Next(32000, 250000).ToString(),
                age = rand.Next(18, 80).ToString()
            };
            return employee;
        }
    }
}