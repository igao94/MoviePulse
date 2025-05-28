using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class WatchlistEntityConfiguration : IEntityTypeConfiguration<Watchlist>
{
    public void Configure(EntityTypeBuilder<Watchlist> builder)
    {
        builder.HasKey(wl => new { wl.UserId, wl.MovieId });

        builder.HasOne(wl => wl.User)
            .WithMany(u => u.Watchlist)
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(wl => wl.Movie)
            .WithMany(m => m.Watchlist)
            .HasForeignKey(wl => wl.MovieId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
