using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	/// <summary>
	/// A <see cref="DropDownList"/> control that is safe to use with the <see cref="CascadingDropDown"/> control even when <c>EnableEventValidation</c> is <c>true</c>.
	/// </summary>
	/// <remarks>
	/// See the following article for more info:
	/// http://ko-sw.blogspot.com/2009/03/using-ajax-cascadingdropdown-extender.html
	/// <para>
	/// Excerpt:
	/// Using the CascadingDropDown control extender that ships with the AJAX Control Toolkit requires that EnableEventValidation property of the Page containing the target DropDownList control be set to false.
	/// However, this can potentially expose our page to a malicious attack, since nothing can then prevent "generated" post-backs.
	/// A more elegant approach is creating a class that derives from DropDownList as follows:
	/// <c>public class NoValidationDropDownList : DropDownList { }</c>
	/// After the class has been created, all we need to do is replace the instances of the DropDownList class with respective instances of the NoValidationDropDownList class.
	/// The reason why this works is simple: ASP.NET only validates controls that are marked with the SupportsEventValidation attribute. Since our class is not marked with this attribute, ASP.NET does not validate items on post-back and, consequently, no exception is thrown.
	/// Cheers,
	/// Kirill 
	/// Posted by kerido at 11:17 AM  
	/// Labels: ajax, asp.net, controls 
	/// </para>
	/// </remarks>
	public class CascadingDropDownList : DropDownList
	{

		//TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

	}
}