using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class MovieRoleConfiguration : IEntityTypeConfiguration<MovieRole>
{
    public void Configure(EntityTypeBuilder<MovieRole> builder)
    {
        builder.HasKey(mr => new { mr.MovieId, mr.CelebrityId, mr.RoleTypeId });

        builder.HasOne(mr => mr.Celebrity)
            .WithMany(c => c.Roles)
            .HasForeignKey(mr => mr.CelebrityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(mr => mr.RoleType)
            .WithMany(crt => crt.Celebrities)
            .HasForeignKey(mr => mr.RoleTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(mr => mr.Movie)
            .WithMany(m => m.Celebrities)
            .HasForeignKey(mr => mr.MovieId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
