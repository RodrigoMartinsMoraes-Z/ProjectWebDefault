using Microsoft.EntityFrameworkCore;

using Project.Domain.People;
using Project.Domain.Users;

using Starlight.Core.DbHelper;

namespace Project.Domain.Context
{
    public interface IDataBaseContext : IDbContext
    {
        DbSet<Person> People { get; }
        DbSet<User> Users { get; }
    }
}
