using Microsoft.EntityFrameworkCore;

using Project.Context.Types;
using Project.Domain.Context;
using Project.Domain.People;
using Project.Domain.Products;
using Project.Domain.Shopping;
using Project.Domain.Users;

using System;

namespace Project.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //User
            builder.ApplyConfiguration(new PersonTypeConfiguration());
            builder.ApplyConfiguration(new UserTypeConfiguration());

            //product
            builder.ApplyConfiguration(new ProductTypeConfiguration());
            builder.ApplyConfiguration(new ImageTypeConfiguration());
            builder.ApplyConfiguration(new CategoryTypeConfiguration());

            //cart
            builder.ApplyConfiguration(new ItemTypeConfiguration());
            builder.ApplyConfiguration(new ShoppingListTypeConfiguration());
        }
    }
}
