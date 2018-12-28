@echo off
Title VisualPlus NuGet Packer
color 17

echo This wizard will help you pack a NuGet package.
echo.

echo Creating the package...
%nuget% pack VisualPlus.nuspec
pause