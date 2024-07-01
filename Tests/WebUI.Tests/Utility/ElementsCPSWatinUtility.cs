using System;
using Gallio.Framework;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;
using System.Configuration;
using HP.HPFx.Extensions.Watin;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Utility
{
	public class ElementsCPSWatinUtility
	{

		#region Fields

		//TODO: Refactoring: Code Smell: Inline this field
#warning Refactoring: Code Smell: Inline this field
		public static string Creator = DataUtility.GetTestUserWindowsId();

		//TODO: Refactoring: Code Smell: Inline this field
#warning Refactoring: Code Smell: Inline this field
		public static string Modifier = DataUtility.GetTestUserWindowsId();

		#endregion

		static ElementsCPSWatinUtility()
		{
			ElementsCPSWatinUtility.ConfigureWatin();
		}

		public static void DoNothing()
		{
			//does nothing, but will ensure that the static constructor has been called.
		}

		public static void ConfigureWatin()
		{
			///HACK: Watin-to-Gallio logging is disabled for now, since it appears to be causing RemotingExceptions (but only during CI builds).
			/// See here for details:
			/// http://code.google.com/p/mb-unit/issues/detail?id=557
			/// http://groups.google.com/group/gallio-dev/browse_thread/thread/876c6a8807213cbb/cfb9af09925a85a3?lnk=gst&q=RemotingException#cfb9af09925a85a3
			/// http://groups.google.com.ag/group/mbunituser/browse_thread/thread/5ae5a5ed9695431b
			/// http://groups.google.com/group/gallio-dev/browse_thread/thread/dbcbf053de78895d/623691c7c1257fea?lnk=gst&q=RemotingException#623691c7c1257fea
			//ConfigureWatinLogging();

			IE.Settings.WaitUntilExistsTimeOut = 180; //change the default of 30s
			IE.Settings.WaitForCompleteTimeOut = 180; //change the default of 30s

			////auto-close any existing browser instances
			//WatinUtility.CloseAllExistingBrowserInstances(true);
			////IE.Settings.CloseExistingBrowserInstances = true;
		}

		/// <summary>
		/// Configures Watin's logging to log to the Gallio TestLog.
		/// </summary>
		private static void ConfigureWatinLogging()
		{
			WatiN.Core.Logging.Logger.LogWriter = new GallioLogWatinLogWriter();
		}

		public static void InitializeIIS()
		{
			//HACK: The following method call is intended to try to resolve the intermittent "IE Timeout" errors which have been failing some of the CI builds.
			WatinUtility.OpenWebPageToInitializeWebApplication(ElementsCPSWatinUtility.CreateElementsCPSUri(""), true);
			WatinUtility.OpenWebPageToInitializeWebApplication(ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx"), true);
		}

		#region ElementsCPS-specific utility methods

		/// <summary>
		/// Returns an absolute Uri based on either an app-relative or absolute ElementsCPS URL.
		/// </summary>
		/// <param name="pageUrl">a ElementsCPS URL (either app-relative or absolute).</param>
		/// <returns>an absolute Uri.</returns>
		public static Uri CreateElementsCPSUri(string pageUrl)
		{
			string appRootUrl = ConfigurationManager.AppSettings["AppRootUrl"]; // "http://localhost/ElementsCPS/WebUI/";
			//appRootUrl = "http://localhost:1790/";
			Uri baseUri = new Uri(appRootUrl);
			return new Uri(baseUri, pageUrl);
		}

		/// <summary>
		/// Returns the file system path of the ElementsCPS web root folder.
		/// </summary>
		/// <returns></returns>
		public static string GetElementsCPSWebRootPath()
		{
			return ConfigurationManager.AppSettings["AppRootPath"]; // "C:\...\ElementsCPS\trunk\Source\Production\WebRoot\";
		}

		#region Specialized Assertion Methods

		/// <summary>
		/// Check whether the IE has duplicate message
		/// </summary>
		/// <param name="ie"></param>
		public static void AssertPageIsDisplayingUniqueConstraintViolationMessage(EnhancedIE ie)
		{
			WatinAssert.IsVisible(true, ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblError")));
			WatinAssert.PageBodyTextContains("This item would be a duplicate of an item that alreadys exists.", ie);
		}

		public static void AssertPageBodyTextContains(string[] substrings, EnhancedIE ie)
		{
			ie.WaitUntilContainsText(substrings[0], 5);
			WatinAssert.PageBodyTextContains(substrings, ie);
		}

		public static void AssertPageBodyTextContains(string substring, EnhancedIE ie)
		{
			ie.WaitUntilContainsText(substring, 5);
			WatinAssert.PageBodyTextContains(substring, ie);
		}

		public static void AssertPageBodyTextExcludes(string[] substrings, EnhancedIE ie)
		{
			ie.WaitUntilExcludesText(substrings[0], 5);
			WatinAssert.PageBodyTextExcludes(substrings, ie);
		}

		public static void AssertPageBodyTextExcludes(string substring, EnhancedIE ie)
		{
			ie.WaitUntilExcludesText(substring, 5);
			WatinAssert.PageBodyTextExcludes(substring, ie);
		}

		public static void AssertPageBodyHtmlContains(string substring, EnhancedIE ie)
		{
			ie.WaitUntilContainsHtml(substring, 5);
			WatinAssert.PageBodyHtmlContains(substring, ie);
		}

		public static void AssertPageBodyHtmlContains(string[] substrings, EnhancedIE ie)
		{
			ie.WaitUntilContainsHtml(substrings[0], 5);
			WatinAssert.PageBodyHtmlContains(substrings, ie);
		}

		public static void AssertPageBodyHtmlExcludes(string substring, EnhancedIE ie)
		{
			ie.WaitUntilExcludesHtml(substring, 5);
			WatinAssert.PageBodyHtmlExcludes(substring, ie);
		}

		public static void AssertPageBodyHtmlExcludes(string[] substrings, EnhancedIE ie)
		{
			ie.WaitUntilExcludesHtml(substrings[0], 5);
			WatinAssert.PageBodyHtmlExcludes(substrings, ie);
		}

		public static void AssertPageBodyTextContainsSequentially(string[] textFragments, EnhancedIE ie)
		{
			ie.WaitUntilContainsText(textFragments[0], 5);
			WatinAssert.PageBodyTextContainsSequentially(textFragments, ie);
		}

		public static void AssertPageBodyHtmlContainsSequentially(string[] htmlFragments, EnhancedIE ie)
		{
			ie.WaitUntilContainsHtml(htmlFragments[0], 5);
			WatinAssert.PageBodyHtmlContainsSequentially(htmlFragments, ie);
		}

		#endregion

		public static void CheckCreated(EnhancedIE ie, string creator, string createTime)
		{
			string expectedTime = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblCreatedOnValue")).Text.Split(' ').GetValue(0).ToString();
			string actualTime = createTime.Split(' ').GetValue(0).ToString();
			Assert.AreEqual(expectedTime, actualTime);
			Assert.AreEqual(ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblCreatedByValue")).Text, creator);
		}

		/// <summary>
		/// Check modifier and modifytime in the ie page
		/// </summary>
		/// <param name="ie">ie instance</param>
		/// <param name="modifier">modifier of the item</param>
		/// <param name="modifyTime">modifytime of the item</param>
		public static void CheckModified(EnhancedIE ie, string modifier, string modifyTime)
		{
			string expectedTime = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblModifiedOnValue")).Text.Split(' ').GetValue(0).ToString();
			string actualTime = modifyTime.Split(' ').GetValue(0).ToString();
			Assert.AreEqual(expectedTime, actualTime);
			Assert.AreEqual(ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblModifiedByValue")).Text, modifier);
		}

		/// <summary>
		/// Check modifier,modifytime,creator and createtime in the ie page
		/// </summary>
		/// <param name="ie">ie instance</param>
		/// <param name="creator">creator of the item</param>
		/// <param name="modifier">modifier of the item</param>
		/// <param name="createTime">createTime of the item</param>
		/// <param name="modifyTime">modifyTime of the item</param>
		public static void CheckCreatedAndModified(EnhancedIE ie, string creator, string modifier, string createTime, string modifyTime)
		{
			CheckCreated(ie, creator, createTime);
			CheckModified(ie, modifier, modifyTime);
		}

		public static void ClickSecondEditButton(EnhancedIE ie)
		{
			//bypass the first edit...
			bool isFirstTime = true;
			foreach (Button button in ie.Buttons)
			{
				if (button.Text == Constants.ButtonText_EditDotDotDot)
				{
					if (isFirstTime == true)
					{
						isFirstTime = false;
					}
					else
					{
						button.Click();
						break;
					}
				}
			}
		}

		public static void ClickThirdEditButton(EnhancedIE ie)
		{
			//bypass the first edit...
			int countNumber = 0;
			foreach (Button button in ie.Buttons)
			{
				if (button.Text == Constants.ButtonText_EditDotDotDot)
				{
					if (countNumber != 2)
					{
						countNumber++;
					}
					else
					{
						button.Click();
						break;
					}
				}
			}
		}

		/// <summary>
		/// Verify ie page contains contentToVerify and creator,createTime,modifier,modifyTime are correct
		/// </summary>
		/// <param name="ie"></param>
		/// <param name="contentToVerify"></param>
		/// <param name="creator"></param>
		/// <param name="createTime"></param>
		/// <param name="modifier"></param>
		/// <param name="modifyTime"></param>
		public static void VerifyCreateSuccess(EnhancedIE ie, string[] contentToVerify, string creator, DateTime createTime, string modifier, DateTime modifyTime)
		{
			AssertPageBodyHtmlContains(contentToVerify, ie);
			CheckModified(ie, creator, createTime.ToString());
			CheckCreated(ie, modifier, modifyTime.ToString());
		}

		/// <summary>
		/// Verify ie page contains contentToVerify and creator,createTime,modifier,modifyTime are correct
		/// </summary>
		/// <param name="ie"></param>
		/// <param name="contentToVerify"></param>
		/// <param name="creator"></param>
		/// <param name="createTime"></param>
		/// <param name="modifier"></param>
		/// <param name="modifyTime"></param>
		public static void VerifyModifySuccess(EnhancedIE ie, string[] contentToVerify, string modifier, DateTime modifyTime)
		{
			AssertPageBodyHtmlContains(contentToVerify, ie);
			CheckModified(ie, modifier, modifyTime.ToString());

		}

		#endregion

	}

	/// <summary>
	/// An implementation of <see cref="WatiN.Core.Interfaces.ILogWriter"/> that redirects the logged messages to the Gallio logs.
	/// </summary>
	/// <remarks>
	/// This class was adapted from the article found here:
	/// http://www.theroamingcoder.com/node/22
	/// </remarks>
	public class GallioLogWatinLogWriter : WatiN.Core.Interfaces.ILogWriter
	{
		//TODO: Refactoring: Migrate toHPFx
#warning Refactoring: Migrate toHPFx

		#region Implementation of ILogWriter

		public void LogAction(string message)
		{
			TestLog.WriteLine(message);
		}

		public void LogDebug(string message)
		{
			DiagnosticLog.WriteLine(message);
		}

		#endregion
	}

}