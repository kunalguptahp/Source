<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryParameterValueUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.QueryParameterValueUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Parameter Value Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedOnValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel" AssociatedControlID="lblCreatedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel" AssociatedControlID="lblModifiedByValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblModifiedByValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Parameter Value: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<hpfx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
					ErrorMessage="Please enter a parameter value.<br/>" />
                <hpFx:CustomValidator ID="cvNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                    OnServerValidate="cvNameUnique_ServerValidate" ValidationGroup="SaveQueryParameterValue"/>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblParameter" runat="server" Text="Parameter" SkinID="DataFieldLabel" AssociatedControlID="ddlParameter"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlParameter" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Comment:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="8" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
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
            <td style="text-align: center" colspan="2">
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnCreate" runat="server" SkinID="CreateItem" OnClick="btnCreate_Click"
                                ValidationGroup="SaveQueryParameterValue" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" SkinID="SaveChanges" OnClick="btnSave_Click"
                                ValidationGroup="SaveQueryParameterValue" />
                            <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                            <asp:Button ID="btnCancel" runat="server" SkinID="CancelChanges" CausesValidation="False"
                                OnClick="btnCancel_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
	</table>
</asp:Panel>
