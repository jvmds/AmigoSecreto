using AmigoSecreto.Context;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.AddDbContext();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return builder;
    }

    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SQLite");
        builder.Services.AddDbContext<AmigoSecretoContext>(opt => opt.UseSqlite(connectionString));

        return builder;
    }
}