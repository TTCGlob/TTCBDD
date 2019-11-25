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

        public bool Equals(Product other)
        {
            return id == other.id
                   && product_name.Equals(other.product_name)
                   && stock_level == other.stock_level
                   && last_restocked.CompareTo(other.last_restocked) == 0;
        }
    }
}
