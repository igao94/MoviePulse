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
                    Id = "arnold-id",
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger",
                    Bio = "Arnold Schwarzenegger is an Austrian-American actor, businessman, former politician and professional bodybuilder. He gained worldwide fame as a Hollywood action film icon and served as the 38th governor of California.",
                    DateOfBirth = new DateOnly(1947, 7, 30)
                },

                new ()
                {
                    Id = "tom-id",
                    FirstName = "Tom",
                    LastName = "Hanks",
                    Bio = "Tom Hanks is an American actor and filmmaker known for his roles in iconic movies such as Forrest Gump, Saving Private Ryan, and Cast Away. He is widely regarded as one of the most beloved figures in American cinema.",
                    DateOfBirth = new DateOnly(1956, 7, 9)
                },

                new ()
                {
                    Id = "jennifer-id",
                    FirstName = "Jennifer",
                    LastName = "Lawrence",
                    Bio = "Jennifer Lawrence is an American actress who gained recognition for her role in The Hunger Games series. She has received numerous accolades, including an Academy Award for Best Actress for her role in Silver Linings Playbook.",
                    DateOfBirth = new DateOnly(1990, 8, 15)
                },

                new ()
                {
                    Id = "chris-id",
                    FirstName = "Chris",
                    LastName = "Hemsworth",
                    Bio = "Chris Hemsworth is an Australian actor best known for his portrayal of Thor in the Marvel Cinematic Universe. He has become one of the leading action stars in Hollywood.",
                    DateOfBirth = new DateOnly(1983, 8, 11)
                },

                new ()
                {
                    Id = "emma-id",
                    FirstName = "Emma",
                    LastName = "Watson",
                    Bio = "Emma Watson is a British actress and activist who rose to fame for playing Hermione Granger in the Harry Potter film series. She is also known for her advocacy work on gender equality.",
                    DateOfBirth = new DateOnly(1990, 4, 15)
                },

                new ()
                {
                    Id = "leonardo-id",
                    FirstName = "Leonardo",
                    LastName = "DiCaprio",
                    Bio = "Leonardo DiCaprio is an American actor and environmentalist. He is known for his roles in Titanic, Inception, and The Revenant, for which he won the Academy Award for Best Actor.",
                    DateOfBirth = new DateOnly(1974, 11, 11)
                },

                new ()
                {
                    Id = "scarlett-id",
                    FirstName = "Scarlett",
                    LastName = "Johansson",
                    Bio = "Scarlett Johansson is an American actress and singer. She is best known for her roles in films such as Lost in Translation and as Black Widow in the Marvel Cinematic Universe.",
                    DateOfBirth = new DateOnly(1984, 11, 22)
                },

                new ()
                {
                    Id = "dwayne-id",
                    FirstName = "Dwayne",
                    LastName = "Johnson",
                    Bio = "Dwayne Johnson, also known as 'The Rock', is an American actor, producer, and former professional wrestler. He became one of the highest-paid actors in Hollywood thanks to roles in the Fast & Furious franchise and other blockbusters.",
                    DateOfBirth = new DateOnly(1972, 5, 2)
                },

                new ()
                {
                    Id = "gal-id",
                    FirstName = "Gal",
                    LastName = "Gadot",
                    Bio = "Gal Gadot is an Israeli actress and model best known for her role as Wonder Woman in the DC Extended Universe. Before acting, she served in the Israel Defense Forces and won Miss Israel in 2004.",
                    DateOfBirth = new DateOnly(1985, 4, 30)
                },

                new ()
                {
                    Id = "will-id",
                    FirstName = "Will",
                    LastName = "Smith",
                    Bio = "Will Smith is an American actor, rapper, and film producer. He gained fame with The Fresh Prince of Bel-Air and went on to star in numerous box office hits like Men in Black, Ali, and The Pursuit of Happyness.",
                    DateOfBirth = new DateOnly(1968, 9, 25)
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
