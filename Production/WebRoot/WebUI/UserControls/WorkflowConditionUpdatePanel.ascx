<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowConditionUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowConditionUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Condition Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedOnValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
                <asp:Panel ID="pnlDataControls_Name" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="25"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Please enter a name.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
                        SkinID="MinLength3Validator" ErrorMessage="Name is too short.</br>" />
                </asp:Panel>
            </td>
		</tr>
        <tr>
            <td>
                <asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtDescription"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="8" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
			<td>
				<asp:Label ID="lblOperator" runat="server" Text="Operator: *" SkinID="DataFieldLabel" AssociatedControlID="ddlOperator"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Operator" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlOperator" runat="server">
                        <asp:ListItem><</asp:ListItem>
                        <asp:ListItem><=</asp:ListItem>
                        <asp:ListItem>=</asp:ListItem>
                        <asp:ListItem>!=</asp:ListItem>
                        <asp:ListItem>></asp:ListItem>
                        <asp:ListItem>>=</asp:ListItem>
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvOperator" runat="server" ControlToValidate="ddlOperator"
                        ErrorMessage="Please enter an operator.<br/>" />
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblValue" runat="server" Text="Value: *" SkinID="DataFieldLabel" AssociatedControlID="txtValue"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Value" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtValue" runat="server" MaxLength="25"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
                        ErrorMessage="Please enter a value.<br/>" />
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblStatus" runat="server" Text="Status:" SkinID="DataFieldLabel" AssociatedControlID="ddlStatus"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Status" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </asp:Panel>
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
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" Enabled='<%# this.IsDataModificationAllowed() %>' />
				&nbsp;
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				&nbsp;
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" Enabled='<%# this.IsDataModificationAllowed() %>' />
			</td>
		</tr>
	</table>
</asp:Panel>
