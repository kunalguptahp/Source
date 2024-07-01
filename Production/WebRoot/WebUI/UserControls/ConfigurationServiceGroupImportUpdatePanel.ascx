<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupImportUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupImportUpdatePanel" %>
<%@ Register Src="~/UserControls/ConfigurationServiceLabelValueImportListPanel.ascx"
    TagName="ConfigurationServiceLabelValueImportListPanel" TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/ConfigurationServiceQueryParameterValueImportListPanel.ascx"
    TagName="ConfigurationServiceQueryParameterValueImportListPanel" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCreatedByValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedOnValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCreatedByValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedByValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCPSId" runat="server" Text="CPS Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCPSIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCPSIdValue" runat="server"></asp:Label>
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
                <asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtDescription"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="8"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceApplicationName" runat="server" Text="Application Name: *"
                    SkinID="DataFieldLabel" AssociatedControlID="txtConfigurationServiceApplicationName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfigurationServiceApplicationName" runat="server" MaxLength="256"
                    Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvConfigurationServiceApplicationName" runat="server"
                    ControlToValidate="txtConfigurationServiceApplicationName" ErrorMessage="Please enter an application name.<br/>" />
                <HPFx:CustomValidator ID="cvConfigurationServiceApplicationName" runat="server" ErrorMessage="Please enter a validate application name."
                    ValidationGroup="SaveConfigurationServiceGroupImport" OnServerValidate="cvConfigurationServiceApplicationName_ServerValidate" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceGroupTypeName" runat="server" Text="Group Type Name: *"
                    SkinID="DataFieldLabel" AssociatedControlID="txtConfigurationServiceGroupTypeName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfigurationServiceGroupTypeName" runat="server" MaxLength="256"
                    Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvConfigurationServiceGroupTypeName" runat="server"
                    ControlToValidate="txtConfigurationServiceGroupTypeName" ErrorMessage="Please enter a group type name.<br/>" />
                <HPFx:CustomValidator ID="cvConfigurationServiceGroupTypeName" runat="server" ErrorMessage="Please enter a validate group type for this application name."
                    ValidationGroup="SaveConfigurationServiceGroupImport" OnServerValidate="cvConfigurationServiceGroupTypeName_ServerValidate" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblImportStatus" runat="server" Text="Import Status: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtImportStatus"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtImportStatus" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvImportStatus" runat="server" ControlToValidate="txtImportStatus"
                    ErrorMessage="Please enter an import status.<br/>" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblImportMessage" runat="server" Text="Import Message:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtImportMessage"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtImportMessage" runat="server" Columns="150" Rows="8" MaxLength="256"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblImportId" runat="server" Text="Import Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtImportId"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtImportId" runat="server" Columns="100" MaxLength="50"></asp:TextBox>
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
                <table>
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
                <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" SkinID="ImportItem" ValidationGroup="SaveConfigurationServiceGroupImport" Enabled="false" />
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                &nbsp; &nbsp;
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="SaveConfigurationServiceGroupImport" />
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" SkinID="CancelChanges" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlConfigurationServiceLabelValueImportListPanel" runat="server">
                    <div class="ChildGridTitle">
                        Label Values</div>
                    <ElementsCPSuc:ConfigurationServiceLabelValueImportListPanel ID="ucConfigurationServiceLabelValueImportList"
                        runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlConfigurationServiceQueryParameterValueImportListPanel" runat="server">
                    <div class="ChildGridTitle">
                        Query Parameter Values</div>
                    <ElementsCPSuc:ConfigurationServiceQueryParameterValueImportListPanel ID="ucConfigurationServiceQueryParameterValueImportList"
                        runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
