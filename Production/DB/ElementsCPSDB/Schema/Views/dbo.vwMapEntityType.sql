SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapEntityType
AS
SELECT     dbo.EntityType.Id
          ,dbo.EntityType.Name
          ,dbo.EntityType.CreatedBy
          ,dbo.EntityType.CreatedOn
          ,dbo.EntityType.ModifiedBy
          ,dbo.EntityType.ModifiedOn
          ,dbo.EntityType.RowStatusId
      ,dbo.RowStatus.Name AS RowStatusName
      ,(SELECT   COUNT(Id) AS Expr1
        FROM     dbo.Note WITH (NOLOCK)
        WHERE    (dbo.Note.EntityTypeId = 14) AND (dbo.Note.EntityId = dbo.EntityType.Id)) AS NoteCount
           ,dbo.EntityType.Comment
FROM         dbo.EntityType WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.EntityType.RowStatusId = dbo.RowStatus.Id

GO
