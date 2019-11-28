using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.PageObject
{
    public class Company
    {
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
            if (_other == null)
                return false;

            if (!this.GetType().Equals(_other.GetType()))
                return false;
            var other = (Company)_other;

            return this.id.Equals(other.id)
                && this.companyName.Equals(other.companyName)
                && this.netWorth.Equals(other.netWorth)
                && this.address.Equals(other.address)
                && this.employees.Equals(other.employees)
                && this.shareholders.Equals(other.shareholders)
                && this.creationDate.Equals(other.creationDate);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"ID: {this.id} Name: {this.companyName} Net Worth: {this.netWorth} " +
                $"Address: {this.address} Num Employees: {this.employees.Count}  Num Shareholders: {this.shareholders.Count} Creation Date: {creationDate}";
        }
    }
}
