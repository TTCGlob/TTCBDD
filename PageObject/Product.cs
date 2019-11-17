using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.PageObject
{
    public class Product
    {
        //For use with local server at http://192.168.2.73:3000/products
        public int id { get; set; }
        public string product_name { get; set; }
        public int stock_level { get; set; }
        public DateTime last_restocked { get; set; }
    }
}
