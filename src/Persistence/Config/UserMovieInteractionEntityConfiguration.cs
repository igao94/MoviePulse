using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class UserMovieInteractionEntityConfiguration : IEntityTypeConfiguration<UserMovieInteraction>
{
    public void Configure(EntityTypeBuilder<UserMovieInteraction> builder)
    {
        builder.HasKey(um => new { um.UserId, um.MovieId });

        builder.HasIndex(um => um.IsWatched);

        builder.HasIndex(um => um.IsInWatchlist);

        builder.HasIndex(um => um.Rating);

        builder.HasOne(um => um.User)
            .WithMany(u => u.UserMovieInteractions)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(um => um.Movie)
            .WithMany(m => m.UserMovieInteractions)
            .HasForeignKey(um => um.MovieId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
