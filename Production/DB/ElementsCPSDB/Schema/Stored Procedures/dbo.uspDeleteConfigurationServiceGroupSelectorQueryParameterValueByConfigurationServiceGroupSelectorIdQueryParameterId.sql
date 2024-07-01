SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		Robert Mukai
-- Create date: 8/04/10
-- Description:	Delete ConfigurationServiceGroupSelector_QueryParameterValue by ConfigurationServiceGroupSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId]
	@configurationServiceGroupSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.ConfigurationServiceGroupSelector_QueryParameterValue
	WHERE 
		ConfigurationServiceGroupSelectorId = @configurationServiceGroupSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)

END

GO
