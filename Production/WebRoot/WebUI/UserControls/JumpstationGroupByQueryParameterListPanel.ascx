<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationGroupByQueryParameterListPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupByQueryParameterListPanel" %>
<asp:Panel ID="pnlList" runat="server">
    <table border="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button ID="btnCreate" SkinID="CreateItem..." runat="server" ToolTip="Create new jumpstation"
                    OnClick="btnCreate_Click" />
                <asp:Button ID="btnExport" SkinID="ExportData" runat="server" OnClick="btnExport_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit..." ToolTip="Edit the selected jumpstation(s)"
                    OnClick="btnEdit_Click" />
                <asp:Button ID="btnMultiReplace" runat="server" Text="Multi-Replace..." ToolTip="Multi-replace the selected published jumpstation(s)"
                    OnClick="btnMultiReplace_Click" />
                <asp:Button ID="btnReport" runat="server" Text="Report..." ToolTip="Report jumpstation(s)"
                    OnClick="btnReport_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
<%--                    <asp:CollapsiblePanelExtender ID="ajaxextPnlFilterAreaCpe" runat="Server" TargetControlID="pnlFilterArea"
                        ExpandControlID="pnlFilterAreaHeader" TextLabelID="lblFilterAreaHeaderPrompt"
                        CollapseControlID="pnlFilterAreaHeader" ImageControlID="ibtnFilterAreaToggle" />--%>
                    <table class="layoutTable">
<%--                        <tr>
                            <td>
                                <asp:Panel ID="pnlFilterAreaHeader" runat="server" CssClass="ajaxCollapsiblePanelHeader">
                                    <table class="ajaxCollapsiblePanelTitleTable">
                                        <tr>
                                            <td class="ajaxCollapsiblePanelToggleTD" align="left" nowrap="NoWrap">
                                                <asp:ImageButton ID="ibtnFilterAreaToggle" runat="server" OnClientClick="return false;"
                                                    SkinID="FilterAreaToggleButton" />
                                                <asp:Label ID="lblFilterAreaHeaderPrompt" runat="server" SkinID="FilterAreaHeaderPromptLabel" />
                                            </td>
                                            <td class="ajaxCollapsiblePanelTitleTD" align="center" nowrap="NoWrap">
                                                <span class="ajaxCollapsiblePanelTitle"></span>
                                            </td>
                                            <td class="ajaxCollapsiblePanelRightTD" align="right">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlFilterArea" runat="server" DefaultButton="btnFilter">
                                    <table border="1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblJumpstationGroupStatus" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlJumpstationGroupStatus"
                                                    Text="Status:"></asp:Label>
                                                <asp:DropDownList ID="ddlJumpstationGroupStatus" SkinID="ForeignKeyColumnFilter"
                                                    runat="server" ToolTip="Filter status">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%--IMPORTANT - Do not increase the page count past 20 because you can only multiselect 20 max--%>
                                                <asp:Label ID="lblItemsPerPage" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlItemsPerPage"
                                                    Text="Jumpstations per page:"></asp:Label>
                                                <asp:DropDownList ID="ddlItemsPerPage" runat="server" ToolTip="Enter the number of jumpstations to display per page">
                                                    <asp:ListItem Value="5" Text="5" />
                                                    <asp:ListItem Value="10" Text="10" />
                                                    <asp:ListItem Value="20" Text="20" Selected="True" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblJumpstationGroupType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlJumpstationGroupType"
                                                    Text="Jumpstation Type:"></asp:Label>
                                                <asp:DropDownList ID="ddlJumpstationGroupType" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter jumpstation type" AutoPostBack="true" OnSelectedIndexChanged="ddlJumpstationGroupType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:GridView ID="gvQueryParameterList" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                                                    AllowPaging="False" OnRowDataBound="gvQueryParameterList_RowDataBound" DataKeyNames="QueryParameterId">
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
                                                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no query parameters found</asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
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
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager"
                    OnInit="DataPager_Init">
                </asp:DataPager>
                <HPFx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" DataKeyNames="Id,ProductionId,TargetURL"
                    AutoGenerateCheckBoxColumn="True" DataSourceID="odsDataSource">
                    <Columns>
                        <asp:ButtonField Text="Edit..." ButtonType="Button" CausesValidation="false" CommandName="Edit..." />
                        <asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id"
                            DataNavigateUrlFormatString="~/Jumpstation/JumpstationGroupUpdate.aspx?id={0}" />
                        <asp:TemplateField HeaderText="Source URL">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlSourceURL" runat="server" Target="_blank"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target URL">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTargetURL" runat="server" Target="_blank"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="JumpstationApplicationName" HeaderText="Application" SortExpression="JumpstationApplicationName" />
                        <asp:BoundField DataField="JumpstationGroupStatusName" HeaderText="Jumpstation Status" SortExpression="JumpstationGroupStatusName" />
						<asp:BoundField DataField="JumpstationGroupTypeName" HeaderText="Jumpstation Type" SortExpression="JumpstationGroupTypeName" />
                        <asp:TemplateField HeaderText="Selected Value">
                            <ItemTemplate >
                                <asp:Label ID="label1" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PersonName" HeaderText="Owner" SortExpression="PersonName" />
                        <asp:BoundField DataField="ValidationId" HeaderText="Val Id" SortExpression="ValidationId" />
                        <asp:BoundField DataField="ProductionId" HeaderText="Pub Id" SortExpression="ProductionId" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created" SortExpression="CreatedOn" />
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified" SortExpression="ModifiedOn" />
                        <HPFx:PeopleFinderHyperLinkField DataTextField="CreatedBy" HeaderText="Created By"
                            SortExpression="CreatedBy" DataNavigateUrlFields="CreatedBy" />
                        <HPFx:PeopleFinderHyperLinkField DataTextField="ModifiedBy" HeaderText="Modified By"
                            SortExpression="ModifiedBy" DataNavigateUrlFields="ModifiedBy" />
                        <asp:CommandField Visible="false" ShowHeader="false" ButtonType="Button" CausesValidation="false" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no jumpstation(s) found</asp:Label>
                    </EmptyDataTemplate>
                </HPFx:EnhancedGridView>
                <asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager"
                    OnInit="DataPager_Init">
                </asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetchByQueryParameter"
    SelectCountMethod="ODSFetchCountByQueryParameter" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapJumpstationGroupController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
         <asp:Parameter Name="tenantGroupId" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
