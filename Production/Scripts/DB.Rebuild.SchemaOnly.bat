@SETLOCAL
@CALL .\_BatchFileConfig.bat

nant.exe -buildfile:ScriptLib.Build.xml DB.Rebuild.SchemaOnly

SET /P variable="Hit Enter to continue."