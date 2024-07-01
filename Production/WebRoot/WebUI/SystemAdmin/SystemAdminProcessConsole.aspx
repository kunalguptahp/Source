<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="SystemAdminProcessConsole.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.SystemAdminProcessConsole"
	Title="CPS - System Admin - File Download" %>
<%@ MasterType TypeName="HP.ElementsCPS.Apps.WebUI.MasterPages.SystemAdminMaster" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<h1 class="SecurityNotice">NOTE: Do NOT use the browser's Refresh button on this page.</h1>

		<h4>Process Console:</h4>
		<table class="layoutTable">
<%--			<tr>
				<td>
					Working folder: 
				</td>
				<td>
					<asp:TextBox ID="txtWorkingFolder" runat="server" Columns="100" />
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td>
					<HPFx:CustomValidator ID="cvWorkingFolderExists" runat="server" ErrorMessage="The specified working folder does not exist."
						OnServerValidate="cvWorkingFolderExists_ServerValidate" />
				</td>
			</tr>
--%>			<tr>
				<td>
					Process/Program File: 
				</td>
				<td>
					<asp:TextBox ID="txtProcessFilePath" runat="server" Columns="100" />
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td>
					<HPFx:RequiredFieldValidator ID="rfvProcessFilePath" runat="server" ControlToValidate="txtProcessFilePath"
						ErrorMessage="Please enter a program or process file.<br/>" />
					<HPFx:CustomValidator ID="cvProcessFileExists" runat="server" ErrorMessage="The specified process file does not exist."
						OnServerValidate="cvProcessFileExists_ServerValidate" />
				</td>
			</tr>
			<tr>
				<td>
					Process Arguments: 
				</td>
				<td>
					<asp:TextBox ID="txtProcessArguments" runat="server" Columns="100" />
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td>
					<asp:Button ID="btnExecute" Text="Execute" OnClick="btnExecute_Click" runat="server"
						CausesValidation="true"
					>
					</asp:Button>
				</td>
			</tr>
		</table>

		<hr />
		<asp:Label ID="lblStatus" runat="server" Font-Size="X-Large">
		</asp:Label>

		<br />
		Output:
		<br />
		<pre class="SourceCode" style="width: 80%;"><code><asp:Literal ID="litOutput" runat="server" /></code></pre>

		<br />
		Error Output:
		<br />
		<pre class="SourceCode" style="width: 80%;"><code><asp:Literal ID="litErrorOutput" runat="server" /></code></pre>
	</div>
</asp:Content>
