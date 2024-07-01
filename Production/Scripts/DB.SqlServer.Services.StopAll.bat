@echo off
cls
echo.********************************************
echo.**** Stopping All SQL Server Services ****
echo.********************************************
echo.
echo. Shutting Down Integration Services...
net stop "SQL Server Integration Services"
echo.
echo. Shutting Down Full Text Search...
net stop "SQL Server FullText Search (MSSQLSERVER)"
echo.
echo. Shutting Down SQL Agent...
net stop "SQL Server Agent (MSSQLSERVER)"
echo. 
echo. Shutting Down Analysis Services..
net stop "SQL Server Analysis Services (MSSQLSERVER)"
echo.
echo. Shutting Down Reporting Services...
net stop "SQL Server Reporting Services (MSSQLSERVER)"
echo.
echo. Shutting Down SQL Browser...
net stop "SQL Server Browser"
echo.
echo. Shutting Down SQL Server...
net stop "SQL Server (MSSQLSERVER)"
echo.
echo.
echo.*************************************************
echo.**** SQL Server Services Shut Down Completed ****
echo.*************************************************
echo.
