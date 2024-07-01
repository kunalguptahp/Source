SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapNoteType
AS
SELECT     dbo.NoteType.Id
          ,dbo.NoteType.Name
          ,dbo.NoteType.CreatedBy
          ,dbo.NoteType.CreatedOn
          ,dbo.NoteType.ModifiedBy
          ,dbo.NoteType.ModifiedOn
          ,dbo.NoteType.RowStatusId
      ,dbo.RowStatus.Name AS RowStatusName
           ,dbo.NoteType.Comment
FROM         dbo.NoteType WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.NoteType.RowStatusId = dbo.RowStatus.Id

GO
