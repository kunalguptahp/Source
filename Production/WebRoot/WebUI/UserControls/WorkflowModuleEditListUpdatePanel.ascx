<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleEditListUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleEditListUpdatePanel" %>
<asp:Panel ID="pnlList" runat="server">
    <table border="0">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table border="1">
                    <tr>
                        <td valign="top">
                            <b>Selected Module(s):</b><br />
                            <asp:GridView ID="gvSelectionList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSelectionList_RowCommand"
                                AllowSorting="False" AllowPaging="False" DataKeyNames="Id">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" Text="Remove" CommandName="remove" />
                                    <asp:BoundField DataField="Id" Visible="true" HeaderText="Id" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="ModuleCategoryName" HeaderText="Category" />
                                    <asp:BoundField DataField="ModuleSubCategoryName" HeaderText="Sub-Category" />
                                    <asp:BoundField DataField="VersionMajor" HeaderText="Major" />
                                    <asp:BoundField DataField="VersionMinor" HeaderText="Minor" />
                                    <asp:BoundField DataField="Filename" HeaderText="Filename" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:ButtonField ButtonType="Button" Text="Up" CommandName="up" />
                                    <asp:ButtonField ButtonType="Button" Text="Down" CommandName="down" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no selected modules found</asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Available Module(s):</b><br />
                            <div>
                                <asp:CollapsiblePanelExtender ID="ajaxextPnlFilterAreaCpe" runat="Server" TargetControlID="pnlFilterArea"
                                    ExpandControlID="pnlFilterAreaHeader" TextLabelID="lblFilterAreaHeaderPrompt"
                                    CollapseControlID="pnlFilterAreaHeader" ImageControlID="ibtnFilterAreaToggle" />
                                <table class="layoutTable">
                                    <tr>
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
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlFilterArea" runat="server" DefaultButton="btnFilter">
                                                <table border="1">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblIdList" runat="server" SkinID="FilterLabel" AssociatedControlID="txtIdList"
                                                                Text="Module IDs:"></asp:Label>
                                                            <asp:TextBox ID="txtIdList" runat="server" SkinID="IdListFilter" MaxLength="9999"></asp:TextBox>
                                                            <HPFx:RegularExpressionValidator ID="revTxtIdList" runat="server" ControlToValidate="txtIdList"
                                                                SkinID="IntListValidator">
                                                            </HPFx:RegularExpressionValidator>
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblName" runat="server" Text="Name:" SkinID="FilterLabel" AssociatedControlID="txtName"></asp:Label>
                                                            <asp:TextBox ID="txtName" SkinID="NameFilter" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblModuleCategory" runat="server" Text="Category:" SkinID="FilterLabel"
                                                                AssociatedControlID="ddlModuleCategory"></asp:Label>
                                                            <asp:DropDownList ID="ddlModuleCategory" SkinID="ForeignKeyColumnFilter" runat="server"
                                                                ToolTip="Filter module category">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblModuleSubCategory" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlModuleSubCategory"
                                                                Text="Sub Category:"></asp:Label>
                                                            <asp:DropDownList ID="ddlModuleSubCategory" SkinID="ForeignKeyColumnFilter" runat="server"
                                                                ToolTip="Filter module sub category">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblVersionMajor" runat="server" Text="Major Version:" SkinID="FilterLabel"
                                                                AssociatedControlID="txtVersionMajor"></asp:Label>
                                                            <asp:TextBox ID="txtVersionMajor" SkinID="IdFilter" runat="server" ToolTip="Filter major version">
                                                            </asp:TextBox>
                                                            <HPFx:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                ControlToValidate="txtVersionMajor" SkinID="IntValidator">
                                                            </HPFx:RegularExpressionValidator>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblVersionMinor" runat="server" Text="Minor Version:" SkinID="FilterLabel"
                                                                AssociatedControlID="txtVersionMinor"></asp:Label>
                                                            <asp:TextBox ID="txtVersionMinor" SkinID="IdFilter" runat="server" ToolTip="Filter minor version">
                                                            </asp:TextBox>
                                                            <HPFx:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                ControlToValidate="txtVersionMinor" SkinID="IntValidator">
                                                            </HPFx:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblFilename" runat="server" Text="Filename:" SkinID="FilterLabel"
                                                                AssociatedControlID="txtFilename"></asp:Label>
                                                            <asp:TextBox ID="txtFilename" SkinID="NameFilter" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td align="left">
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
                            <asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager"
                                OnInit="DataPager_Init">
                            </asp:DataPager>
                            <HPFx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                AllowPaging="True" OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound"
                                OnRowCommand="gvList_RowCommand" DataKeyNames="Id" DataSourceID="odsDataSource">
                                <Columns>
                                    <asp:ButtonField Text="Add" ButtonType="Button" CausesValidation="false" CommandName="Insert" />
                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="ModuleCategoryName" HeaderText="Category" SortExpression="ModuleCategoryName" />
                                    <asp:BoundField DataField="ModuleSubCategoryName" HeaderText="Sub-Category" SortExpression="ModuleSubCategoryName" />
                                    <asp:BoundField DataField="VersionMajor" HeaderText="Major" SortExpression="VersionMajor" />
                                    <asp:BoundField DataField="VersionMinor" HeaderText="Minor" SortExpression="VersionMinor" />
                                    <asp:BoundField DataField="Filename" HeaderText="Filename" SortExpression="Filename" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:CommandField Visible="false" ShowHeader="false" ButtonType="Button" CausesValidation="false" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no available modules found</asp:Label>
                                </EmptyDataTemplate>
                            </HPFx:EnhancedGridView>
                            <asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager"
                                OnInit="DataPager_Init">
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetch"
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapWorkflowModuleController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
