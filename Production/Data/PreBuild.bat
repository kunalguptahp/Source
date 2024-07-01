
@echo off
SETLOCAL

@REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
@REM '' Modified by JW on 6/19/08.
@REM '' Modified by JW on 6/26/08.
@REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
@REM '' This file was created based upon this article:
@REM '' http://www.hanselman.com/blog/ManagingMultipleConfigurationFileEnvironmentsWithPreBuildEvents.aspx
@REM '' 
@REM '' Basically this batch file will:
@REM '' 1. (Re)Generate all of the SubSonic-generated SubSonicClient DAL files into a temporary location
@REM '' 2. Compare the newly generated files to any existing (i.e. old) generated files in the output folder
@REM '' 3. If the comparison shows any differences, it will add/replace/remove files in the output folder
@REM '' 
@REM '' Since modifying a config file can cause VS.NET, IIS, etc. to restart apps (and other side effects), 
@REM '' this batch file uses the temporary location/compare strategy to prevent the generated files from getting 
@REM '' "modified" unless there is truly a difference.
@REM '' 
@REM '' NOTE: This bat file is intended to be called as a Pre-Build Event of the DAL project. Sample command line:
@REM ''       $(SolutionDir)CodeGen.Data.SubSonicClient.bat $(SolutionDir)
@REM '' 
@REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

:Start
	echo.
	echo     ------ START: (Re)Generating SubSonic DAL files ------

:EnvConstants
	SET SOURCE_PATH=%1
	@REM Remove trailing slash from SOURCE_PATH
	if "%SOURCE_PATH:~-1%"=="\" SET SOURCE_PATH=%SOURCE_PATH:~0,-1%

	rem SET TRUNK_PATH=%SOURCE_PATH%\..
	

:DerivedEnvConstants
	SET DAL_PATH=%SOURCE_PATH%\Production\Data
	SET TEMP_PATH=%SOURCE_PATH%\temp
	SET DAL_TEMP_PATH=%TEMP_PATH%\Production\Data
	SET DAL_TEMP_OUTPUT_PATH=%DAL_TEMP_PATH%\Generated
	SET DAL_OUTPUT_PATH=%DAL_PATH%\Generated
	SET DAL_CONFIG_PATH=%DAL_PATH%\app.config
	SET DAL_TEMP_CLASSES_FILEMASK=%DAL_TEMP_OUTPUT_PATH%\*.generated.cs
	SET DAL_CLASSES_FILEMASK=%DAL_OUTPUT_PATH%\*.generated.cs

	SET SYNCFILEBAT_PATH=%SOURCE_PATH%\SynchronizeFile.bat
	SET SUBSONIC_PATH=%SOURCE_PATH%\Dependencies\SubSonic
	SET SUBCOMMANDER_PATH=%SUBSONIC_PATH%\SubCommander
	SET SONIC_EXE=%SUBCOMMANDER_PATH%\Sonic.exe
	SET SUBSONIC_TEMPLATE_PATH=%SUBSONIC_PATH%\Templates

:DoGenerateDAL
	@REM delete the temporary output folder (even though it shouldn't be there)
	rmdir /s /q "%DAL_TEMP_PATH%\"

	echo.
	echo     ------ Calling Sonic.exe ------
	@REM (Re)Generate all of the DAL files in the temporary output folder
	"%SONIC_EXE%" generate /config "%DAL_CONFIG_PATH%" /out "%DAL_TEMP_OUTPUT_PATH%" /templateDirectory "%SUBSONIC_TEMPLATE_PATH%"

	echo.
	echo     ------ Renaming generated files ------
	@REM First, delete any existing *.generated.cs files in the output folder (even though none should be there)
	if exist "%DAL_TEMP_CLASSES_FILEMASK%" del "%DAL_TEMP_CLASSES_FILEMASK%"

	@REM Second, change the file extension of all the *.cs files in the temporary location to *.generated
	for %%F in (%DAL_TEMP_OUTPUT_PATH%\*.cs) do call rename "%%~fF" "%%~nF.generated"

	@REM Third, change the file extension of all the *.generated files in the temporary location to *.generated.cs
	for %%F in (%DAL_TEMP_OUTPUT_PATH%\*.generated) do call rename "%%~fF" "%%~nxF.cs"

	echo.
	echo     ------ Updating modified SubSonic DAL files ------
	@REM If there are any differences, sync the DAL files in the output folder to the temporary output folder
	@REM First, sync (i.e. replace or delete) all of the existing files in the output folder
	for %%F in (%DAL_CLASSES_FILEMASK%) do call %SYNCFILEBAT_PATH% "%DAL_TEMP_OUTPUT_PATH%\%%~nxF" "%%~fF"

	@REM Second, sync (i.e. add/copy) any files that exist only in the temporary location
	for %%F in (%DAL_TEMP_CLASSES_FILEMASK%) do call %SYNCFILEBAT_PATH% "%%~fF" "%DAL_OUTPUT_PATH%\%%~nxF"

	echo.
	echo     ------ Removing temporary files ------
	@REM delete the temporary output folder
	rmdir /s /q "%DAL_TEMP_PATH%\"
	rmdir /s /q "%TEMP_PATH%\"

	@REM Finally, delete any existing files in the temp output folder (in case the following rmdir fails)
	if exist "%DAL_TEMP_PATH%" del /s /q "%DAL_TEMP_PATH%\*.*"
	
	@REM HACK: delete the copy of the temporary output folder from the bin\CIBuild folder (not sure how it gets there, but it does)
	if exist "%DAL_PATH%\bin\CIBuild\Generated\" rmdir /s /q "%DAL_PATH%\bin\CIBuild\Generated\"

:End
	echo.
	echo     ------ DONE: (Re)Generating SubSonic DAL files ------
	echo.
rem 	pause
