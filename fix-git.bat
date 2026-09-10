@echo off
echo Fixing Git index...
del /f /q .git\index 2>nul
git reset
echo Git index has been completely restored!
