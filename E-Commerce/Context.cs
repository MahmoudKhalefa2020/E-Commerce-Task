using E_Commerce.Dtos;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context>options):base(options) 
        {
            
        }
        
        public virtual DbSet<Category> Categories { get; set;}
        public virtual DbSet<SubCategory> SubCategories { get; set;}
        public virtual DbSet<Product> Products { get; set;}
        
    }
}
