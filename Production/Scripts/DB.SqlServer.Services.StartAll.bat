@echo off
cls
echo.**************************************
echo.**** Starting SQL Server Services ****
echo.**************************************
echo.
echo. Starting Up SQL Server...
net start "SQL Server (MSSQLSERVER)"
echo.
echo. Starting Up SQL Agent...
net start "SQL Server Agent (MSSQLSERVER)"
echo.
echo. Starting Up Integration Services...
net start "SQL Server Integration Services"
echo.
echo. Starting Up Full Text Search...
net start "SQL Server FullText Search (MSSQLSERVER)"
echo. 
echo. Starting Up Analysis Services..
net start "SQL Server Analysis Services (MSSQLSERVER)"
echo.
echo. Starting Up Reporting Services...
net start "SQL Server Reporting Services (MSSQLSERVER)"
echo.
echo. Starting Up SQL Browser...
echo. 
echo. To start the SQL Server Browser use this command: net start "SQL Server Browser"
echo.
echo.************************************************
echo.**** SQL Server Services Start Up Completed ****
echo.************************************************
echo.
