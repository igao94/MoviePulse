using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;

namespace Persistence.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Celebrity> Celebrities { get; set; }
    public DbSet<CelebrityRoleType> CelebrityRoleTypes { get; set; }
    public DbSet<MovieRole> MovieRoles { get; set; }
    public DbSet<Watchlist> Watchlist { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(MovieEntityConfiguration).Assembly);
    }
}
