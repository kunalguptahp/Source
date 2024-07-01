<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JumpstationMacroUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationMacroUpdatePanel" %>
<%@ Register Src="~/UserControls/JumpstationMacroValueListPanel.ascx" TagName="JumpstationMacroValueListPanel"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Macro Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblValidationId" runat="server" Text="Validation Id:" SkinID="DataFieldLabel" AssociatedControlID="lblValidationIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblValidationIdValue" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblPublicationId" runat="server" Text="Publication Id:" SkinID="DataFieldLabel" AssociatedControlID="lblPublicationIdValue"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblPublicationIdValue" runat="server"></asp:Label>
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
                <asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Name" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ValidationGroup="SaveJumpstationMacro" ErrorMessage="Please enter a name.<br/>" />
                    <hpFx:CustomValidator ID="cvNameUnique" runat="server" ErrorMessage="Please enter a unique name.</br>"
                        OnServerValidate="cvNameUnique_ServerValidate" ValidationGroup="SaveJumpstationMacro"/>
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
		<tr>
            <td>
                <asp:Label ID="lblDefaultResultValue" runat="server" Text="Default Result Value: *" SkinID="DataFieldLabel" AssociatedControlID="txtDefaultResultValue"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDefaultResultValue" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDefaultResultValue" runat="server" MaxLength="256" Columns="150"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvDefaultResultValue" runat="server" ControlToValidate="txtDefaultResultValue"
                        ValidationGroup="SaveJumpstationMacro" ErrorMessage="Please enter a default result value.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
			<td>
				<asp:Label ID="lblOwner" runat="server" Text="Owner: *" SkinID="DataFieldLabel" AssociatedControlID="ddlOwner"></asp:Label>
			</td>
			<td>
			<asp:Panel ID="pnlDataControls_Owner" runat="server"
				Enabled='<%# this.IsDataModificationAllowed() %>'>
				<asp:DropDownList ID="ddlOwner" runat="server" Enabled="false">
				</asp:DropDownList>
            </asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblJumpstationMacroStatus" runat="server" Text="Macro Status: *" SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationMacroStatus"></asp:Label>
			</td>
			<td>
    			<asp:Panel ID="pnlDataControls_Status" runat="server"
				Enabled='<%# this.IsDataModificationAllowed() %>'>
				<asp:DropDownList ID="ddlJumpstationMacroStatus" runat="server" Enabled="false">
				</asp:DropDownList>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
				<asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
				<hpfx:CustomValidator ID="cvJumpstationMacroPublishable" runat="server" 
					ValidationGroup="PublishableJumpstationMacro" 
					ErrorMessage="This Macro is not ready to be published. Please make sure that all Macro data is complete and valid.<br>" 
					OnServerValidate="cvJumpstationMacroPublishable_ServerValidate"
					 />
				<hpfx:CustomValidator ID="cvJumpstationMacroDuplidate" runat="server" 
					ValidationGroup="DuplicateJumpstationMacro" 
					ErrorMessage="Macro cannot move to ready to validate when another Macro is already ready to validate, validated or published with the same parameters.<br>" 
					OnServerValidate="cvJumpstationMacroDuplicate_ServerValidate"
					 />
				<hpfx:CustomValidator ID="cvJumpstationMacroReplacement" runat="server" 
					ValidationGroup="ReplacementJumpstationMacro" 
					ErrorMessage="Macro cannot be published or unpublished because another Macro is replacing this one.<br>" 
					OnServerValidate="cvJumpstationMacroReplacement_ServerValidate"
					 />
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveJumpstationMacro" />
				<asp:ValidationSummary ID="vsPublishableOfferValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="PublishableJumpstationMacro" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="SaveJumpstationMacro" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
					ValidationGroup="SaveJumpstationMacro" OnClick="btnSaveAsNew_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnAbandon" runat="server" Text="Abandon" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnAbandon_Click" />
				<asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the Macro Validator(s)" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnReadyForValidation_Click" />
				<asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the Macro Coordinator(s)" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnValidate_Click" />
				<asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the Macro Owner for modification" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnRework_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnPublish" runat="server" Text="Publish" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnPublish_Click" />
				<asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" 
					CausesValidation="true" ValidationGroup="SaveJumpstationMacro" OnClick="btnUnPublish_Click" />
			</td>
		</tr>
	</table>
</asp:Panel>
<table>
    <tr>
        <td>
            <asp:Panel ID="pnlJumpstationMacroValueListPanel" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                <div class="ChildGridTitle">
                    Macro Values</div>
                <ElementsCPSuc:JumpstationMacroValueListPanel ID="ucJumpstationMacroValueList" runat="server" />
            </asp:Panel>
        </td>
    </tr>
</table>
