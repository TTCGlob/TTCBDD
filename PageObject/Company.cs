using System;
using System.Collections.Generic;
using System.Linq;

namespace TTCBDD.PageObject
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
            this.creationDate = DateTime.UtcNow;
        }
        public Company(string company_name = "", int company_id = 0)
        {
            this.id = company_id;
            this.companyName = company_name;
            this.creationDate = DateTime.UtcNow;
        }
        public override bool Equals(Object _other)
        {
            return _other is Company other
                && this.id.Equals(other.id)
                && this.companyName.Equals(other.companyName)
                && this.netWorth.Equals(other.netWorth)
                && this.address.Equals(other.address)
                && this.employees.Equals(other.employees)
                && this.shareholders.Equals(other.shareholders)
                && this.creationDate.Equals(other.creationDate);
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
