namespace StudentTest.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int orderUserID {  get; set; }

        public int orderProductID {  get; set; }
        public int OrderQTY { get; set; }
        public double orderTotal { get; set; }
    }
}
