@SETLOCAL
@CALL .\_BatchFileConfig.bat

nant.exe -buildfile:ScriptLib.Build.xml DB.RestoreData.Demo

SET /P variable="Hit Enter to continue."
