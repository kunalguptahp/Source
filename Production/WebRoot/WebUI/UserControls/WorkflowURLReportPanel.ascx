<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowURLReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowURLReportPanel" %>
<table border="1">
    <asp:Repeater ID="repWorkflowURL" runat="server">
        <HeaderTemplate>
            <tr>
                <td>URL</td>
                <td>%</td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><asp:HyperLink ID="lnkURL" runat="server" Target="_blank" NavigateURL='<%#DataBinder.Eval(Container.DataItem, "URL")%>'><%#DataBinder.Eval(Container.DataItem, "URL")%></asp:HyperLink></td>
                <td><asp:Label ID="lblURLPercentValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Percent")%>' /></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
