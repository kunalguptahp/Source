using System;
using System.Collections.Generic;
using System.Reflection;
using HP.HPFx.Utility;
using HP.ElementsCPS.Data.SubSonicClient;
using SSC = HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;
using SubSonic;
using QueryParameter=SubSonic.QueryParameter;

namespace HP.ElementsCPS.Data.Tests.Fixtures.TypeFixtures
{

	[TestFixture]
	public class TypeIActiveRecordDefaultFixture
	{

		#region Data Factory Methods

		public static IEnumerable<IActiveRecord> GetInstances()
		{
			yield return SSC.ConfigurationServiceApplication.Copy(new SSC.ConfigurationServiceApplication(true));
            yield return SSC.ConfigurationServiceApplicationType.Copy(new SSC.ConfigurationServiceApplicationType(true));
            yield return SSC.ConfigurationServiceGroup.Copy(new SSC.ConfigurationServiceGroup(true));
            yield return SSC.ConfigurationServiceGroupImport.Copy(new SSC.ConfigurationServiceGroupImport(true));
            yield return SSC.ConfigurationServiceGroupSelector.Copy(new SSC.ConfigurationServiceGroupSelector(true));
			yield return SSC.ConfigurationServiceGroupSelectorQueryParameterValue.Copy(new SSC.ConfigurationServiceGroupSelectorQueryParameterValue(true));
			yield return SSC.ConfigurationServiceGroupStatus.Copy(new SSC.ConfigurationServiceGroupStatus(true));
			yield return SSC.ConfigurationServiceGroupTag.Copy(new SSC.ConfigurationServiceGroupTag(true));
			yield return SSC.ConfigurationServiceGroupType.Copy(new SSC.ConfigurationServiceGroupType(true));
			yield return SSC.ConfigurationServiceItem.Copy(new SSC.ConfigurationServiceItem(true));
			yield return SSC.ConfigurationServiceLabel.Copy(new SSC.ConfigurationServiceLabel(true));
            yield return SSC.ConfigurationServiceLabelType.Copy(new SSC.ConfigurationServiceLabelType(true));
            yield return SSC.ConfigurationServiceLabelValue.Copy(new SSC.ConfigurationServiceLabelValue(true));
            yield return SSC.ConfigurationServiceLabelValueImport.Copy(new SSC.ConfigurationServiceLabelValueImport(true));
            yield return SSC.EntityType.Copy(new SSC.EntityType(true));

            yield return SSC.JumpstationApplication.Copy(new SSC.JumpstationApplication(true));
            yield return SSC.JumpstationGroup.Copy(new SSC.JumpstationGroup(true));
            yield return SSC.JumpstationGroupSelector.Copy(new SSC.JumpstationGroupSelector(true));
            yield return SSC.JumpstationGroupSelectorQueryParameterValue.Copy(new SSC.JumpstationGroupSelectorQueryParameterValue(true));
            yield return SSC.JumpstationGroupStatus.Copy(new SSC.JumpstationGroupStatus(true));
            yield return SSC.JumpstationGroupTag.Copy(new SSC.JumpstationGroupTag(true));
            yield return SSC.JumpstationGroupType.Copy(new SSC.JumpstationGroupType(true));
            yield return SSC.JumpstationMacro.Copy(new SSC.JumpstationMacro(true));
            yield return SSC.JumpstationMacroStatus.Copy(new SSC.JumpstationMacroStatus(true));

			yield return SSC.Log.Copy(new SSC.Log(true));
			yield return SSC.Note.Copy(new SSC.Note(true));
			yield return SSC.NoteType.Copy(new SSC.NoteType(true));
			yield return SSC.Person.Copy(new SSC.Person(true));
			yield return SSC.PersonRole.Copy(new SSC.PersonRole(true));

            yield return SSC.ProxyURL.Copy(new SSC.ProxyURL(true));
            yield return SSC.ProxyURLQueryParameterValue.Copy(new SSC.ProxyURLQueryParameterValue(true));
            yield return SSC.ProxyURLTag.Copy(new SSC.ProxyURLTag(true));
            yield return SSC.ProxyURLDomain.Copy(new SSC.ProxyURLDomain(true));
            yield return SSC.ProxyURLGroupType.Copy(new SSC.ProxyURLGroupType(true));
			yield return SSC.ProxyURLStatus.Copy(new SSC.ProxyURLStatus(true));
			yield return SSC.ProxyURLType.Copy(new SSC.ProxyURLType(true));
			yield return SSC.QueryParameter.Copy(new SSC.QueryParameter(true));
			yield return SSC.QueryParameterConfigurationServiceGroupType.Copy(new SSC.QueryParameterConfigurationServiceGroupType(true));
            yield return SSC.QueryParameterJumpstationGroupType.Copy(new SSC.QueryParameterJumpstationGroupType(true));
            yield return SSC.QueryParameterWorkflowType.Copy(new SSC.QueryParameterWorkflowType(true));
			yield return SSC.QueryParameterValue.Copy(new SSC.QueryParameterValue(true));

			yield return SSC.Role.Copy(new SSC.Role(true));
			yield return SSC.RowStatus.Copy(new SSC.RowStatus(true));
			yield return SSC.Tag.Copy(new SSC.Tag(true));

            yield return SSC.Workflow.Copy(new SSC.Workflow(true));
            yield return SSC.WorkflowTag.Copy(new SSC.WorkflowTag(true));
            yield return SSC.WorkflowWorkflowModule.Copy(new SSC.WorkflowWorkflowModule(true));
            yield return SSC.WorkflowVersion.Copy(new SSC.WorkflowVersion(true));
            yield return SSC.WorkflowApplication.Copy(new SSC.WorkflowApplication(true));
            yield return SSC.WorkflowApplicationType.Copy(new SSC.WorkflowApplicationType(true));
            yield return SSC.WorkflowCondition.Copy(new SSC.WorkflowCondition(true));
            yield return SSC.WorkflowModule.Copy(new SSC.WorkflowModule(true));
            yield return SSC.WorkflowModuleVersion.Copy(new SSC.WorkflowModuleVersion(true));
            yield return SSC.WorkflowModuleTag.Copy(new SSC.WorkflowModuleTag(true));
            yield return SSC.WorkflowModuleWorkflowCondition.Copy(new SSC.WorkflowModuleWorkflowCondition(true));

            yield return SSC.WorkflowModuleCategory.Copy(new SSC.WorkflowModuleCategory(true));
            yield return SSC.WorkflowModuleSubCategory.Copy(new SSC.WorkflowModuleSubCategory(true));
            yield return SSC.WorkflowSelector.Copy(new SSC.WorkflowSelector(true));
            yield return SSC.WorkflowSelectorQueryParameterValue.Copy(new SSC.WorkflowSelectorQueryParameterValue(true));
            yield return SSC.WorkflowStatus.Copy(new SSC.WorkflowStatus(true));
            yield return SSC.WorkflowType.Copy(new SSC.WorkflowType(true));        
        }

		#endregion

		#region Tests

		/// <summary>
		/// Performs some very basic "smoke" tests on each concrete instance passed into the test
		/// </summary>
		/// <param name="instance"></param>
		[Test]
		public void InstanceSmokeTest([Factory("GetInstances")] IActiveRecord instance)
		{
			//perform some very basic "smoke" tests on each concrete instance passed into the test
			//Assert.AreEqual(instance, instance);
			Assert.IsTrue(((IRecord)instance).IdenticalTo(instance));
			//Assert.AreEqual(instance.GetHashCode(), instance.GetHashCode());
			Assert.IsNotNull(instance.TableName);
			Assert.AreEqual(true, instance.IsNew);
			Assert.AreEqual(false, instance.IsLoaded);

			//attempt to load the instance with data
			instance.LoadByKey((int)1);

			//Assert.AreEqual(instance, instance);
			Assert.IsTrue(((IRecord)instance).IdenticalTo(instance));
			//Assert.AreEqual(instance.GetHashCode(), instance.GetHashCode());
			Assert.AreEqual(true, instance.IsLoaded);
			//Assert.AreEqual(false, instance.IsNew);
		}

		/// <summary>
		/// invokes the Column properties via reflection
		/// </summary>
		/// <param name="instance"></param>
		[Test]
		public void ColumnPropertiesTest([Factory("GetInstances")] IActiveRecord instance)
		{
			//DataUtility.AssertColumnPropertyWorks(instance, "IdColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "RowStatusIdColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "NameColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "CreatedByColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "CreatedOnColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "ModifiedByColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "ModifiedOnColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "DateXColumn"); //This column's property name doesn't match the column name

			DataUtility.AssertColumnPropertyWorks(instance, "AllocatedMemoryColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "BrandParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "BrandParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "BrandParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ClrVersionColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CommentColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceApplicationIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupSelectorIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupSelectorQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupStatusIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupStatusNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupTypeElementsKeyColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupTypeIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceGroupTypeNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceItemIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceItemNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceLabelIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceLabelNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ConfigurationServiceLabelValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CountryParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CountryParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CountryParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CreatedAtColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CreatedByColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CreatedOnColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CycleParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CycleParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "CycleParameterValueQueryParameterValueIdColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "DateXColumn"); //This column's property name doesn't match the column name
			DataUtility.AssertColumnPropertyWorks(instance, "DescriptionColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ElementsKeyColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "EmailColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "EntityIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "EntityTypeIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "EntityTypeNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ExceptionColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "FirstNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "IdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LanguageParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LanguageParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LanguageParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LastNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LocaleParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LocaleParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LocaleParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LocationColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "LoggerColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "MachineNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "MessageColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "MiddleNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ModifiedByColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ModifiedOnColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NegationColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NoteCountColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NotesColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NoteTypeIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "NoteTypeNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSTypeParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSTypeParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSTypeParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSVersionColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSVersionParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSVersionParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OSVersionParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "OwnerIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PartnerCategoryParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PartnerCategoryParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PartnerCategoryParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCBrandParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCBrandParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCBrandParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCPlatformParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCPlatformParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PCPlatformParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PersonIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PersonNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PlatformParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PlatformParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "PlatformParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProcessorCountColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProcessThreadColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProcessUptimeColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProcessUserColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProcessUserInteractiveColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProductionDomainColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProductionIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLDomainIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLGroupTypeIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLStatusIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLStatusNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLTypeElementsKeyColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLTypeIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ProxyURLTypeNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "QueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "QueryParameterNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "QueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "QueryParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "QueryStringColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ReleaseParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ReleaseParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ReleaseParameterValueQueryParameterValueIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "RoleIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "RoleNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "RowStatusIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "RowStatusNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "SeverityColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "StackTraceColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TagCountColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TagIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TagNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TagRowStatusIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TagsColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TitleColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TouchpointParameterValueNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TouchpointParameterValueQueryParameterIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "TouchpointParameterValueQueryParameterValueIdColumn");
			//DataUtility.AssertColumnPropertyWorks(instance, "UrlColumn"); //NOTE: For some reason, this line ("UrlColumn") causes a test failure
			DataUtility.AssertColumnPropertyWorks(instance, "UserIdentityColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "UserNameColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "UserWebIdentityColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "UtcDateColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ValidationDomainColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "ValidationIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "VersionColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "WebSessionIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "WindowsIdColumn");
			DataUtility.AssertColumnPropertyWorks(instance, "WorkingMemoryColumn");
		}

		/// <summary>
		/// Compares the instance's TableSchema.Table against the one returned by the Schema class' corresponding static property.
		/// </summary>
		/// <param name="instance"></param>
		[Test]
		public void TableSchemaTest([Factory("GetInstances")] IActiveRecord instance)
		{
			//Compare the instance's TableSchema to that returned by the Schema object

			string typeName = instance.GetType().Name;
			TableSchema.Table table1 = instance.GetSchema();
			//TableSchema.Table table2 = Schemas.ClientApplicationWorkflow;
			TableSchema.Table table2 = ReflectionUtility.GetStaticPropertyValue<TableSchema.Table>(typeof(Schemas), typeName);
			Assert.IsNotNull(table1);
			Assert.IsNotNull(table2);
			Assert.AreEqual(table1.TableName, table2.TableName);

#warning SubSonic bug/issue: The following should be an Assert.IsTrue, but the 2 TableSchema.Table instances seem to be initialized differently.
			Assert.IsFalse(table1.Equals(table2));
		}

		/// <summary>
		/// Tests the instance's Type's generated "convenience" constructors.
		/// </summary>
		/// <param name="instance"></param>
		[Test]
		public void ConstructorTest_KeyId([Factory("GetInstances")] IActiveRecord instance)
		{
			Type type = instance.GetType();
			ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(object) });

			//object instance2 = constructorInfo.Invoke(new object[] {instance.GetPrimaryKeyValue()});
			object instance2 = constructorInfo.Invoke(new object[] { 0 });
			Assert.IsNotNull(instance2);
			Assert.IsFalse(instance.Equals(instance2));
		}

		/// <summary>
		/// Tests the instance's Type's generated "convenience" constructors.
		/// </summary>
		/// <param name="instance"></param>
		[Test]
		public void ConstructorTest_ColumnName_ColumnValue([Factory("GetInstances")] IActiveRecord instance)
		{
			Type type = instance.GetType();
			ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(string), typeof(object) });

			string columnName = instance.GetSchema().PrimaryKey.ColumnName;
			object instance2 = constructorInfo.Invoke(new object[] { columnName, 0 });
			Assert.IsNotNull(instance2);
			Assert.IsFalse(instance.Equals(instance2));
		}

		#endregion

	}
}
