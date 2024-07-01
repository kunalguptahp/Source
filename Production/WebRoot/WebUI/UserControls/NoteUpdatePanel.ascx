<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.NoteUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Note Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server" ToolTip="Id"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
				(by
				<asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedOnValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
				(by
				<asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
				)
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblEntityType" runat="server" Text="Entity Type: *" SkinID="DataFieldLabel" AssociatedControlID="ddlEntityType"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlEntityType" runat="server">
				</asp:DropDownList>
				<asp:ListSearchExtender ID="ajaxextDdlEntityTypeSearcher" runat="server" TargetControlID="ddlEntityType"
					IsSorted="true" QueryPattern="Contains" />
				<asp:HyperLink ID="lnkEntityType" runat="server" SkinID="ViewDetailsLink" />
				<HPFx:RequiredFieldValidator ID="rfvEntityType" runat="server" ControlToValidate="ddlEntityType"
					ErrorMessage="Please select a Entity Type.<br/>" />

			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblEntityId" runat="server" Text="Entity Id: *" SkinID="DataFieldLabel" AssociatedControlID="txtEntityId"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtEntityId" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvEntityId" runat="server" ControlToValidate="txtEntityId"
					ErrorMessage="Please enter an Entity Id.<br/>" 
					/>
			    <asp:RangeValidator ID="rvEntityIdRange" runat="server" 
                    ErrorMessage="Invalid Entity Id." MinimumValue="1" 
                    MaximumValue="2147483647" ControlToValidate="txtEntityId" Type="Integer"></asp:RangeValidator>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblNoteType" runat="server" Text="Note Type: " SkinID="DataFieldLabel" AssociatedControlID="ddlNoteType"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlNoteType" runat="server">
				</asp:DropDownList>
				<asp:ListSearchExtender ID="ajaxextDdlNoteTypeSearcher" runat="server" TargetControlID="ddlNoteType"
					IsSorted="true" QueryPattern="Contains" />
				<asp:HyperLink ID="lnkNoteType" runat="server" SkinID="ViewDetailsLink" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Title: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100" ToolTip="Name"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
					ErrorMessage="Please enter a name.<br/>" />
				<hpfx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
					SkinID="MinLength3Validator"
					ErrorMessage="Name is too short.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblComment" runat="server" Text="Details:" SkinID="DataFieldLabel" AssociatedControlID="txtComment"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtComment" runat="server" Columns="100" Rows="8" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblStatus" runat="server" Text="Status:" SkinID="DataFieldLabel" AssociatedControlID="ddlStatus"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlStatus" runat="server">
				</asp:DropDownList>
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
				<asp:HyperLink ID="lnkDone" runat="server" SkinID="DoneLink"
					Visible='<%# !this.IsNewRecord %>'
					NavigateUrl='<%# Global.GetNoteDetailPageUri(this.DataSourceId, null) %>'
					/>
				&nbsp;
				<asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click"
					Enabled='<%# this.IsNewRecord || HP.ElementsCPS.Core.Security.SecurityManager.IsCurrentUserInRole(HP.ElementsCPS.Core.Security.UserRoleId.Administrator) %>'
					/>
				&nbsp;
				<asp:Button ID="btnSaveAndDone" runat="server" SkinID="SaveChangesAndDone" OnClick="btnSaveAndDone_Click"
					Enabled='<%# this.IsNewRecord || HP.ElementsCPS.Core.Security.SecurityManager.IsCurrentUserInRole(HP.ElementsCPS.Core.Security.UserRoleId.Administrator) %>'
					/>
				&nbsp;
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				&nbsp;
				<asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
					OnClick="btnCancel_Click" />
			</td>
		</tr>
	</table>
</asp:Panel>
