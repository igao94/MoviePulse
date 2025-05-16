using Domain.Entities;

namespace Persistence.Data;

public class Seed
{
    public static async Task SeedDatabaseAsync(AppDbContext context)
    {
        AddMovies(context);

        await context.SaveChangesAsync();
    }

    private static void AddMovies(AppDbContext context)
    {
        List<Movie> movies =
        [
            new ()
            {
                Id = "avatar-id",
                Title = "Avatar",
                Description = "A paraplegic Marine dispatched to the moon Pandora becomes torn between following his orders and protecting the world he feels is his home.",
                ReleaseDate = "2009",
                Duration = 162,
                Rating = 7.9
            },

            new ()
            {
                Id = "avengers-endgame-id",
                Title = "Avengers: Endgame",
                Description = "After the devastating events of Infinity War, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                ReleaseDate = "2019",
                Duration = 181,
                Rating = 8.4
            },

            new ()
            {
                Id = "the-dark-knight-id",
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                ReleaseDate = "2008",
                Duration = 152,
                Rating = 9.0
            },

            new ()
            {
                Id = "inception-id",
                Title = "Inception",
                Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into a CEO's mind.",
                ReleaseDate = "2010",
                Duration = 148,
                Rating = 8.8
            },

            new ()
            {
                Id = "titanic-id",
                Title = "Titanic",
                Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                ReleaseDate = "1997",
                Duration = 195,
                Rating = 7.9
            },

            new ()
            {
                Id = "interstellar-id",
                Title = "Interstellar",
                Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                ReleaseDate = "2014",
                Duration = 169,
                Rating = 8.6
            },

            new ()
            {
                Id = "gladiator-id",
                Title = "Gladiator",
                Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                ReleaseDate = "2000",
                Duration = 155,
                Rating = 8.5
            },

            new ()
            {
                Id = "pulp-fiction-id",
                Title = "Pulp Fiction",
                Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                ReleaseDate = "1994",
                Duration = 154,
                Rating = 8.9
            },

            new ()
            {
                Id = "lord-of-the-rings-id",
                Title = "The Lord of the Rings: The Return of the King",
                Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom.",
                ReleaseDate = "2003",
                Duration = 201,
                Rating = 9.0
            },

            new ()
            {
                Id = "matrix-id",
                Title = "The Matrix",
                Description = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
                ReleaseDate = "1999",
                Duration = 136,
                Rating = 8.7
            }
        ];

        context.Movies.AddRange(movies);
    }
}
