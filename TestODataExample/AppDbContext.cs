using Microsoft.EntityFrameworkCore;
using TestODataExample.Model;

namespace TestODataExample;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(opt =>
        {
            opt.HasData(
                new List<Product>()
                {
                    new Product()
                    {
                        Id = 1,
                        OrderId = 1,
                        Name = "Product Name 1",
                        IsDelete = false
                    },
                    new Product()
                    {
                        Id = 2,
                        OrderId = 1,
                        Name = "Product Name 2",
                        IsDelete = false
                    },
                    new Product()
                    {
                        Id = 3,
                        OrderId = 2,
                        Name = "Product Name 3",
                        IsDelete = false
                    },
                    new Product()
                    {
                        Id = 4,
                        OrderId = 3,
                        Name = "Product Name 4",
                        IsDelete = false
                    }
                });
        });
        
        modelBuilder.Entity<Order>(opt =>
        {
            opt.HasData(
                new List<Order>()
                {
                    new Order()
                    {
                        Id = 1,
                        Name = "Order Name 1",
                        IsDelete = false,
                        DateCreated = DateTime.UtcNow

                    },
                    new Order()
                    {
                        Id = 2,
                        Name = "Order Name 2",
                        IsDelete = false,
                        DateCreated = DateTime.UtcNow

                    },
                    new Order()
                    {
                        Id = 3,
                        Name = "Order Name 3",
                        IsDelete = false,
                        DateCreated = DateTime.UtcNow
                    }
                });
        });
    }
}