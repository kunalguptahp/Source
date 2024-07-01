<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleReportPanel" %>
<%@ Register Src="~/UserControls/WorkflowModuleConditionReportPanel.ascx" TagName="WorkflowModuleConditionReportPanel"
    TagPrefix="ElementsCPSuc" %>
<table border="1">
    <asp:Repeater ID="repWorkflowModule" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblIdLabel" runat="server" Text="Id:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td width="100%">
                    <asp:Label ID="lblIdValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNameLabel" runat="server" Text="Name:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td width="100%">
                    <asp:Label ID="lblNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescriptionLabel" runat="server" Text="Description:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDescriptionValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTitleLabel" runat="server" Text="Title:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTitleValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategoryLabel" runat="server" Text="Category:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCategoryValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModuleCategoryName")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSubCategoryLabel" runat="server" Text="Sub-Category:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSubCategoryValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModuleSubCategoryName")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVersionMajorLabel" runat="server" Text="Version Major:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVersionMajorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMajor")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVersionMinorLabel" runat="server" Text="Version Minor:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVersionMinorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMinor")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFilenameLabel" runat="server" Text="Filename:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFilenameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Filename")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWorkflowModuleConditionLabel" runat="server" Text="Condition(s):"
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="pnlWorkflowModuleCondition" runat="server">
                        <ElementsCPSuc:WorkflowModuleConditionReportPanel ID="ucWorkflowModuleConditionReport"
                            WorkflowModuleId='<%#DataBinder.Eval(Container.DataItem, "Id")%>' runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCreatedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedOn")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblModifiedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedOn")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCreatedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedBy")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblModifiedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedBy")%>' />
                </td>
            </tr>
        </ItemTemplate>
        <SeparatorTemplate>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </SeparatorTemplate>
    </asp:Repeater>
</table>
