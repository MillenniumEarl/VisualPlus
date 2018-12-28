@echo off
Title VisualPlus Nuget Packer
color 17

echo This wizard will help you pack a nuget package.
echo.

echo Creating the package...
%nuget% pack VisualPlus.nuspec
pause