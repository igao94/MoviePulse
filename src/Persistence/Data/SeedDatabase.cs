using Domain.Entities;
using Shared.Constants;
using Shared.Interfaces;

namespace Persistence.Data;

public class SeedDatabase(AppDbContext context, IHmacPasswordHasher hmacPasswordHasher) : ISeedDatabase
{
    public async Task SeedDatabaseAsync()
    {
        AddMovies();

        AddRoles();

        AddUsers();

        await context.SaveChangesAsync();
    }

    private void AddRoles()
    {
        if (!context.Roles.Any())
        {
            List<Role> roles =
            [
                new()
                {
                    Id = UserRoles.MemberRoleId,
                    Name = UserRoles.Member,
                    NormalizedName = UserRoles.Member.ToUpper()
                },

                new()
                {
                    Id = UserRoles.AdminRoleId,
                    Name = UserRoles.Admin,
                    NormalizedName = UserRoles.Admin.ToUpper()
                }
            ];

            context.Roles.AddRange(roles);
        }
    }

    private void AddUsers()
    {
        if (!context.Users.Any())
        {
            var admin = CreateUser("admin", "admin@test.com", "Admin", "Admin");

            context.Users.Add(admin);

            List<User> users =
            [
                CreateUser("john", "john@test.com", "John", "Doe"),
                CreateUser("jane", "jane@test.com", "Jane", "Smith"),
                CreateUser("michael", "michael@test.com", "Michael", "Johnson"),
                CreateUser("emily", "emily@test.com", "Emily", "Wilson"),
                CreateUser("alexander", "alexander@test.com", "Alexander", "Brown")
            ];

            context.Users.AddRange(users);

            AddUsersToRoles(admin, users);
        }
    }

    private void AddUsersToRoles(User admin, List<User> users)
    {
        context.UserRoles.Add(new UserRole { UserId = admin.Id, RoleId = UserRoles.AdminRoleId });

        foreach (var user in users)
        {
            context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = UserRoles.MemberRoleId });
        }
    }

    private User CreateUser(string username, string email, string firstName, string lastName)
    {
        var (hash, salt) = hmacPasswordHasher.HashPassword("Pa$$w0rd");

        return new User
        {
            Username = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = hash,
            PasswordSalt = salt
        };
    }

    private void AddMovies()
    {
        if (!context.Movies.Any())
        {
            List<Movie> movies =
            [
                new ()
                {
                    Id = "avatar-id",
                    Title = "Avatar",
                    Description = "A paraplegic Marine dispatched to the moon Pandora becomes torn between following his orders and protecting the world he feels is his home.",
                    ReleaseDate = new DateOnly(2009, 12, 17),
                    DurationInMinutes = 162,
                    Rating = 7.9
                },

                new ()
                {
                    Id = "endgame-id",
                    Title = "Avengers: Endgame",
                    Description = "After the devastating events of Infinity War, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                    ReleaseDate = new DateOnly(2019, 4, 26),
                    DurationInMinutes = 181,
                    Rating = 8.4
                },

                new ()
                {
                    Id = "darkKnight-id",
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    ReleaseDate = new DateOnly(2008, 7, 18),
                    DurationInMinutes = 152,
                    Rating = 9.0
                },

                new ()
                {
                    Id = "inception-id",
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into a CEO's mind.",
                    ReleaseDate = new DateOnly(2010, 7, 8),
                    DurationInMinutes = 148,
                    Rating = 8.8
                },

                new ()
                {
                    Id = "titanic-id",
                    Title = "Titanic",
                    Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                    ReleaseDate = new DateOnly(1997, 12, 19),
                    DurationInMinutes = 195,
                    Rating = 7.9
                },

                new ()
                {
                    Id = "interstellar-id",
                    Title = "Interstellar",
                    Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    ReleaseDate = new DateOnly(2014, 10, 26),
                    DurationInMinutes = 169,
                    Rating = 8.6
                },

                new ()
                {
                    Id = "gladiator-id",
                    Title = "Gladiator",
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                    ReleaseDate = new DateOnly(2000, 5, 5),
                    DurationInMinutes = 155,
                    Rating = 8.5
                },

                new ()
                {
                    Id = "pulpFiction-id",
                    Title = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    ReleaseDate = new DateOnly(1994, 10, 14),
                    DurationInMinutes = 154,
                    Rating = 8.9
                },

                new ()
                {
                    Id = "lotr3-id",
                    Title = "The Lord of the Rings: The Return of the King",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom.",
                    ReleaseDate = new DateOnly(2003, 12, 3),
                    DurationInMinutes = 201,
                    Rating = 9.0
                },

                new ()
                {
                    Id = "matrix-id",
                    Title = "The Matrix",
                    Description = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
                    ReleaseDate = new DateOnly(1999, 3, 31),
                    DurationInMinutes = 136,
                    Rating = 8.7
                }
            ];

            context.Movies.AddRange(movies);
        }
    }
}
