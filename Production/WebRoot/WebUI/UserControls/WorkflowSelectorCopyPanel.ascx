<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowSelectorCopyPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowSelectorCopyPanel" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblId" runat="server" Text="Group Selector Ids:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblIdText"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdText" runat="server"></asp:Label>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
					ErrorMessage="Please enter a name.<br/>" />
				<HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
					SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
			<td>
    	    	<asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="2" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="saveWorkflowSelector" />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"  ValidationGroup="saveWorkflowSelector" />
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    SkinID="CancelChanges" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Panel ID="pnlCPSLog" runat="server">
                    <ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
