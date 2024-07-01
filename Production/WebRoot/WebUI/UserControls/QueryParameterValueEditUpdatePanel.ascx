<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryParameterValueEditUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.QueryParameterValueEditUpdatePanel" %>
<asp:Panel ID="pnlList" runat="server">
    <table border="0">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<HPFx:PageableItemContainerGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                    AllowPaging="False" OnRowDataBound="gvList_RowDataBound" DataKeyNames="QueryParameterId"
                    DataSourceID="odsDataSource">
                    <Columns>
                        <asp:BoundField DataField="QueryParameterId" HeaderText="ID" SortExpression="QueryParameterId" />
                        <asp:BoundField DataField="QueryParameterName" HeaderText="Parameter" SortExpression="QueryParameterName" />
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlParameterValue">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no redirector parameters found</asp:Label>
                    </EmptyDataTemplate>
                </HPFx:PageableItemContainerGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetch"
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapQueryParameterProxyURLTypeController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
