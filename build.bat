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
call nuget Install RabbitBus.Serialization.Json -Version 1.3.3 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Topshelf.Log4Net -Version 3.0.2 -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease
call nuget Install Autofac -Version 2.6.2.859 -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease
call nuget Install Autofac.Mvc4 -Version 2.6.2.859 -o %LIB_PATH% -Source %FEED% -ExcludeVersion -Prerelease

REM Standard MVC Packages
call nuget Install jQuery -Version 1.6.2 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install jQuery.UI.Combined -Version 1.8.11 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install jQuery.Validation -Version 1.8.1 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install knockoutjs -Version 2.0.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.Mvc -Version 4.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.Providers -Version 1.1 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.Providers.Core -Version 1.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.Razor -Version 2.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.Web.Optimization -Version 1.0.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.WebApi -Version 4.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.WebApi.Client -Version 4.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.WebApi.Core -Version 4.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.WebApi.WebHost -Version 4.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.AspNet.WebPages -Version 2.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.jQuery.Unobtrusive.Ajax -Version 2.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.jQuery.Unobtrusive.Validation -Version 2.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.Net.Http -Version 2.0.20505.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Microsoft.Web.Infrastructure -Version 1.0.0.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Modernizr -Version 2.0.6 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install Newtonsoft.Json -Version 4.5.1 -o %LIB_PATH% -Source %FEED% -ExcludeVersion
call nuget Install WebGrease -Version 1.0.0 -o %LIB_PATH% -Source %FEED% -ExcludeVersion

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
