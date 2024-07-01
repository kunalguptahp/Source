<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteSummariesListPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.NoteSummariesListPanel" %>
<asp:Panel ID="pnlList" runat="server">
	<table border="0">
		<tr>
			<td align="left" colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left" colspan="2">
				<asp:HyperLink ID="lnkDirectAccessLink" runat="server" SkinID="NavigateIntoPopupLink" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:DataPager ID="dpListTop" runat="server" PagedControlID="gvList" SkinID="StandardDataPager" OnInit="DataPager_Init"></asp:DataPager>
				<HPFx:PageableItemContainerGridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
					AllowPaging="True" DataKeyNames="Id" DataSourceID="odsDataSource">
					<Columns>
						<asp:HyperLinkField HeaderText="ID" SortExpression="Id" DataTextField="Id" DataNavigateUrlFields="Id" Target="_top" DataNavigateUrlFormatString="~/DataAdmin/NoteDetail.aspx?id={0}" />
						<asp:BoundField DataField="NoteTypeName" HeaderText="Note Type" SortExpression="NoteTypeName" />
						<asp:BoundField DataField="EntityTypeName" HeaderText="Entity Type" SortExpression="EntityTypeName" />
						<%--<asp:HyperLinkField HeaderText="Entity Type" SortExpression="EntityTypeName" DataTextField="EntityTypeName"
							DataNavigateUrlFields="EntityTypeId" Target="_top" DataNavigateUrlFormatString="~/DataAdmin/EntityTypeUpdate.aspx?id={0}" />--%>
						<asp:BoundField DataField="EntityID" HeaderText="Entity ID" SortExpression="EntityID" />
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
<asp:ObjectDataSource ID="odsDataSource" runat="server" EnablePaging="true" SelectMethod="ODSFetch"
    SelectCountMethod="ODSFetchCount" TypeName="HP.ElementsCPS.Data.SubSonicClient.VwMapNoteController"
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="serializedQuerySpecificationXml" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
