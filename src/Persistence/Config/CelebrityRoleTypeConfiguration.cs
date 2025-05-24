using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class CelebrityRoleTypeConfiguration : IEntityTypeConfiguration<CelebrityRoleType>
{
    public void Configure(EntityTypeBuilder<CelebrityRoleType> builder)
    {
        builder.Property(cr => cr.Name).IsRequired();
    }
}
