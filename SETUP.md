# Lotto Application - Setup Guide

This guide provides comprehensive instructions for setting up the Lotto Application development environment.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Development Environment](#development-environment)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Building for Production](#building-for-production)
- [Git Workflow](#git-workflow)
- [Troubleshooting](#troubleshooting)

## Prerequisites

Before setting up the Lotto Application, ensure you have the following installed on your system:

- **Git** (version 2.x or higher)
- **.NET SDK** (version 6.0 or higher)
  - Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
  - Verify installation: `dotnet --version`
- **Database** (optional, for data persistence):
  - SQL Server Express (recommended for Windows)
  - SQLite (lightweight, recommended for development)
  - PostgreSQL 12+ (cross-platform option)
- **Code Editor/IDE** (recommended):
  - Visual Studio 2022 (Community, Professional, or Enterprise)
  - Visual Studio Code with C# extension
  - JetBrains Rider

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application
```

### 2. Restore Dependencies

```bash
# Restore NuGet packages
dotnet restore

# Or if using Visual Studio, it will restore automatically when you open the solution
```

### 3. Build the Solution

```bash
# Build in Debug mode
dotnet build

# Build in Release mode
dotnet build --configuration Release
```

## Configuration

### appsettings.json

Create or modify `appsettings.json` for application-specific settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=lotto.db",
    "SqlServerConnection": "Server=(localdb)\\mssqllocaldb;Database=LottoApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "LotterySettings": {
    "Type": "standard",
    "NumbersPerDraw": 6,
    "NumberRangeMin": 1,
    "NumberRangeMax": 49
  },
  "AnalysisSettings": {
    "HistoricalDataLimit": 1000,
    "HotNumberThreshold": 0.15,
    "ColdNumberThreshold": 0.05
  },
  "GenerationSettings": {
    "DefaultRows": 5,
    "Algorithms": [
      "FrequencyBased",
      "PatternBased",
      "RandomWeighted"
    ]
  },
  "DataSource": {
    "Url": "https://api.example.com/lottery-data",
    "ApiKey": "your_api_key_here"
  }
}
```

### appsettings.Development.json

For development-specific settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=lotto_dev.db"
  }
}
```

### User Secrets (for sensitive data)

For local development, use .NET User Secrets to store sensitive information:

```bash
# Initialize user secrets
dotnet user-secrets init

# Set API key
dotnet user-secrets set "DataSource:ApiKey" "your_actual_api_key"

# Set connection string
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "your_connection_string"
```

## Development Environment

### Setting Up the Database

#### Entity Framework Core Migrations

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Add initial migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

#### SQLite (Development):

SQLite database will be created automatically when running migrations. Connection string example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=lotto.db"
}
```

#### SQL Server (Production):

```bash
# Ensure SQL Server is running
# Update connection string in appsettings.json

# Run migrations
dotnet ef database update --connection "your_connection_string"
```

### Importing Historical Data

```bash
# Using a custom import command (if implemented)
dotnet run -- import-data data/historical_draws.csv

# Or using a separate console application
dotnet run --project ImportTool -- data/historical_draws.csv
```

Example CSV format for historical draws:
```csv
DrawDate,Number1,Number2,Number3,Number4,Number5,Number6
2024-01-01,5,12,23,34,41,49
2024-01-08,3,15,27,33,38,45
```

## Running the Application

### Development Mode

#### Console Application:

```bash
# Run the application
dotnet run

# Run with specific arguments
dotnet run -- generate --rows 5

# Run with watch (auto-reload on file changes)
dotnet watch run
```

#### Web Application (ASP.NET Core):

```bash
# Run the web application
dotnet run

# Or with watch for development
dotnet watch run

# Specify a different port
dotnet run --urls "http://localhost:5000;https://localhost:5001"
```

The application will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

#### Using Visual Studio

1. Open the solution file (`.sln`) in Visual Studio
2. Press `F5` to run with debugging, or `Ctrl+F5` to run without debugging

### Using the Application

#### Command Line Interface:

```bash
# Generate lottery numbers
dotnet run -- generate --rows 5

# Analyze historical data
dotnet run -- analyze --draws 100

# Check number frequency
dotnet run -- frequency --number 7

# View hot and cold numbers
dotnet run -- stats
```

#### Web Interface (if implemented):

Navigate to the web interface and use the UI to:
- Generate lottery numbers
- View historical analysis
- Check number statistics
- Track generated numbers

## Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Run tests with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/LottoApp.Tests/LottoApp.Tests.csproj
```

### Test Structure

```
tests/
├── LottoApp.Tests/              # Unit tests
├── LottoApp.IntegrationTests/   # Integration tests
└── LottoApp.TestData/           # Test data and fixtures
```

### Running Specific Tests

```bash
# Run tests by filter
dotnet test --filter "FullyQualifiedName~NumberGenerator"

# Run tests by category
dotnet test --filter "Category=Unit"

# Run a specific test method
dotnet test --filter "FullyQualifiedName=LottoApp.Tests.NumberGeneratorTests.GenerateNumbers_ReturnsCorrectCount"
```

### Code Coverage Report

```bash
# Generate coverage report (requires ReportGenerator tool)
dotnet test --collect:"XPlat Code Coverage"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

### Watch Mode (for TDD)

```bash
# Run tests in watch mode
dotnet watch test
```

## Building for Production

### Creating a Production Build

#### Publish the Application:

```bash
# Publish for the current platform
dotnet publish -c Release

# Publish for specific runtime (self-contained)
dotnet publish -c Release -r win-x64 --self-contained
dotnet publish -c Release -r linux-x64 --self-contained
dotnet publish -c Release -r osx-x64 --self-contained

# Publish as single file
dotnet publish -c Release -r win-x64 --self-contained /p:PublishSingleFile=true
```

#### Create NuGet Package (for libraries):

```bash
# Pack the project
dotnet pack -c Release

# Pack with specific version
dotnet pack -c Release /p:Version=1.0.0
```

### Deployment

#### IIS (Windows):

1. Publish the application:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. Install the .NET Hosting Bundle on the server
3. Create a new IIS site pointing to the publish folder
4. Configure the application pool to use "No Managed Code"

#### Docker:

Create a `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LottoApp/LottoApp.csproj", "LottoApp/"]
RUN dotnet restore "LottoApp/LottoApp.csproj"
COPY . .
WORKDIR "/src/LottoApp"
RUN dotnet build "LottoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LottoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LottoApp.dll"]
```

Build and run:

```bash
# Build Docker image
docker build -t lotto-app:latest .

# Run container
docker run -d -p 8080:80 --name lotto-app lotto-app:latest

# Or using Docker Compose
docker-compose up -d
```

#### Azure App Service:

```bash
# Login to Azure
az login

# Create resource group (if needed)
az group create --name LottoAppRG --location eastus

# Create App Service plan
az appservice plan create --name LottoAppPlan --resource-group LottoAppRG --sku B1

# Create web app
az webapp create --name lotto-app --resource-group LottoAppRG --plan LottoAppPlan

# Deploy
dotnet publish -c Release
cd bin/Release/net6.0/publish
zip -r deploy.zip .
az webapp deployment source config-zip --resource-group LottoAppRG --name lotto-app --src deploy.zip
```

## Git Workflow

### Branch Structure

This repository uses the following branching strategy:

- **main** - Production-ready code. All releases are tagged from this branch.
- **develop** - Integration branch for features. All feature branches merge here first.
- **feature/** - Feature branches created from `develop` for new features.
- **bugfix/** - Bug fix branches for addressing issues.
- **hotfix/** - Critical fixes that need immediate deployment to production.

### Creating Feature Branches

```bash
# Create and switch to a new feature branch
git checkout develop
git pull origin develop
git checkout -b feature/your-feature-name

# Work on your feature...
git add .
git commit -m "Add feature description"

# Push to remote
git push -u origin feature/your-feature-name
```

### Merging Changes

```bash
# Update your feature branch with latest develop
git checkout feature/your-feature-name
git fetch origin
git merge origin/develop

# Create a pull request on GitHub
# After PR approval, merge to develop
git checkout develop
git pull origin develop
git merge feature/your-feature-name
git push origin develop
```

### Branch Protection Rules (Recommended)

Configure the following protection rules for `main` and `develop` branches:

1. Require pull request reviews before merging
2. Require status checks to pass before merging
3. Require branches to be up to date before merging
4. Include administrators in restrictions
5. Require linear history (optional)

## Troubleshooting

### Common Issues

#### .NET SDK Not Found

```bash
# Verify .NET installation
dotnet --version
dotnet --list-sdks

# If not installed, download from https://dotnet.microsoft.com/download
```

#### NuGet Package Restore Failures

```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore --force

# If using Visual Studio, try:
# Tools > NuGet Package Manager > Package Manager Console
# Run: Update-Package -reinstall
```

#### Database Connection Errors

```bash
# Verify connection string in appsettings.json
# For SQLite, ensure the directory exists:
mkdir -p data

# For SQL Server, test connection:
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"

# Check EF Core migrations status
dotnet ef migrations list
dotnet ef database update
```

#### Build Errors

```bash
# Clean the solution
dotnet clean

# Rebuild
dotnet build --no-incremental

# If using Visual Studio:
# Build > Clean Solution
# Build > Rebuild Solution
```

#### Port Already in Use

```bash
# Windows - find process using port 5000
netstat -ano | findstr :5000

# Kill the process (replace PID with actual process ID)
taskkill /PID <PID> /F

# Linux/macOS
lsof -i :5000
kill -9 <PID>

# Or change the port in launchSettings.json or use:
dotnet run --urls "http://localhost:5005"
```

#### Entity Framework Issues

```bash
# Reinstall EF Core tools
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef

# Verify EF Core tools
dotnet ef --version

# Reset migrations (development only!)
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### SSL/HTTPS Certificate Issues (Development)

```bash
# Trust the development certificate
dotnet dev-certs https --trust

# Clean and regenerate
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

### Getting Help

If you encounter issues not covered in this guide:

1. Check the [GitHub Issues](https://github.com/Bejap/Lotto-Application/issues) for existing problems
2. Review the application logs in the `logs/` directory or console output
3. Enable debug logging by setting `"LogLevel": { "Default": "Debug" }` in `appsettings.Development.json`
4. Create a new issue with detailed error messages and steps to reproduce

## Additional Resources

- [README.md](README.md) - Project overview and features
- [CONTRIBUTING.md](CONTRIBUTING.md) - Contribution guidelines (if available)
- [API Documentation](docs/API.md) - API reference (if applicable)
- [Architecture Documentation](docs/ARCHITECTURE.md) - System design (if available)

## Next Steps

After completing the setup:

1. Review the [README.md](README.md) to understand the application features
2. Explore the codebase structure
3. Run the test suite to ensure everything is working
4. Try generating some lottery numbers using sample data
5. Read the contributing guidelines before making changes
6. Join the development team discussions

---

For questions or support, please open an issue on GitHub or contact the maintainers.
