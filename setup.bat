@echo off
echo ================================================
echo    Duobingo - Setup Automatico
echo ================================================
echo.

echo [1/4] Verificando .NET 8.0...
dotnet --version
if errorlevel 1 (
    echo ERRO: .NET 8.0 nao encontrado!
    echo Baixe em: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo [2/4] Restaurando pacotes NuGet...
dotnet restore

echo [3/4] Compilando projeto...
dotnet build

echo [4/4] Criando/Atualizando banco de dados...
cd Duobingo.WebApp
dotnet ef database update --project ../Duobingo.InfraestruturaEmOrm
if errorlevel 1 (
    echo.
    echo AVISO: Erro ao criar banco de dados.
    echo Verifique se o SQL Server LocalDB esta instalado.
    echo Download: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb
    echo.
    pause
    cd ..
    exit /b 1
)

cd ..

echo.
echo ================================================
echo    Setup concluido com sucesso!
echo ================================================
echo.
echo Para executar a aplicacao:
echo    cd Duobingo.WebApp
echo    dotnet run
echo.
echo A aplicacao estara disponivel em:
echo    https://localhost:7029
echo    http://localhost:5137
echo.
pause 