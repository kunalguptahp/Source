@SETLOCAL
@CALL .\_BatchFileConfig.bat

::For PROD and ITG environments
nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=E:\CommonApps\PRODUCTION\wwwroot\ElementsCPS -D:Private.IIS.Setup.ServicesRootPhysicalPath=E:\CommonApps\PRODUCTION\wwwroot\ElementsCPS\Services

SET /P variable="Hit Enter to continue."
