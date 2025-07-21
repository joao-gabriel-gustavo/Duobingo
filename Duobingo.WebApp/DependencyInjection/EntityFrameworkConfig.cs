using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.WebApp.DependencyInjection
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFrameworkConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["SQL_CONNECTION_STRING"];

            services.AddDbContext<duobingoDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("Duobingo.InfraestruturaEmOrm")
                )
            );
        }
    }
}