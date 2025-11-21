@echo off
REM Lotto Application Startup Script for Windows
REM This script automatically detects available tools and starts the server

echo ==========================================
echo 🎰 Lotto Application Startup
echo ==========================================
echo.

set PORT=8000

REM Check if port is in use (Windows)
netstat -an | findstr ":%PORT%" | findstr "LISTENING" >nul
if %errorlevel% equ 0 (
    echo ⚠️  Port %PORT% is already in use.
    echo    Finding an available port...
    set /a PORT=%PORT%+1
    goto :check_port
)
goto :start_server

:check_port
netstat -an | findstr ":%PORT%" | findstr "LISTENING" >nul
if %errorlevel% equ 0 (
    set /a PORT=%PORT%+1
    goto :check_port
)
echo ✅ Using port %PORT% instead
echo.

:start_server

REM Try Node.js first
where node >nul 2>nul
if %errorlevel% equ 0 (
    echo ✅ Node.js detected
    echo 🚀 Starting server on port %PORT%...
    echo.
    node server.js
    exit /b 0
)

REM Try Python 3
where python >nul 2>nul
if %errorlevel% equ 0 (
    python --version 2>&1 | findstr /C:"Python 3" >nul
    if %errorlevel% equ 0 (
        echo ✅ Python 3 detected
        echo 🚀 Starting server on port %PORT%...
        echo.
        python -m http.server %PORT%
        exit /b 0
    )
    
    REM Try Python 2 syntax
    echo ✅ Python detected
    echo 🚀 Starting server on port %PORT%...
    echo.
    python -m SimpleHTTPServer %PORT%
    exit /b 0
)

REM Try py launcher
where py >nul 2>nul
if %errorlevel% equ 0 (
    echo ✅ Python detected (via py launcher)
    echo 🚀 Starting server on port %PORT%...
    echo.
    py -m http.server %PORT%
    exit /b 0
)

REM No suitable server found
echo ❌ Error: No suitable server found!
echo.
echo Please install one of the following:
echo   • Node.js: https://nodejs.org/
echo   • Python: https://www.python.org/
echo.
echo Or use npx (no installation required):
echo   npx http-server -p %PORT%
echo.
pause
exit /b 1
