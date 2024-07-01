<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="SystemAdminFileDownload.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.SystemAdminFileDownload"
	Title="CPS - System Admin - File Download" %>
<%@ MasterType TypeName="HP.ElementsCPS.Apps.WebUI.MasterPages.SystemAdminMaster" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<h1 class="SecurityNotice">NOTE: Do NOT use the browser's Refresh button on this page.</h1>

		<h4>Select a file to download:</h4>
		File path: 
		<asp:TextBox ID="txtTargetFilePath" runat="server" Columns="100" />
		<br />
		<asp:Button ID="btnDownload" Text="Download file" OnClick="btnDownload_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<asp:Button ID="btnViewFileContent" Text="View file contents" OnClick="btnViewFileContent_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<br />
		<HPFx:RequiredFieldValidator ID="rfvTargetFilePath" runat="server" ControlToValidate="txtTargetFilePath"
			ErrorMessage="Please enter a file path for the downloaded file.<br/>" />
		<HPFx:CustomValidator ID="cvFileExists" runat="server" ErrorMessage="The specified file does not exist."
			OnServerValidate="cvFileExists_ServerValidate" />

		<hr />
		<asp:Label ID="lblStatus" runat="server" Font-Size="X-Large">
		</asp:Label>

		<br />
		<pre class="SourceCode" style="width: 80%;"><code><asp:Literal ID="litOutput" runat="server" /></code></pre>
	</div>
</asp:Content>
