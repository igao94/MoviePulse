using API.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Shared.Constants;

namespace API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddTransient<ExceptionMiddleware>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyTypes.RequireAdminRole, opt => opt.RequireRole(UserRoles.Admin));

        return services;
    }
}
