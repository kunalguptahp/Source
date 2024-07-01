<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.PersonUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Person Id:" SkinID="DataFieldLabel"
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
                (by
                <asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
                )
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedOnValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
                (by
                <asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
                )
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Name:" SkinID="DataFieldLabel" AssociatedControlID="lblNameText"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNameText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWindowsId" runat="server" Text="Windows Id: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtWindowsId"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWindowsId" runat="server" MaxLength="256" Columns="100" ToolTip="Windows Id (e.g. AMERICAS\smith)"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvWindowsId" runat="server" ControlToValidate="txtWindowsId"
                    ErrorMessage="Please enter a windows Id (e.g. AMERICAS\smith).<br/>" />
                <HPFx:RegularExpressionValidator ID="revWindowsId" runat="server" SkinID="WindowsNTIdentifierValidator"
                    ControlToValidate="txtWindowsId" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtFirstName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="Please enter a first name.<br/>" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtMiddleName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtLastName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="Please enter a last name.<br/>" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email: *" SkinID="DataFieldLabel" AssociatedControlID="txtEmail"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                <HPFx:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter an email (e.g. john.smith@hp.com).<br/>" />
                <HPFx:RegularExpressionValidator ID="revEmail" runat="server" SkinID="EmailAddressValidator"
                    ControlToValidate="txtEmail" />
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                <asp:Label ID="lblRole" runat="server" Text="Roles:" SkinID="DataFieldLabel" AssociatedControlID="chkRoleList"></asp:Label>
            </td>
            <td  >
                <asp:CheckBoxList ID="chkRoleList" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                Viewer: <asp:CheckBoxList ID="chkApplicationList1" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
                Editor: <asp:CheckBoxList ID="chkApplicationList2" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
                Validator: <asp:CheckBoxList ID="chkApplicationList3" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
                DataAdmin: <asp:CheckBoxList ID="chkApplicationList4" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTenantGroup" runat="server" Text="Tenant : " SkinID="DataFieldLabel"
                    AssociatedControlID="ddlTenantGroup"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_TenantGroup" runat="Server">
                    <asp:DropDownList ID="ddlTenantGroup" runat="server" AutoPostBack="true" Width="120px">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvTenantGroup" runat="server" ControlToValidate="ddlTenantGroup"
                        ErrorMessage="Please select a TenantGroup.<br/>" />
                </asp:Panel>
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
                <asp:HyperLink ID="lnkDone" runat="server" SkinID="DoneLink" Visible='<%# !this.DataItem.IsNew %>'
                    NavigateUrl='<%# Global.GetPersonDetailPageUri(this.DataItem.Id, null) %>' />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnSaveAndDone" runat="server" SkinID="SaveChangesAndDone" OnClick="btnSaveAndDone_Click" />
                &nbsp;
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
