using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities.Authentication;

namespace WebApi.Infrastructure.Configurations.Authentication;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");
        builder.HasKey(c => c.FirebaseUserId);
        builder.Property(c => c.Name);
    }
}