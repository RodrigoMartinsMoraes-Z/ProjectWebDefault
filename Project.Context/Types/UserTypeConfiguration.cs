using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.Users;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Context.Types
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Person);

            builder.HasOne(u => u.Person).WithMany().HasForeignKey(u => u.PersonId);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(u => u.Login).IsUnique();
        }
    }
}
