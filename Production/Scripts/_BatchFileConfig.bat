:: <summary>
:: This script configures certain common environment settings (and any other prerequisite configuration settings) that other BAT scripts require in order to execute properly.
:: </summary>
:: <remarks>
:: <para>
:: This script performs the following actions/operations:
:: 1. Attempts to update the PATH such that NAnt.exe can be called directly (i.e. without the caller having to specify the exe's path).
:: 2. Initializes certain custom variables that are used by/within some of the scripts which call this script.
:: </para>
:: <para>
:: The primary purposes of this script are to centralize and hide certain common initialization and setup operations 
:: that would otherwise need to be embedded directly (and redundantly) within multiple script files.
:: Delegating these operations to a single utility script reduces the complexity of the rest of the scripts 
:: and also increases the maintainability of those scripts.
:: </para>
:: <para>
:: This BAT script is only intended to be called from other BAT scripts.
:: </para>
:: </remarks>

@echo off

:: Intentionally not calling SETLOCAL, because this BAT file is intended to set environment variables on behalf of other BAT files
:: SETLOCAL

:Step1
	:: 1. Attempts to update the PATH such that NAnt.exe can be called directly (i.e. without the caller having to specify the exe's path).
	SET NANT_PATH=.
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	SET NANT_PATH=..\..\..\Build\nant\bin
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	SET NANT_PATH=c:\nant\bin
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	SET NANT_PATH=E:\CommonApps\PRODUCTION\nAnt\bin
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	SET NANT_PATH=d:\nant\bin
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	SET NANT_PATH=c:\Program Files\nant\bin
	IF EXIST "%NANT_PATH%\nant.exe" GOTO :NAntFound
	GOTO :NAntNotFound

	:NAntFound
	SET PATH=%NANT_PATH%;%PATH%
	::ECHO NAnt.exe was found at %NANT_PATH%
	::nant.exe -help
	GOTO :Step2

	:NAntNotFound
	GOTO :Step2

:Step2
	::Not currently implemented
	GOTO :End

:End
