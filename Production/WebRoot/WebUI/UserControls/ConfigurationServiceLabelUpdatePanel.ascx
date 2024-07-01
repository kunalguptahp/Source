<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceLabelUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceLabelUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Label Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
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
				<asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="8" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblHelp" runat="server" Text="Help:" SkinID="DataFieldLabel" AssociatedControlID="txtHelp"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtHelp" runat="server" Columns="100" Rows="4" MaxLength="256" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
        <tr>
            <td>
                <asp:Label ID="lblElementsKey" runat="server" Text="Elements Key: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtElementsKey"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtElementsKey" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvElementsKey" runat="server" ControlToValidate="txtElementsKey"
                    ErrorMessage="Please enter a key.<br/>" />
            </td>
        </tr>
        <tr>
			<td>
				<asp:Label ID="lblLabelType" runat="server" Text="Label Type: *" SkinID="DataFieldLabel" AssociatedControlID="ddlLabelType"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlLabelType" runat="server">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvLabelType" runat="server" ControlToValidate="ddlLabelType"
					ErrorMessage="Please select a label type.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblValueList" runat="server" Text="List Values (comma separated):" SkinID="DataFieldLabel" AssociatedControlID="txtValueList"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtValueList" runat="server" Columns="100" Rows="5" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
        <tr>
			<td>
				<asp:Label ID="lblItem" runat="server" Text="Item: *" SkinID="DataFieldLabel" AssociatedControlID="ddlItem"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlItem" runat="server">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="ddlItem"
					ErrorMessage="Please select an configuration service item.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblInputRequired" runat="server" Text="Input Required:" SkinID="DataFieldLabel" AssociatedControlID="chkInputRequired"></asp:Label>
			</td>
			<td>
                <asp:CheckBox ID="chkInputRequired" runat="server"/>
            </td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblSortOrder" runat="server" Text="Order: *" SkinID="DataFieldLabel" AssociatedControlID="txtSortOrder"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvSortOrder" runat="server" ControlToValidate="txtSortOrder"
					ErrorMessage="Please enter order number.<br/>" />
				<HPFx:RegularExpressionValidator ID="revSortOrder" runat="server" ControlToValidate="txtSortOrder"
					SkinID="IntValidator"/>
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
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" />
				&nbsp;
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				&nbsp;
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
			</td>
		</tr>
	</table>
</asp:Panel>
