@SETLOCAL
@CALL .\_BatchFileConfig.bat

::For DEMO and QA environments
nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=C:\inetpub\wwwroot\ElementsCPS -D:Private.IIS.Setup.ServicesRootPhysicalPath=C:\inetpub\wwwroot\ElementsCPS\Services

SET /P variable="Hit Enter to continue."
