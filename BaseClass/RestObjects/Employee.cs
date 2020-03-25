using System;
using Newtonsoft.Json;

namespace TTCBDD.BaseClass.RestObjects
{
    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
        public string age { get; set; }
        public string profile_image { get; set; } = "";
        public DateTime creationDate { get; set; }

        public Employee()
        {
            creationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Employee(string employee_name, string employee_salary, string employee_age, string id = "", string profile_image = "")
        {
            this.id = id;
            name = employee_name;
            salary = employee_salary;
            age = employee_age;
            creationDate = DateTime.UtcNow;
            this.profile_image = profile_image;
        }

        public override bool Equals(object _other)
        {
            return _other is Employee other
                && id.Equals(other.id)
                && name.Equals(other.name)
                && salary.Equals(other.salary)
                && age.Equals(other.age);
            //&& this.creationDate.Date.CompareTo(other.creationDate.Date) == 0;
        }
        public override string ToString()
        {
            return $"ID: {id} Name: {name} Age: {age} Salary: {salary} Creation Date: {creationDate}";
        }

        public static Employee Random()
        {
            var rand = new Random();
            var randomPerson = RandomUser.Random();
            var employee = new Employee()
            {
                name = randomPerson.fullName,
                salary = rand.Next(32000, 250000).ToString(),
                age = rand.Next(18, 80).ToString()
            };
            return employee;
        }
    }
}