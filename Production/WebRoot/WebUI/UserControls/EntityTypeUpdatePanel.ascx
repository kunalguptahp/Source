<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityTypeUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.EntityTypeUpdatePanel" %>
<%@ Register Src="~/UserControls/NoteSummariesListPanel.ascx" TagName="NoteListPanel"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Entity Type Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server" ToolTip="Id"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
				(by
				<asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedOnValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
				(by
				<asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100" ToolTip="Name"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
					ErrorMessage="Please enter a name.<br/>" />
				<hpfx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
					SkinID="MinLength3Validator"
					ErrorMessage="Name is too short.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblStatus" runat="server" Text="Status:" SkinID="DataFieldLabel" AssociatedControlID="ddlStatus"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlStatus" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblComment" runat="server" Text="Comment:" SkinID="DataFieldLabel" AssociatedControlID="txtComment"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtComment" runat="server" Columns="100" Rows="8" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table class="table.layoutTable">
					<tr>
						<td>
							<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2">
				<asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click" />
				&nbsp;
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				&nbsp;
				<asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
					OnClick="btnCancel_Click" />
			</td>
		</tr>
	</table>
	<table>
		<tr>
			<td colspan="2">
				<asp:Panel ID="pnlNoteList" runat="server">
					<div class="ChildGridTitle">
						Related Notes</div>
					<ElementsCPSuc:NoteListPanel ID="ucNoteList" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
