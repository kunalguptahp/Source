<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationMacroValueReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationMacroValueReportPanel" %>
<table border="1">
    <asp:Repeater ID="repJumpstationMacroValue" runat="server">
        <HeaderTemplate>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMatchName" runat="server" Text="Matching Name" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblResult" runat="server" Text="Result Value" SkinID="DataFieldLabel" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblMatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MatchName")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblResultValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ResultValue")%>'
                        SkinID="DataFieldLabel"></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
