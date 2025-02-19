using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Infrastructure.Data.Configurations.UserAccess;

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(b => b.UserName).HasMaxLength(50).IsRequired();
        builder.Property(b => b.PasswordHash).HasMaxLength(100).IsRequired();
        builder.Property(b => b.Email).HasMaxLength(100).IsRequired();

        base.Configure(builder);
    }
}
