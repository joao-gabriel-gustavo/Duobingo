# Duobingo - Sistema de Testes

## ⚠️ Configuração Importante - "Funciona só na minha máquina"

Este projeto estava configurado para funcionar apenas na máquina do desenvolvedor original. Siga os passos abaixo para configurar em qualquer ambiente.

## 🔧 Configuração do Ambiente

### 1. Pré-requisitos

- .NET 8.0 SDK
- SQL Server ou SQL Server LocalDB
- Visual Studio 2022 ou Visual Studio Code

### 2. Configuração do Banco de Dados

#### Opção A: Usando SQL Server LocalDB (Desenvolvimento)

```bash
# Instalar SQL Server LocalDB se não tiver
# Download: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb
```

#### Opção B: Usando SQL Server Completo

1. Instale o SQL Server
2. Atualize a connection string em `appsettings.json`:

```json
{
  "SQL_CONNECTION_STRING": "Server=YOUR_SERVER_NAME;Database=DuobingoDB;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

### 3. Configuração por Ambiente

#### Desenvolvimento Local:

```json
// appsettings.Development.json
{
  "SQL_CONNECTION_STRING": "Server=(localdb)\\mssqllocaldb;Database=DuobingoDB;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

#### Produção:

```json
// appsettings.Production.json
{
  "SQL_CONNECTION_STRING": "Server=YOUR_PRODUCTION_SERVER;Database=DuobingoDB;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

### 4. Executar Migrations

```bash
# Navegue até a pasta do projeto
cd Duobingo.WebApp

# Execute as migrations
dotnet ef database update --project ../Duobingo.InfraestruturaEmOrm
```

### 5. Executar a Aplicação

```bash
# Compilar e executar
dotnet run --project Duobingo.WebApp
```

## 🐛 Problemas Comuns

### "Cannot connect to database"

- Verifique se o SQL Server LocalDB está instalado
- Confirme a connection string no appsettings.json
- Execute `dotnet ef database update`

### "Logs folder access denied"

- Os logs são salvos em `%LOCALAPPDATA%\Duobingo\erro.log`
- Certifique-se que o usuário tem permissão de escrita

### "ApplicationHost.config path errors"

- Delete a pasta `.vs` e `.idea`
- Reabra o projeto no Visual Studio

## 🔧 Para Outros Desenvolvedores

1. Clone o repositório
2. Instale os pré-requisitos
3. Configure sua connection string
4. Execute migrations
5. Rode o projeto

## 📁 Estrutura do Projeto

- `Duobingo.Dominio` - Camada de domínio
- `Duobingo.InfraestruturaEmOrm` - Camada de dados (Entity Framework)
- `Duobingo.WebApp` - Aplicação web (ASP.NET Core)

## 🚀 Deploy

Para deploy em produção:

1. Configure a connection string de produção
2. Execute migrations no servidor
3. Configure IIS ou hosting de sua escolha
