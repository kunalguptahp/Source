SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- User Defined Function
/*
Usage Examples:
	SELECT [ConfigurationServiceGroupImport].[Id], [dbo].[ConfigurationServiceGroup_GetLabelValueList]([ConfigurationServiceGroupImport].[Id], ', ', 1, 1) FROM [dbo].[ConfigurationServiceGroupImport]
	SELECT dbo.ConfigurationServiceGroupImport_GetLabelValueList (1 ', ', 1, 1)
	SELECT TOP 10 [ConfigurationServiceGroupImport].[Id], dbo.ConfigurationServiceGroup_GetLabelValueList ([ConfigurationServiceGroupImport].[Id], '/', 1, 1) FROM [dbo].[ConfigurationServiceGroupImport] ORDER BY [ConfigurationServiceGroupImport].[Id] DESC
*/
-- =============================================
-- Author:		Robert Mukai
-- Create date: 10/6/11
-- Description:	Returns a delimited list of LabelValues for a specified ConfigurationServiceGroupImport.
-- =============================================
CREATE FUNCTION [dbo].[ConfigurationServiceGroupImport_GetLabelValueList]
    (
     @ConfigurationServiceGroupImportId INT,
     @Delimiter VARCHAR(2),
     @RowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @LabelValueList NVARCHAR(MAX)
	DECLARE @TempLabelValueList TABLE (LabelValue NVARCHAR(256))
    SELECT
        @LabelValueList = NULL
        
	--Build the delimited string of LabelValue
	INSERT INTO @TempLabelValueList(LabelValue)
    SELECT
        DISTINCT [LabelValue]
    FROM
		ConfigurationServiceLabelValueImport WITH (NOLOCK)
    WHERE
        ((ConfigurationServiceGroupImportId = @ConfigurationServiceGroupImportId) 
        AND (@RowStatusId IS NULL
             OR [RowStatusId] = @RowStatusId))
    ORDER BY
        LabelValue
	SELECT @labelValueList = COALESCE(@LabelValueList + @Delimiter, '') + [LabelValue]
	FROM @TempLabelValueList
    RETURN @LabelValueList
   END

GO
