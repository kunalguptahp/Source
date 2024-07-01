<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceLabelValueReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceLabelValueReportPanel" %>
<table border="1">
    <asp:Repeater ID="repConfigurationServiceLabelValue" runat="server">
        <HeaderTemplate>
            <tr>
                <td align="center">
                    <asp:Label ID="lblItemLabel" runat="server" Text="Item" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblLabelLabel" runat="server" Text="Label" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblValuelabel" runat="server" Text="Value" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConfigurationServiceItemName")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblLabelName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConfigurationServiceLabelName")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblLabelValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValueX")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
