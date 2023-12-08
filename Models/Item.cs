namespace WebApplication2.Models
{
    public class Item
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Price { get; set; }
        public int Unit { get; set; }
    }

    public class Cart
    {
        public int Cart_ID { get; set; }
        public string Product_ID { get; set; }
        public string Product_Name { get; set;}
        public int Price { get; set; }
        public int Unit { get; set; }

    }
    public class total
    {
        public double Total { get; set; }
    }
}
