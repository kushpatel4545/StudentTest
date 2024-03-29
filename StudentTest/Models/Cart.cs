namespace StudentTest.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int cartUserID { get; set; }
        public int cartProdutID { get; set; }
        public int productQty {  get; set; }
        public double cartTotal { get; set; }
    }
}
