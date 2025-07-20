using Duobingo.Dominio.ModuloTeste;
using Duobingo.InfraestruturaEmOrm.ModuloTeste;
using Duobingo.WebApp.ActionFilters;
using Duobingo.WebApp.DependencyInjection;
namespace eAgenda.WebApp;

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


        builder.Services.AddScoped<IRepositorioTeste, RepositorioTesteEmOrm>();
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