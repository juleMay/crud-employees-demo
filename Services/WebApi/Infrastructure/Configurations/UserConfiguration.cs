using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("UserId");

        builder.Property(u => u.Email)
                .HasConversion(
                    e => e.Address,
                    e => Email.Create(e))
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Email");

        builder.Property(u => u.Password)
                .HasConversion(
                    p => p.Hash,
                    p => Password.Create(p))
                .IsRequired()
                .HasMaxLength(512)
                .HasColumnName("PasswordHash");


        builder.HasOne(u => u.Employee)
            .WithOne(e => e.User)
            .HasForeignKey<Employee>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Portal)
            .WithMany(c => c.Users)
            .HasForeignKey(e => e.PortalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithMany(c => c.Users)
            .HasForeignKey(e => e.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}