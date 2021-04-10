using Microsoft.EntityFrameworkCore;

using Project.Context.Types;
using Project.Domain.Context;
using Project.Domain.People;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //User
            builder.ApplyConfiguration(new PersonTypeConfiguration());
            builder.ApplyConfiguration(new UserTypeConfiguration());

            //Seeder(builder);
        }

        private void Seeder(ModelBuilder builder)
        {
            var person = new Person
            {
                Name = "administrator",
                Birth = DateTime.Today,
                Id = 1
            };

            var user = new User
            {
                Email = "starlighttecnologia@hotmail.com",
                Login = "admin",
                Password = "123",
                PersonId = 1,
            };

            builder.Entity<User>().HasData(user);
            builder.Entity<Person>().HasData(person);
        }
    }
}
