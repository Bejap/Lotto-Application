# Getting Started with Lotto Application

## Quick Start Guide

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- A web browser (Chrome, Firefox, Safari, or Edge)

### Installation & Running

1. **Clone the repository**
   ```bash
   git clone https://github.com/Bejap/Lotto-Application.git
   cd Lotto-Application
   ```

2. **Build and run the application**
   ```bash
   # Build the application
   dotnet build

   # Run the application
   dotnet run
   ```

3. **Open your browser**
   - Navigate to `http://localhost:5000` (or the URL shown in the console)
   - The page will load automatically

4. **Use the application**
   - Click **"Load Frequency Data"** button (green)
   - Wait for "Successfully loaded 36 numbers" message
   - Click **"Generate Lotto Numbers"** button (purple)
   - View your generated numbers!
   - Generate as many times as you want

## What You'll See

The application displays:
- **Generated Numbers**: Your lotto numbers in circular badges, sorted in ascending order
- **Timestamp**: When each set was generated
- **History**: Last 5 generated sets are kept visible
- **Frequency Table**: All numbers with their historical frequencies
- **Success Messages**: Clear feedback on data loading and generation

## Features

✅ **Weighted Generation**: Numbers with higher historical frequencies have greater chance of selection  
✅ **No Duplicates**: Each generated set contains unique numbers  
✅ **Responsive Design**: Works on desktop, tablet, and mobile  
✅ **Clean UI**: Modern gradient design with smooth animations  
✅ **Real-time Updates**: Instant number generation with no page reload  
✅ **RESTful API**: Backend powered by ASP.NET Core Web API  

## Running Tests

```bash
# Navigate to the test project
cd LottoApplication.Tests

# Run all tests
dotnet test
```

## Troubleshooting

**Port already in use?**
```bash
# Configure a different port in Properties/launchSettings.json
# Or use the command line:
dotnet run --urls "http://localhost:3000"
```

**Application won't start?**
- Ensure .NET SDK is installed: `dotnet --version`
- Rebuild the application: `dotnet build`
- Check for errors in the console output

**Data won't load?**
- Ensure `data/lotto_frequency.csv` exists
- Check the browser console (F12) for errors
- Verify the API is running at `/api/lotto/load-data`

**Need help?**
- Check [SETUP.md](SETUP.md) for detailed instructions
- Review [README.md](README.md) for project information
- Open an issue on GitHub

## Next Steps

- Customize the frequency data in `data/lotto_frequency.csv`
- Modify number generation parameters in `Services/LottoService.cs`
- Update colors and styling in `wwwroot/style.css`
- Read the code to understand the weighted algorithm
- Explore the API endpoints at `/api/lotto/*`

Enjoy your lotto number generation! 🎰
