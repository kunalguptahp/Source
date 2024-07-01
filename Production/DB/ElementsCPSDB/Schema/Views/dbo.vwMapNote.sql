SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapNote
AS
SELECT     dbo.Note.Id
          ,dbo.Note.Name
          ,dbo.Note.CreatedBy
          ,dbo.Note.CreatedOn
          ,dbo.Note.ModifiedBy
          ,dbo.Note.ModifiedOn
          ,dbo.Note.RowStatusId
          ,dbo.RowStatus.Name AS RowStatusName
          ,dbo.Note.Comment
          ,dbo.Note.EntityTypeId
          ,dbo.EntityType.Name As EntityTypeName
          ,dbo.Note.EntityId
          ,dbo.Note.NoteTypeId
          ,dbo.NoteType.Name As NoteTypeName
      ,(SELECT   COUNT(Id) AS Expr1
        FROM     dbo.Note WITH (NOLOCK)
        WHERE    (dbo.Note.EntityTypeId = 27) AND (dbo.Note.EntityId = dbo.Note.Id)) AS NoteCount
FROM         dbo.Note WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.Note.RowStatusId = dbo.RowStatus.Id INNER JOIN
             dbo.EntityType WITH (NOLOCK) ON dbo.Note.EntityTypeId = dbo.EntityType.Id LEFT OUTER JOIN
             dbo.NoteType WITH (NOLOCK) ON dbo.Note.NoteTypeId = dbo.NoteType.Id 

GO
