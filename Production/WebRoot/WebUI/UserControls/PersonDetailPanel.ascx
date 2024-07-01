<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonDetailPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.PersonDetailPanel" %>
<%--<%@ Register Src="~/UserControls/NoteSummariesListPanel.ascx" TagName="NoteListPanel"
    TagPrefix="ElementsCPSuc" %>
--%><asp:Panel ID="pnlPopupArea" runat="server" SkinID="PopupControlExtenderPopupControl">
    <asp:Panel ID="pnlPopupAreaDynamicContent" runat="server" SkinID="PopupControlExtenderPopupControlDynamicContent"
        Width="800" Height="600">
        <!--Dynamic content will appear here-->
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlEditArea" runat="server" Visible='<%# !this.DataItem.IsNew %>'>
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Person Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdValue" runat="server" Text='<%# this.DataItem.Id %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNameLabel" runat="server" Text="Name:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblNameValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNameValue" runat="server" Text='<%# this.DataItem.Name %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCreatedByValue"></asp:Label>
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
                <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedOnValue"></asp:Label>
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
                <asp:Label ID="lblWindowsId" runat="server" Text="Windows Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblWindowsIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWindowsIdValue" runat="server" Text='<%# this.DataItem.WindowsId %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblFirstNameValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFirstNameValue" runat="server" Text='<%# this.DataItem.FirstName %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblMiddleNameValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMiddleNameValue" runat="server" Text='<%# this.DataItem.MiddleName %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblLastNameValue"></asp:Label>
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
                <asp:Label ID="lblEffectiveRoles" runat="server" Text="Effective Roles:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblEffectiveRolesValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEffectiveRolesValue" runat="server" Text='<%# (this.IsNewRecord || (this.DataItem == null) || (this.DataItem.IsNew)) ? "" : this.DataItem.InnerPerson.GetEffectiveRolesAsString() %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRole" runat="server" Text="Assigned Roles:" SkinID="DataFieldLabel"
                    AssociatedControlID="chkRoleList"></asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="chkRoleList" runat="server" RepeatColumns="3" Enabled="false">
                </asp:CheckBoxList>
                
                Viewer: <asp:CheckBoxList ID="chkApplicationList1" runat="server" RepeatColumns="4" Enabled="false">
                </asp:CheckBoxList>
                Editor: <asp:CheckBoxList ID="chkApplicationList2" runat="server" RepeatColumns="4" Enabled="false">
                </asp:CheckBoxList>
                Validator: <asp:CheckBoxList ID="chkApplicationList3" runat="server" RepeatColumns="4" Enabled="false">
                </asp:CheckBoxList>
                DataAdmin: <asp:CheckBoxList ID="chkApplicationList4" runat="server" RepeatColumns="4" Enabled="false">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTenant" runat="Server" SkinID="DataFieldLabel" AssociatedControlID="Tenant"  
                    Text="Tenant: "></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="Tenant" runat="server"  Enabled="false" Text='<%# this.DataItem.TenantGroupName %>'>
                </asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text="Status:" SkinID="ddlStatus" AssociatedControlID="lblStatusValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStatusValue" runat="server" Text='<%# this.DataItem.RowStatusName %>'></asp:Label>
            </td>
        </tr>
        <%--		<tr>
			<td>
				<asp:Label ID="lblNoteCount" runat="server" Text="# of Notes:"
					SkinID="DataFieldLabel" AssociatedControlID="lblNoteCountValue"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlNoteCountPopupTargetControl" runat="server" SkinId="PopupControlExtenderTargetControlInline" 
					CssClass='<%# (this.DataItem.NoteCount == 0) ? "" : this.pnlNoteCountPopupTargetControl.CssClass %>' 
					ToolTip='<%# (this.DataItem.NoteCount == 0) ? "" : this.pnlNoteCountPopupTargetControl.ToolTip %>' 
				>
					<asp:Label ID="lblNoteCountValue" runat="server" Text='<%# this.DataItem.NoteCount %>'>
					</asp:Label>
					Notes
				</asp:Panel>
				<asp:PopupControlExtender ID="ajaxhmeNoteCountPopup" runat="server" 
					Enabled='<%# (this.DataItem.NoteCount > 0) %>'
					OffsetX="-200"
					OffsetY="0"
					Position="Bottom"
					TargetControlID="pnlNoteCountPopupTargetControl" PopupControlID="pnlPopupArea" 
					DynamicControlID="pnlPopupAreaDynamicContent" DynamicContextKey='<%# this.DataItem.Id %>' 
					DynamicServiceMethod="GetDynamicContentNoteSummariesList_Person" DynamicServicePath="~/WebMethods.asmx" />
				<asp:HyperLink ID="lnkNewNote" runat="server" SkinID="NewRecordLink"
					Visible='<%# !this.DataItem.IsNew %>'
					NavigateUrl='<%# Global.GetNoteUpdatePageUri(null, new NoteQuerySpecification { EntityTypeId = (int)HP.ElementsCPS.Data.SubSonicClient.EntityTypeIdentifier.Person, EntityId = this.DataSourceId }) %>'
					/>
				<asp:CheckBox ID="chkNoteChildListToggle" runat="server" SkinID="DisplayDetailsToggle" AutoPostBack="true"
					Enabled='<%# (this.DataItem.NoteCount > 0) %>'
					OnCheckedChanged="OnUpdateChildListVisibility" />
			</td>
		</tr>
--%>
        <tr>
            <td>
            </td>
            <td>
                <asp:CheckBox ID="chkAllChildListToggle" runat="server" SkinID="DisplayAllDetailsToggle"
                    AutoPostBack="true" OnCheckedChanged="chkAllChildListToggle_CheckedChanged" />
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
        <tr>
            <td align="center" colspan="2">
                <asp:HyperLink ID="lnkEdit" runat="server" SkinID="EditRecordLink" Visible='<%# !this.DataItem.IsNew %>'
                    NavigateUrl='<%# Global.GetPersonUpdatePageUri(this.DataItem.Id, null) %>' />
            </td>
        </tr>
    </table>
    <table>
        <%--		<tr>
			<td colspan="2">
				<asp:Panel ID="pnlNoteList" runat="server">
					<div class="ChildGridTitle">Related Notes</div>
					<ElementsCPSuc:NoteListPanel ID="ucNoteList" runat="server"
						ImmutableQueryConditions='<%# new NoteQuerySpecification { EntityTypeId = (int)HP.ElementsCPS.Data.SubSonicClient.EntityTypeIdentifier.Person, EntityId = (this.DataSourceId ?? -1) } %>'
						/>
				</asp:Panel>
			</td>
		</tr>
--%>
    </table>
</asp:Panel>
