using API.Extensions;
using API.Middleware;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();

builder.Services.AddApplicationServices();

builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using IServiceScope scope = await SeedDatabaseAsync(app);

app.Run();

static async Task<IServiceScope> SeedDatabaseAsync(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        var useInMemoryDatabase = services.GetRequiredService<IConfiguration>()
            .GetValue<bool>("UseInMemoryDatabase");

        if (!useInMemoryDatabase)
        {
            await context.Database.MigrateAsync();
        }

        await Seed.SeedDatabaseAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occured.");
    }

    return scope;
}