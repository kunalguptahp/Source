SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 4/6/11
-- Description:	Delete WorkflowSelector_QueryParameterValue by WorkflowSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId]
	@WorkflowSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.WorkflowSelector_QueryParameterValue
	WHERE 
		WorkflowSelectorId = @WorkflowSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)
END
GO
