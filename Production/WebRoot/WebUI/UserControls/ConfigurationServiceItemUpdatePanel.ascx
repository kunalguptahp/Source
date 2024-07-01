<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceItemUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceItemUpdatePanel" %>
<%@ Register Src="~/UserControls/ConfigurationServiceLabelListPanel.ascx" TagName="ConfigurationServiceLabelListPanel"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Item Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
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
					SkinID="MinLength3Validator" ErrorMessage="Name is too short.</br>" />
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
				<asp:Label ID="lblParent" runat="server" Text="Parent:" SkinID="DataFieldLabel" AssociatedControlID="chkParent"></asp:Label>
			</td>
			<td>
				<asp:CheckBox ID="chkParent" runat="server">
				</asp:CheckBox>
			</td>
		</tr>		
		<tr>
			<td>
				<asp:Label ID="lblElementsKey" runat="server" Text="Elements Key: *" SkinID="DataFieldLabel" AssociatedControlID="txtElementsKey"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtElementsKey" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvElementsKey" runat="server" ControlToValidate="txtElementsKey"
					ErrorMessage="Please enter a key.<br/>" />
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
				<asp:Label ID="lblConfigurationServiceGroupType" runat="server" Text="Group Type:" SkinID="DataFieldLabel" AssociatedControlID="chkConfigurationServiceGroupTypeList"></asp:Label>
			</td>
			<td>
				<asp:CheckBoxList ID="chkConfigurationServiceGroupTypeList" runat="server" RepeatColumns="3">
				</asp:CheckBoxList>
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
		<tr>
			<td colspan="2">
				<asp:Panel ID="pnlConfigurationServiceLabelListPanel" runat="server">
					<div class="ChildGridTitle">
						Configuration Service Label</div>
					<ElementsCPSuc:ConfigurationServiceLabelListPanel ID="ucConfigurationServiceLabelList" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
