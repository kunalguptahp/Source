<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationGroupListPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupListPanel" %>
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
                                                    Text="Jumpstation IDs:"></asp:Label>
                                                <asp:TextBox ID="txtIdList" runat="server" SkinID="IdListFilter" MaxLength="9999"></asp:TextBox>
                                                <HPFx:RegularExpressionValidator ID="revTxtIdList" runat="server" ControlToValidate="txtIdList"
                                                    SkinID="IntListValidator">
                                                </HPFx:RegularExpressionValidator>
                                                <HPFx:CustomValidator ID="cvTxtIdList" runat="server" ErrorMessage="Must be a comma-separated list of integers.</br>"
                                                    OnServerValidate="cvTxtIdList_ServerValidate" ControlToValidate="txtIdList" />
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
                                            <td align="left">
                                                <asp:Label ID="lblName" runat="server" Text="Name:" SkinID="FilterLabel" AssociatedControlID="txtName"></asp:Label>
                                                <asp:TextBox ID="txtName" SkinID="NameFilter" runat="server" ToolTip="Filter name">
                                                </asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblJumpstationGroupType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlJumpstationGroupType"
                                                    Text="Jumpstation Type:"></asp:Label>
                                                <asp:DropDownList ID="ddlJumpstationGroupType" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter jumpstation type">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtDescription"></asp:Label>
                                                <asp:TextBox ID="txtDescription" SkinID="NameFilter" runat="server" ToolTip="Filter description">
                                                </asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblJumpstationApplication" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlJumpstationApplication"
                                                    Text="Application:"></asp:Label>
                                                <asp:DropDownList ID="ddlJumpstationApplication" SkinID="ForeignKeyColumnFilter"
                                                    runat="server" ToolTip="Filter application">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOwner" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlOwner"
                                                    Text="Owner:"></asp:Label>
                                                <asp:DropDownList ID="ddlOwner" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter owner">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblJumpstationGroupStatus" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlJumpstationGroupStatus"
                                                    Text="Status:"></asp:Label>
                                                <asp:DropDownList ID="ddlJumpstationGroupStatus" SkinID="ForeignKeyColumnFilter"
                                                    runat="server" ToolTip="Filter status">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblBrand" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlBrand"
                                                    Text="Brand:"></asp:Label>
                                                <asp:DropDownList ID="ddlBrand" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter brand">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblBrandHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlBrand"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblCycle" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlCycle"
                                                    Text="Cycle:"></asp:Label>
                                                <asp:DropDownList ID="ddlCycle" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter cycle">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCycleHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlCycle"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblLocale" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlLocale"
                                                    Text="Locale:"></asp:Label>
                                                <asp:DropDownList ID="ddlLocale" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter locale">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblLocaleHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlLocale"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblPartnerCategory" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlPartnerCategory"
                                                    Text="Partner Category:"></asp:Label>
                                                <asp:DropDownList ID="ddlPartnerCategory" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter partner category">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblPartnerCategoryHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlPartnerCategory"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblPlatform" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlPlatform"
                                                    Text="Platform:"></asp:Label>
                                                <asp:DropDownList ID="ddlPlatform" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter platform">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblPlatformHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlPlatform"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblTouchpoint" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlTouchpoint"
                                                    Text="Touchpoint:"></asp:Label>
                                                <asp:DropDownList ID="ddlTouchpoint" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter touchpoint">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblTouchpointHelp" runat="server" Text="&nbsp;Note: Filter containing only"
                                                    SkinID="FilterLabel" AssociatedControlID="ddlTouchpoint"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblValidationId" runat="server" Text="Validation Id:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtValidationId"></asp:Label>
                                                <asp:TextBox ID="txtValidationId" SkinID="IdFilter" runat="server" ToolTip="Filter validation id">
                                                </asp:TextBox>
                                                <HPFx:RegularExpressionValidator ID="revValidationId" runat="server" ControlToValidate="txtValidationId"
                                                    SkinID="IntValidator">
                                                </HPFx:RegularExpressionValidator>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblPublicationId" runat="server" Text="Publication Id:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtPublicationId"></asp:Label>
                                                <asp:TextBox ID="txtPublicationId" SkinID="IdFilter" runat="server" ToolTip="Filter publication id">
                                                </asp:TextBox>
                                                <HPFx:RegularExpressionValidator ID="revPublicationId" runat="server" ControlToValidate="txtPublicationId"
                                                    SkinID="IntValidator">
                                                </HPFx:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblCreatedBy" runat="server" Text="Created By:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtCreatedBy"></asp:Label>
                                                <asp:TextBox ID="txtCreatedBy" SkinID="CreatedByFilter" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="ajaxexttxtCreatedByAutoComplete" runat="server" TargetControlID="txtCreatedBy"
                                                    ServiceMethod="GetPersonNameCompletionList" />
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblCreatedAfter" runat="server" Text="Created After:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtCreatedAfter"></asp:Label>
                                                <asp:TextBox ID="txtCreatedAfter" SkinID="CreatedAfterFilter" runat="server"></asp:TextBox>
                                                <asp:Image ID="imgCreatedAfter" runat="server" SkinID="CalendarExtenderImage" />
                                                <asp:CalendarExtender ID="ajaxcxtCreatedAfter" runat="server" TargetControlID="txtCreatedAfter"
                                                    PopupButtonID="imgCreatedAfter" />
                                                <HPFx:CompareValidator ID="cvCreatedAfter" runat="server" SkinID="DateValidator"
                                                    ControlToValidate="txtCreatedAfter" />
                                                <asp:Label ID="lblCreatedBefore" runat="server" Text="Before:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtCreatedBefore"></asp:Label>
                                                <asp:TextBox ID="txtCreatedBefore" SkinID="CreatedBeforeFilter" runat="server"></asp:TextBox>
                                                <asp:Image ID="imgCreatedBefore" runat="server" SkinID="CalendarExtenderImage" />
                                                <asp:CalendarExtender ID="ajaxcxtCreatedBefore" runat="server" TargetControlID="txtCreatedBefore"
                                                    PopupButtonID="imgCreatedBefore" />
                                                <HPFx:CompareValidator ID="cvCreatedBefore" runat="server" SkinID="DateValidator"
                                                    ControlToValidate="txtCreatedBefore" />
                                                <HPFx:CompareValidator ID="cvCreatedOnRange" runat="server" SkinID="DateRangeValidator_BeforeGreaterThanAfter"
                                                    ControlToValidate="txtCreatedBefore" ControlToCompare="txtCreatedAfter" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblModifiedBy" runat="server" Text="Modified By:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtModifiedBy"></asp:Label>
                                                <asp:TextBox ID="txtModifiedBy" SkinID="ModifiedByFilter" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="ajaxexttxtModifiedByAutoComplete" runat="server" TargetControlID="txtModifiedBy"
                                                    ServiceMethod="GetPersonNameCompletionList" />
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblModifiedAfter" runat="server" Text="Modified After:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtModifiedAfter"></asp:Label>
                                                <asp:TextBox ID="txtModifiedAfter" SkinID="ModifiedAfterFilter" runat="server"></asp:TextBox>
                                                <asp:Image ID="imgModifiedAfter" runat="server" SkinID="CalendarExtenderImage" />
                                                <asp:CalendarExtender ID="ajaxcxtModifiedAfter" runat="server" TargetControlID="txtModifiedAfter"
                                                    PopupButtonID="imgModifiedAfter" />
                                                <HPFx:CompareValidator ID="cvModifiedAfter" runat="server" SkinID="DateValidator"
                                                    ControlToValidate="txtModifiedAfter" />
                                                <asp:Label ID="lblModifiedBefore" runat="server" Text="Before:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtModifiedBefore"></asp:Label>
                                                <asp:TextBox ID="txtModifiedBefore" SkinID="ModifiedBeforeFilter" runat="server"></asp:TextBox>
                                                <asp:Image ID="imgModifiedBefore" runat="server" SkinID="CalendarExtenderImage" />
                                                <asp:CalendarExtender ID="ajaxcxtModifiedBefore" runat="server" TargetControlID="txtModifiedBefore"
                                                    PopupButtonID="imgModifiedBefore" />
                                                <HPFx:CompareValidator ID="cvModifiedBefore" runat="server" SkinID="DateValidator"
                                                    ControlToValidate="txtModifiedBefore" />
                                                <HPFx:CompareValidator ID="cvModifiedOnRange" runat="server" SkinID="DateRangeValidator_BeforeGreaterThanAfter"
                                                    ControlToValidate="txtModifiedBefore" ControlToCompare="txtModifiedAfter" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblTargetURL" runat="server" Text="Target URL:" SkinID="FilterLabel"
                                                    AssociatedControlID="txtTargetURL"></asp:Label>
                                                <asp:TextBox ID="txtTargetURL" SkinID="NameFilter" runat="server" ToolTip="Filter target URL"
                                                    Columns="150">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblAppClient" runat="Server" SkinID="FilterLabel" AssociatedControlID="ddlAppClient"
                                                    Text="App Client:"></asp:Label>
                                                <asp:DropDownList ID="ddlAppClient" SkinID="ForeignKeyColumnFilter" runat="server"
                                                    ToolTip="Filter AppClient">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblTagsFilter" runat="server" Text="Tags:" AssociatedControlID="txtTagsFilter"></asp:Label>
                                                <asp:TextBox ID="txtTagsFilter" Columns="100" ToolTip="Filter by group tag(s)" runat="server"
                                                    Style="width: 99%;"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="ajaxextTxtTagsFilterAutoComplete" runat="server" SkinID="AutoComplete1"
                                                    TargetControlID="txtTagsFilter" ServiceMethod="GetTagFilterCompletionList" DelimiterCharacters=","
                                                    ShowOnlyCurrentWordInCompletionListItem="false" />
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
                <HPFx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    AllowPaging="True" OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound"
                    DataKeyNames="Id,ProductionId,PublicationDomain,QueryString,TargetURL" AutoGenerateCheckBoxColumn="true"
                    DataSourceID="odsDataSource">
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
                        <asp:BoundField DataField="JumpstationGroupStatusName" HeaderText="Jumpstation Status"
                            SortExpression="JumpstationGroupStatusName" />
                        <asp:BoundField DataField="JumpstationGroupTypeName" HeaderText="Jumpstation Type"
                            SortExpression="JumpstationGroupTypeName" />
                        <asp:BoundField DataField="PersonName" HeaderText="Owner" SortExpression="PersonName" />
                        <asp:BoundField DataField="BrandQueryParameterValue" HeaderText="Brand" SortExpression="BrandQueryParameterValue" />
                        <asp:BoundField DataField="CycleQueryParameterValue" HeaderText="Cycle" SortExpression="CycleQueryParameterValue" />
                        <asp:BoundField DataField="LocaleQueryParameterValue" HeaderText="Locale" SortExpression="LocaleQueryParameterValue" />
                        <asp:BoundField DataField="PartnerCategoryQueryParameterValue" HeaderText="Partner Category"
                            SortExpression="PartnerCategoryQueryParameterValue" />
                        <asp:BoundField DataField="PlatformQueryParameterValue" HeaderText="Platform" SortExpression="PlatformQueryParameterValue" />
                        <asp:BoundField DataField="TouchpointQueryParameterValue" HeaderText="Touchpoint"
                            SortExpression="TouchpointQueryParameterValue" />
                        <asp:BoundField DataField="Tags" HeaderText="Tags" />
                        <HPFx:BoundField DataField="TagCount" HeaderText="# Tags" SortExpression="TagCount"
                            HeaderToolTip="Number of Tags" />
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
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true"  
   
    SelectMethod="ODSFetch" SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapJumpstationGroupController"
    SortParameterName="sortExpression" >
    
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
        <asp:Parameter Name="tenantGroupId" Type="String" />
    </SelectParameters>
    
</asp:ObjectDataSource>

