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
        public List<Employee> employees { get; set; }
        public List<Shareholder> shareholders { get; set; }
    }
}
