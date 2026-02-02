using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("EmployeeId");

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.CompanyId).IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Telephone)
                .HasDefaultValue(PhoneNumber.Empty())
                .HasConversion(
                    t => t.Number,
                    t => PhoneNumber.Create(t))
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("TelephoneNumber");


        builder.Property(e => e.Fax)
                .HasDefaultValue(PhoneNumber.Empty())
                .HasConversion(
                    f => f.Number,
                    f => PhoneNumber.Create(f))
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("FaxNumber");

        builder.Property(e => e.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}