using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebScrapData.Models;

namespace WebScrapData.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }  
        public DbSet<Reliance> Reliance { get; set; }  
        public DbSet<Shopclues> Shopclues { get; set; }  
    }
    
}

