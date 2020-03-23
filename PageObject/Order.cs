using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.PageObject
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ShipCountry { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
    }
}
