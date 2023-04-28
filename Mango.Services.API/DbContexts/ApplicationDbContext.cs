using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ProductId = 1,
                Name = "A",
                CategoryName = "Alphabets",
                Price = 10,
                Description = "Attitude matters",
                ImageUrl = ""
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ProductId = 2,
                Name = "B",
                CategoryName = "Alphabets",
                Price = 10,
                Description = "Be Yourself",
                ImageUrl = ""
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ProductId = 3,
                Name = "C",
                CategoryName = "Alphabets",
                Price = 10,
                Description = "Dont Compromise for unneccessary people, its worthless",
                ImageUrl = ""
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ProductId = 4,
                Name = "D",
                CategoryName = "Alphabets",
                Price = 10,
                Description = "Dare and dont be afraid of anyone who is not worth of being anything to you",
                ImageUrl = ""
            });
        }
    }
}
