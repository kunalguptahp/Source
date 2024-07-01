<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JumpstationMacroValueUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationMacroValueUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Parameter Value Id:" SkinID="DataFieldLabel"
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
                <asp:Label ID="lblMatchName" runat="server" Text="Match Name: " SkinID="DataFieldLabel"
                    AssociatedControlID="txtMatchName"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlMatchName" runat="server">
                    <asp:TextBox ID="txtMatchName" runat="server" MaxLength="256" Columns="150"></asp:TextBox>
                </asp:Panel>
                <hpFx:CustomValidator ID="cvNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                    OnServerValidate="cvNameUnique_ServerValidate" ValidationGroup="SaveJumpstationMacroValue"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResultValue" runat="server" Text="Result Value: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtResultValue"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlResultValue" runat="server">
                    <asp:TextBox ID="txtResultValue" runat="server" MaxLength="256" Columns="150"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvResultValue" runat="server" ControlToValidate="txtResultValue"
                        ValidationGroup="SaveJumpstationMacroValue" ErrorMessage="Please enter a result value.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJumpstationMacro" runat="server" Text="Jumpstation Macro" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlJumpstationMacro"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlJumpstationMacro" runat="server">
                </asp:DropDownList>
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
			<td style="text-align: Center" colspan="2">
                <table width="100%">
                    <tr>
                        <td align="left">
            				<asp:Button ID="btnCreate" runat="server" SkinID="CreateItem" OnClick="btnCreate_Click" ValidationGroup="SaveJumpsationMacroValue" />		        
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click"
                                ValidationGroup="SaveJumpstationMacroValue" />
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            <asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
                                OnClick="btnCancel_Click" />
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="lnkJumpstationMacro" runat="server" SkinID="NewRecordLink"
                                NavigateUrl='<%# Global.GetJumpstationMacroUpdatePageUri(Convert.ToInt32(this.ddlJumpstationMacro.SelectedValue), null) %>'
                                Text="Return to Jumpstation Macro" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
