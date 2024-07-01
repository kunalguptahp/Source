<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupImportEditUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupImportEditUpdatePanel" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblId" runat="server" Text="Group Import Ids:" SkinID="DataFieldLabel" AssociatedControlID="lblIdText"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdText" runat="server"></asp:Label>
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
				<asp:Label ID="lblName" runat="server" Text="Name:" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
                        SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="2" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblConfigurationServiceApplication" runat="server" Text="Application: " SkinID="DataFieldLabel" AssociatedControlID="ddlConfigurationServiceApplication"></asp:Label>
			</td>
            <td>
                <asp:DropDownList ID="ddlConfigurationServiceApplication" runat="server" AutoPostBack="true" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceGroupType" runat="server" Text="Group Type: "
                    SkinID="DataFieldLabel" AssociatedControlID="ddlConfigurationServiceGroupType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlConfigurationServiceGroupType" runat="server" ></asp:DropDownList>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblImportStatus" runat="server" Text="Import Status: " SkinID="DataFieldLabel" AssociatedControlID="txtImportStatus"></asp:Label>
			</td>
            <td>
                <asp:TextBox ID="txtImportStatus" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblImportMessage" runat="server" Text="Import Message:" SkinID="DataFieldLabel" AssociatedControlID="txtImportMessage"></asp:Label>
			</td>
            <td>
                <asp:TextBox ID="txtImportMessage" runat="server" Columns="150" MaxLength="512" Rows="2" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
		<tr>
			<td colspan="2">
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" />
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
				&nbsp;
				<asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" SkinID="ImportItem" />
				&nbsp;
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Panel ID="pnlCPSLog" runat="server" Visible="false">
					<ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
