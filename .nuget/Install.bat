@echo off
Title VisualPlus Nuget Installer
color 17

echo This wizard will help you setup nuget on your system.
echo.

:: Copies the file to system directory
echo Copy to system install directory.
copy /b nuget.exe %SystemRoot%\System32
echo.

:: Update the system environment variable to the path
echo Update system environment variable path.
setx nuget "%SystemRoot%\System32\nuget.exe" /M
echo.

:: Check nuget for updates
echo Updates check.
%nuget% update -self

pause