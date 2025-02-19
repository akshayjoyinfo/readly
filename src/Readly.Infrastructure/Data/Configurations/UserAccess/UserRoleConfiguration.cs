using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Infrastructure.Data.Configurations.UserAccess;

public class UserRoleConfiguration : BaseEntityTypeConfiguration<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.Id, ur.UserId, ur.RoleId });

        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(b => b.CreatedAtUtc).ValueGeneratedOnAdd();

        builder.Property(b => b.CreatedBy).HasMaxLength(50);

        builder.Property(b => b.LastModifiedAtUtc).IsRequired(false);

        builder.Property(b => b.LastModifiedBy).IsRequired(false).HasMaxLength(50);

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Specify the delete behavior for the relationship

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade); // Specify the delete behavior for the relationship
    }
}
