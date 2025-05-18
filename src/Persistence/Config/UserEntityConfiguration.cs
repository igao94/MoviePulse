using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        builder.Property(u => u.FirstName).IsRequired();

        builder.Property(u => u.LastName).IsRequired();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.PasswordHash).IsRequired();
    }
}
