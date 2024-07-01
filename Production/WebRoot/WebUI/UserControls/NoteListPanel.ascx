<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteListPanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.NoteListPanel" %>
<asp:Panel ID="pnlList" runat="server">
	<table border="0">
		<tr>
			<td align="left" colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left" colspan="2">
				<asp:Button ID="btnCreate" SkinID="CreateItem..." runat="server" ToolTip="Create new Note"
					OnClick="btnCreate_Click" />
				<asp:Button ID="btnExport" SkinID="ExportData" runat="server" OnClick="btnExport_Click" />
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
												<span class="ajaxCollapsiblePanelTitle">Filters</span>
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
												<asp:Label ID="lblIdList" runat="server" SkinID="FilterLabel" AssociatedControlID="txtIdList" Text="Note IDs:"></asp:Label>
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
												<asp:Label ID="lblStatus" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlStatus" Text="Status:"></asp:Label>
												<asp:DropDownList ID="ddlStatus" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter Row Status">
												</asp:DropDownList>
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
												<asp:TextBox ID="txtCreatedAfter" SkinID="CreatedAfterFilter" runat="server"></asp:TextBox>
												<asp:Image ID="imgCreatedAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtCreatedAfter" runat="server" TargetControlID="txtCreatedAfter" PopupButtonID="imgCreatedAfter" />
												<HPFx:CompareValidator ID="cvCreatedAfter" runat="server" SkinID="DateValidator" ControlToValidate="txtCreatedAfter" />

												<asp:Label ID="lblCreatedBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtCreatedBefore"></asp:Label>
												<asp:TextBox ID="txtCreatedBefore" SkinID="CreatedBeforeFilter" runat="server"></asp:TextBox>
												<asp:Image ID="imgCreatedBefore" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtCreatedBefore" runat="server" TargetControlID="txtCreatedBefore" PopupButtonID="imgCreatedBefore" />
												<HPFx:CompareValidator ID="cvCreatedBefore" runat="server" SkinID="DateValidator" ControlToValidate="txtCreatedBefore" />

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
												<asp:TextBox ID="txtModifiedAfter" SkinID="ModifiedAfterFilter" runat="server"></asp:TextBox>
												<asp:Image ID="imgModifiedAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtModifiedAfter" runat="server" TargetControlID="txtModifiedAfter" PopupButtonID="imgModifiedAfter" />
												<HPFx:CompareValidator ID="cvModifiedAfter" runat="server" SkinID="DateValidator" ControlToValidate="txtModifiedAfter" />

												<asp:Label ID="lblModifiedBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtModifiedBefore"></asp:Label>
												<asp:TextBox ID="txtModifiedBefore" SkinID="ModifiedBeforeFilter" runat="server"></asp:TextBox>
												<asp:Image ID="imgModifiedBefore" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtModifiedBefore" runat="server" TargetControlID="txtModifiedBefore" PopupButtonID="imgModifiedBefore" />
												<HPFx:CompareValidator ID="cvModifiedBefore" runat="server" SkinID="DateValidator" ControlToValidate="txtModifiedBefore" />

												<HPFx:CompareValidator ID="cvModifiedOnRange" runat="server" SkinID="DateRangeValidator_BeforeGreaterThanAfter"
													ControlToValidate="txtModifiedBefore" ControlToCompare="txtModifiedAfter" />
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblEntityType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlEntityType" Text="Entity Type:"></asp:Label>
												<asp:DropDownList ID="ddlEntityType" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter Entity Type">
												</asp:DropDownList>
											</td>
											<td align="left">
												<asp:Label ID="lblEntityId" runat="server" Text="Entity Id:" SkinID="FilterLabel" AssociatedControlID="txtEntityId"></asp:Label>
												<asp:TextBox ID="txtEntityId" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter Entity Id">
												</asp:TextBox>
												<asp:RangeValidator ID="rvEntityIdRange" runat="server" 
                                                    ErrorMessage="Invalid Entity Id." MinimumValue="1" 
                                                    MaximumValue="2147483647" ControlToValidate="txtEntityId" Type="Integer">
                                                </asp:RangeValidator>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblNoteType" runat="server" SkinID="FilterLabel" AssociatedControlID="ddlNoteType" Text="Note Type:"></asp:Label>
												<asp:DropDownList ID="ddlNoteType" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter Note Type">
												</asp:DropDownList>
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
				<HPFx:PageableItemContainerGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
					AllowPaging="True"
					DataKeyNames="Id" DataSourceID="odsDataSource">
					<Columns>
						<asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id" Target="_top" DataNavigateUrlFormatString="~/DataAdmin/NoteDetail.aspx?id={0}" />
						<asp:BoundField DataField="NoteTypeName" HeaderText="Note Type" SortExpression="NoteTypeName" />
						<asp:BoundField DataField="EntityTypeName" HeaderText="Entity Type" SortExpression="EntityTypeName" />
						<asp:BoundField DataField="EntityID" HeaderText="Entity ID" SortExpression="EntityID" />
						<%--<asp:HyperLinkField HeaderText="Note Type" SortExpression="NoteTypeName" DataTextField="NoteTypeName"
							DataNavigateUrlFields="NoteTypeId" Target="_top" DataNavigateUrlFormatString="~/DataAdmin/NoteTypeUpdate.aspx?id={0}" />--%>
						<asp:BoundField DataField="Name" HeaderText="Title" SortExpression="Name" />
						<asp:BoundField HeaderText="Details" DataField="Comment" />
						<asp:BoundField DataField="CreatedOn" HeaderText="Created" SortExpression="CreatedOn" />
						<%--<asp:BoundField DataField="ModifiedOn" HeaderText="Modified" SortExpression="ModifiedOn" />--%>
						<HPFx:PeopleFinderHyperLinkField DataTextField="CreatedBy" HeaderText="Created By"
							SortExpression="CreatedBy" DataNavigateUrlFields="CreatedBy" />
						<%--<HPFx:PeopleFinderHyperLinkField DataTextField="ModifiedBy" HeaderText="Modified By"
							SortExpression="ModifiedBy" DataNavigateUrlFields="ModifiedBy" />--%>
						<%--<asp:BoundField DataField="RowStatusName" HeaderText="Status" SortExpression="RowStatusName" />--%>
						<HPFx:BoundField HeaderText="#N" DataField="NoteCount" SortExpression="NoteCount" HeaderToolTip="Number of Notes" />
						<%--<asp:TemplateField HeaderText="#N" SortExpression="NoteCount">
							<ItemTemplate>
								<asp:Panel ID="pnlNoteCountPopupTargetControl" runat="server" SkinId="PopupControlExtenderTargetControl" 
									CssClass='<%# (((int)Eval("NoteCount")) == 0) ? "" : ((WebControl)((Control)Container).FindControl("pnlNoteCountPopupTargetControl")).CssClass %>' 
									ToolTip='<%# (((int)Eval("NoteCount")) == 0) ? "" : ((WebControl)((Control)Container).FindControl("pnlNoteCountPopupTargetControl")).ToolTip %>' 
									Width="100%" HorizontalAlign="Right"
								>
									<asp:Label ID="lblNoteCount" runat="server" Text='<%# Eval("NoteCount") %>' />
								</asp:Panel>
								<asp:PopupControlExtender ID="ajaxhmeNoteCountPopup" runat="server" 
									Enabled='<%# ((int)Eval("NoteCount")) > 0 %>'
									OffsetX="-800"
									OffsetY='<%# (Container.DataItemIndex < (0.5 * this.Grid.PageSize)) ? 0 : -600 %>'
									Position='<%# (Container.DataItemIndex < (0.5 * this.Grid.PageSize)) ? AjaxControlToolkit.PopupControlPopupPosition.Bottom : AjaxControlToolkit.PopupControlPopupPosition.Top %>'
									TargetControlID="pnlNoteCountPopupTargetControl" PopupControlID="pnlPopupArea" 
									DynamicControlID="pnlPopupAreaDynamicContent" DynamicContextKey='<%# Eval("Id") %>' 
									DynamicServiceMethod="GetDynamicContentNoteSummariesList_Note" DynamicServicePath="~/WebMethods.asmx" />
							</ItemTemplate>
						</asp:TemplateField>--%>
						<%--<asp:ButtonField Text="Edit..." ButtonType="Button" CausesValidation="false" CommandName="Edit..." />--%>
						<%--<asp:CommandField Visible="false" ShowHeader="false" ButtonType="Button" CausesValidation="false" />--%>
					</Columns>
					<EmptyDataTemplate>
						<asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no Notes found</asp:Label>
					</EmptyDataTemplate>
				</HPFx:PageableItemContainerGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true"
	SelectMethod="ODSFetch" SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapNoteController"
	SortParameterName="sortExpression">
	<SelectParameters>
		<asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
	</SelectParameters>
</asp:ObjectDataSource>