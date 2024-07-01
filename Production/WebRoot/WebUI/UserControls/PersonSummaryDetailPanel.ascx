<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonSummaryDetailPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.PersonSummaryDetailPanel" %>
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
				<asp:Label ID="lblIdLabel" runat="server" Text="Person Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server" Text='<%# this.DataItem.Id %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblNameLabel" runat="server" Text="Name:" SkinID="DataFieldLabel" AssociatedControlID="lblNameValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblNameValue" runat="server" Text='<%# this.DataItem.Name %>'></asp:Label>
			</td>
		</tr>
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
			<td>
				<asp:Label ID="lblWindowsId" runat="server" Text="Windows Id:" SkinID="DataFieldLabel" AssociatedControlID="lblWindowsIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblWindowsIdValue" runat="server" Text='<%# this.DataItem.WindowsId %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblFirstName" runat="server" Text="First Name:" SkinID="DataFieldLabel" AssociatedControlID="lblFirstNameValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblFirstNameValue" runat="server" Text='<%# this.DataItem.FirstName %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" SkinID="DataFieldLabel" AssociatedControlID="lblMiddleNameValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblMiddleNameValue" runat="server" Text='<%# this.DataItem.MiddleName %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblLastName" runat="server" Text="Last Name:" SkinID="DataFieldLabel" AssociatedControlID="lblLastNameValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblLastNameValue" runat="server" Text='<%# this.DataItem.LastName %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblEmail" runat="server" Text="Email:" SkinID="DataFieldLabel" AssociatedControlID="lblEmailValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblEmailValue" runat="server" Text='<%# this.DataItem.Email %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblEffectiveRoles" runat="server" Text="Effective Roles:" SkinID="DataFieldLabel" AssociatedControlID="lblEffectiveRolesValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblEffectiveRolesValue" runat="server" Text='<%# (this.IsNewRecord || (this.DataItem == null) || (this.DataItem.IsNew)) ? "" : this.DataItem.InnerPerson.GetEffectiveRolesAsString() %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblRole" runat="server" Text="Assigned Roles:" SkinID="DataFieldLabel" AssociatedControlID="chkRoleList"></asp:Label>
			</td>
			<td>
				<asp:CheckBoxList ID="chkRoleList" runat="server" RepeatColumns="3" Enabled="false">
				</asp:CheckBoxList>
			</td>
		</tr>
<%--		<tr>
			<td>
				<asp:Label ID="lblComment" runat="server" Text="Comment:" SkinID="DataFieldLabel" AssociatedControlID="lblCommentValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCommentValue" runat="server" Text='<%# this.DataItem.Comment %>'></asp:Label>
			</td>
		</tr>
--%>		<tr>
			<td>
				<asp:Label ID="lblStatus" runat="server" Text="Status:" SkinID="ddlStatus" AssociatedControlID="lblStatusValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblStatusValue" runat="server" Text='<%# this.DataItem.RowStatusName %>'></asp:Label>
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

