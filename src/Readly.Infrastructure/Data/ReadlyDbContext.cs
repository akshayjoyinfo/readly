using Microsoft.EntityFrameworkCore;
using Readly.Application.Common.Interfaces;
using Readly.Domain.Common;
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    var source = string.IsNullOrEmpty(entry.Entity.CreatedBy) ? "SYSTEM" : entry.Entity.CreatedBy;
                    var occured = DateTime.UtcNow;
                    entry.Entity.CreatedBy = source;
                    entry.Entity.CreatedAtUtc = occured;
                    entry.Entity.LastModifiedBy = source;
                    entry.Entity.LastModifiedAtUtc = occured;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = Convert.ToString(string.IsNullOrEmpty(entry.Entity.LastModifiedBy)
                        ? "SYSTEM"
                        : entry.Entity.LastModifiedAtUtc);
                    entry.Entity.LastModifiedAtUtc = DateTime.UtcNow;
                    break;
            }
        }

        await base.SaveChangesAsync(cancellationToken);
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadlyDbContext).Assembly);
    }
}
