using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readly.Domain.Common;

namespace Readly.Infrastructure.Data.Configurations;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(b => b.CreatedAtUtc).ValueGeneratedOnAdd();

        builder.Property(b => b.CreatedBy).HasMaxLength(50);

        builder.Property(b => b.LastModifiedAtUtc).IsRequired(false);

        builder.Property(b => b.LastModifiedBy).IsRequired(false).HasMaxLength(50);
    }
}
