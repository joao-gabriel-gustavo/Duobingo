using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

public class GeradorDePdfTeste
{
    public  void GeradorPDFNormal()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var nomeArquivo = $"relatorio_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        var caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nomeArquivo);

        try
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content().Column(col =>
                    {
                        col.Item().Text("Opa");
                        col.Item().Text("Outro texto aqui");
                    });
                });
            })
            .GeneratePdf(caminho);

            Console.WriteLine($"PDF gerado com sucesso em: {caminho}");

            System.Threading.Thread.Sleep(500);

            Process.Start(new ProcessStartInfo
            {
                FileName = caminho,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao gerar ou abrir PDF:");
        }

        Console.ReadKey();
    }

    public  void GeradorPDFGabarito()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var nomeArquivo = $"relatorio_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        var caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nomeArquivo);

        try
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content().Column(col =>
                    {
                        
                        col.Item().Text("Opa");
                        col.Item().Text("Outro texto aqui");
                    });
                });
            })
            .GeneratePdf(caminho);

            Console.WriteLine($"PDF gerado com sucesso em: {caminho}");

            System.Threading.Thread.Sleep(500);

            Process.Start(new ProcessStartInfo
            {
                FileName = caminho,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao gerar ou abrir PDF:");
        }

        Console.ReadKey();
    }
}
