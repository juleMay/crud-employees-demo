using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Configurations;

public class PortalConfiguration : IEntityTypeConfiguration<Portal>
{
    public void Configure(EntityTypeBuilder<Portal> builder)
    {
        builder.ToTable("Portal");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("PortalId");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}