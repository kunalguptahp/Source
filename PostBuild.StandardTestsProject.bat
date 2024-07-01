@echo off
SETLOCAL

REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' Modified by JW on 11/20/08.
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' Description: This script performs the Post-Build Event logic for the app.
REM '' 
REM '' Basically this batch file will do the following:
REM '' 1. Copies all project config files in the project's root into the output directory.
REM '' 
REM '' NOTE: This file is intended to be called as a Post-Build Event target.
REM '' 
REM '' NOTE 2: The output (i.e. echos) from this bat file are optimized for compatibility/informative value when called 
REM '' as part of a Visual Studio.NET Pre/Post-Build event.
REM '' 
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

:Start
	echo.

	if "%3"=="" goto Help

:DoCopyConfigFiles
	echo Copying all project config files into the output directory...
	copy %2*.config %2%3
	echo.

	REM Exit properly because the build will not fail
	REM Unless the final step exits with an error code
	goto End

:Help
	echo.
	echo     ------ ERROR ------
	echo ERROR: Invalid operation! Unable to complete configuration.
	echo.
	echo Usage syntax:
	echo     PostBuild.bat {ConfigName} {PathToAppFolder} {PathToAppOutputFolder}
	echo Usage example:
	echo     PostBuild.bat CIDebug . .\bin
	echo Usage example:
	echo     PostBuild.bat CIDebug "C:\...\Tests\ProjectName.Tests" "C:\...\Tests\ProjectName.Tests\bin"
	goto End

:End
