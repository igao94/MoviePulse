using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class MovieEntityConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(m => m.Title).IsRequired();

        builder.Property(m => m.Description).IsRequired();

        builder.Property(m => m.ReleaseDate).IsRequired();

        builder.Property(m => m.DurationInMinutes).IsRequired();
    }
}
