<%@ Import Namespace="HP.HPFx.Extensions.Text.StringManipulation"%>
<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowSelectorUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowSelectorUpdatePanel" %>
<%@ Register Src="~/UserControls/WorkflowQueryParameterValueEditListUpdatePanel.ascx" TagName="WorkflowQueryParameterValueEditListUpdatePanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Group Selector Id:" SkinID="DataFieldLabel"
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
                <asp:Label ID="lblWorkflow" runat="server" Text="Configuration Service Group:"
                    SkinID="DataFieldLabel" AssociatedControlID="lblWorkflowValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWorkflowValue" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnWorkflowId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Name" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="20" Columns="150"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ValidationGroup="SaveWorkflowSelector"
                        ErrorMessage="Please enter a name.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName" ValidationGroup="SaveWorkflowSelector"
                        SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
                    <HPFx:CustomValidator ID="cvSaveNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                        OnServerValidate="cvSaveNameUnique_ServerValidate" ValidationGroup="SaveUniqueNameWorkflowSelector"/>
                    <HPFx:CustomValidator ID="cvSaveAsNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                        OnServerValidate="cvSaveAsNameUnique_ServerValidate" ValidationGroup="SaveAsUniqueNameWorkflowSelector"/>
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
                    <asp:TextBox ID="txtDescription" runat="server" Columns="100" MaxLength="512" Rows="8" TextMode="MultiLine"></asp:TextBox>
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
                                        <ElementsCPSuc:WorkflowQueryParameterValueEditListUpdatePanel ID="ucQueryParameterValue" MinimumSelection="1" runat="server" />
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </table>
                    <HPFx:CustomValidator ID="cvQueryParameterValueMinimum" runat="server" ErrorMessage="Please check parameters.</br>"
                        OnServerValidate="cvQueryParameterValue_ServerValidate" ValidationGroup="SaveWorkflowSelector" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="table.layoutTable">
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveWorkflowSelector" />
                            <asp:ValidationSummary ID="vsSaveUniqueNameSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveUniqueNameWorkflowSelector" />
                            <asp:ValidationSummary ID="vsSaveAsUniqueNameSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveAsUniqueNameWorkflowSelector" />
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
                            <asp:Button ID="btnSaveAsNew" runat="server" OnClick="btnSaveAsNew_Click" SkinID="SaveAsChanges" Text="Save As New" ValidationGroup="SaveWorkflowSelector" />
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="SaveWorkflowSelector" />
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                SkinID="CancelChanges" />
                            <asp:HyperLink ID="lnkWorkflow" runat="server" SkinID="NewRecordLink"
                                NavigateUrl='<%# Global.GetWorkflowUpdatePageUri(this.hdnWorkflowId.Value.TryParseInt32(), null) %>'
                                Text="Return to Workflow" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
