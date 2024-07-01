INSERT JumpStationGroupPivot(JumpStationGroupId ,Brand, Cycle, Locale, Touchpoint, PartnerCategory, [Platform])
	SELECT Id, BrandQueryParameterValue, CycleQueryParameterValue, LocaleQueryParameterValue, TouchpointQueryParameterValue, PartnerCategoryQueryParameterValue, [PlatformQueryParameterValue] FROM vwMapJumpStationGroup
	
	
GO