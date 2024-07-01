@SETLOCAL
@CALL .\_BatchFileConfig.bat

nant.exe -buildfile:ScriptLib.Build.xml DB.RestoreData.Representative

SET /P variable="Hit Enter to continue."
