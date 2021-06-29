using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.People;

using System;

namespace Project.Context.Types
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasData(new Person
            {
                Name = "administrator",
                Birth = DateTime.Today,
                Id = 1
            });

        }
    }
}
