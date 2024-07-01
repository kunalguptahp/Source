<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CPSLogPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.CPSLogPanel" %>
<asp:DataPager ID="dpListTop" runat="server" PagedControlID="grvCPSLog" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
<HPFx:PageableItemContainerGridView ID="grvCPSLog" runat="server" AutoGenerateColumns="False" AllowSorting="False"
    AllowPaging="False">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="id"/>
        <asp:BoundField DataField="message" HeaderText="Message">
        <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="description" HeaderText="Description" >
        <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="status" HeaderText="Status">
        <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no logs found</asp:Label>
    </EmptyDataTemplate>
</HPFx:PageableItemContainerGridView>
<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="grvCPSLog" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
