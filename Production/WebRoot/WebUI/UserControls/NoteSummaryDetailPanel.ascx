<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteSummaryDetailPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.NoteSummaryDetailPanel" %>
<asp:Panel ID="pnlEditArea" runat="server"
	Visible='<%# !this.DataItem.IsNew %>'
>
	<div>
		<asp:HyperLink ID="lnkDirectAccessLink" runat="server" SkinID="NavigateIntoPopupLink"
			Visible='<%# !this.IsNewRecord %>'
			NavigateUrl='<%# this.GenerateDetailPageUrl(this.DataSourceId) %>'
			/>
	</div>
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Note Id:" SkinID="DataFieldLabel" />
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server" Text='<%# this.DataItem.Id %>'></asp:Label>
			</td>
		</tr>
<%--		<tr>
			<td>
				<asp:Label ID="lblEntityTypeLabel" runat="server" Text="Entity Type:" SkinID="DataFieldLabel" AssociatedControlID="lblEntityTypeValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblEntityTypeValue" runat="server" Text='<%# this.DataItem.EntityTypeName %>'></asp:Label>
			</td>
		</tr>--%>
		<tr>
			<td>
				<asp:Label ID="lblEntityLabel" runat="server" Text="Entity:" SkinID="DataFieldLabel" AssociatedControlID="lblEntityValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblEntityValue" runat="server" Text='<%# string.Format("{0} #{1}", this.DataItem.EntityTypeName, this.DataItem.EntityId) %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblNameLabel" runat="server" Text="Title:" SkinID="DataFieldLabel" AssociatedControlID="lblNameValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblNameValue" runat="server" Text='<%# this.DataItem.Name %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCommentLabel" runat="server" Text="Details:" SkinID="DataFieldLabel" AssociatedControlID="lblCommentValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCommentValue" runat="server" Text='<%# this.DataItem.Comment %>'></asp:Label>
			</td>
		</tr>
<%--		<tr>
			<td>
				<asp:Label ID="lblNoteTypeLabel" runat="server" Text="Note Type:" SkinID="DataFieldLabel" AssociatedControlID="lblNoteTypeValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblNoteTypeValue" runat="server" Text='<%# this.DataItem.NoteTypeName %>'></asp:Label>
				<asp:HyperLink ID="lnkNoteType" runat="server" SkinID="ViewReadOnlyLink"
					Visible='<%# this.DataItem.NoteTypeId != null %>'
					NavigateUrl='<%# Global.GetNoteTypeUpdatePageUri(this.DataItem.NoteTypeId, null) %>'
					/>
			</td>
		</tr>--%>
		<tr>
			<td>
				<asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedOnValue" runat="server" Text='<%# this.DataItem.CreatedOn %>'></asp:Label>
				(by
				<asp:Label ID="lblCreatedByValue" runat="server" Text='<%# this.DataItem.CreatedBy %>'></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedOnValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedOnValue" runat="server" Text='<%# this.DataItem.ModifiedOn %>'></asp:Label>
				(by
				<asp:Label ID="lblModifiedByValue" runat="server" Text='<%# this.DataItem.ModifiedBy %>'></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table class="table.layoutTable">
					<tr>
						<td>
							<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Panel>