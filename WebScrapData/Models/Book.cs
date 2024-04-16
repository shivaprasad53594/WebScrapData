using System.ComponentModel.DataAnnotations;

namespace WebScrapData.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Price { get; set; }
    }
}
