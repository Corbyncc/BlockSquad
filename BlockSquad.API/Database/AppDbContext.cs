using BlockSquad.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace BlockSquad.API.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> User { get; set; }
}

