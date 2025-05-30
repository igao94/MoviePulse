using Domain.Entities;
using Shared.Constants;
using Shared.Interfaces;

namespace Persistence.Data;

public class SeedDatabase(AppDbContext context, IHmacPasswordHasher hmacPasswordHasher) : ISeedDatabase
{
    public async Task SeedDatabaseAsync()
    {
        AddUserRoles();

        AddUsers();

        AddMovies();

        AddCelebrityRoleTypes();

        AddCelebrities();

        await context.SaveChangesAsync();
    }

    private void AddCelebrities()
    {
        if (!context.Celebrities.Any())
        {
            List<Celebrity> celebrities =
            [
                new ()
                {
                    Id = "cameron-id",
                    FirstName = "James",
                    LastName = "Cameron",
                    Bio = "James Cameron is a Canadian filmmaker known for directing epic films such as Titanic and Avatar, both of which became the highest-grossing films of all time.",
                    DateOfBirth = new DateOnly(1954, 8, 16)
                },

                new ()
                {
                    Id = "downey-id",
                    FirstName = "Robert",
                    LastName = "Downey Jr.",
                    Bio = "Robert Downey Jr. is an American actor best known for his portrayal of Tony Stark / Iron Man in the Marvel Cinematic Universe, including Avengers: Endgame.",
                    DateOfBirth = new DateOnly(1965, 4, 4)
                },

                new ()
                {
                    Id = "bale-id",
                    FirstName = "Christian",
                    LastName = "Bale",
                    Bio = "Christian Bale is a British actor known for his role as Batman in The Dark Knight trilogy. He is praised for his intense method acting.",
                    DateOfBirth = new DateOnly(1974, 1, 30)
                },

                new ()
                {
                    Id = "dicaprio-id",
                    FirstName = "Leonardo",
                    LastName = "DiCaprio",
                    Bio = "Leonardo DiCaprio is an American actor and environmentalist. He is known for his roles in Titanic, Inception, and The Revenant, for which he won the Academy Award for Best Actor.",
                    DateOfBirth = new DateOnly(1974, 11, 11)
                },

                new ()
                {
                    Id = "winslet-id",
                    FirstName = "Kate",
                    LastName = "Winslet",
                    Bio = "Kate Winslet is an English actress who gained international fame for her role as Rose in Titanic. She is the recipient of numerous awards, including an Academy Award.",
                    DateOfBirth = new DateOnly(1975, 10, 5)
                },

                new ()
                {
                    Id = "nolan-id",
                    FirstName = "Christopher",
                    LastName = "Nolan",
                    Bio = "Christopher Nolan is a British-American filmmaker known for directing mind-bending blockbusters like Inception, Interstellar, and The Dark Knight Trilogy.",
                    DateOfBirth = new DateOnly(1970, 7, 30)
                },

                new ()
                {
                    Id = "crowe-id",
                    FirstName = "Russell",
                    LastName = "Crowe",
                    Bio = "Russell Crowe is a New Zealand actor who won an Academy Award for his performance as Maximus in Gladiator.",
                    DateOfBirth = new DateOnly(1964, 4, 7)
                },

                new ()
                {
                    Id = "tarantino-id",
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                    Bio = "Quentin Tarantino is an American filmmaker known for his distinctive style and nonlinear storytelling. He directed Pulp Fiction, a cult classic.",
                    DateOfBirth = new DateOnly(1963, 3, 27)
                },

                new ()
                {
                    Id = "jackson-id",
                    FirstName = "Peter",
                    LastName = "Jackson",
                    Bio = "Peter Jackson is a New Zealand film director best known for adapting J.R.R. Tolkien's The Lord of the Rings trilogy into award-winning films.",
                    DateOfBirth = new DateOnly(1961, 10, 31)
                },

                new ()
                {
                    Id = "reeves-id",
                    FirstName = "Keanu",
                    LastName = "Reeves",
                    Bio = "Keanu Reeves is a Canadian actor best known for his roles in The Matrix series and John Wick franchise.",
                    DateOfBirth = new DateOnly(1964, 9, 2)
                }
            ];

            context.AddRange(celebrities);
        }
    }

    private void AddCelebrityRoleTypes()
    {
        if (!context.CelebrityRoleTypes.Any())
        {
            List<CelebrityRoleType> roleTypes =
            [
                new()
                {
                    Id = CelebrityRoleTypes.ActorId,
                    Name = CelebrityRoleTypes.Actor
                },

                new()
                {
                    Id = CelebrityRoleTypes.ProducerId,
                    Name = CelebrityRoleTypes.Producer
                },

                new()
                {
                    Id = CelebrityRoleTypes.DirectorId,
                    Name = CelebrityRoleTypes.Director
                },

                new()
                {
                    Id = CelebrityRoleTypes.WriterId,
                    Name = CelebrityRoleTypes.Writer
                }
            ];

            context.CelebrityRoleTypes.AddRange(roleTypes);
        }
    }

    private void AddUserRoles()
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
            var admin = CreateUser("admin-id", "admin", "admin@test.com", "Admin", "Admin");

            context.Users.Add(admin);

            List<User> users =
            [
                CreateUser("john-id", "john", "john@test.com", "John", "Doe"),
                CreateUser("jane-id", "jane", "jane@test.com", "Jane", "Smith"),
                CreateUser("michael-id", "michael", "michael@test.com", "Michael", "Johnson"),
                CreateUser("emily-id", "emily", "emily@test.com", "Emily", "Wilson"),
                CreateUser("alexander-id", "alexander", "alexander@test.com", "Alexander", "Brown")
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

    private User CreateUser(string id, string username, string email, string firstName, string lastName)
    {
        var (hash, salt) = hmacPasswordHasher.HashPassword("Pa$$w0rd");

        return new User
        {
            Id = id,
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
                    DurationInMinutes = 162
                },

                new ()
                {
                    Id = "endgame-id",
                    Title = "Avengers: Endgame",
                    Description = "After the devastating events of Infinity War, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                    ReleaseDate = new DateOnly(2019, 4, 26),
                    DurationInMinutes = 181
                },

                new ()
                {
                    Id = "darkKnight-id",
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    ReleaseDate = new DateOnly(2008, 7, 18),
                    DurationInMinutes = 152
                },

                new ()
                {
                    Id = "inception-id",
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into a CEO's mind.",
                    ReleaseDate = new DateOnly(2010, 7, 8),
                    DurationInMinutes = 148
                },

                new ()
                {
                    Id = "titanic-id",
                    Title = "Titanic",
                    Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                    ReleaseDate = new DateOnly(1997, 12, 19),
                    DurationInMinutes = 195
                },

                new ()
                {
                    Id = "interstellar-id",
                    Title = "Interstellar",
                    Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    ReleaseDate = new DateOnly(2014, 10, 26),
                    DurationInMinutes = 169
                },

                new ()
                {
                    Id = "gladiator-id",
                    Title = "Gladiator",
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                    ReleaseDate = new DateOnly(2000, 5, 5),
                    DurationInMinutes = 155
                },

                new ()
                {
                    Id = "pulpFiction-id",
                    Title = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    ReleaseDate = new DateOnly(1994, 10, 14),
                    DurationInMinutes = 154
                },

                new ()
                {
                    Id = "lotr3-id",
                    Title = "The Lord of the Rings: The Return of the King",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom.",
                    ReleaseDate = new DateOnly(2003, 12, 3),
                    DurationInMinutes = 201
                },

                new ()
                {
                    Id = "matrix-id",
                    Title = "The Matrix",
                    Description = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
                    ReleaseDate = new DateOnly(1999, 3, 31),
                    DurationInMinutes = 136
                }
            ];

            context.Movies.AddRange(movies);
        }
    }
}
