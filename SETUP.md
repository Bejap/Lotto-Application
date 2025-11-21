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

Before setting up the Lotto Application, ensure you have ONE of the following installed on your system:

**Option 1: Node.js (Recommended)**
- **Node.js** (version 14.x or higher)
  - Download from [https://nodejs.org/](https://nodejs.org/)
  - Verify installation: `node --version`
  - npm is included with Node.js

**Option 2: Python**
- **Python** (version 3.x or 2.7)
  - Download from [https://www.python.org/downloads/](https://www.python.org/downloads/)
  - Verify installation: `python --version` or `python3 --version`

**Optional:**
- **Git** (version 2.x or higher) - for version control
- **Code Editor** (recommended):
  - Visual Studio Code
  - Sublime Text
  - Atom
  - Any text editor of your choice

## Quick Start

The fastest way to get started:

```bash
# Clone the repository
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application

# Start the server (Node.js)
npm start

# Or using Python
python3 -m http.server 8000
```

Then open your browser to `http://localhost:8000`


## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application
```

### 2. No Dependencies Required

This is a static web application - no additional dependencies need to be installed! The application runs entirely in the browser using vanilla HTML, CSS, and JavaScript.

However, if you want to use the Node.js development server:

```bash
# Optional: Initialize npm (if package.json doesn't exist)
npm init -y

# The application has no external dependencies
# but you can install dev tools if needed
```

## Running the Application

There are multiple ways to run the Lotto Application:

### Method 1: Using Node.js (Recommended)

The project includes a simple Node.js server for development:

```bash
# Start the built-in server
npm start

# Or run directly
node server.js
```

The application will be available at `http://localhost:8000`

### Method 2: Using Python

If you have Python installed:

```bash
# Using Python 3
python3 -m http.server 8000

# Using Python 2
python -m SimpleHTTPServer 8000
```

Then open your browser to `http://localhost:8000`

### Method 3: Using npx (No installation required)

If you have npm/npx installed:

```bash
npx http-server -p 8000 -o
```

This will automatically open your browser to the application.

### Method 4: Direct File Opening (Limited Functionality)

You can open `index.html` directly in your browser, but this may cause CORS issues when loading the CSV file. Using a local server (methods 1-3) is recommended.

## Project Structure

```
Lotto-Application/
├── index.html              # Main HTML file - application entry point
├── style.css               # Application styles and responsive design
├── app.js                  # JavaScript logic for number generation
├── server.js              # Simple Node.js development server
├── package.json           # Node.js project configuration
├── data/                  # Data folder
│   └── lotto_frequency.csv # Frequency data file
├── README.md             # Project overview and features
├── SETUP.md              # This file - setup instructions
└── .gitignore            # Git ignore patterns
```

### Key Files

- **index.html**: The main entry point of the application
- **app.js**: Contains the `LottoApp` class with all the application logic
- **style.css**: All styling including responsive design for mobile
- **data/lotto_frequency.csv**: Historical frequency data used for weighted number generation
- **server.js**: Development server (optional, for convenience)

## Development

### Using the Application

1. **Start the Server**: Use any of the methods above to start a local server
2. **Open in Browser**: Navigate to `http://localhost:8000`
3. **Load Data**: Click "Load Frequency Data" to load the CSV file
4. **Generate Numbers**: Click "Generate Lotto Numbers" to create a new set of numbers

### Understanding the Code

The application consists of a single `LottoApp` class that:

1. **Loads CSV Data**: Reads the frequency data from `data/lotto_frequency.csv`
2. **Parses Data**: Converts CSV into a usable format
3. **Generates Numbers**: Uses weighted probability based on frequency
4. **Displays Results**: Shows generated numbers with timestamps

### Customizing the Application

#### Modify Number Generation

Edit `app.js` to change generation parameters:

```javascript
// In the LottoApp constructor
this.numbersPerRow = 6;  // Change number of balls per draw
this.maxAttempts = 1000; // Change max attempts for unique selection
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

Edit `style.css` to change:
- Color schemes (look for gradient definitions)
- Layout and spacing
- Responsive breakpoints
- Animation effects

## Configuration

### Server Port

To change the server port, set the `PORT` environment variable:

```bash
# Unix/Mac/Linux
PORT=3000 npm start

# Windows Command Prompt
set PORT=3000 && npm start

# Windows PowerShell
$env:PORT=3000; npm start
```

Or edit `server.js` directly:

```javascript
const PORT = process.env.PORT || 8000; // Change 8000 to your preferred port
```

### Application Settings

The application uses no external configuration files. All settings are in `app.js`:

```javascript
// Number of lotto balls to generate
this.numbersPerRow = 6;

// Maximum attempts to find unique numbers
this.maxAttempts = 1000;
```


## Troubleshooting

### Common Issues

#### Server Won't Start

**Node.js server:**
```bash
# Check if Node.js is installed
node --version

# If not installed, download from https://nodejs.org/

# Check if port is already in use
# On Unix/Linux/Mac:
lsof -i :8000

# On Windows:
netstat -ano | findstr :8000

# Use a different port
PORT=3000 npm start
```

**Python server:**
```bash
# Check Python installation
python --version
python3 --version

# Try with python3 explicitly
python3 -m http.server 8000

# Or try Python 2 syntax
python -m SimpleHTTPServer 8000
```

#### CSV File Won't Load

**CORS Issues:**
- **Problem**: Opening `index.html` directly in browser causes CORS errors
- **Solution**: Always use a local server (Node.js, Python, or npx)
- **Error Message**: "CORS policy: Cross origin requests are only supported for protocol schemes..."

**File Path Issues:**
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
3. **Check CSV Format**: Ensure the CSV has the correct format with `number,frequency` header

#### Port Already in Use

```bash
# Find process using the port
# Unix/Linux/Mac:
lsof -i :8000
kill -9 <PID>

# Windows:
netstat -ano | findstr :8000
taskkill /PID <PID> /F

# Or use a different port
PORT=3001 npm start
```

#### Application Looks Broken

1. **Clear Browser Cache**: Ctrl+Shift+R (or Cmd+Shift+R on Mac)
2. **Check All Files Loaded**: Open Network tab in Developer Tools
3. **Verify File Paths**: Ensure `style.css` and `app.js` are in the same directory as `index.html`

### Browser Compatibility

The application works best on modern browsers:
- ✅ Chrome/Chromium (version 90+)
- ✅ Firefox (version 88+)
- ✅ Safari (version 14+)
- ✅ Edge (version 90+)

**Note**: Internet Explorer is not supported (uses ES6+ JavaScript features)

### Development Tips

1. **Use Browser DevTools**: Press F12 to open Developer Tools for debugging
2. **Check Console Logs**: The app logs useful information to the console
3. **Inspect Network Requests**: Use the Network tab to verify CSV file loading
4. **Test Responsive Design**: Use DevTools device emulation to test mobile views

### Getting Help

If you encounter issues not covered in this guide:

1. Check the [GitHub Issues](https://github.com/Bejap/Lotto-Application/issues) for existing problems
2. Review the browser console for error messages
3. Verify all files are in the correct locations
4. Ensure you're using a local server (not opening files directly)
5. Create a new issue with:
   - Description of the problem
   - Steps to reproduce
   - Browser and OS information
   - Console error messages (if any)
   - Screenshots (if relevant)

## Additional Information

### Performance Considerations

- The application runs entirely in the browser - no backend required
- CSV file is loaded once and cached in memory
- Number generation is instant using weighted random selection
- Suitable for datasets with thousands of numbers

### Security Notes

- This is a client-side application with no server-side data processing
- CSV data is loaded from the local filesystem
- No external API calls or data transmission
- No user data is collected or stored

### Future Enhancements

Potential improvements for contributors:

- Add ability to upload custom CSV files
- Implement different lottery game types (Powerball, Mega Millions, etc.)
- Add data visualization (charts showing frequency distribution)
- Export generated numbers to CSV
- Save favorite combinations to localStorage
- Add statistics dashboard with hot/cold number analysis
- Mobile app version (React Native or Progressive Web App)

## Resources

- [Node.js Documentation](https://nodejs.org/docs/)
- [MDN Web Docs - JavaScript](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
- [MDN Web Docs - Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API)
- [GitHub Repository](https://github.com/Bejap/Lotto-Application)

## Next Steps

After completing the setup:

1. ✅ Start the local server using your preferred method
2. ✅ Open the application in your browser
3. ✅ Click "Load Frequency Data" to load the CSV
4. ✅ Click "Generate Lotto Numbers" to test the functionality
5. 📖 Read the [README.md](README.md) to understand the algorithm
6. 🛠️ Explore the code in `app.js` to see how it works
7. 🎨 Customize `style.css` to change the appearance
8. 📊 Modify `data/lotto_frequency.csv` with your own data

---

**For questions, support, or contributions, please visit the [GitHub repository](https://github.com/Bejap/Lotto-Application) or open an issue.**
