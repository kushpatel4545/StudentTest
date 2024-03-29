using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTest.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? email { get; set; }


    }
}
