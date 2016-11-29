@echo off
dotnet restore
dotnet publish -c release -r win10-x86
REM dotnet publish -c release -r win10-x64
REM dotnet publish -c release -r ubuntu.14.04-x64