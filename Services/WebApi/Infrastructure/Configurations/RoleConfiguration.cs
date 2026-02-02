using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("RoleId");

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}