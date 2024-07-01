<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="SystemAdminFileUpload.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.SystemAdminFileUpload"
	Title="CPS - System Admin - File Upload" %>
<%@ MasterType TypeName="HP.ElementsCPS.Apps.WebUI.MasterPages.SystemAdminMaster" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<h1 class="SecurityNotice">NOTE: Do NOT use the browser's Refresh button on this page.</h1>

		<h4>Select a file to upload:</h4>

		<asp:FileUpload ID="fuFile" runat="server"></asp:FileUpload>

		<br />
		<br />
		<h4 style="color:Red;">NOTE: You will need to re-select the file before each action (e.g. view, preview, import):</h4>

		<br />
		Destination path: 
		<asp:TextBox ID="txtDestinationPath" runat="server" Columns="100" />
		<br />
		<asp:Button ID="btnUpload" Text="Upload file" OnClick="btnUpload_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<asp:Button ID="btnViewFileContent" Text="View raw data" OnClick="btnViewFileContent_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<br />
<%--		<HPFx:RequiredFieldValidator ID="rfvDestinationPath" runat="server" ControlToValidate="txtDestinationPath"
			ErrorMessage="Please enter a destination path for the uploaded file.<br/>" />
--%>
		<HPFx:CustomValidator ID="cvFileRequired" runat="server" ErrorMessage="Please select a data file."
			OnServerValidate="cvFileRequired_ServerValidate" />
<%--		<HPFx:CustomValidator ID="cvFileNotEmpty" runat="server" ErrorMessage="The uploaded file is empty."
			OnServerValidate="cvFileNotEmpty_ServerValidate" />
--%>
		<hr />
		<asp:Label ID="lblStatus" runat="server" Font-Size="X-Large">
		</asp:Label>

		<br />
		<asp:TextBox ID="txtOutput" runat="server" TextMode="MultiLine" Rows="20" Columns="250" Wrap="false" Visible="false">
		</asp:TextBox>
	</div>
</asp:Content>
