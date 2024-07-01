<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowConditionEditListUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowConditionEditListUpdatePanel" %>

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
				<hpfx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    AllowPaging="True" OnRowDataBound="gvList_RowDataBound" AutoGenerateCheckBoxColumn="true"
                    DataKeyNames="Id" DataSourceID="odsDataSource">
                    <Columns>
                        <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="OperatorX" HeaderText="Operator" SortExpression="Operator" />
                        <asp:BoundField DataField="ValueX" HeaderText="Value" SortExpression="Value" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no conditions found</asp:Label>
                    </EmptyDataTemplate>
                </hpfx:EnhancedGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
                <br />
                <br />
                <b>Important: Multiple conditions are logical "AND" only</b>
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
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapWorkflowConditionController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
        
    </SelectParameters>
</asp:ObjectDataSource>
