<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleDescriptionMultiReplaceUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleDescriptionMultiReplaceUpdatePanel" %>
<asp:Panel ID="pnlList" runat="server">
    <table border="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<HPFx:PageableItemContainerGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                    AllowPaging="False" OnRowDataBound="gvList_RowDataBound" DataKeyNames="Id">
                    <Columns>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">Please select Workflow Module(s) to replace.</asp:Label>
                    </EmptyDataTemplate>
                </HPFx:PageableItemContainerGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Panel>
