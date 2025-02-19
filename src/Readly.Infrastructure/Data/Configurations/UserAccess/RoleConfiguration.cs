using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Infrastructure.Data.Configurations.UserAccess;

public class RoleConfiguration : BaseEntityTypeConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(b => b.Name).HasMaxLength(50).IsRequired();

        base.Configure(builder);
    }
}
