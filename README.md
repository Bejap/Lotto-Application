# Lotto-Application

A .NET C# web application that generates lottery numbers based on historical frequency data. The application uses weighted probability to generate numbers that are more likely to appear based on past draw frequencies.

## Features

- 🎰 Generate lotto numbers based on frequency data
- 📊 Display number frequency statistics
- 📈 Weighted probability selection algorithm
- 💾 CSV data file support
- 📱 Responsive design for mobile and desktop
- ⏱️ Timestamped generation history
- 🔧 RESTful API endpoints

## Technology Stack

- .NET 10.0
- ASP.NET Core Web API
- xUnit for testing
- HTML/CSS/JavaScript frontend

## File Structure

```
Lotto-Application/
├── Controllers/
│   └── LottoController.cs      # API controller
├── Models/
│   ├── FrequencyData.cs        # Frequency data model
│   └── LottoResult.cs          # Lotto result model
├── Services/
│   └── LottoService.cs         # Business logic service
├── wwwroot/
│   ├── index.html              # Frontend HTML
│   ├── style.css               # Application styles
│   └── app.js                  # Frontend JavaScript
├── data/
│   └── lotto_frequency.csv     # Frequency data file
├── LottoApplication.Tests/
│   └── UnitTest1.cs            # Unit tests
├── Program.cs                  # Application entry point
├── LottoApplication.csproj     # Project file
└── README.md                   # This file
```

## How It Works

1. **Load Frequency Data**: The application reads a CSV file containing historical lotto number frequencies via the API
2. **Weighted Selection**: Numbers with higher frequencies have a greater chance of being selected
3. **Generate Numbers**: Click the generate button to create a row of lotto numbers (7 numbers)
4. **View Results**: Generated numbers are displayed with timestamps, keeping the last 5 generations

### Algorithm

The app uses a weighted random selection algorithm:
- Creates a pool where each number appears proportional to its frequency
- Randomly selects unique numbers from this weighted pool
- Ensures no duplicate numbers in a single row
- Sorts the final numbers in ascending order

## Quick Start

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later

### Running the Application

```bash
# Clone the repository
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application

# Restore dependencies and build
dotnet build

# Run the application
dotnet run

# Or run in development mode
dotnet run --launch-profile https
```

The application will start and be available at `http://localhost:5000` (or the configured port).

### Running Tests

```bash
# Run all tests
cd LottoApplication.Tests
dotnet test

# Run tests with verbose output
dotnet test --logger "console;verbosity=detailed"
```

### Using the Application

1. Open your browser and navigate to the application URL
2. Click **"Load Frequency Data"** to load the CSV file via the API
3. Click **"Generate Lotto Numbers"** to create a new row of numbers
4. View your generated numbers with timestamps
5. Generate as many rows as you want - the last 5 are kept visible

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/lotto/load-data` | Load frequency data from CSV file |
| GET | `/api/lotto/frequency-data` | Get currently loaded frequency data |
| POST | `/api/lotto/generate` | Generate a new row of lotto numbers |

### CSV Data Format

The frequency data file should be in CSV format with two columns:

```csv
number,frequency
1,45
2,38
3,52
...
```

- **number**: The lotto number (integer)
- **frequency**: How many times this number has appeared in historical draws (integer)

### Customizing the Data

To use your own frequency data:

1. Modify the CSV file in the `data/` folder
2. Follow the format: `number,frequency` (with header row)
3. Restart the application and reload the data

## Configuration

Application settings can be configured in `appsettings.json`:

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

## Browser Compatibility

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## Future Enhancements

- Upload custom CSV files through the UI
- Multiple lotto game types (different number ranges)
- Statistics visualization (charts/graphs)
- Export generated numbers
- Save favorite number combinations
- Historical tracking of generated numbers
- Database persistence

## License

This project is open source and available under the MIT License.
