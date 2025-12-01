# Lotto Application - Setup Guide

This guide provides comprehensive instructions for setting up and running the Lotto Application.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Development](#development)
- [Troubleshooting](#troubleshooting)

## Prerequisites

Before setting up the Lotto Application, ensure you have the following installed:

**.NET SDK (Required)**
- **.NET 10.0 SDK** or later
  - Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
  - Verify installation: `dotnet --version`

**Optional:**
- **Git** (version 2.x or higher) - for version control
- **Code Editor** (recommended):
  - Visual Studio 2022
  - Visual Studio Code with C# extension
  - JetBrains Rider
  - Any text editor of your choice

## Quick Start

The fastest way to get started:

```bash
# Clone the repository
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application

# Build and run
dotnet build
dotnet run
```

Then open your browser to the URL shown in the console (default: `http://localhost:5000`)


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
```

### 3. Build the Application

```bash
# Build in Debug mode
dotnet build

# Or build in Release mode
dotnet build -c Release
```

## Running the Application

### Development Mode

```bash
# Run the application
dotnet run

# Or with hot reload
dotnet watch run
```

The application will start and display the URLs in the console:
```
========================================
🎰 Lotto Application Server Running
========================================
Server running at http://localhost:5000/
Press Ctrl+C to stop the server
========================================
```

### Production Mode

```bash
# Build for production
dotnet publish -c Release -o ./publish

# Run the published application
cd publish
dotnet LottoApplication.dll
```

## Project Structure

```
Lotto-Application/
├── Controllers/
│   └── LottoController.cs          # API controller for lotto operations
├── Models/
│   ├── FrequencyData.cs            # Frequency data model
│   └── LottoResult.cs              # Generated result model
├── Services/
│   └── LottoService.cs             # Business logic service
├── wwwroot/
│   ├── index.html                  # Frontend HTML
│   ├── style.css                   # Application styles
│   └── app.js                      # Frontend JavaScript
├── data/
│   └── lotto_frequency.csv         # Frequency data file
├── LottoApplication.Tests/
│   ├── UnitTest1.cs                # Unit tests
│   └── LottoApplication.Tests.csproj
├── Properties/
│   └── launchSettings.json         # Development launch settings
├── Program.cs                      # Application entry point
├── LottoApplication.csproj         # Project file
├── appsettings.json                # Application configuration
├── README.md                       # Project overview
├── SETUP.md                        # This file
└── GETTING_STARTED.md              # Quick start guide
```

### Key Files

- **Program.cs**: Application entry point and configuration
- **Controllers/LottoController.cs**: API endpoints for data loading and number generation
- **Services/LottoService.cs**: Core business logic for weighted number selection
- **wwwroot/**: Static files served by the application
- **data/lotto_frequency.csv**: Historical frequency data used for weighted number generation

## Development

### Using the Application

1. **Start the Server**: Run `dotnet run` to start the application
2. **Open in Browser**: Navigate to the URL shown in the console
3. **Load Data**: Click "Load Frequency Data" to load the CSV file via the API
4. **Generate Numbers**: Click "Generate Lotto Numbers" to create a new set of numbers

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/lotto/load-data` | Load frequency data from CSV file |
| GET | `/api/lotto/frequency-data` | Get currently loaded frequency data |
| POST | `/api/lotto/generate` | Generate a new row of lotto numbers |

### Understanding the Code

The application follows a typical ASP.NET Core architecture:

1. **LottoService**: Core business logic
   - Loads CSV data from the file system
   - Implements weighted random selection algorithm
   - Generates unique, sorted number sets

2. **LottoController**: API endpoints
   - Handles HTTP requests
   - Returns JSON responses
   - Manages error handling

3. **Frontend (wwwroot/)**: Static web application
   - Calls the API endpoints
   - Displays frequency data and generated numbers
   - Provides responsive UI

### Customizing the Application

#### Modify Number Generation

Edit `Services/LottoService.cs` to change generation parameters:

```csharp
// In the LottoService class
private readonly int _numbersPerRow = 6;  // Change number of balls per draw
private readonly int _maxAttempts = 1000; // Change max attempts for unique selection
```

#### Update Frequency Data

Edit `data/lotto_frequency.csv` to use your own historical data:

```csv
number,frequency
1,45
2,38
...
```

Format requirements:
- Header row: `number,frequency`
- Each row: `number` (integer) and `frequency` (integer)

#### Customize Styling

Edit `wwwroot/style.css` to change:
- Color schemes (look for gradient definitions)
- Layout and spacing
- Responsive breakpoints
- Animation effects

## Configuration

### Application Settings

Configure the application in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Launch Settings

Development settings in `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

### Running on a Different Port

```bash
# Using command line
dotnet run --urls "http://localhost:3000"

# Or set environment variable
ASPNETCORE_URLS=http://localhost:3000 dotnet run
```


## Troubleshooting

### Common Issues

#### Application Won't Start

```bash
# Check if .NET SDK is installed
dotnet --version

# If not installed, download from https://dotnet.microsoft.com/download

# Restore packages
dotnet restore

# Rebuild
dotnet build
```

#### Port Already in Use

```bash
# Use a different port
dotnet run --urls "http://localhost:3001"

# Or find and kill the process using the port
# On Linux/Mac:
lsof -i :5000
kill -9 <PID>

# On Windows:
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

#### CSV File Won't Load

```bash
# Verify the data file exists
ls -la data/lotto_frequency.csv

# Check file permissions (Unix/Linux/Mac)
chmod 644 data/lotto_frequency.csv

# Verify CSV format
head data/lotto_frequency.csv
```

#### Numbers Not Generating

1. **Check Console**: Open browser Developer Tools (F12) and check the Console tab for errors
2. **Verify Data Loaded**: Make sure you clicked "Load Frequency Data" first
3. **Check API**: Test the API directly: `curl -X POST http://localhost:5000/api/lotto/load-data`

### Browser Compatibility

The application works best on modern browsers:
- ✅ Chrome/Chromium (version 90+)
- ✅ Firefox (version 88+)
- ✅ Safari (version 14+)
- ✅ Edge (version 90+)

### Running Tests

```bash
# Run all tests
cd LottoApplication.Tests
dotnet test

# Run with verbose output
dotnet test --logger "console;verbosity=detailed"

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Development Tips

1. **Use Hot Reload**: Run `dotnet watch run` for automatic rebuilding on changes
2. **Use Browser DevTools**: Press F12 to open Developer Tools for debugging
3. **Check Console Logs**: The app logs useful information to the console
4. **Inspect Network Requests**: Use the Network tab to verify API calls

### Getting Help

If you encounter issues not covered in this guide:

1. Check the [GitHub Issues](https://github.com/Bejap/Lotto-Application/issues) for existing problems
2. Review the browser console for error messages
3. Check the application console for server-side errors
4. Create a new issue with:
   - Description of the problem
   - Steps to reproduce
   - Browser and OS information
   - Console error messages (if any)
   - Screenshots (if relevant)

## Additional Information

### Performance Considerations

- The application uses a singleton service for frequency data
- CSV file is loaded once and cached in memory
- Number generation is instant using weighted random selection
- Static files are served efficiently by ASP.NET Core

### Security Notes

- This is a demonstration application
- CSV data is loaded from the local filesystem
- API endpoints do not require authentication
- No user data is collected or stored

### Future Enhancements

Potential improvements for contributors:

- Add ability to upload custom CSV files via API
- Implement different lottery game types (Powerball, Mega Millions, etc.)
- Add data visualization (charts showing frequency distribution)
- Export generated numbers to CSV
- Save favorite combinations to database
- Add statistics dashboard with hot/cold number analysis
- Add user authentication
- Deploy to cloud platforms (Azure, AWS, etc.)

## Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)
- [C# Documentation](https://docs.microsoft.com/dotnet/csharp/)
- [GitHub Repository](https://github.com/Bejap/Lotto-Application)

## Next Steps

After completing the setup:

1. ✅ Start the application with `dotnet run`
2. ✅ Open the application in your browser
3. ✅ Click "Load Frequency Data" to load the CSV
4. ✅ Click "Generate Lotto Numbers" to test the functionality
5. 📖 Read the [README.md](README.md) to understand the algorithm
6. 🛠️ Explore the code in `Services/LottoService.cs` to see how it works
7. 🎨 Customize `wwwroot/style.css` to change the appearance
8. 📊 Modify `data/lotto_frequency.csv` with your own data
9. 🧪 Run the tests with `dotnet test`

---

**For questions, support, or contributions, please visit the [GitHub repository](https://github.com/Bejap/Lotto-Application) or open an issue.**
