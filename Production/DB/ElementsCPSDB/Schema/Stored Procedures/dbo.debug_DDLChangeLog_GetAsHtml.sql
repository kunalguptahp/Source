SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
--// =============================================
--// Author: Justin Webster
--// Create date: 2007/05/31
--// Description: Exports the contents of the DDLChangeLog table as a HTML-formatted string.
--// Modifications:
--// Exec Script:
--// Used in SP:
--// Used in Code:
--// Comments:
--// =============================================
CREATE PROCEDURE [dbo].[debug_DDLChangeLog_GetAsHtml]
AS 
BEGIN
    SET NOCOUNT ON ;

    DECLARE @HTMLCode VARCHAR(MAX) 

    SELECT
        @HTMLCode = COALESCE(@HTMLCode,
                             ' <style type="text/css"> 
		<!-- 
		#changes{ 
		 border: 1px solid silver; 
		 font-family: Arial, Helvetica, sans-serif; 
		 font-size: 11px; 
		 padding: 10px 10px 10px 10px; 
		} 
		#changes td.date{ font-style: italic; } 
		#changes td.tsql{ border-bottom: 1px solid silver; color: #00008B; } 
		--> 
		</style><table id="changes"> 
	') + '<tr class="recordtop"> 
	<td class="date">' + CONVERT(CHAR(18), [DDLChangeLog].[Inserted], 113)
        + '</td> 
	<td class="currentuser">' + [DDLChangeLog].[currentUser] + '</td> 
	<td class="loginname">' + [DDLChangeLog].[LoginName]
        + CASE WHEN [DDLChangeLog].[loginName] <> [DDLChangeLog].[UserName]
               THEN '(' + [DDLChangeLog].[UserName] + ')'
               ELSE ''
          END + '</td> 
	<td class="eventtype">' + [DDLChangeLog].[EventType] + '</td> 
	<td class="objectname">' + [DDLChangeLog].[ObjectName] + ' ('
        + [DDLChangeLog].[objectType] + ')' + '</td></tr> 
	<tr class="recordbase"><td colspan="6" class="tsql"><pre>'
        + [DDLChangeLog].[tsql] + '</pre></td></tr> 
	'
    FROM
        [dbo].[DDLChangeLog]
    ORDER BY
        [DDLChangeLog].[Inserted] ; 

    SELECT
        @HTMLCode + ' 
	</table>' 
END




GO
