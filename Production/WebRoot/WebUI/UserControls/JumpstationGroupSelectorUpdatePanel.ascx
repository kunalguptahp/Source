<%@ Import Namespace="HP.HPFx.Extensions.Text.StringManipulation"%>
<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationGroupSelectorUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupSelectorUpdatePanel" %>
<%@ Register Src="~/UserControls/JumpstationQueryParameterValueEditListUpdatePanel.ascx" TagName="JumpstationQueryParameterValueEditListUpdatePanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Jumpstation Selector Id:" SkinID="DataFieldLabel"
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
                <asp:Label ID="lblJumpstationGroup" runat="server" Text="Jumpstation:"
                    SkinID="DataFieldLabel" AssociatedControlID="lblJumpstationGroupValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblJumpstationGroupValue" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnJumpstationGroupId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Name" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ValidationGroup="SaveJumpstationGroupSelector"
                        ErrorMessage="Please enter a name.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName" ValidationGroup="SaveJumpstationGroupSelector"
                        SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
                    <HPFx:CustomValidator ID="cvNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                        OnServerValidate="cvNameUnique_ServerValidate" ValidationGroup="SaveJumpstationGroupSelector"/>
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
                    <asp:TextBox ID="txtDescription" runat="server" Columns="100" Rows="8" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblParameter" runat="server" Text="Parameter:" SkinID="DataFieldLabel"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_QueryParameterValueEditListUpdate" runat="server"
                    Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <table border="1" width="100%">
                        <tr>
                            <asp:Repeater ID="repQueryParameter" runat="server" OnItemDataBound="repQueryParameter_ItemDataBound">
                                <ItemTemplate>
                                    <td valign="top">
                                        <asp:HiddenField ID="hdnQueryParameterId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "QueryParameterId")%>' />
                                        <asp:HiddenField ID="hdnWildcard" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Wildcard") %>' />
                                        <asp:HiddenField ID="hdnMaximumSelection" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "MaximumSelection") %>' />
                                        <ElementsCPSuc:JumpstationQueryParameterValueEditListUpdatePanel ID="ucQueryParameterValue"
                                            runat="server" />
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </table>
                    <HPFx:CustomValidator ID="cvQueryParameterValueMinimum" runat="server" ErrorMessage="Please check parameters.</br>"
                        OnServerValidate="cvQueryParameterValue_ServerValidate" ValidationGroup="SaveJumpstationGroupSelector" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="table.layoutTable">
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveJumpstationGroupSelector" />
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
                            <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" SkinID="CreateItem" ValidationGroup="SaveJumpstationGroupSelector" />
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem"  />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="SaveJumpstationGroupSelector" />
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                SkinID="CancelChanges" />
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="lnkJumpstationGroup" runat="server" SkinID="NewRecordLink"
                                NavigateUrl='<%# Global.GetJumpstationGroupUpdatePageUri(this.hdnJumpstationGroupId.Value.TryParseInt32(), null) %>'
                                Text="Return to Jumpstation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
