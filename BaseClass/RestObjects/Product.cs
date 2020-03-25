using System;

namespace TTCBDD.BaseClass.RestObjects
{
    public class Product
    {
        //For use with local server at http://192.168.2.73:3000/products
        public int id { get; set; }
        public string product_name { get; set; }
        public int stock_level { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime updated_date { get; set; }
        public decimal unit_price { get; set; }
        public string sku { get; set; }

        public Product()
        {
            creation_date = DateTime.UtcNow;
            updated_date = DateTime.UtcNow;
        }
        public Product(string product_name = "", int company_id = 0, int stock_level = 0, int unit_price = 0, string sku = "")
        {
            id = company_id;
            this.product_name = product_name;
            this.stock_level = stock_level;
            creation_date = DateTime.UtcNow;
            updated_date = DateTime.UtcNow;
            this.unit_price = unit_price;
            this.sku = sku;
        }

        public bool Equals(Product other)
        {
            return id.Equals(other.id)
                && product_name.Equals(other.product_name)
                && stock_level.Equals(other.stock_level)
                && unit_price.Equals(other.unit_price)
                && sku.Equals(other.sku)
                && creation_date.Date.CompareTo(other.creation_date.Date) == 0
                && updated_date.Date.CompareTo(other.updated_date.Date) == 0;
        }

        public override string ToString()
        {
            return $"ID: {id} Name: {product_name} Stock Level: {stock_level} " +
                $"Unit Price: {unit_price} SKU: {sku}  Last Updated: {updated_date} Creation Date: {creation_date}";
        }
    }
}
