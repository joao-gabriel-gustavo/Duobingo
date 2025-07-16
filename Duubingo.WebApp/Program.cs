using System.Data;

namespace Duobingo.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidarModeloAttribute>();
            options.Filters.Add<LogarAcaoAttribute>();
        });

        builder.Services.AddScoped<IDbConnection>(provider =>
        {
            var connectionString = builder.Configuration["SQL_CONNECTION_STRING"];

            return new SqlConnection(connectionString);
        });

        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmOrm>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmOrm>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmOrm>();

        builder.Services.AddEntityFrameworkConfig(builder.Configuration);

        builder.Services.AddSerilogConfig(builder.Logging);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/erro");
        else
            app.UseDeveloperExceptionPage();

        app.UseAntiforgery();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}