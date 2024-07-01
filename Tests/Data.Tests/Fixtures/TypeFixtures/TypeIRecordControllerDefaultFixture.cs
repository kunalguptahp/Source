using System.Collections.Generic;
using System.Globalization;
using HP.ElementsCPS.Data.SubSonicClient;
using SSC = HP.ElementsCPS.Data.SubSonicClient;
using MbUnit.Framework;

namespace HP.ElementsCPS.Data.Tests.Fixtures.TypeFixtures
{

	[TestFixture]
	public class TypeIRecordControllerDefaultFixture
	{

		#region Data Factory Methods

		public static IEnumerable<IRecordController> GetInstances()
		{
			yield return ConfigurationServiceApplicationController.NewController();
            yield return ConfigurationServiceApplicationTypeController.NewController();
            yield return ConfigurationServiceGroupController.NewController();
            yield return ConfigurationServiceGroupTagController.NewController();
            yield return ConfigurationServiceGroupImportController.NewController();
            yield return ConfigurationServiceGroupSelectorController.NewController();
			yield return ConfigurationServiceGroupSelectorQueryParameterValueController.NewController();
			yield return ConfigurationServiceGroupStatusController.NewController();
			yield return ConfigurationServiceGroupTypeController.NewController();
			yield return ConfigurationServiceItemController.NewController();
            yield return ConfigurationServiceItemConfigurationServiceGroupTypeController.NewController();
            yield return ConfigurationServiceLabelController.NewController();
            yield return ConfigurationServiceLabelTypeController.NewController();
            yield return ConfigurationServiceLabelValueController.NewController();
            yield return ConfigurationServiceLabelValueImportController.NewController();

			yield return EntityTypeController.NewController();

            yield return JumpstationApplicationController.NewController();
            yield return JumpstationGroupController.NewController();
            yield return JumpstationGroupTagController.NewController();
            yield return JumpstationGroupSelectorController.NewController();
            yield return JumpstationGroupSelectorQueryParameterValueController.NewController();
            yield return JumpstationGroupStatusController.NewController();
            yield return JumpstationGroupTypeController.NewController();
            yield return JumpstationMacroController.NewController();
            yield return JumpstationMacroStatusController.NewController();           
            
            yield return LogController.NewController();
			yield return NoteController.NewController();
			yield return NoteTypeController.NewController();
			yield return PersonController.NewController();
			yield return PersonRoleController.NewController();

			yield return ProxyURLController.NewController();
            yield return ProxyURLQueryParameterValueController.NewController();
            yield return ProxyURLTagController.NewController();
            yield return ProxyURLDomainController.NewController();
			yield return ProxyURLGroupTypeController.NewController();
			yield return ProxyURLStatusController.NewController();
            yield return ProxyURLTypeController.NewController();

            yield return QueryParameterController.NewController();
            yield return QueryParameterConfigurationServiceGroupTypeController.NewController();
            yield return QueryParameterJumpstationGroupTypeController.NewController();
            yield return QueryParameterProxyURLTypeController.NewController();
            yield return QueryParameterWorkflowTypeController.NewController();
			yield return QueryParameterValueController.NewController();
			yield return RoleController.NewController();
			yield return RowStatusController.NewController();
			yield return TagController.NewController();

            yield return WorkflowController.NewController();
            yield return WorkflowVersionController.NewController();
            yield return WorkflowTagController.NewController();
            yield return WorkflowWorkflowModuleController.NewController();
            yield return WorkflowApplicationController.NewController();
            yield return WorkflowApplicationTypeController.NewController();
            yield return WorkflowConditionController.NewController();
            yield return WorkflowModuleController.NewController();
            yield return WorkflowModuleVersionController.NewController();
            yield return WorkflowModuleTagController.NewController();
            yield return WorkflowModuleCategoryController.NewController();
            yield return WorkflowModuleSubCategoryController.NewController();
            yield return WorkflowSelectorController.NewController();
            yield return WorkflowSelectorQueryParameterValueController.NewController();
            yield return WorkflowStatusController.NewController();
            yield return WorkflowTypeController.NewController();

            yield return VwMapConfigurationServiceApplicationController.NewController();
            yield return VwMapConfigurationServiceApplicationTypeController.NewController();
            yield return VwMapConfigurationServiceGroupController.NewController();
            yield return VwMapConfigurationServiceGroupImportController.NewController();
			yield return VwMapConfigurationServiceGroupSelectorController.NewController();
			yield return VwMapConfigurationServiceGroupSelectorQueryParameterValueController.NewController();
			yield return VwMapConfigurationServiceItemConfigurationServiceGroupTypeController.NewController();
			yield return VwMapConfigurationServiceGroupTagTagController.NewController();
			yield return VwMapConfigurationServiceGroupTypeController.NewController();
			yield return VwMapConfigurationServiceItemController.NewController();
			yield return VwMapConfigurationServiceLabelController.NewController();
			yield return VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.NewController();
			yield return VwMapConfigurationServiceLabelValueController.NewController();
            yield return VwMapConfigurationServiceLabelValueImportController.NewController();

            yield return VwMapEntityTypeController.NewController();

            yield return VwMapJumpstationApplicationController.NewController();
            yield return VwMapJumpstationGroupController.NewController();
            yield return VwMapJumpstationGroupTagTagController.NewController();
            yield return VwMapJumpstationGroupSelectorController.NewController();
            yield return VwMapJumpstationGroupSelectorQueryParameterValueController.NewController();
            yield return VwMapJumpstationGroupTypeController.NewController();
            yield return VwMapJumpstationMacroController.NewController();

            yield return VwMapNoteController.NewController();
			yield return VwMapNoteTypeController.NewController();
			yield return VwMapPersonController.NewController();
			yield return VwMapPersonRoleController.NewController();

            yield return VwMapProxyURLController.NewController();
            yield return VwMapProxyURLQueryParameterValueController.NewController();
            yield return VwMapProxyURLDomainController.NewController();
			yield return VwMapProxyURLGroupTypeController.NewController();
			yield return VwMapProxyURLTagTagController.NewController();
			yield return VwMapProxyURLTypeController.NewController();
            yield return VwMapQueryParameterController.NewController();
            yield return VwMapQueryParameterConfigurationServiceGroupTypeController.NewController();
            yield return VwMapQueryParameterJumpstationGroupTypeController.NewController();
            yield return VwMapQueryParameterProxyURLTypeController.NewController();
            yield return VwMapQueryParameterWorkflowTypeController.NewController();
            yield return VwMapQueryParameterValueController.NewController();

            yield return VwMapWorkflowController.NewController();
            yield return VwMapWorkflowWorkflowModuleController.NewController();
            yield return VwMapWorkflowApplicationController.NewController();
            yield return VwMapWorkflowApplicationTypeController.NewController();
            yield return VwMapWorkflowConditionController.NewController();
            yield return VwMapWorkflowModuleController.NewController();
            yield return VwMapWorkflowModuleVersionController.NewController();
            yield return VwMapWorkflowModuleCategoryController.NewController();
            yield return VwMapWorkflowModuleSubCategoryController.NewController();
            yield return VwMapWorkflowModuleTagTagController.NewController();
            yield return VwMapWorkflowSelectorController.NewController();
            yield return VwMapWorkflowSelectorQueryParameterValueController.NewController();
            yield return VwMapWorkflowTagTagController.NewController();
            yield return VwMapWorkflowTypeController.NewController();
		}

		#endregion

		#region Tests

		[Test]
		public void InstanceSmokeTest([Factory("GetInstances")] IRecordController instance)
		{
			//perform some very basic "smoke" tests on each concrete instance passed into the test
			Assert.IsNotNull(instance.GetRecordSchema());
			Assert.IsNotNull(instance.GetRecordType());
			Assert.AreEqual(instance.GetType().Name, string.Format(CultureInfo.InvariantCulture, "{0}Controller", instance.GetRecordType().Name));
		}

		#endregion

	}
}
