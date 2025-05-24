using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CelebrityRoleConfiguration : IEntityTypeConfiguration<CelebrityRole>
{
    public void Configure(EntityTypeBuilder<CelebrityRole> builder)
    {
        builder.HasOne(cr => cr.Celebrity)
            .WithMany(c => c.Roles)
            .HasForeignKey(cr => cr.CelebrityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(cr => cr.RoleType)
            .WithMany(crt => crt.Celebrities)
            .HasForeignKey(cr => cr.RoleTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
