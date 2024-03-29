namespace StudentTest.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int commentUserID { get; set; }
        public int commentProductID {  get; set; }
        public double userRating { get; set; }
        public string commentImages{get; set;} 
        public string CommentText {  get; set; }
    }
}
