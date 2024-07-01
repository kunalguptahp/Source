using System;
using System.Globalization;
using System.Threading;
using HP.HPFx.Extensions.Watin;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Testing;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures.PageFixtures
{
	[TestFixture][ApartmentState(ApartmentState.STA)] //[TestFixture(ApartmentState = ApartmentState.STA)] //, TimeOut = 90)]
	//[DependsOn(typeof(SmokeTestFixture))]
	[Importance(Importance.Default)]
	[Category(TestCategory.Optional.QuickBuilds)]
	[Category(TestCategory.Kind.Functional)]
	[Category(TestCategory.Speed.Slow)]
	[Category(TestCategory.DependsOn.DB)]
	[Category(TestCategory.DependsOn.IIS)]
	[Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	//[Category(TestCategory.ExcludeFrom.HeavyBuilds)]
	public class NoteTypeDetailFixture : SharedIEInstanceTestFixture
	{
		[SetUp] //[SetUp("Restore the DB to the SimpleData state.")]
		public void Setup()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

		/// <summary>
		/// Add new project with only mandatory fields
		/// </summary>
		[Test]
		public void AddNewWithOnlyMandatoryTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				string[] componentTypeInfo = { "NewNoteTypes"};

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);	
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie,componentTypeInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

		/// <summary>
		/// Add new project with all fields
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure add new record continues to work.
		/// </remarks>
		[Test]
		public void AddNewWithAllFieldsTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				string[] componentTypeInfo = { "NewNoteTypes", "New", "Inactive" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlStatus").Select(componentTypeInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtComment").TypeText(componentTypeInfo[1]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie, componentTypeInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

		/// <summary>
		/// Test edit
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure edit record continues to work.fail,don't know why
		/// </remarks>
		[Test]
		public void EditTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				ie.GoTo(pageUri);
				string project = @"NoteTypeOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
				ie.FindFilterButton().Click();

				string[] componentTypeInfo = { "NewNoteTypes","New", "Inactive" };

				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlStatus").Select(componentTypeInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtComment").TypeText(componentTypeInfo[1]); 
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(componentTypeInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, componentTypeInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				Assert.AreEqual(ie.Span(Find.ById(Constants.ClientIdDetailPanelPrefix + "lblIdValue")).Text, "9");
			}
		}




		/// <summary>
		/// Test for required fields
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure the required field validators work
		/// </remarks>
		[Test]
		public void RequiredFieldValidatorTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);

			}
		}

		/// <summary>
		/// Test cancel button
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that cancel button to work properly. 
		/// </remarks>
		[Test]
		public void CancelButtonTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
				string[] componentTypeInfo = { "NewNoteTypes" };
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(componentTypeInfo[0]);

				// Click Cancel
				ie.FindCancelButton().Click();

				// new project exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(componentTypeInfo[0]);
				ie.FindFilterButton().Click();
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("");
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(componentTypeInfo[0], ie);
			}
		}

		#endregion

		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)
		/// <summary>
		/// Name Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[Test]
		[Row(true, @"六一")]
		[Row(true, @"六1")]
		[Row(true, @" 12 ")]
		[Row(true, @"`~!@#$%^&*0123456789()[]{}/?;:\<|,.'>")]
		[Row(true, "a\"e")] // for "
		[Row(false, @" a")]
		[Row(false, @"c ")]
		[Row(false, @"六")]
		[Row(true, @"六一;drop table RowStatus")]
		[Row(true, "\"><script>alert('xss');</script><\"")]
		[Row(true, "\"><script>document.write('&#x3C;&#x73;&#x63;&#x72;&#x69;&#x70;&#x74;&#x20;&#x4C;&#x41;&#x4E;&#x47;&#x55;&#x41;&#x47;&#x45;&#x3D;&#x22;&#x6A;&#x61;&#x76;&#x61;&#x73;&#x63;&#x72;&#x69;&#x70;&#x74;&#x22;&#x3E;&#x61;&#x6C;&#x65;&#x72;&#x74;&#x28;&#x22;&#x78;&#x73;&#x73;&#x22;&#x29;&#x3B;&#x3C;&#x2F;&#x73;&#x63;&#x72;&#x69;&#x70;&#x74;&#x3E;'.replace(/&#(x[a-z0-9]+)\\;/ig,function($0,$1){return String.fromCharCode('0'+$1)}));</script><\"")]
		public void NameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/NoteTypeList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIdDetailPanelPrefix + "revNameMinLength"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
				}
			}
		}


		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
