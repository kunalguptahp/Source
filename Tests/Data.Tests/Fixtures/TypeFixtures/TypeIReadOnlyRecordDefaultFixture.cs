using System.Collections.Generic;
using HP.ElementsCPS.Data.SubSonicClient;
using SSC = HP.ElementsCPS.Data.SubSonicClient;
using MbUnit.Framework;
using SubSonic;

namespace HP.ElementsCPS.Data.Tests.Fixtures.TypeFixtures
{

	[TestFixture]
	public class TypeIReadOnlyRecordDefaultFixture
	{

		#region Data Factory Methods

		public static IEnumerable<IReadOnlyRecord>  GetInstances()
		{
			yield return SSC.VwMapConfigurationServiceApplication.Copy(new SSC.VwMapConfigurationServiceApplication(true));
            yield return SSC.VwMapConfigurationServiceApplicationType.Copy(new SSC.VwMapConfigurationServiceApplicationType(true));
            yield return SSC.VwMapConfigurationServiceGroup.Copy(new SSC.VwMapConfigurationServiceGroup(true));
            yield return SSC.VwMapConfigurationServiceGroupImport.Copy(new SSC.VwMapConfigurationServiceGroupImport(true));
            yield return SSC.VwMapConfigurationServiceGroupSelector.Copy(new SSC.VwMapConfigurationServiceGroupSelector(true));
			yield return SSC.VwMapConfigurationServiceGroupSelectorQueryParameterValue.Copy(new SSC.VwMapConfigurationServiceGroupSelectorQueryParameterValue(true));
			yield return SSC.VwMapConfigurationServiceItemConfigurationServiceGroupType.Copy(new SSC.VwMapConfigurationServiceItemConfigurationServiceGroupType());
			yield return SSC.VwMapConfigurationServiceGroupTagTag.Copy(new SSC.VwMapConfigurationServiceGroupTagTag(true));
			yield return SSC.VwMapConfigurationServiceGroupType.Copy(new SSC.VwMapConfigurationServiceGroupType(true));
			yield return SSC.VwMapConfigurationServiceItem.Copy(new SSC.VwMapConfigurationServiceItem(true));
			yield return SSC.VwMapConfigurationServiceLabel.Copy(new SSC.VwMapConfigurationServiceLabel(true));
			yield return SSC.VwMapConfigurationServiceLabelConfigurationServiceGroupType.Copy(new SSC.VwMapConfigurationServiceLabelConfigurationServiceGroupType(true));
			yield return SSC.VwMapConfigurationServiceLabelValue.Copy(new SSC.VwMapConfigurationServiceLabelValue(true));
            yield return SSC.VwMapConfigurationServiceLabelValueImport.Copy(new SSC.VwMapConfigurationServiceLabelValueImport(true));

			yield return SSC.VwMapEntityType.Copy(new SSC.VwMapEntityType(true));
			yield return SSC.VwMapNote.Copy(new SSC.VwMapNote(true));
			yield return SSC.VwMapNoteType.Copy(new SSC.VwMapNoteType(true));
			yield return SSC.VwMapPerson.Copy(new SSC.VwMapPerson(true));
			yield return SSC.VwMapPersonRole.Copy(new SSC.VwMapPersonRole(true));

            yield return SSC.VwMapProxyURL.Copy(new SSC.VwMapProxyURL(true));
			yield return SSC.VwMapProxyURLDomain.Copy(new SSC.VwMapProxyURLDomain(true));
			yield return SSC.VwMapProxyURLGroupType.Copy(new SSC.VwMapProxyURLGroupType(true));
			yield return SSC.VwMapProxyURLQueryParameterValue.Copy(new SSC.VwMapProxyURLQueryParameterValue(true));
			yield return SSC.VwMapProxyURLTagTag.Copy(new SSC.VwMapProxyURLTagTag(true));
			yield return SSC.VwMapProxyURLType.Copy(new SSC.VwMapProxyURLType(true));

            yield return SSC.VwMapQueryParameter.Copy(new SSC.VwMapQueryParameter(true));
			yield return SSC.VwMapQueryParameterConfigurationServiceGroupType.Copy(new SSC.VwMapQueryParameterConfigurationServiceGroupType(true));
			yield return SSC.VwMapQueryParameterJumpstationGroupType.Copy(new SSC.VwMapQueryParameterJumpstationGroupType(true));
            yield return SSC.VwMapQueryParameterProxyURLType.Copy(new SSC.VwMapQueryParameterProxyURLType(true));
            yield return SSC.VwMapQueryParameterWorkflowType.Copy(new SSC.VwMapQueryParameterWorkflowType(true));
            yield return SSC.VwMapQueryParameterValue.Copy(new SSC.VwMapQueryParameterValue(true));

            yield return SSC.VwMapWorkflow.Copy(new SSC.VwMapWorkflow(true));
            yield return SSC.VwMapWorkflowWorkflowModule.Copy(new SSC.VwMapWorkflowWorkflowModule(true));
            yield return SSC.VwMapWorkflowApplication.Copy(new SSC.VwMapWorkflowApplication(true));
            yield return SSC.VwMapWorkflowApplicationType.Copy(new SSC.VwMapWorkflowApplicationType(true));
            yield return SSC.VwMapWorkflowCondition.Copy(new SSC.VwMapWorkflowCondition(true));
            yield return SSC.VwMapWorkflowModule.Copy(new SSC.VwMapWorkflowModule(true));
            yield return SSC.VwMapWorkflowModuleVersion.Copy(new SSC.VwMapWorkflowModuleVersion(true));
            yield return SSC.VwMapWorkflowModuleWorkflowCondition.Copy(new SSC.VwMapWorkflowModuleWorkflowCondition(true));
            yield return SSC.VwMapWorkflowModuleCategory.Copy(new SSC.VwMapWorkflowModuleCategory(true));
            yield return SSC.VwMapWorkflowModuleSubCategory.Copy(new SSC.VwMapWorkflowModuleSubCategory(true));
            yield return SSC.VwMapWorkflowModuleTagTag.Copy(new SSC.VwMapWorkflowModuleTagTag(true));;
            yield return SSC.VwMapWorkflowSelector.Copy(new SSC.VwMapWorkflowSelector(true));
            yield return SSC.VwMapWorkflowSelectorQueryParameterValue.Copy(new SSC.VwMapWorkflowSelectorQueryParameterValue(true));
            yield return SSC.VwMapWorkflowTagTag.Copy(new SSC.VwMapWorkflowTagTag(true));
            yield return  SSC.VwMapWorkflowType.Copy(new SSC.VwMapWorkflowType(true));
		}

		#endregion

		#region Tests

		[Test]
		public void InstanceSmokeTest([Factory("GetInstances")] IReadOnlyRecord instance)
		{
			//perform some very basic "smoke" tests on each concrete instance passed into the test
			//Assert.AreEqual(instance, instance);
			Assert.IsTrue(((IRecord)instance).IdenticalTo(instance));
		}

		#endregion

	}
}
