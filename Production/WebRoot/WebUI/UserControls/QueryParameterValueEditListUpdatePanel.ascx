
<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryParameterValueEditListUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.QueryParameterValueEditListUpdatePanel" %>

<asp:Panel ID="pnlList" runat="server">
    <table border="0">
        <tr>
            <td align="center">
                <asp:Label ID="lblQueryParameterName" runat="server" Font-Bold="true" ></asp:Label>
            </td>
            <td align="right">
                <asp:CheckBox ID="chkNot" runat="server" Text="Not" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="layoutTable">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlFilterArea" runat="server" DefaultButton="btnFilter">
                                <table border="1">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblValue" runat="server" Text="Value:" SkinID="FilterLabel" AssociatedControlID="txtValue"></asp:Label>
                                            <asp:TextBox ID="txtValue" SkinID="NameFilter" runat="server" Columns="15">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <%= Global.CreateHtml_ClearFiltersButton("btnClearFilters") %>
                                                        <%= Global.CreateHtml_ResetFormButton("btnReset") %>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="btnFilter" runat="server" SkinID="FilterData" OnClick="btnFilter_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<hpfx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    AllowPaging="True" OnRowDataBound="gvList_RowDataBound" AutoGenerateCheckBoxColumn="true"
                    DataKeyNames="QueryParameterId,Id" DataSourceID="odsDataSource">
                    <Columns>
                        <asp:BoundField DataField="QueryParameterId" Visible="false" HeaderText="QPId" />
                        <asp:BoundField DataField="Id" Visible="false" HeaderText="QPVId" />
                        <asp:BoundField DataField="QueryParameterName" HeaderText="Name" />
                        <asp:BoundField DataField="Name" HeaderText="Value" SortExpression="Name" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no parameter values found</asp:Label>
                    </EmptyDataTemplate>
                </hpfx:EnhancedGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblHint" runat="server"></asp:Label>
            </td>
        </tr>
       <asp:Panel ID="pnlPersistanceControls" runat="server">
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click" />
                    &nbsp;
                    <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </asp:Panel>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetch"
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapQueryParameterValueController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
