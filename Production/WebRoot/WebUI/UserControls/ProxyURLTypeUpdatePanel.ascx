<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProxyURLTypeUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ProxyURLTypeUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Redirector Type Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
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
				<asp:Label ID="lblElementsKey" runat="server" Text="Elements Key: *" SkinID="DataFieldLabel" AssociatedControlID="txtElementsKey"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtElementsKey" runat="server" Columns="50" MaxLength="50" ></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvElementsKey" runat="server" ControlToValidate="txtElementsKey"
					ErrorMessage="Please enter a key.<br/>" />
				<HPFx:RegularExpressionValidator ID="revElementsKeyNumber" runat="server" ControlToValidate="txtElementsKey"
					SkinID="IntValidator"/>
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
				<asp:Label ID="lblDomain" runat="server" Text="Domain: *" SkinID="DataFieldLabel" AssociatedControlID="ddlDomain"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlDomain" runat="server">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvDomain" runat="server" ControlToValidate="ddlDomain"
					ErrorMessage="Please select a Domain.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblGroupType" runat="server" Text="Group Type: *" SkinID="DataFieldLabel" AssociatedControlID="ddlGroupType"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlGroupType" runat="server">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvGroupType" runat="server" ControlToValidate="ddlGroupType"
					ErrorMessage="Please select a Group Type.<br/>" />
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
