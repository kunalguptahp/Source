<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowVersionReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowVersionReportPanel" %>
<table border="1">
    <asp:Repeater ID="repWorkflowVersion" runat="server">
        <HeaderTemplate>
            <tr>
                <td colspan="6" align="center">
                    Workflow Version
                </td>
            </tr>
            <tr>
                <td>
                    Major Version
                </td>
                <td>
                    Minor Version
                </td>
                <td>
                    Created On
                </td>
                <td>
                    Modified On
                </td>
                <td>
                    Created By
                </td>
                <td>
                    Modified By
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblVersionMajorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMajor")%>' />
                </td>
                <td>
                    <asp:Label ID="lblVersionMinorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMinor")%>' />
                </td>
                <td>
                    <asp:Label ID="lblCreatedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedOn")%>' />
                </td>
                <td>
                    <asp:Label ID="lblModifiedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedOn")%>' />
                </td>
                <td>
                    <asp:Label ID="lblCreatedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedBy")%>' />
                </td>
                <td>
                    <asp:Label ID="lblModifiedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedBy")%>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<br/>
<br/>
<table border="1">
    <asp:Repeater ID="repWorkflowModuleVersion" runat="server">
        <HeaderTemplate>
            <tr>
                <td colspan="8" align="center">
                    Workflow Module Version
                </td>
            </tr>
            <tr>
                <td>
                    Category
                </td>
                <td>
                    Sub-Category
                </td>
                <td>
                    Major Version
                </td>
                <td>
                    Minor Version
                </td>
                <td>
                    Created On
                </td>
                <td>
                    Modified On
                </td>
                <td>
                    Created By
                </td>
                <td>
                    Modified By
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblCategoryNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WorkflowModuleCategoryName")%>' />
                </td>
                <td>
                    <asp:Label ID="lblSubCategoryNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WorkflowModuleSubCategoryName")%>' />
                </td>
                <td>
                    <asp:Label ID="lblVersionMajorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMajor")%>' />
                </td>
                <td>
                    <asp:Label ID="lblVersionMinorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VersionMinor")%>' />
                </td>
                <td>
                    <asp:Label ID="lblCreatedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedOn")%>' />
                </td>
                <td>
                    <asp:Label ID="lblModifiedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedOn")%>' />
                </td>
                <td>
                    <asp:Label ID="lblCreatedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedBy")%>' />
                </td>
                <td>
                    <asp:Label ID="lblModifiedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedBy")%>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
