<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupListPanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupListPanel" %>
	
<asp:Panel ID="pnlList" runat="server">
	<table border="0">
		<tr>
			<td align="left">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
        <tr>
            <td align="left">
                <asp:Button ID="btnCreate" SkinID="CreateItem..." runat="server" ToolTip="Create new group"
                    OnClick="btnCreate_Click" />
				<asp:Button ID="btnExport" SkinID="ExportData" runat="server" OnClick="btnExport_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit..." ToolTip="Edit the selected group(s)"
                    OnClick="btnEdit_Click" />
                <asp:Button ID="btnCopy" runat="server" Text="Copy..." ToolTip="Saves copies of each of the selected group(s)"
                    OnClick="btnCopy_Click" Visible="false" />
                <asp:Button ID="btnMultiReplace" runat="server" Text="Multi-Replace..." ToolTip="Multi-replace the selected published configuration service group(s)"
                    OnClick="btnMultiReplace_Click" />
                <asp:Button ID="btnReport" runat="server" Text="Report..." ToolTip="Report configuration service group(s)"
                    OnClick="btnReport_Click" />
            </td>
        </tr>
        <tr>
			<td>
				<div>
					<asp:CollapsiblePanelExtender ID="ajaxextPnlFilterAreaCpe" runat="Server" TargetControlID="pnlFilterArea"
						ExpandControlID="pnlFilterAreaHeader" TextLabelID="lblFilterAreaHeaderPrompt"
						CollapseControlID="pnlFilterAreaHeader" ImageControlID="ibtnFilterAreaToggle"
						/>
					<table class="layoutTable">
						<tr>
							<td>
								<asp:Panel ID="pnlFilterAreaHeader" runat="server" CssClass="ajaxCollapsiblePanelHeader">
									<table class="ajaxCollapsiblePanelTitleTable">
										<tr>
											<td class="ajaxCollapsiblePanelToggleTD" align="left" nowrap="NoWrap">
												<asp:ImageButton ID="ibtnFilterAreaToggle" runat="server" OnClientClick="return false;" SkinID="FilterAreaToggleButton" />
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
												<asp:Label ID="lblIdList" runat="server" SkinID="FilterLabel" AssociatedControlID="txtIdList" Text="Group IDs:"></asp:Label>
												<asp:TextBox ID="txtIdList" runat="server" SkinID="IdListFilter" MaxLength="9999"></asp:TextBox>
												<HPFx:RegularExpressionValidator ID="revTxtIdList" runat="server" ControlToValidate="txtIdList"
													SkinID="IntListValidator">
												</HPFx:RegularExpressionValidator>
                                                <HPFx:CustomValidator ID="cvTxtIdList" runat="server" ErrorMessage="Must be a comma-separated list of integers.</br>"
                                                    OnServerValidate="cvTxtIdList_ServerValidate" ControlToValidate="txtIdList" />
											</td>
                                            <td>
												<%--IMPORTANT - Do not increase the page count past 20 because you can only multiselect 20 max--%>
												<asp:Label ID="lblItemsPerPage" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlItemsPerPage" Text="Groups per page:"></asp:Label>
                                                <asp:DropDownList ID="ddlItemsPerPage" runat="server" ToolTip="Enter the number of groups to display per page">
                                                    <asp:ListItem Value="5" Text="5"  />
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
												<asp:Label ID="lblConfigurationServiceGroupType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlConfigurationServiceGroupType" Text="Group Type:"></asp:Label>
												<asp:DropDownList ID="ddlConfigurationServiceGroupType" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter group type">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="FilterLabel" AssociatedControlID="txtDescription"></asp:Label>
												<asp:TextBox ID="txtDescription" SkinID="NameFilter" runat="server" ToolTip="Filter description">
												</asp:TextBox>
											</td>
											<td align="left">
												<asp:Label ID="lblConfigurationServiceApplication" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlConfigurationServiceApplication" Text="Application:"></asp:Label>
												<asp:DropDownList ID="ddlConfigurationServiceApplication" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter application">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblOwner" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlOwner" Text="Owner:"></asp:Label>
												<asp:DropDownList ID="ddlOwner" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter owner">
												</asp:DropDownList>
											</td>
											<td align="left">
												<asp:Label ID="lblConfigurationServiceGroupStatus" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlConfigurationServiceGroupStatus" Text="Status:"></asp:Label>
												<asp:DropDownList ID="ddlConfigurationServiceGroupStatus" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter status">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblRelease" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlRelease" Text="Release:"></asp:Label>
												<asp:DropDownList ID="ddlRelease" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter release">
												</asp:DropDownList>
												<asp:Label ID="lblReleaseHelp" runat="server" Text="&nbsp;Note: Filter containing only" SkinID="FilterLabel" AssociatedControlID="ddlRelease"></asp:Label>
											</td>
											<td align="left">
												<asp:Label ID="lblCountry" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlCountry" Text="Country:"></asp:Label>
												<asp:DropDownList ID="ddlCountry" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter country">
												</asp:DropDownList>
												<asp:Label ID="lblCountryHelp" runat="server" Text="&nbsp;Note: Filter containing only" SkinID="FilterLabel" AssociatedControlID="ddlCountry"></asp:Label>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblPlatform" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlPlatform" Text="Platform:"></asp:Label>
												<asp:DropDownList ID="ddlPlatform" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter platform">
												</asp:DropDownList>
												<asp:Label ID="lblPlatformHelp" runat="server" Text="&nbsp;Note: Filter containing only" SkinID="FilterLabel" AssociatedControlID="ddlPlatform"></asp:Label>
											</td>
											<td align="left">
												<asp:Label ID="lblBrand" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlBrand" Text="Brand:"></asp:Label>
												<asp:DropDownList ID="ddlBrand" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter brand">
												</asp:DropDownList>
												<asp:Label ID="lblBrandHelp" runat="server" Text="&nbsp;Note: Filter containing only" SkinID="FilterLabel" AssociatedControlID="ddlBrand"></asp:Label>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblPublisher" runat="server" SkinID="FilterLabel" AssociatedControlID="txtPublisher" Text="Publisher:"></asp:Label>
												<asp:TextBox ID="txtPublisher" SkinID="NameFilter" runat="server" ToolTip="Filter publisher">
												</asp:TextBox>
											</td>
											<td align="left">
												<asp:Label ID="lblInstallerType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlInstallerType" Text="Installer Type:"></asp:Label>
												<asp:DropDownList ID="ddlInstallerType" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter installer type">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblValidationId" runat="server" Text="Validation Id:" SkinID="FilterLabel" AssociatedControlID="txtValidationId"></asp:Label>
												<asp:TextBox ID="txtValidationId" SkinID="IdFilter" runat="server" ToolTip="Filter validation id">
												</asp:TextBox>
												<HPFx:RegularExpressionValidator ID="revValidationId" runat="server" ControlToValidate="txtValidationId"
													SkinID="IntValidator">
												</HPFx:RegularExpressionValidator>
											</td>
											<td align="left">
												<asp:Label ID="lblPublicationId" runat="server" Text="Publication Id:" SkinID="FilterLabel" AssociatedControlID="txtPublicationId"></asp:Label>
												<asp:TextBox ID="txtPublicationId" SkinID="IdFilter" runat="server" ToolTip="Filter publication id">
												</asp:TextBox>
												<HPFx:RegularExpressionValidator ID="revPublicationId" runat="server" ControlToValidate="txtPublicationId"
													SkinID="IntValidator">
												</HPFx:RegularExpressionValidator>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblCreatedBy" runat="server" Text="Created By:" SkinID="FilterLabel" AssociatedControlID="txtCreatedBy"></asp:Label>
												<asp:TextBox ID="txtCreatedBy" SkinID="CreatedByFilter" runat="server"></asp:TextBox>
												<asp:AutoCompleteExtender ID="ajaxexttxtCreatedByAutoComplete" runat="server" TargetControlID="txtCreatedBy" ServiceMethod="GetPersonNameCompletionList" />
											</td>
											<td align="left">
												<asp:Label ID="lblCreatedAfter" runat="server" Text="Created After:" SkinID="FilterLabel" AssociatedControlID="txtCreatedAfter"></asp:Label>
												<asp:TextBox ID="txtCreatedAfter" SkinID="CreatedAfterFilter"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgCreatedAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtCreatedAfter" runat="server" TargetControlID="txtCreatedAfter"
													PopupButtonID="imgCreatedAfter" />
												<HPFx:CompareValidator ID="cvCreatedAfter" runat="server" SkinID="DateValidator"
													ControlToValidate="txtCreatedAfter" />
												<asp:Label ID="lblCreatedBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtCreatedBefore"></asp:Label>
												<asp:TextBox ID="txtCreatedBefore" SkinID="CreatedBeforeFilter"
													runat="server"></asp:TextBox>
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
												<asp:Label ID="lblModifiedBy" runat="server" Text="Modified By:" SkinID="FilterLabel" AssociatedControlID="txtModifiedBy"></asp:Label>
												<asp:TextBox ID="txtModifiedBy" SkinID="ModifiedByFilter" runat="server"></asp:TextBox>
												<asp:AutoCompleteExtender ID="ajaxexttxtModifiedByAutoComplete" runat="server" TargetControlID="txtModifiedBy" ServiceMethod="GetPersonNameCompletionList" />
											</td>
											<td align="left">
												<asp:Label ID="lblModifiedAfter" runat="server" Text="Modified After:" SkinID="FilterLabel" AssociatedControlID="txtModifiedAfter"></asp:Label>
												<asp:TextBox ID="txtModifiedAfter" SkinID="ModifiedAfterFilter"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgModifiedAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtModifiedAfter" runat="server" TargetControlID="txtModifiedAfter"
													PopupButtonID="imgModifiedAfter" />
												<HPFx:CompareValidator ID="cvModifiedAfter" runat="server" SkinID="DateValidator"
													ControlToValidate="txtModifiedAfter" />
												<asp:Label ID="lblModifiedBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtModifiedBefore"></asp:Label>
												<asp:TextBox ID="txtModifiedBefore" SkinID="ModifiedBeforeFilter"
													runat="server"></asp:TextBox>
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
				<asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<hpfx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
					AllowPaging="True" OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound"
					DataKeyNames="Id,ValidationId,ProductionId"
					AutoGenerateCheckBoxColumn="true" DataSourceID="odsDataSource">
					<Columns>
						<asp:ButtonField Text="Edit..." ButtonType="Button" CausesValidation="false" CommandName="Edit..." />
						<asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id"
							DataNavigateUrlFormatString="~/ConfigurationService/ConfigurationServiceGroupUpdate.aspx?id={0}" />
						<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
						<asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
						<asp:BoundField DataField="ConfigurationServiceApplicationName" HeaderText="Application" SortExpression="ConfigurationServiceApplicationName" />
                        <asp:BoundField DataField="ConfigurationServiceGroupStatusName" HeaderText="Group Status" SortExpression="ConfigurationServiceGroupStatusName" />
						<asp:BoundField DataField="ConfigurationServiceGroupTypeName" HeaderText="Group Type" SortExpression="ConfigurationServiceGroupTypeName" />
						<asp:BoundField DataField="PublisherLabelValue" HeaderText="Publisher" SortExpression="PublisherLabelValue" />
						<asp:BoundField DataField="InstallerTypeLabelValue" HeaderText="Installer Type" SortExpression="InstallerTypeLabelValue" />
                        <asp:BoundField DataField="PersonName" HeaderText="Owner" SortExpression="PersonName" />
                        <asp:BoundField DataField="ReleaseQueryParameterValue" HeaderText="Release" SortExpression="ReleaseQueryParameterValue" />
                        <asp:BoundField DataField="CountryQueryParameterValue" HeaderText="Country" SortExpression="CountryQueryParameterValue" />
                        <asp:BoundField DataField="PlatformQueryParameterValue" HeaderText="Platform" SortExpression="PlatformQueryParameterValue" />
                        <asp:BoundField DataField="BrandQueryParameterValue" HeaderText="Brand" SortExpression="BrandQueryParameterValue" />
                        <asp:BoundField DataField="Tags" HeaderText="Tags" />
                        <HPFx:BoundField DataField="TagCount" HeaderText="# Tags" SortExpression="TagCount"
                            HeaderToolTip="Number of Tags" />
                        <asp:TemplateField HeaderText="Validation Id"> 
                            <ItemTemplate> 
                                <asp:HyperLink id="hlValidationId" runat="server" Target="_blank"  ></asp:HyperLink> 
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Publication Id"> 
                            <ItemTemplate> 
                                <asp:HyperLink id="hlPublicationId" runat="server" Target="_blank"  ></asp:HyperLink> 
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created" SortExpression="CreatedOn" />
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified" SortExpression="ModifiedOn" />
                        <HPFx:PeopleFinderHyperLinkField DataTextField="CreatedBy" HeaderText="Created By"
                            SortExpression="CreatedBy" DataNavigateUrlFields="CreatedBy" />
						<HPFx:PeopleFinderHyperLinkField DataTextField="ModifiedBy" HeaderText="Modified By"
							SortExpression="ModifiedBy" DataNavigateUrlFields="ModifiedBy" />
						<asp:CommandField Visible="false" ShowHeader="false" ButtonType="Button" CausesValidation="false" />
					</Columns>
					<EmptyDataTemplate>
						<asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no groups found</asp:Label>
					</EmptyDataTemplate>
				</hpfx:EnhancedGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true"
	SelectMethod="ODSFetch" SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapConfigurationServiceGroupController"
	SortParameterName="sortExpression">
	<SelectParameters>
		<asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
		 <asp:Parameter Name="tenantGroupId" Type="String" />
	</SelectParameters>
</asp:ObjectDataSource>
