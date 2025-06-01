using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            var useInMemory = config.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemory)
            {
                opt.UseInMemoryDatabase("InMemoryDatabase");
            }
            else
            {
                opt.UseSqlServer(config.GetConnectionString("SqlServer"));
            }
        });

        services.AddScoped<IMovieRepository, MovieRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<ICelebrityRepository, CelebrityRepository>();

        services.AddScoped<IUserMovieRatingRepository, UserMovieRatingRepository>();

        services.AddScoped<IUserMovieInteractionRepository, UserMovieInteractionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ISeedDatabase, SeedDatabase>();

        return services;
    }
}
