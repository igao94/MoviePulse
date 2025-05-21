using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
