SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/* now we'll create the trigger that fires whenever any database level 
DDL events occur. We won't bother to record CREATE STATISTIC events*/ 
CREATE TRIGGER trgLogDDLEvent ON DATABASE 
    FOR DDL_DATABASE_LEVEL_EVENTS 
AS 
    DECLARE @data XML 
    SET @data = EVENTDATA() 
    IF @data.value('(/EVENT_INSTANCE/EventType)[1]', 'nvarchar(100)') 
        <> 'CREATE_STATISTICS'  
        INSERT  INTO DDLChangeLog 
                ( 
                  EventType, 
                  ObjectName, 
                  ObjectType, 
                  tsql 
                ) 
        VALUES  ( 
                   @data.value('(/EVENT_INSTANCE/EventType)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/ObjectType)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 
                              'nvarchar(max)') 
                ) ; 

GO
