namespace StudentTest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }

        public string Password { get; set; }
        public string userShippingAddress { get; set; }
        public string userPurchaseHistory {  get; set; }
        
    }
}
