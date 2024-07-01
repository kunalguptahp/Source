<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JumpstationMacroMultiReplaceUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationMacroMultiReplaceUpdatePanel" %>
<%@ Register Src="~/UserControls/JumpstationMacroDescriptionMultiReplaceUpdatePanel.ascx" TagName="JumpstationMacroDescriptionMultiReplaceUpdatePanel"
	TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblMacros" runat="server" Text="Macros:" SkinID="DataFieldLabel"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlQueryParameterValueMultiUpdatePanel" runat="server">
					<ElementsCPSuc:JumpstationMacroDescriptionMultiReplaceUpdatePanel ID="ucJumpstationMacroDescriptionMultiReplaceUpdate" runat="server" />
				</asp:Panel>
            </td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblOwner" runat="server" Text="Owner: " SkinID="DataFieldLabel" AssociatedControlID="ddlOwner"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlOwner" runat="server" Enabled="false">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvOwner" runat="server" ControlToValidate="ddlOwner"
					ErrorMessage="Please select an Owner.<br/>" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveJumpstationMacro" />
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"  ValidationGroup="SaveJumpstationMacro" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Panel ID="pnlCPSLog" runat="server">
					<ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
