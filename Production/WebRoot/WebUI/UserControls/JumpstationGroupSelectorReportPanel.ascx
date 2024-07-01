<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationGroupSelectorReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupSelectorReportPanel" %>
<%@ Register Src="~/UserControls/JumpstationParameterValueReportPanel.ascx"
    TagName="JumpstationParameterValueReportPanel" TagPrefix="ElementsCPSuc" %>
<asp:Repeater ID="repJumpstationGroupSelector" runat="server">
    <ItemTemplate>
        <table border="1">
            <tr>
                <td>
                    <asp:Label ID="lblNameLabel" runat="server" Text="Name:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValuesLabel" runat="server" Text="Parameters:" SkinID="DataFieldLabel"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="pnlJumpstationParameterValue" runat="server">
                        <elementscpsuc:JumpstationParameterValueReportPanel id="ucJumpstationParameterValueReport"
                            JumpstationGroupSelectorId='<%#DataBinder.Eval(Container.DataItem, "Id")%>'
                            runat="server" />
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
        </table>
    </ItemTemplate>
</asp:Repeater>
