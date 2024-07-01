SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a single-column table containing the values obtained from splitting a delimited string.
-- Usage Example:
--SELECT
--    t1.[ItemIndex],
--    t1.[ItemValue] AS value1,
--    t2.[ItemValue] AS value2
--FROM
--    [ElementsCPSDB].[dbo].[SplitStringByDelimiter]('1,2,8,9', ',') AS t1
--INNER JOIN (
--            SELECT * FROM [ElementsCPSDB] . [dbo] . [SplitStringByDelimiter] ('1,6,7,8,9', ',')
--           ) AS t2
--    ON cast(t1.ItemValue as int) < cast(t2.ItemValue as int)
-- =============================================
CREATE FUNCTION [dbo].[SplitStringByDelimiter]
    (
     @DelimitedString VARCHAR(MAX),
     @Delimiter CHAR(1)
    )
RETURNS @ListValues TABLE
    (
     ItemIndex INTEGER IDENTITY,
     ItemValue VARCHAR(MAX)
    )
AS BEGIN
    DECLARE
        @Value VARCHAR(MAX),
        @StringIndex INT
    SET @Delimiter = LTRIM(RTRIM(@Delimiter))
    SET @DelimitedString = LTRIM(RTRIM(@DelimitedString)) + @Delimiter
    SET @StringIndex = CHARINDEX(@Delimiter, @DelimitedString, 1)
    IF REPLACE(@DelimitedString, @Delimiter, '') <> '' 
        BEGIN
            WHILE @StringIndex > 0
                BEGIN
                    SET @Value = LTRIM(RTRIM(LEFT(@DelimitedString, @StringIndex - 1)))
                    IF @Value <> '' 
                        BEGIN
                            INSERT INTO
                                @ListValues (ItemValue)
                            VALUES
                                (@Value) --Use Appropriate conversion
                        END
                    SET @DelimitedString = RIGHT(@DelimitedString, LEN(@DelimitedString) - @StringIndex)
                    SET @StringIndex = CHARINDEX(@Delimiter, @DelimitedString, 1)
                END
        END	
    RETURN
   END

GO
