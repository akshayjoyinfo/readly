using Microsoft.EntityFrameworkCore;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Application.Common.Interfaces;

public interface IReadlyDbContext
{
    DbContext Instance { get; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    Task<int> SaveChangesWithPublishEventsAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
