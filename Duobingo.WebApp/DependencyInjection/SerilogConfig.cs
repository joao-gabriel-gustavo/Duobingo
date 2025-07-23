
using Serilog;
using Serilog.Events;

namespace Duobingo.WebApp.DependencyInjection;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging)
    {
        // Try to create a portable log path that works on different machines
        string caminhoArquivoLogs;
        
        try
        {
            var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var pastaLogs = Path.Combine(caminhoAppData, "Duobingo");
            
            // Create directory if it doesn't exist
            Directory.CreateDirectory(pastaLogs);
            
            caminhoArquivoLogs = Path.Combine(pastaLogs, "erro.log");
        }
        catch
        {
            // Fallback to application directory if user folder is not accessible
            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var logsDir = Path.Combine(appDir, "Logs");
            Directory.CreateDirectory(logsDir);
            caminhoArquivoLogs = Path.Combine(logsDir, "erro.log");
        }

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, 
                LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30)
            .CreateLogger();

        logging.ClearProviders();
        services.AddSerilog();
    }
}