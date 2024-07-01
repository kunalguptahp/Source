SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		Robert Mukai
-- Create date: 8/04/10
-- Description:	Delete JumpstationGroupSelector_QueryParameterValue by JumpstationGroupSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId]
	@JumpstationGroupSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.JumpstationGroupSelector_QueryParameterValue
	WHERE 
		JumpstationGroupSelectorId = @JumpstationGroupSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)

END

GO
