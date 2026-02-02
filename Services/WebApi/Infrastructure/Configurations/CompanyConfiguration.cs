using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");
        builder.HasKey(c => c.Id);
        builder.Property(e => e.Id).HasColumnName("CompanyId");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}