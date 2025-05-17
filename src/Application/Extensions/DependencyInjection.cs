using Application.Core;
using Application.Movies.Queries.GetAllMovies;
using Application.Movies.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(GetAllMoviesQuery).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddValidatorsFromAssemblyContaining<CreateMovieValidator>();

        return services;
    }
}
