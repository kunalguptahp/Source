@SETLOCAL
@CALL .\_BatchFileConfig.bat

::For standard LOCALDEV environments
nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=C:\SVN\Alpha\Dev\ElementsCPS\trunk\Source\Production\WebRoot -D:Private.IIS.Setup.ServicesRootPhysicalPath=C:\SVN\Alpha\Dev\ElementsCPS\trunk\Source\Production\Services

::For PROD and ITG environments
::nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=E:\CommonApps\PRODUCTION\wwwroot\ElementsCPS -D:Private.IIS.Setup.ServicesRootPhysicalPath=E:\CommonApps\PRODUCTION\wwwroot\ElementsCPS\Services

::For DEMO and QA environments
::nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=C:\inetpub\wwwroot\ElementsCPS -D:Private.IIS.Setup.ServicesRootPhysicalPath=C:\inetpub\wwwroot\ElementsCPS\Services 

SET /P variable="Hit Enter to continue."
