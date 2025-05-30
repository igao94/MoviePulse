using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class UserMovieRatingEntityConfiguration : IEntityTypeConfiguration<UserMovieRating>
{
    public void Configure(EntityTypeBuilder<UserMovieRating> builder)
    {
        builder.HasKey(umr => new { umr.UserId, umr.MovieId });

        builder.HasOne(umr => umr.User)
            .WithMany(u => u.MovieRatings)
            .HasForeignKey(umr => umr.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(umr => umr.Movie)
            .WithMany(m => m.MovieRatings)
            .HasForeignKey(umr => umr.MovieId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
