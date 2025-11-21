# Lotto-Application

A web-based lotto number generator that creates lottery numbers based on historical frequency data. The application uses weighted probability to generate numbers that are more likely to appear based on past draw frequencies.

## Features

- 🎰 Generate lotto numbers based on frequency data
- 📊 Display number frequency statistics
- 📈 Weighted probability selection algorithm
- 💾 CSV data file support
- 📱 Responsive design for mobile and desktop
- ⏱️ Timestamped generation history

## File Structure

```
Lotto-Application/
├── index.html              # Main HTML file
├── style.css               # Application styles
├── app.js                  # JavaScript logic
├── data/                   # Data folder
│   └── lotto_frequency.csv # Frequency data file
└── README.md              # This file
```

## How It Works

1. **Load Frequency Data**: The application reads a CSV file containing historical lotto number frequencies
2. **Weighted Selection**: Numbers with higher frequencies have a greater chance of being selected
3. **Generate Numbers**: Click the generate button to create a row of lotto numbers (typically 6 numbers)
4. **View Results**: Generated numbers are displayed with timestamps, keeping the last 5 generations

### Algorithm

The app uses a weighted random selection algorithm:
- Creates a pool where each number appears proportional to its frequency
- Randomly selects unique numbers from this weighted pool
- Ensures no duplicate numbers in a single row
- Sorts the final numbers in ascending order

## Usage

### Running the Application

1. **Simple HTTP Server**:
   ```bash
   # Using Python 3
   python3 -m http.server 8000
   
   # Using Python 2
   python -m SimpleHTTPServer 8000
   
   # Using Node.js (if you have http-server installed)
   npx http-server
   ```

2. Open your browser and navigate to `http://localhost:8000`

3. Click "Load Frequency Data" to load the CSV file

4. Click "Generate Lotto Numbers" to create a new row of numbers

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

1. Create or modify the CSV file in the `data/` folder
2. Follow the format: `number,frequency` (with header row)
3. The app will automatically load and use your data

## Browser Compatibility

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

Note: The app uses modern JavaScript features (ES6+) and the Fetch API.

## Future Enhancements

- Upload custom CSV files through the UI
- Multiple lotto game types (different number ranges)
- Statistics visualization (charts/graphs)
- Export generated numbers
- Save favorite number combinations
- Historical tracking of generated numbers

## License

This project is open source and available for personal and educational use.
## Description

The Lotto Application is an intelligent lottery number provider that generates optimized lotto row suggestions based on historical draw data analysis. This application leverages past lottery draw results to identify patterns, frequencies, and trends to help users make more informed number selections.

### Key Features

- **Historical Data Analysis**: Analyzes past lottery draw results to identify patterns and trends
- **Smart Number Generation**: Generates lotto row suggestions based on statistical analysis of historical data
- **Data-Driven Insights**: Provides insights into number frequencies, hot and cold numbers, and common combinations
- **Flexible Configuration**: Supports different lottery formats and configurations

### How It Works

1. **Data Collection**: Imports and stores historical lottery draw data
2. **Pattern Analysis**: Analyzes the historical data to identify:
   - Most frequently drawn numbers (hot numbers)
   - Least frequently drawn numbers (cold numbers)
   - Common number combinations
   - Temporal patterns and trends
3. **Number Generation**: Generates suggested lotto rows based on the analyzed patterns and user preferences
4. **Results Tracking**: Allows users to track generated numbers and compare against actual draw results

### Technology Stack

This application is designed to be extensible and can be implemented using various technologies depending on requirements.

### Getting Started

Instructions for installation, configuration, and usage will be added as the application is developed.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## License

This project is licensed under the MIT License.
