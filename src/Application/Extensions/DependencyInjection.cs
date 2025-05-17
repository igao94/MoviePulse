using Application.Core;
using Application.Movies.Queries.GetAllMovies;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllMoviesQuery).Assembly));

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
