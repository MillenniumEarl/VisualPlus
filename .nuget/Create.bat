@echo off
Title VisualPlus Nuget Creator
color 17

echo This wizard will help you create a nuget package.
echo.

echo Creating...
%nuget% spec VisualPlus
pause