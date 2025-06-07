using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class MoviePosterEntityConfiguration : IEntityTypeConfiguration<MoviePoster>
{
    public void Configure(EntityTypeBuilder<MoviePoster> builder)
    {
        builder.Property(mp => mp.PublicId).IsRequired();

        builder.Property(mp => mp.Url).IsRequired();

        builder.HasOne(mp => mp.Movie)
            .WithMany(m => m.Posters)
            .HasForeignKey(mp => mp.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
