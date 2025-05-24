using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CelebrityEntityConfiguration : IEntityTypeConfiguration<Celebrity>
{
    public void Configure(EntityTypeBuilder<Celebrity> builder)
    {
        builder.Property(c => c.FirstName).IsRequired();

        builder.Property(c => c.LastName).IsRequired();

        builder.Property(c => c.Bio).IsRequired();

        builder.Property(c => c.DateOfBirth).IsRequired();
    }
}
