using Microsoft.EntityFrameworkCore;
using Readly.Application.Common.Interfaces;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Infrastructure.Data;

public class ReadlyDbContext(DbContextOptions<ReadlyDbContext> options) : DbContext(options), IReadlyDbContext
{
    public Task<int> SaveChangesWithPublishEventsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public DbContext Instance => this;

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadlyDbContext).Assembly);
    }
}
