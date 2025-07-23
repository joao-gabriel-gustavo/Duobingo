using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.WebApp.DependencyInjection
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFrameworkConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Try different connection string sources in order of preference
            var connectionString = 
                Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING") ??        // Environment variable first
                configuration.GetConnectionString("DefaultConnection") ??             // Standard .NET way
                configuration["SQL_CONNECTION_STRING"] ??                             // Legacy way
                throw new InvalidOperationException("No connection string found. Please configure SQL_CONNECTION_STRING in appsettings.json or as environment variable.");

            services.AddDbContext<duobingoDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("Duobingo.InfraestruturaEmOrm")
                )
            );
        }
    }
}