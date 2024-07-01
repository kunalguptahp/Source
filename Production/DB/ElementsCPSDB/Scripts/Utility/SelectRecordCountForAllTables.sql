CREATE TABLE #tRecordCount
    (
      TableName VARCHAR(512),
      RecordCount INT
    )

EXEC sp_MSforeachtable 'INSERT INTO #tRecordCount SELECT ''?'', COUNT(*) FROM ?'

SELECT  *
FROM    #tRecordCount
ORDER BY TableName -- RecordCount DESC -- 

DROP TABLE #tRecordCount
