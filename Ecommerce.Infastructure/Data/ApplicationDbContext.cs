using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infastructure.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<LocalUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(e => new {  e.Id , e.OrderId, e.ProductId});

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
               .Property(p => p.price)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Electronics", Description = "Electronic items" },
               new Category { Id = 2, Name = "Books", Description = "Various books" }
           );

            modelBuilder.Entity<LocalUser>().HasData(
                new LocalUser { FirstName = "ghada", LastName = "nofal", Address = "Idna"},
                new LocalUser { FirstName = "manal", LastName = "amro", Address = "Idna" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", price = 699, Image = "smartphone.jpg", CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", price = 999, Image = "laptop.jpg", CategoryId = 1 },
                new Product { Id = 3, Name = "Book A", price = 19, Image = "bookA.jpg", CategoryId = 2 },
                new Product { Id = 4, Name = "Book B", price = 29, Image = "bookB.jpg", CategoryId = 2 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, LocalUserId = 1, OrderSatutus = "Completed", OrderDate = DateTime.Now },
                new Order { Id = 2, LocalUserId = 1, OrderSatutus = "Pending", OrderDate = DateTime.Now }
            );

            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails { Id = 1, OrderId = 1, ProductId = 1, price = 699.99m, quantity = 1 },
                new OrderDetails { Id = 2, OrderId = 1, ProductId = 3, price = 19.99m, quantity = 2 },
                new OrderDetails { Id = 3, OrderId = 2, ProductId = 2, price = 999.99m, quantity = 1 }
            );
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }

       // public DbSet <IdentityUser> User { get; set; }


    }
}
