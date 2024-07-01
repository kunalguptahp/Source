@SETLOCAL
@CALL .\_BatchFileConfig.bat

::For DEMO and QA environments
nant.exe -buildfile:ScriptLib.Build.xml IIS.Setup -D:Private.IIS.Setup.WebRootPhysicalPath=C:\svn\alpha\dev\ElementsCPS\trunk\Source\Production\WebRoot -D:Private.IIS.Setup.ServicesRootPhysicalPath=C:\svn\alpha\dev\ElementsCPS\trunk\Source\Production\Services

SET /P variable="Hit Enter to continue."
