SELECT	OBJECT_NAME(id) AS TableName
	, ROWS AS RecordCount 
FROM	sysindexes 
WHERE	indid IN (1,0)