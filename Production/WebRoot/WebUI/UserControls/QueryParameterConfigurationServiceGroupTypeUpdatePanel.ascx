<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryParameterConfigurationServiceGroupTypeUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.QueryParameterConfigurationServiceGroupTypeUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Application Id:" SkinID="DataFieldLabel"
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
                <asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="8" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceGroupType" runat="server" Text="Group Type: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlConfigurationServiceGroupType"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ConfigurationServiceGroupType" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlConfigurationServiceGroupType" runat="server">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvConfigurationServiceGroupType" runat="server" ControlToValidate="ddlConfigurationServiceGroupType"
                        ErrorMessage="Please select a configuration service group type.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQueryParameter" runat="server" Text="Query Parameter: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlQueryParameter"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_QueryParameter" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlQueryParameter" runat="server">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvQueryParameter" runat="server" ControlToValidate="ddlQueryParameter"
                        ErrorMessage="Please select a query parameter.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWildcard" runat="server" Text="Include Wildcard (*):" SkinID="DataFieldLabel"
                    AssociatedControlID="chkWildcard"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Wildcard" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:CheckBox ID="chkWildcard" runat="server" Checked="true"></asp:CheckBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMaximumSelection" runat="server" Text="Maximum Selections Allowed (0 = all): *"
                    SkinID="DataFieldLabel" AssociatedControlID="txtMaximumSelection"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_MaxmimumSelection" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtMaximumSelection" runat="server" Text="0"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvMaxiumSelection" runat="server" ControlToValidate="txtMaximumSelection"
                        ErrorMessage="Please enter a maximum selection value.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revMaximumSelection" runat="server" ControlToValidate="txtMaximumSelection"
                        SkinID="IntValidator" />
                    <asp:RangeValidator ID="rvMaximumSelection" runat="server" ErrorMessage="Please enter a valid integer (0 - 2147483647)."
                        MinimumValue="0" MaximumValue="2147483647" ControlToValidate="txtMaximumSelection"
                        Type="Integer">
                    </asp:RangeValidator>
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
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" Enabled='<%# this.IsDataModificationAllowed() %>' />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" />
                            &nbsp;
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                SkinID="CancelChanges" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
