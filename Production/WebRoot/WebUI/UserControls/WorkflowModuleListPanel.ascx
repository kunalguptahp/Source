<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleListPanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleListPanel" %>
<asp:Panel ID="pnlList" runat="server">
	<table border="0">
		<tr>
			<td align="left" colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left" colspan="2">
				<asp:Button ID="btnCreate" SkinID="CreateItem..." runat="server" ToolTip="Create new Module"
					OnClick="btnCreate_Click" />
				<asp:Button ID="btnExport" SkinID="ExportData" runat="server" OnClick="btnExport_Click" />
                 <asp:Button ID="btnEdit" runat="server" Text="Edit..." ToolTip="Edit the selected workflow module(s)"
                    OnClick="btnEdit_Click" />
               <asp:Button ID="btnMultiReplace" runat="server" Text="Multi-Replace..." ToolTip="Multi-replace the selected published workflow module(s)"
                    OnClick="btnMultiReplace_Click" />
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
												<asp:Label ID="lblIdList" runat="server" SkinID="FilterLabel" AssociatedControlID="txtIdList" Text="Module IDs:"></asp:Label>
												<asp:TextBox ID="txtIdList" runat="server" SkinID="IdListFilter" MaxLength="9999"></asp:TextBox>
												<HPFx:RegularExpressionValidator ID="revTxtIdList" runat="server" ControlToValidate="txtIdList"
													SkinID="IntListValidator">
												</HPFx:RegularExpressionValidator>
                                                <HPFx:CustomValidator ID="cvTxtIdList" runat="server" ErrorMessage="Must be a comma-separated list of integers.</br>"
                                                    OnServerValidate="cvTxtIdList_ServerValidate" ControlToValidate="txtIdList" />
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
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblOwner" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlOwner" Text="Owner:"></asp:Label>
												<asp:DropDownList ID="ddlOwner" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter owner">
												</asp:DropDownList>
											</td>
											<td align="left">
												<asp:Label ID="lblModuleStatus" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlModuleStatus" Text="Module Status:"></asp:Label>
												<asp:DropDownList ID="ddlModuleStatus" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter module status">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblModuleCategory" runat="server" Text="Category:" SkinID="FilterLabel" AssociatedControlID="ddlModuleCategory"></asp:Label>
												<asp:DropDownList ID="ddlModuleCategory" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter module category">
												</asp:DropDownList>
											</td>
											<td align="left">
												<asp:Label ID="lblModuleSubCategory" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlModuleSubCategory" Text="Sub Category:"></asp:Label>
												<asp:DropDownList ID="ddlModuleSubCategory" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter module sub category">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblVersionMajor" runat="server" Text="Major Version:" SkinID="FilterLabel" AssociatedControlID="txtVersionMajor"></asp:Label>
												<asp:TextBox ID="txtVersionMajor" SkinID="IdFilter" runat="server" ToolTip="Filter major version">
												</asp:TextBox>
												<HPFx:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtVersionMajor"
													SkinID="IntValidator">
												</HPFx:RegularExpressionValidator>
											</td>
											<td align="left">
												<asp:Label ID="lblVersionMinor" runat="server" Text="Minor Version:" SkinID="FilterLabel" AssociatedControlID="txtVersionMinor"></asp:Label>
												<asp:TextBox ID="txtVersionMinor" SkinID="IdFilter" runat="server" ToolTip="Filter minor version">
												</asp:TextBox>
												<HPFx:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtVersionMinor"
													SkinID="IntValidator">
												</HPFx:RegularExpressionValidator>
											</td>
										</tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblFilename" runat="server" Text="Filename:" SkinID="FilterLabel" AssociatedControlID="txtFilename"></asp:Label>
                                                <asp:TextBox ID="txtFilename" SkinID="NameFilter" runat="server">
                                                </asp:TextBox>
                                            </td>
                                            <td align="left">
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
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblTagsFilter" runat="server" Text="Tags:" AssociatedControlID="txtTagsFilter"></asp:Label>
                                                <asp:TextBox ID="txtTagsFilter" Columns="100" ToolTip="Filter by module tag(s)" runat="server"
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
			<td colspan="2">
				<asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<hpfx:EnhancedGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
					AllowPaging="True" OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" AutoGenerateCheckBoxColumn="true"
					DataKeyNames="Id,ValidationId,ProductionId" DataSourceID="odsDataSource">
					<Columns>
						<asp:ButtonField Text="Edit..." ButtonType="Button" CausesValidation="false" CommandName="Edit..." />
						<asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id"
							DataNavigateUrlFormatString="~/DataAdmin/WorkflowModuleUpdate.aspx?id={0}" />
						<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
						<asp:BoundField DataField="ModuleCategoryName" HeaderText="Category" SortExpression="ModuleCategoryName" />
						<asp:BoundField DataField="ModuleSubCategoryName" HeaderText="Sub Category" SortExpression="ModuleSubCategoryName" />
                        <asp:BoundField DataField="WorkflowModuleStatusName" HeaderText="Status" SortExpression="WorkflowModuleStatusName" />
						<asp:BoundField DataField="VersionMajor" HeaderText="Major Version" SortExpression="VersionMajor" />
						<asp:BoundField DataField="VersionMinor" HeaderText="Minor Version" SortExpression="VersionMinor" />
						<asp:BoundField DataField="Filename" HeaderText="Filename" SortExpression="Filename" />
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
                        <asp:BoundField DataField="PersonName" HeaderText="Owner" SortExpression="PersonName" />
						<asp:BoundField DataField="CreatedOn" HeaderText="Created" SortExpression="CreatedOn" />
						<asp:BoundField DataField="ModifiedOn" HeaderText="Modified" SortExpression="ModifiedOn" />
						<HPFx:PeopleFinderHyperLinkField DataTextField="CreatedBy" HeaderText="Created By"
							SortExpression="CreatedBy" DataNavigateUrlFields="CreatedBy" />
						<HPFx:PeopleFinderHyperLinkField DataTextField="ModifiedBy" HeaderText="Modified By"
							SortExpression="ModifiedBy" DataNavigateUrlFields="ModifiedBy" />
						<asp:CommandField Visible="false" ShowHeader="false" ButtonType="Button" CausesValidation="false" />
					</Columns>
					<EmptyDataTemplate>
						<asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no Modules found</asp:Label>
					</EmptyDataTemplate>
				</hpfx:EnhancedGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true"
	SelectMethod="ODSFetch" SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapWorkflowModuleController"
	SortParameterName="sortExpression">
	<SelectParameters>
		<asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
		
	</SelectParameters>
</asp:ObjectDataSource>
