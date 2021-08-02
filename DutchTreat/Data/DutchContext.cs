using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DutchTreat.Data.Entities
{
    public class DutchContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DutchContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DutchContextDb"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //    .HasData(new Order()
            //    { Id = 1, OrderDate = System.DateTime.UtcNow,
            //        OrderNumber = "12345"
            //    });
        }
    }
}
