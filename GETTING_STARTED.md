# Getting Started with Lotto Application

## Quick Start Guide

### Prerequisites
You need ONE of the following installed:
- Node.js (recommended) - [Download](https://nodejs.org/)
- Python 3 or Python 2 - [Download](https://www.python.org/)

### Installation & Running

1. **Clone the repository**
   ```bash
   git clone https://github.com/Bejap/Lotto-Application.git
   cd Lotto-Application
   ```

2. **Start the application** (choose one method)

   **Method 1: Auto-detect (Easiest)**
   ```bash
   # On macOS/Linux
   ./run.sh
   
   # On Windows
   run.bat
   ```

   **Method 2: Using npm**
   ```bash
   npm start
   ```

   **Method 3: Using Node.js**
   ```bash
   node server.js
   ```

   **Method 4: Using Python**
   ```bash
   python3 -m http.server 8000
   ```

3. **Open your browser**
   - Navigate to `http://localhost:8000`
   - The page will load automatically

4. **Use the application**
   - Click **"Load Frequency Data"** button (green)
   - Wait for "Successfully loaded 49 numbers" message
   - Click **"Generate Lotto Numbers"** button (purple)
   - View your generated numbers!
   - Generate as many times as you want

## What You'll See

The application displays:
- **Generated Numbers**: Your lotto numbers in circular badges, sorted in ascending order
- **Timestamp**: When each set was generated
- **History**: Last 5 generated sets are kept visible
- **Frequency Table**: All 49 numbers with their historical frequencies
- **Success Messages**: Clear feedback on data loading and generation

## Features

✅ **Weighted Generation**: Numbers with higher historical frequencies have greater chance of selection  
✅ **No Duplicates**: Each generated set contains unique numbers  
✅ **Responsive Design**: Works on desktop, tablet, and mobile  
✅ **Clean UI**: Modern gradient design with smooth animations  
✅ **Real-time Updates**: Instant number generation with no page reload  

## Troubleshooting

**Port already in use?**
```bash
# Use a different port
PORT=3000 npm start
# or
PORT=3000 node server.js
```

**CSV won't load?**
- Make sure you're using a local server (not opening index.html directly)
- Check that `data/lotto_frequency.csv` exists
- Look for errors in browser console (F12)

**Need help?**
- Check [SETUP.md](SETUP.md) for detailed instructions
- Review [README.md](README.md) for project information
- Open an issue on GitHub

## Next Steps

- Customize the frequency data in `data/lotto_frequency.csv`
- Modify number of balls in `app.js` (change `numbersPerRow`)
- Update colors and styling in `style.css`
- Read the code to understand the weighted algorithm

Enjoy your lotto number generation! 🎰
