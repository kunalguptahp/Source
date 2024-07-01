<%@ Import Namespace="HP.HPFx.Extensions.Text.StringManipulation" %>
<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceQueryParameterValueImportUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceQueryParameterValueImportUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Label Value Id:" SkinID="DataFieldLabel"
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
                <asp:Label ID="lblConfigurationServiceGroupImport" runat="server" Text="Group Import Name:"
                    SkinID="DataFieldLabel" AssociatedControlID="lblConfigurationServiceGroupImportValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblConfigurationServiceGroupImportValue" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnConfigurationServiceGroupImportId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLabelQueryParameterName" runat="server" Text="Query Parameter Name: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtQueryParameterName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQueryParameterName" runat="server" MaxLength="64" Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvQueryParameterName" runat="server" ControlToValidate="txtQueryParameterName"
                    ErrorMessage="Please enter a query parameter name.<br/>" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQueryParameterValue" runat="server" Text="Query Parameter Value: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtQueryParameterValue"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQueryParameterValue" runat="server" MaxLength="256" Columns="150"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvQueryParameterValue" runat="server" ControlToValidate="txtQueryParameterValue"
                    ErrorMessage="Please enter an query parameter value.<br/>" />
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
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" />
                            &nbsp;
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                SkinID="CancelChanges" />
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="lnkConfigurationServiceGroupImport" runat="server" NavigateUrl='<%# Global.GetConfigurationServiceGroupImportUpdatePageUri(this.hdnConfigurationServiceGroupImportId.Value.TryParseInt32(), null) %>'
                                Text="Return to Configuration Service Group Import" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
