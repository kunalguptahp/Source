@SETLOCAL
@CALL .\_BatchFileConfig.bat

nant.exe -buildfile:ScriptLib.Build.xml DB.Rebuild

SET /P variable="Hit Enter to continue."
