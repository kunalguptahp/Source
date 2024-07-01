<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogListPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.LogListPanel" %>
<asp:Panel ID="pnlList" runat="server">
	<table border="0">
		<tr>
			<td align="left" colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left" colspan="2">
				<%--<asp:Button ID="btnCreate" SkinID="CreateItem..." runat="server" ToolTip="Create new Log"
					OnClick="btnCreate_Click" />--%>
				<asp:Button ID="btnExport" SkinID="ExportData" runat="server" OnClick="btnExport_Click" />
				<asp:Button ID="btnExportPage" SkinID="ExportData" runat="server" OnClick="btnExportPage_Click" Text="Export Page... (current page only)" />
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
												<asp:Label ID="lblIdList" runat="server" SkinID="FilterLabel" AssociatedControlID="txtIdList" Text="Log IDs:"></asp:Label>
												<asp:TextBox ID="txtIdList" runat="server" SkinID="IdListFilter" MaxLength="9999"></asp:TextBox>
												<HPFx:RegularExpressionValidator ID="revTxtIdList" runat="server" ControlToValidate="txtIdList"
													SkinID="IntListValidator">
												</HPFx:RegularExpressionValidator>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblDateAfter" runat="server" Text="Date After:" SkinID="FilterLabel" AssociatedControlID="txtDateAfter"></asp:Label>
												<asp:TextBox ID="txtDateAfter" Columns="10" ToolTip="Filter Date on or after this date"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgDateAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtDateAfter" runat="server" TargetControlID="txtDateAfter"
													PopupButtonID="imgDateAfter" />
												<HPFx:CompareValidator ID="cvDateAfter" runat="server" SkinID="DateValidator" ControlToValidate="txtDateAfter" />
												<asp:Label ID="lblDateBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtDateBefore"></asp:Label>
												<asp:TextBox ID="txtDateBefore" Columns="10" ToolTip="Filter Date prior to this date"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgDateBefore" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtDateBefore" runat="server" TargetControlID="txtDateBefore"
													PopupButtonID="imgDateBefore" />
												<HPFx:CompareValidator ID="cvDateBefore" runat="server" SkinID="DateValidator" ControlToValidate="txtDateBefore" />
												<HPFx:CompareValidator ID="cvDateOnRange" runat="server" SkinID="DateRangeValidator_BeforeGreaterThanAfter"
													ControlToValidate="txtDateBefore" ControlToCompare="txtDateAfter" />
											</td>
											<td align="left">
												<asp:Label ID="lblUtcDateAfter" runat="server" Text="UtcDate After:" SkinID="FilterLabel" AssociatedControlID="txtUtcDateAfter"></asp:Label>
												<asp:TextBox ID="txtUtcDateAfter" Columns="10" ToolTip="Filter UtcDate on or after this date"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgUtcDateAfter" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtUtcDateAfter" runat="server" TargetControlID="txtUtcDateAfter"
													PopupButtonID="imgUtcDateAfter" />
												<HPFx:CompareValidator ID="cvUtcDateAfter" runat="server" SkinID="DateValidator" ControlToValidate="txtUtcDateAfter" />
												<asp:Label ID="lblUtcDateBefore" runat="server" Text="Before:" SkinID="FilterLabel" AssociatedControlID="txtUtcDateBefore"></asp:Label>
												<asp:TextBox ID="txtUtcDateBefore" Columns="10" ToolTip="Filter UtcDate prior to this date"
													runat="server"></asp:TextBox>
												<asp:Image ID="imgUtcDateBefore" runat="server" SkinID="CalendarExtenderImage" />
												<asp:CalendarExtender ID="ajaxcxtUtcDateBefore" runat="server" TargetControlID="txtUtcDateBefore"
													PopupButtonID="imgUtcDateBefore" />
												<HPFx:CompareValidator ID="cvUtcDateBefore" runat="server" SkinID="DateValidator" ControlToValidate="txtUtcDateBefore" />
												<HPFx:CompareValidator ID="cvUtcDateOnRange" runat="server" SkinID="DateRangeValidator_BeforeGreaterThanAfter"
													ControlToValidate="txtUtcDateBefore" ControlToCompare="txtUtcDateAfter" />
											</td>
										</tr>
										<tr>
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
											<td align="left">
												<asp:Label ID="lblSeverity" runat="server" Text="Min. Severity:" SkinID="FilterLabel" AssociatedControlID="ddlSeverity"></asp:Label>
												<asp:DropDownList ID="ddlSeverity" SkinID="ForeignKeyColumnFilter" runat="server" ToolTip="Filter severity">
												</asp:DropDownList>
												<asp:Label ID="lblOnlyExceptions" runat="server" Text="Only List Exceptions?" SkinID="FilterLabel" ></asp:Label>
												<asp:CheckBox ID="cbOnlyExceptions" SkinID="CheckBoxFilter" runat="server" ToolTip="Select to view only Exceptions" />
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblMachineName" runat="server" Text="Machine Name:" SkinID="FilterLabel" AssociatedControlID="txtMachineName"></asp:Label>
												<asp:TextBox ID="txtMachineName" runat="server" ToolTip="Filter machine name. Wild cards('%', '_') allowed.">
												</asp:TextBox>
											</td> 
											<td align="left">
												<asp:Label ID="lblWebSessionId" runat="server" Text="Web Session ID:" SkinID="FilterLabel" AssociatedControlID="txtWebSessionId"></asp:Label>
												<asp:TextBox ID="txtWebSessionId" runat="server" ToolTip="Filter web session ID. No wildcard allowed.">
												</asp:TextBox>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblUserName" runat="server" Text="User Name:" SkinID="FilterLabel" AssociatedControlID="txtUserName"></asp:Label>
												<asp:TextBox ID="txtUserName" runat="server" ToolTip="Filter user name. Wild cards('%', '_') allowed.">
												</asp:TextBox>
											</td>
											<td align="left">
												<asp:Label ID="lblUserIdentity" runat="server" Text="User Identity:" SkinID="FilterLabel" AssociatedControlID="txtUserIdentity"></asp:Label>
												<asp:TextBox ID="txtUserIdentity" runat="server" ToolTip="Filter user id">
												</asp:TextBox>
											</td>
										</tr>
										<tr>
											<td align="left">
												<asp:Label ID="lblUserWebIdentity" runat="server" Text="User Web Identity:" SkinID="FilterLabel" AssociatedControlID="txtUserWebIdentity"></asp:Label>
												<asp:TextBox ID="txtUserWebIdentity" runat="server" ToolTip="Filter user web identity. Wild cards('%', '_') allowed.">
												</asp:TextBox>
											</td>
											<td align="left">
												<asp:Label ID="lblProcessUser" runat="server" Text="Process User:" SkinID="FilterLabel" AssociatedControlID="txtProcessUser"></asp:Label>
												<asp:TextBox ID="txtProcessUser" runat="server" ToolTip="Filter process user. Wild cards('%', '_') allowed.">
												</asp:TextBox>
											</td>
										</tr>
										<tr>
											<td align="left" colspan = 2>
												<asp:Label ID="lblLogger" runat="server" Text="Logger:" SkinID="FilterLabel" AssociatedControlID="txtLogger"></asp:Label>
												<asp:TextBox ID="txtLogger" runat="server" Columns=100 ToolTip="Filter logger"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td align="left" colspan = 2>
												<asp:Label ID="lblLocation" runat="server" Text="Location:" SkinID="FilterLabel" AssociatedControlID="txtLocation"></asp:Label>
												<asp:TextBox ID="txtLocation" runat="server" Columns=100 ToolTip="Filter Location. Wild cards('%', '_') allowed.">
												</asp:TextBox>
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
						<asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id"
							Target="_top" DataNavigateUrlFormatString="~/SystemAdmin/LogDetail.aspx?id={0}" />
						<asp:BoundField HeaderText="Server" DataField="MachineName" SortExpression="MachineName" />
						<asp:BoundField HeaderText="Created At" DataField="CreatedAt" SortExpression="CreatedAt" />
						<asp:BoundField HeaderText="Date" DataField="DateX" SortExpression="Date" />
						<asp:BoundField HeaderText="UTC Date" DataField="UtcDate" SortExpression="UtcDate" />
						<asp:BoundField HeaderText="Severity" DataField="Severity" SortExpression="Severity" />
						<asp:BoundField HeaderText="Exception?" DataField="Exception" SortExpression="Exception" DataFormatString="Yes" />
						<asp:BoundField HeaderText="User Identity" DataField="UserIdentity" SortExpression="UserIdentity" />
						<asp:BoundField HeaderText="UserName" DataField="UserName" SortExpression="UserName" />
						<asp:BoundField HeaderText="User Web Identity" DataField="UserWebIdentity" SortExpression="UserWebIdentity" />
						<asp:BoundField HeaderText="Message" DataField="Message" SortExpression="Message" />
						<asp:BoundField HeaderText="Logger" DataField="Logger" SortExpression="Logger" />
						<asp:BoundField HeaderText="Location" DataField="Location" SortExpression="Location" />
						<asp:BoundField HeaderText="Web Session ID" DataField="WebSessionId" SortExpression="WebSessionId" />
						<asp:BoundField HeaderText="Process Uptime" DataField="ProcessUptime" SortExpression="ProcessUptime" />
						<asp:BoundField HeaderText="Process Thread" DataField="ProcessThread" SortExpression="ProcessThread" />
						<asp:BoundField HeaderText="Allocated Memory" DataField="AllocatedMemory" SortExpression="AllocatedMemory" />
						<asp:BoundField HeaderText="Working Memory" DataField="WorkingMemory" SortExpression="WorkingMemory" />
						<asp:BoundField HeaderText="ProcessUser" DataField="ProcessUser" SortExpression="ProcessUser" />
						<asp:BoundField Visible="false" HeaderText="Process User Interactive?" DataField="ProcessUserInteractive" SortExpression="ProcessUserInteractive" />
						<asp:BoundField Visible="false" HeaderText="# Processors" DataField="ProcessorCount" SortExpression="ProcessorCount" />
						<asp:BoundField Visible="false" HeaderText="OS Version" DataField="OSVersion" SortExpression="OSVersion" />
						<asp:BoundField Visible="false" HeaderText="CLR Version" DataField="ClrVersion" SortExpression="ClrVersion" />
						<asp:BoundField Visible="false" HeaderText="Exception" DataField="Exception" SortExpression="Exception" />
						<asp:BoundField Visible="false" HeaderText="Stack Trace" DataField="StackTrace" SortExpression="StackTrace" />
					</Columns>
					<EmptyDataTemplate>
						<asp:Label ID="lblEmptyData" runat="server" Text="Label">There are no log(s) found</asp:Label>
					</EmptyDataTemplate>
				</HPFx:PageableItemContainerGridView>
				<asp:DataPager ID="dpListBottom" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetch"
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.LogController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
