@echo off
SETLOCAL

REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' Modified by JW on 4/24/08.
REM '' Modified by JW on 6/26/08.
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
REM '' This file was created based upon this article:
REM '' http://www.hanselman.com/blog/ManagingMultipleConfigurationFileEnvironmentsWithPreBuildEvents.aspx
REM '' 
REM '' Description: This script will synchronize the state (i.e. existence and/or content) of a target file to that of a "source" file.
REM '' 
REM '' Basically this batch file will:
REM '' 1. Delete the destination file if the source file isn't found
REM '' 2. Create the destination file (by copying the source file) if the destination file isn't found
REM '' 3. Replace the destination file with the source file if they're different
REM '' 4. Do nothing if both exist and are the same.
REM '' 5. Do nothing if neither exist.
REM '' 
REM '' Since modifying a config file can cause VS.NET, IIS, etc. to restart apps (and other side effects), 
REM '' this batch file will prevents the destination file from getting "modified" unless there is truly a difference.
REM '' 
REM '' NOTE: The reason for behavior #1 is to try to force a compile or runtime error 
REM '' in the event that a necessary config file's source file (i.e. for a specific build configuration) doesn't exist.
REM '' 
REM '' NOTE 2: The output (i.e. echos) from this bat file are optimized for compatibility/informative value when called 
REM '' as part of a Visual Studio.NET Pre/Post-Build event..
REM '' 
REM ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

SET COPY_FROM=%1
SET COPY_TO=%2

REM echo Comparing two files: %COPY_FROM% with %COPY_TO%

if not exist %COPY_FROM% goto File1NotFound
if not exist %COPY_TO% goto File2NotFound

REM fc /LB1 %COPY_FROM% %COPY_TO% > NUL
fc /B %COPY_FROM% %COPY_TO% > NUL
if %ERRORLEVEL%==0 GOTO NoCopy

echo Replacing %COPY_TO% with %COPY_FROM%
xcopy /Q /Y /R /O /K %COPY_FROM% %COPY_TO% & goto END

:NoCopy
	REM echo Files are the same.  Did nothing
	goto END

:File1NotFound
	if not exist %COPY_TO% goto END

	echo %COPY_FROM% not found.
	echo Deleting %COPY_TO%
	del /Q /F %COPY_TO%
	goto END

:File2NotFound
	echo Copying %COPY_FROM% to %COPY_TO%
	echo F|xcopy /Q /Y /R /O /K %COPY_FROM% %COPY_TO%
	goto END

:END
	REM echo Done.
	