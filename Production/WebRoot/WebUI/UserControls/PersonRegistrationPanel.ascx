<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonRegistrationPanel.ascx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.PersonRegistrationPanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Name:" SkinID="DataFieldLabel"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblNameText" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblWindowsId" runat="server" Text="Windows Id: *" SkinID="DataFieldLabel" AssociatedControlID="txtWindowsId"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtWindowsId" runat="server" MaxLength="256" Columns="100" ToolTip="Windows Id (e.g. AMERICAS\smith)"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvWindowsId" runat="server" ControlToValidate="txtWindowsId"
					ErrorMessage="Please enter a windows Id (e.g. AMERICAS\smith).<br/>" />
				<hpfx:RegularExpressionValidator ID="revWindowsId" runat="server" SkinID="WindowsNTIdentifierValidator" ControlToValidate="txtWindowsId" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblFirstName" runat="server" Text="First Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtFirstName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtFirstName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
					ErrorMessage="Please enter a first name.<br/>" 
					/>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" SkinID="DataFieldLabel" AssociatedControlID="txtMiddleName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtMiddleName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblLastName" runat="server" Text="Last Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtLastName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtLastName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
					ErrorMessage="Please enter a last name.<br/>" 
					/>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblEmail" runat="server" Text="Email: *" SkinID="DataFieldLabel" AssociatedControlID="txtEmail"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtEmail" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
					ErrorMessage="Please enter an email (e.g. john.smith@hp.com).<br/>" 
					/>
				<hpfx:RegularExpressionValidator ID="revEmail" runat="server" SkinID="EmailAddressValidator"
					ControlToValidate="txtEmail" />
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
				<asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click" Text="Register" />
				&nbsp;
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				&nbsp;
				<asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False" OnClick="btnCancel_Click" />
			</td>
		</tr>
	</table>
</asp:Panel>
