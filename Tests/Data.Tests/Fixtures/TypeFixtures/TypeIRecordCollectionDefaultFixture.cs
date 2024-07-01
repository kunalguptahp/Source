using System.Collections.Generic;
using System.Globalization;
using HP.ElementsCPS.Data.SubSonicClient;
using SSC = HP.ElementsCPS.Data.SubSonicClient;
using MbUnit.Framework;

namespace HP.ElementsCPS.Data.Tests.Fixtures.TypeFixtures
{

	[TestFixture]
	public class TypeIRecordCollectionDefaultFixture
	{

		#region Data Factory Methods

		public static IEnumerable<IRecordCollection> GetInstances()
		{
			yield return new SSC.ConfigurationServiceApplicationCollection();
            yield return new SSC.ConfigurationServiceApplicationTypeCollection();
            yield return new SSC.ConfigurationServiceGroupCollection();
            yield return new SSC.ConfigurationServiceGroupTagCollection();
            yield return new SSC.ConfigurationServiceGroupImportCollection();
            yield return new SSC.ConfigurationServiceGroupSelectorCollection();
			yield return new SSC.ConfigurationServiceGroupSelectorQueryParameterValueCollection();
			yield return new SSC.ConfigurationServiceGroupStatusCollection();
			yield return new SSC.ConfigurationServiceGroupTypeCollection();
			yield return new SSC.ConfigurationServiceItemCollection();
			yield return new SSC.ConfigurationServiceLabelCollection();
            yield return new SSC.ConfigurationServiceLabelTypeCollection();
            yield return new SSC.ConfigurationServiceLabelValueCollection();
            yield return new SSC.ConfigurationServiceLabelValueImportCollection();

            yield return new SSC.EntityTypeCollection();

            yield return new SSC.JumpstationApplicationCollection();
            yield return new SSC.JumpstationGroupCollection();
            yield return new SSC.JumpstationGroupTagCollection();
            yield return new SSC.JumpstationGroupSelectorCollection();
            yield return new SSC.JumpstationGroupSelectorQueryParameterValueCollection();
            yield return new SSC.JumpstationGroupStatusCollection();
            yield return new SSC.JumpstationGroupTypeCollection();
            yield return new SSC.ConfigurationServiceLabelValueCollection();
            yield return new SSC.JumpstationMacroCollection();
            yield return new SSC.JumpstationMacroStatusCollection();           

			yield return new SSC.LogCollection();
			yield return new SSC.NoteCollection();
			yield return new SSC.NoteTypeCollection();
			yield return new SSC.PersonCollection();
			yield return new SSC.PersonRoleCollection();

			yield return new SSC.ProxyURLCollection();
            yield return new SSC.ProxyURLQueryParameterValueCollection();
            yield return new SSC.ProxyURLTagCollection();
            yield return new SSC.ProxyURLDomainCollection();
			yield return new SSC.ProxyURLGroupTypeCollection();
			yield return new SSC.ProxyURLStatusCollection();
			yield return new SSC.ProxyURLTypeCollection();

            yield return new SSC.QueryParameterCollection();
			yield return new SSC.QueryParameterConfigurationServiceGroupTypeCollection();
            yield return new SSC.QueryParameterJumpstationGroupTypeCollection();
            yield return new SSC.QueryParameterProxyURLTypeCollection();
            yield return new SSC.QueryParameterWorkflowTypeCollection();
            yield return new SSC.QueryParameterValueCollection();

			yield return new SSC.RoleCollection();
			yield return new SSC.RowStatusCollection();
			yield return new SSC.TagCollection();

            yield return new SSC.WorkflowApplicationCollection();
            yield return new SSC.WorkflowApplicationTypeCollection();
            yield return new SSC.WorkflowCollection();
            yield return new SSC.WorkflowVersionCollection();
            yield return new SSC.WorkflowTagCollection();
            yield return new SSC.WorkflowWorkflowModuleCollection();
            yield return new SSC.WorkflowModuleCollection();
            yield return new SSC.WorkflowModuleVersionCollection();
            yield return new SSC.WorkflowModuleTagCollection();
            yield return new SSC.WorkflowModuleWorkflowConditionCollection();
            yield return new SSC.WorkflowModuleCategoryCollection();
            yield return new SSC.WorkflowModuleSubCategoryCollection();
            yield return new SSC.WorkflowSelectorCollection();
            yield return new SSC.WorkflowSelectorQueryParameterValueCollection();
            yield return new SSC.WorkflowStatusCollection();
            yield return new SSC.WorkflowTypeCollection();

            yield return new SSC.VwMapConfigurationServiceApplicationCollection();
            yield return new SSC.VwMapConfigurationServiceApplicationTypeCollection();
            yield return new SSC.VwMapConfigurationServiceGroupCollection();
            yield return new SSC.VwMapConfigurationServiceGroupImportCollection();
            yield return new SSC.VwMapConfigurationServiceGroupSelectorCollection();
			yield return new SSC.VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection();
			yield return new SSC.VwMapConfigurationServiceGroupTagTagCollection();
			yield return new SSC.VwMapConfigurationServiceGroupTypeCollection();
			yield return new SSC.VwMapConfigurationServiceItemCollection();
            yield return new SSC.VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection();
            yield return new SSC.VwMapConfigurationServiceLabelCollection();
			yield return new SSC.VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection();
			yield return new SSC.VwMapConfigurationServiceLabelValueCollection();
            yield return new SSC.VwMapConfigurationServiceLabelValueImportCollection();

            yield return new SSC.VwMapEntityTypeCollection();

            yield return new SSC.VwMapJumpstationApplicationCollection();
            yield return new SSC.VwMapJumpstationGroupCollection();
            yield return new SSC.VwMapJumpstationGroupSelectorCollection();
            yield return new SSC.VwMapJumpstationGroupSelectorQueryParameterValueCollection();
            yield return new SSC.VwMapJumpstationGroupTagTagCollection();
            yield return new SSC.VwMapJumpstationGroupTypeCollection();
            yield return new SSC.VwMapJumpstationMacroCollection();
            
            yield return new SSC.VwMapNoteCollection();
			yield return new SSC.VwMapNoteTypeCollection();
			yield return new SSC.VwMapPersonCollection();
			yield return new SSC.VwMapPersonRoleCollection();

            yield return new SSC.VwMapProxyURLCollection();
            yield return new SSC.VwMapProxyURLQueryParameterValueCollection();
            yield return new SSC.VwMapProxyURLDomainCollection();
			yield return new SSC.VwMapProxyURLGroupTypeCollection();
			yield return new SSC.VwMapProxyURLTagTagCollection();
			yield return new SSC.VwMapProxyURLTypeCollection();
			yield return new SSC.VwMapQueryParameterCollection();
			yield return new SSC.VwMapQueryParameterConfigurationServiceGroupTypeCollection();
			yield return new SSC.VwMapQueryParameterJumpstationGroupTypeCollection();
            yield return new SSC.VwMapQueryParameterProxyURLTypeCollection();
            yield return new SSC.VwMapQueryParameterWorkflowTypeCollection();
            yield return new SSC.VwMapQueryParameterValueCollection();

            yield return new SSC.VwMapWorkflowCollection();
            yield return new SSC.VwMapWorkflowWorkflowModuleCollection();
            yield return new SSC.VwMapWorkflowApplicationCollection();
            yield return new SSC.VwMapWorkflowApplicationTypeCollection();
            yield return new SSC.VwMapWorkflowConditionCollection();
            yield return new SSC.VwMapWorkflowModuleCollection();
            yield return new SSC.VwMapWorkflowModuleVersionCollection();
            yield return new SSC.VwMapWorkflowModuleWorkflowConditionCollection();
            yield return new SSC.VwMapWorkflowModuleCategoryCollection();
            yield return new SSC.VwMapWorkflowModuleSubCategoryCollection();
            yield return new SSC.VwMapWorkflowModuleTagTagCollection();
            yield return new SSC.VwMapWorkflowSelectorCollection();
            yield return new SSC.VwMapWorkflowSelectorQueryParameterValueCollection();
            yield return new SSC.VwMapWorkflowTagTagCollection();
            yield return new SSC.VwMapWorkflowTypeCollection();
        }

		#endregion

		#region Tests

		[Test]
		public void InstanceSmokeTest([Factory("GetInstances")] IRecordCollection instance)
		{
			//perform some very basic "smoke" tests on each concrete instance passed into the test
			Assert.IsNotNull(instance.GetRecordType());
			Assert.AreEqual(instance.GetType().Name, string.Format(CultureInfo.InvariantCulture, "{0}Collection", instance.GetRecordType().Name));
		}

		#endregion

	}
}
