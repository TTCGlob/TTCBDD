using System;
using System.Collections.Generic;
using System.Linq;

namespace TTCBDD.BaseClass.RestObjects
{
    public class Company
    {
        private static readonly string[] companyTypes = { "Inc.", "Co.", "Ltd", "GmBH", "Group" };
        private static string companyType
        {
            get
            {
                var index = new Random().Next(0, companyTypes.Length);
                return companyTypes[index];
            }
        }
        public int id { get; set; }
        public string companyName { get; set; }
        public decimal netWorth { get; set; }
        public Address address { get; set; }
        public List<Employee> employees { get; set; } = new List<Employee>();
        public List<Shareholder> shareholders { get; set; } = new List<Shareholder>();
        public DateTime creationDate { get; set; }

        public Company()
        {
            creationDate = DateTime.UtcNow;
        }
        public Company(string company_name = "", int company_id = 0)
        {
            id = company_id;
            companyName = company_name;
            creationDate = DateTime.UtcNow;
        }
        public override bool Equals(object _other)
        {
            return _other is Company other
                && id.Equals(other.id)
                && companyName.Equals(other.companyName)
                && netWorth.Equals(other.netWorth)
                && address.Equals(other.address)
                && employees.Equals(other.employees)
                && shareholders.Equals(other.shareholders)
                && creationDate.Equals(other.creationDate);
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            return $"ID: {id} Name: {companyName} Net Worth: {netWorth} " +
                $"Address: {address} Num Employees: {employees.Count}  Num Shareholders: {shareholders.Count} Creation Date: {creationDate}";
        }

        public static Company Random()
        {
            var rand = new Random();
            var numEmployees = rand.Next(1, 100);
            var people = RandomUser.RandomUsers(numEmployees + 2);
            var person = people.First();
            people = people.Skip(1);
            return new Company()
            {
                companyName = person.fullName + companyType,

                netWorth = rand.Next(1000, 1000000),
                address = new Address()
                {
                    number = person.location.street.number,
                    street = person.location.street.name,
                    city = person.location.city
                },
                shareholders = Shareholder.Randoms(),
                employees = people.Select(p => new Employee() { name = p.fullName, age = p.dob.age.ToString() }).ToList()
            };
        }
    }
}
