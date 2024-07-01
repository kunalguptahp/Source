@echo off
SETLOCAL

REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' Modified by JW on 5/05/10.
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' Description: This script performs the Pre-Build Event logic for the app.
REM '' 
REM '' Basically this batch file will do the following (depending upon the configuration specified):
REM '' 1. Update the app's appSettings.config file.
REM '' 2. Update the app's log4net.config file.
REM '' 3. Update the app's connectionStrings.config file.
REM '' 
REM '' NOTE: This file is intended to be called as a Pre-Build Event target.
REM '' 
REM '' NOTE 2: The output (i.e. echos) from this bat file are optimized for compatibility/informative value when called 
REM '' as part of a Visual Studio.NET Pre/Post-Build event.
REM '' 
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

:Start
	echo.
	echo     ------ START: (Re)Configuring Application ------

	if "%2"=="" goto Help

:EnvConstants
	SET CONFIG_NAME=%1
	if "%CONFIG_NAME%"=="" SET CONFIG_NAME=NONE
	SET APP_PATH=%2
	@REM Remove trailing slash from APP_PATH
	if "%APP_PATH:~-1%"=="\" SET APP_PATH=%APP_PATH:~0,-1%

:DerivedEnvConstants
	SET APPCONFIGDIR_PATH=%APP_PATH%
	SET SYNCFILEBAT_PATH=%APP_PATH%\..\..\SynchronizeFile.bat
	SET SYNCFILEIFSOURCEFILEEXISTSBAT_PATH=%APP_PATH%\..\..\SynchronizeFileIfSourceFileExists.bat

:ValidateArgs
	echo.
	echo     ------ Validating arguments ------
	REM If the appSettings.ConfigName.config file doesn't exist for the specified config name, it is an invalid config name.
	if not exist "%APPCONFIGDIR_PATH%\appSettings.%CONFIG_NAME%.config" SET CONFIG_NAME=NONE
	echo Config Name: %CONFIG_NAME%
	echo Source Path: %SOURCE_PATH%

:DoUpdateConfigFiles
		echo.
		echo     ------ Configuring: appSettings.config ------
		REM Replace appSettings.config with the environment-specific version
		CALL "%SYNCFILEBAT_PATH%" "%APPCONFIGDIR_PATH%\appSettings.%CONFIG_NAME%.config" "%APPCONFIGDIR_PATH%\appSettings.config"
		if errorlevel 1 goto Help
		REM Replace the "config name"-specific config with the "local" version (if one exists)
		CALL "%SYNCFILEIFSOURCEFILEEXISTSBAT_PATH%" "%APPCONFIGDIR_PATH%\appSettings.local.config" "%APPCONFIGDIR_PATH%\appSettings.config"
		if errorlevel 1 goto Help

		echo.
		echo     ------ Configuring: log4net.config ------
		REM Replace log4net.config with the environment-specific version
		CALL "%SYNCFILEBAT_PATH%" "%APPCONFIGDIR_PATH%\log4net.%CONFIG_NAME%.config" "%APPCONFIGDIR_PATH%\log4net.config"
		if errorlevel 1 goto Help
		REM Replace the "config name"-specific config with the "local" version (if one exists)
		CALL "%SYNCFILEIFSOURCEFILEEXISTSBAT_PATH%" "%APPCONFIGDIR_PATH%\log4net.local.config" "%APPCONFIGDIR_PATH%\log4net.config"
		if errorlevel 1 goto Help

		echo.
		echo     ------ Configuring: connectionStrings.config ------
		REM Replace connectionStrings.config with the environment-specific version
		CALL "%SYNCFILEBAT_PATH%" "%APPCONFIGDIR_PATH%\connectionStrings.%CONFIG_NAME%.config" "%APPCONFIGDIR_PATH%\connectionStrings.config"
		if errorlevel 1 goto Help
		REM Replace the "config name"-specific config with the "local" version (if one exists)
		CALL "%SYNCFILEIFSOURCEFILEEXISTSBAT_PATH%" "%APPCONFIGDIR_PATH%\connectionStrings.local.config" "%APPCONFIGDIR_PATH%\connectionStrings.config"
		if errorlevel 1 goto Help

		REM Exit properly because the build will not fail
		REM Unless the final step exits with an error code
		goto End

:Help
	echo.
	echo     ------ ERROR ------
	echo ERROR: Invalid operation! Unable to complete configuration.
	echo.
	echo Usage syntax:
	echo     PreBuild.bat {ConfigName} {PathToAppFolder}
	echo Usage example:
	echo     PreBuild.bat CIDebug .
	echo Usage example:
	echo     PreBuild.bat CIDebug "C:\...\Tests\ProjectName.Tests"
	goto End

:End
	echo.
	echo     ------ DONE: (Re)Configuring Application ------
	echo.
rem 	pause
