@echo off
@echo ==================================================
@echo Building rabbitBusExamples
@echo ==================================================

@echo ==================================================
@echo Checking for nuget.exe in your PATH ...
@echo ==================================================

where nuget.exe 2>NUL
IF NOT %ERRORLEVEL%==0 goto NUGET_MISSING

set SCRIPT_DIR=%~dp0
set FEED=https://nuget.org/api/v2/
set LIB_PATH=%SCRIPT_DIR%\lib
set MSBUILD_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\

@echo ==================================================
@echo Retrieving NuGet packages ...
@echo ==================================================
call nuget Install RabbitBus.Serialization.Json -Version 1.3.3-alpha -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease
call nuget Install Topshelf.Log4Net -Version 3.0.2 -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease
call nuget Install Autofac -Version 2.6.3.862 -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease

@echo ==================================================
@echo "Compiling all solutions ..."
@echo ==================================================
for /f %%F in ('dir /s /b *.sln') do (
@echo ==================================================
@echo "Compiling %%~nF ..."
@echo ==================================================
%MSBUILD_PATH%\msbuild.exe /p:Configuration=DEBUG %%F
)
goto END

:NUGET_MISSING
@echo ==================================================
@echo                  E R R O R
@echo --------------------------------------------------
@echo.
@echo The NuGet.exe command line tool was not found.
@echo You can obtain the NuGet.exe tool from the
@echo following URL:
@echo.
@echo http://nuget.codeplex.com/releases/view/58939
@echo ==================================================

:ERROR
@echo ==================================================
@echo A Build error occurred.
@echo ==================================================

:END

pause
