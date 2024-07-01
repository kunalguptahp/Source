using System.IO;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Tests.Utility;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Testing;
using MbUnit.Framework;
using SubSonic;

namespace HP.ElementsCPS.Data.Tests.Fixtures
{
	[TestFixture]
	//[DependsOn(typeof(RemindersFixture))]
	[Importance(Importance.Default)]
	[Category(TestCategory.Kind.Unit)]
	public class DefaultFixture
	{

		[FixtureSetUp]
		public void TestFixtureSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		[TearDown]
		public void TearDown()
		{
			//DataUtility.SetTestUserRoles(UserRoleId.Administrator);
		}

		#region Assembly Tests

		[Test]
		public void AssemblyTest_SmokeTest()
		{
			OldReflectionAssert.NotCreatable(typeof(ElementsCPSSqlUtility));
		}

		#endregion

		#region Tests

		/// <summary>
		/// This test performs limited testing/coverage of some "code generated" objects that aren't explicitly tested elsewhere.
		/// </summary>
		[Test]
		public void MiscGeneratedCodeSmokeTest()
		{
			//perform some very basic "touch" tests on some misc. portions of the SubSonic-generated DAL
			Assert.IsNotNull(DB.Repository);
			Assert.IsNotNull(Generated.Databases.ElementsCPSDB);
			Assert.IsNotNull(Tables.Log);
			Assert.IsNotNull(Views.VwMapPerson);
		}

		/// <summary>
		/// This test verifies that "map" views are implemented correctly by checking that they contain the same number of records as their "primary" table.
		/// </summary>
		[Test]
		[Row("VwMapConfigurationServiceApplication", "Id", "ConfigurationServiceApplication", "Id")]
        [Row("VwMapConfigurationServiceApplicationType", "Id", "ConfigurationServiceApplicationType", "Id")]
        [Row("VwMapConfigurationServiceGroup", "Id", "ConfigurationServiceGroup", "Id")]
        [Row("VwMapConfigurationServiceGroupImport", "Id", "ConfigurationServiceGroupImport", "Id")]
        [Row("VwMapConfigurationServiceGroupSelector", "Id", "ConfigurationServiceGroupSelector", "Id")]
		[Row("VwMapConfigurationServiceGroupType", "Id", "ConfigurationServiceGroupType", "Id")]
		[Row("VwMapConfigurationServiceItem", "Id", "ConfigurationServiceItem", "Id")]
		[Row("VwMapConfigurationServiceLabel", "Id", "ConfigurationServiceLabel", "Id")]
		[Row("VwMapConfigurationServiceLabelValue", "Id", "ConfigurationServiceLabelValue", "Id")]
        [Row("VwMapConfigurationServiceLabelValueImport", "Id", "ConfigurationServiceLabelValueImport", "Id")]

        [Row("VwMapJumpstationApplication", "Id", "JumpstationApplication", "Id")]
        [Row("VwMapJumpstationGroup", "Id", "JumpstationGroup", "Id")]
        [Row("VwMapJumpstationGroupSelector", "Id", "JumpstationGroupSelector", "Id")]
        [Row("VwMapJumpstationGroupType", "Id", "JumpstationGroupType", "Id")]
        [Row("VwMapJumpstationMacro", "Id", "JumpstationMacro", "Id")]
        [Row("VwMapJumpstationMacroValue", "Id", "JumpstationMacroValue", "Id")]

        [Row("VwMapProxyURL", "Id", "ProxyURL", "Id")]
        [Row("VwMapProxyURLDomain", "Id", "ProxyURLDomain", "Id")]
        [Row("VwMapProxyURLGroupType", "Id", "ProxyURLGroupType", "Id")]
        [Row("VwMapProxyURLType", "Id", "ProxyURLType", "Id")]
        [Row("VwMapQueryParameter", "Id", "QueryParameter", "Id")]
        [Row("VwMapQueryParameterValue", "Id", "QueryParameterValue", "Id")]

        [Row("VwMapWorkflow", "Id", "Workflow", "Id")]
        [Row("VwMapWorkflowApplication", "Id", "WorkflowApplication", "Id")]
        [Row("VwMapWorkflowApplicationType", "Id", "WorkflowApplicationType", "Id")]
        [Row("VwMapWorkflowCondition", "Id", "WorkflowCondition", "Id")]
        [Row("VwMapWorkflowModule", "Id", "WorkflowModule", "Id")]
        [Row("VwMapWorkflowSelector", "Id", "WorkflowSelector", "Id")]
        [Row("VwMapWorkflowType", "Id", "WorkflowType", "Id")]
        [Row("VwMapWorkflowModuleVersion", "Id", "WorkflowModuleVersion", "Id")]

        [Row("VwMapNote", "Id", "Note", "Id")]
        [Row("VwMapNoteType", "Id", "NoteType", "Id")]
        [Row("VwMapEntityType", "Id", "EntityType", "Id")]
		[Row("VwMapPerson", "Id", "Person", "Id")]
		public void MapViewCountTest_DirectSubSonicQuery(string viewName, string viewColumnName, string tableName, string tableColumnName)
		{
			SubSonic.Query qryViewCount = new Query(viewName, Generated.Databases.ElementsCPSDB);
			SubSonic.Query qryTableCount = new Query(tableName, Generated.Databases.ElementsCPSDB);
			Assert.AreEqual(qryViewCount.GetCount(viewColumnName), qryTableCount.GetCount(tableColumnName));
		}

		/// <summary>
		/// This test verifies that "map" views are implemented correctly by checking that they contain the same number of records as their "primary" table.
		/// </summary>
		[Test]
		public void MapViewCountTest_Controllers()
		{
			Assert.AreEqual(VwMapConfigurationServiceApplicationController.FetchAllCount(), ConfigurationServiceApplicationController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceApplicationTypeController.FetchAllCount(), ConfigurationServiceApplicationTypeController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceGroupImportController.FetchAllCount(), ConfigurationServiceGroupImportController.FetchAllCount());
			Assert.AreEqual(VwMapConfigurationServiceGroupSelectorController.FetchAllCount(), ConfigurationServiceGroupSelectorController.FetchAllCount());
			Assert.AreEqual(VwMapConfigurationServiceGroupSelectorQueryParameterValueController.FetchAllCount(), ConfigurationServiceGroupSelectorQueryParameterValueController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceGroupTypeController.FetchAllCount(), ConfigurationServiceGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceItemController.FetchAllCount(), ConfigurationServiceItemController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceItemConfigurationServiceGroupTypeController.FetchAllCount(), ConfigurationServiceItemConfigurationServiceGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceLabelController.FetchAllCount(), ConfigurationServiceLabelController.FetchAllCount());
			Assert.AreEqual(VwMapConfigurationServiceLabelValueController.FetchAllCount(), ConfigurationServiceLabelValueController.FetchAllCount());
            Assert.AreEqual(VwMapConfigurationServiceLabelValueImportController.FetchAllCount(), ConfigurationServiceLabelValueImportController.FetchAllCount());

            Assert.AreEqual(VwMapJumpstationApplicationController.FetchAllCount(), JumpstationApplicationController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationGroupController.FetchAllCount(), JumpstationGroupController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationGroupSelectorController.FetchAllCount(), JumpstationGroupSelectorController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationGroupSelectorQueryParameterValueController.FetchAllCount(), JumpstationGroupSelectorQueryParameterValueController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationGroupTypeController.FetchAllCount(), JumpstationGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationMacroController.FetchAllCount(), JumpstationMacroController.FetchAllCount());
            Assert.AreEqual(VwMapJumpstationMacroValueController.FetchAllCount(), JumpstationMacroValueController.FetchAllCount());

			Assert.AreEqual(VwMapProxyURLController.FetchAllCount(), ProxyURLController.FetchAllCount());
			Assert.AreEqual(VwMapProxyURLQueryParameterValueController.FetchAllCount(), ProxyURLQueryParameterValueController.FetchAllCount());
			Assert.AreEqual(VwMapProxyURLDomainController.FetchAllCount(), ProxyURLDomainController.FetchAllCount());
			Assert.AreEqual(VwMapProxyURLGroupTypeController.FetchAllCount(), ProxyURLGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapProxyURLQueryParameterValueController.FetchAllCount(), ProxyURLQueryParameterValueController.FetchAllCount());
            Assert.AreEqual(VwMapProxyURLTypeController.FetchAllCount(), ProxyURLTypeController.FetchAllCount());
			Assert.AreEqual(VwMapQueryParameterController.FetchAllCount(), QueryParameterController.FetchAllCount());
			Assert.AreEqual(VwMapQueryParameterConfigurationServiceGroupTypeController.FetchAllCount(), QueryParameterConfigurationServiceGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapQueryParameterJumpstationGroupTypeController.FetchAllCount(), QueryParameterJumpstationGroupTypeController.FetchAllCount());
            Assert.AreEqual(VwMapQueryParameterProxyURLTypeController.FetchAllCount(), QueryParameterProxyURLTypeController.FetchAllCount());
            Assert.AreEqual(VwMapQueryParameterWorkflowTypeController.FetchAllCount(), QueryParameterWorkflowTypeController.FetchAllCount());
            Assert.AreEqual(VwMapQueryParameterValueController.FetchAllCount(), QueryParameterValueController.FetchAllCount());

            Assert.AreEqual(VwMapEntityTypeController.FetchAllCount(), EntityTypeController.FetchAllCount());
            Assert.AreEqual(VwMapNoteController.FetchAllCount(), NoteController.FetchAllCount());
            Assert.AreEqual(VwMapNoteTypeController.FetchAllCount(), NoteTypeController.FetchAllCount());
            Assert.AreEqual(VwMapPersonController.FetchAllCount(), PersonController.FetchAllCount());
            Assert.AreEqual(VwMapPersonRoleController.FetchAllCount(), PersonRoleController.FetchAllCount());

            Assert.AreEqual(VwMapWorkflowController.FetchAllCount(), WorkflowController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowWorkflowModuleController.FetchAllCount(), WorkflowWorkflowModuleController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowApplicationController.FetchAllCount(), WorkflowApplicationController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowApplicationTypeController.FetchAllCount(), WorkflowApplicationTypeController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowConditionController.FetchAllCount(), WorkflowConditionController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowModuleController.FetchAllCount(), WorkflowModuleController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowModuleVersionController.FetchAllCount(), WorkflowModuleVersionController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowModuleCategoryController.FetchAllCount(), WorkflowModuleCategoryController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowModuleSubCategoryController.FetchAllCount(), WorkflowModuleSubCategoryController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowSelectorController.FetchAllCount(), WorkflowSelectorController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowSelectorQueryParameterValueController.FetchAllCount(), WorkflowSelectorQueryParameterValueController.FetchAllCount());
            Assert.AreEqual(VwMapWorkflowTypeController.FetchAllCount(), WorkflowTypeController.FetchAllCount());
        }

		/// <summary>
		/// This test verifies that specific views which have an atypical (i.e. not 1:1) count correlation with their underlying primary table 
		/// are implemented correctly by checking that they contain the correct number of records based upon the current table data.
		/// </summary>
		[Test]
		public void SpecialViewCountTest_Controllers()
		{
			//Assert.AreEqual(VwProductComponentProductComponentController.FetchAllCount(), 2 * ProductComponentProductComponentController.FetchAllCount());
		}

		#endregion

	}
}
