#!/bin/bash
# Lotto Application Startup Script
# This script automatically detects available tools and starts the server

echo "=========================================="
echo "🎰 Lotto Application Startup"
echo "=========================================="
echo ""

# Function to check if a command exists
command_exists() {
    command -v "$1" >/dev/null 2>&1
}

# Function to check if a port is in use
port_in_use() {
    lsof -i :"$1" >/dev/null 2>&1 || netstat -an 2>/dev/null | grep -E ":$1[[:space:]]" | grep -q LISTEN
}

# Default port
PORT=8000

# Check if port is already in use
if port_in_use $PORT; then
    echo "⚠️  Port $PORT is already in use."
    echo "   Finding an available port..."
    PORT=8001
    while port_in_use $PORT; do
        PORT=$((PORT + 1))
    done
    echo "✅ Using port $PORT instead"
    echo ""
fi

# Try Node.js first
if command_exists node; then
    echo "✅ Node.js detected"
    echo "🚀 Starting server on port $PORT..."
    echo ""
    PORT=$PORT node server.js
    exit 0
fi

# Try Python 3
if command_exists python3; then
    echo "✅ Python 3 detected"
    echo "🚀 Starting server on port $PORT..."
    echo ""
    cd "$(dirname "$0")"
    python3 -m http.server $PORT
    exit 0
fi

# Try Python 2
if command_exists python; then
    echo "✅ Python detected"
    echo "🚀 Starting server on port $PORT..."
    echo ""
    cd "$(dirname "$0")"
    python -m SimpleHTTPServer $PORT
    exit 0
fi

# No suitable server found
echo "❌ Error: No suitable server found!"
echo ""
echo "Please install one of the following:"
echo "  • Node.js: https://nodejs.org/"
echo "  • Python: https://www.python.org/"
echo ""
echo "Or use npx (no installation required):"
echo "  npx http-server -p $PORT"
echo ""
exit 1
