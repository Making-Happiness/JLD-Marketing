@echo off
title JLD Subdivision Dashboard Launcher
echo ========================================================
echo        Starting JLD Subdivision SaaS Admin Dashboard
echo ========================================================
echo.
cd /d "%~dp0jld-dashboard"
echo Starting Vite development server...
start "" http://localhost:5173/
npm run dev

