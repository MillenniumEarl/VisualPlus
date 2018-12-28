@echo off
Title VisualPlus NuGet Creator
color 17

echo This wizard will help you create a NuGet package.
echo.

echo Creating...
%nuget% spec VisualPlus
pause