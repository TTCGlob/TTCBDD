using System;

namespace TTCBDD.PageObject
{
    public class Product
    {
        //For use with local server at http://192.168.2.73:3000/products
        public int id { get; set; }
        public string product_name { get; set; }
        public int stock_level { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime updated_date { get; set; }
        public Decimal unit_price { get; set; }
        public string sku { get; set; }

        public Product()
        {
            this.creation_date = DateTime.UtcNow;
            this.updated_date = DateTime.UtcNow;
        }
        public Product(string product_name = "", int company_id = 0, int stock_level = 0, int unit_price = 0, string sku = "")
        {
            this.id = company_id;
            this.product_name = product_name;
            this.stock_level = stock_level;
            this.creation_date = DateTime.UtcNow;
            this.updated_date = DateTime.UtcNow;
            this.unit_price = unit_price;
            this.sku = sku;
        }

        public bool Equals(Product other)
        {
            return this.id.Equals(other.id)
                && this.product_name.Equals(other.product_name)
                && this.stock_level.Equals(other.stock_level)
                && this.unit_price.Equals(other.unit_price)
                && this.sku.Equals(other.sku)
                && this.creation_date.Date.CompareTo(other.creation_date.Date) == 0
                && this.updated_date.Date.CompareTo(other.updated_date.Date) == 0;
        }

        public override string ToString()
        {
            return $"ID: {this.id} Name: {this.product_name} Stock Level: {this.stock_level} " +
                $"Unit Price: {this.unit_price} SKU: {this.sku}  Last Updated: {this.updated_date} Creation Date: {this.creation_date}";
        }
    }
}
