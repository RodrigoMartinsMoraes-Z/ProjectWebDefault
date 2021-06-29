using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.Users;

namespace Project.Context.Types
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Person).WithOne(p => p.User).HasForeignKey<User>(u => u.PersonId);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasData(new User
            {
                Email = "email@email.com",
                Login = "admin",
                Password = "123",
                PersonId = 1,
                Id = 1
            });
        }
    }
}
