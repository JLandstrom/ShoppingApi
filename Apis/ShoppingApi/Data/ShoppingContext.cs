using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Model.Domain;

namespace ShoppingApi.Data
{
    public class ShoppingContext : IdentityDbContext<AppUser>
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        { }
        
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shopping.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemCategory>()
                .HasKey(c => c.CategoryId);
            modelBuilder.Entity<ShoppingItem>()
                .HasKey(c => c.ItemId);
            modelBuilder.Entity<ShoppingList>()
                .HasKey(c => c.ListId);

            modelBuilder.Entity<ItemCategory>().HasData(new ItemCategory()
            {
                CategoryId = 1,
                Name = "Test"
            });
            modelBuilder.Entity<ShoppingList>().HasData(new ShoppingList()
            {
                Name = "TestList",
                ListId = 1,
                Status = 0

            });
            modelBuilder.Entity<ShoppingItem>().HasData(new ShoppingItem()
            {
                ItemId = 1,
                ShoppingListId = 1,
                ItemCategoryId = 1,
                Name = "TestItem",
                Quantity = 1,
                UnitOfMeasure = "kg"
            });
        }
    }
}
