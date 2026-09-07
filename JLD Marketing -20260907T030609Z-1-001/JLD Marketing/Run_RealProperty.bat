@echo off
title Real Property System Launcher
echo ========================================================
echo          Starting Real Property System (JLD)
echo ========================================================
echo.

:: 1. Check if MariaDB/MySQL is running on port 3306
netstat -ano | findstr :3306 | findstr LISTENING >nul
if %errorlevel% neq 0 (
    echo Starting MariaDB database server...
    start "" "C:\xampp\mysql\bin\mysqld.exe" --defaults-file="C:\xampp\mysql\bin\my.ini" --standalone
    ping 127.0.0.1 -n 4 >nul
) else (
    echo Database server is already active on port 3306.
)

:: 2. Launch RealProperty.exe from project output
echo Starting Real Property System application...
cd /d "%~dp0System to Fix\RealProperty\RealProperty\bin\Debug\net48"
if not exist "RealProperty.exe" (
    cd /d "c:\Users\Kayeen Campana\JLD Subdivision\JLD Marketing -20260907T030609Z-1-001\JLD Marketing\System to Fix\RealProperty\RealProperty\bin\Debug\net48"
)
start "" "RealProperty.exe"

echo Application launched successfully!
ping 127.0.0.1 -n 2 >nul
exit
