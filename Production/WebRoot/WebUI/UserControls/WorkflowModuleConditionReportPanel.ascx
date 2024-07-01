<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleConditionReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleConditionReportPanel" %>
<table border="1">
    <asp:Repeater ID="repWorkflowModuleCondition" runat="server">
        <HeaderTemplate>
            <tr>
                <td align="center">
                    <asp:Label ID="lblNameLabel" runat="server" Text="Name" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblOperatorLabel" runat="server" Text="Operator" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblValueLabel" runat="server" Text="Value" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WorkflowConditionName")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblOperatorValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OperatorX")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblValueValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValueX")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
