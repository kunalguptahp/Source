SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS OFF
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/28/08
-- Description:	Returns a single value (specified by index) from a set of values in a delimited string.
-- =============================================
CREATE FUNCTION [dbo].[GetValueFromDelimitedString]
    (
     @DelimitedString VARCHAR(MAX),
     @Delimiter CHAR(1),
     @ItemIndex INT
    )
RETURNS VARCHAR(MAX)
AS BEGIN
    DECLARE @ItemValue VARCHAR(MAX)
    SELECT
        @ItemValue = [t].[ItemValue]
    FROM
        [dbo].[SplitStringByDelimiter](@DelimitedString, @Delimiter) t
    WHERE
        [t].[ItemIndex] = @ItemIndex
    RETURN @ItemValue
   END

GO
