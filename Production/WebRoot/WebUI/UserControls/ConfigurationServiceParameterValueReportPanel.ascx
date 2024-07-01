<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceParameterValueReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceParameterValueReportPanel" %>
<table border="1">
    <asp:Repeater ID="repConfigurationServiceParameterValue" runat="server">
        <HeaderTemplate>
            <tr>
                <td align="center">
                    <asp:Label ID="lblParameterLabel" runat="server" Text="Parameter" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblValueLabel" runat="server" Text="Value" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblNegationLabel" runat="server" Text="Negation" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblParameterValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "QueryParameterName")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblValueValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "QueryParameterValue")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNegationValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "QueryParameterValueNegation")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
