using System.ComponentModel.DataAnnotations;

namespace WebScrapData.Models
{
    public class Mobile
    {
       
            [Key]
            public int Id { get; set; }
            public string? MobileName { get; set; }
            public string? Price { get; set; }
    }
}
