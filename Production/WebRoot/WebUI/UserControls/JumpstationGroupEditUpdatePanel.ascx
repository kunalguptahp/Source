<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JumpstationGroupEditUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupEditUpdatePanel" %>
<%@ Register Src="~/UserControls/JumpstationLogPanel.ascx" TagName="JumpstationLog"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblId" runat="server" Text="Jumpstation Ids:" SkinID="DataFieldLabel" AssociatedControlID="lblIdText"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdText" runat="server"></asp:Label>
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
				<asp:Label ID="lblName" runat="server" Text="Name:" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Name" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" Columns="100"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ValidationGroup="SaveJumpstationGroup"
                        ErrorMessage="Please enter a name.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName" ValidationGroup="SaveJumpstationGroup"
                        SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="200" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblURL" runat="server" Text="Target URL: " SkinID="DataFieldLabel"
                    AssociatedControlID="txtURL"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_URL" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtURL" runat="server" MaxLength="512" Columns="150"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblJumpstationApplication" runat="server" Text="Application: " SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationApplication"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_JumpstationApplication" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlJumpstationApplication" runat="server" AutoPostBack="true" >
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJumpstationGroupType" runat="server" Text="Jumpstation Type: "
                    SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationGroupType"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_JumpstationGroupType" runat="server" Enabled="false">
                    <asp:DropDownList ID="ddlJumpstationGroupType" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlJumpstationGroupType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvJumpstationGroupType" runat="server"
                        ControlToValidate="ddlJumpstationGroupType" ValidationGroup="SaveJumpstationGroup"
                        ErrorMessage="Please select a jumpstation type.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
			<td>
				<asp:Label ID="lblOwner" runat="server" Text="Owner: " SkinID="DataFieldLabel" AssociatedControlID="ddlOwner"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Owner" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlOwner" runat="server" Enabled="false">
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblJumpstationGroupStatus" runat="server" Text="Jumpstation Status: " SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationGroupStatus"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_JumpstationGropStatus" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlJumpstationGroupStatus" runat="server" Enabled="false">
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblTagsToAdd" runat="server" Text="Add Tags:" AssociatedControlID="txtTagsToAdd"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlDataControls_TagsToAdd" runat="server"
					Enabled='<%# this.IsMetadataModificationAllowed() %>'
				>
					<asp:TextBox ID="txtTagsToAdd" runat="server" Columns="100"></asp:TextBox>
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddInvalidTagNameChars" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names must begin wtih letter,and its string length is between 5 and 256 ,and contain only letters, numbers, comma,and underscores (ex. tag_01, tag_02)." 
						ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"
						 />
						 <%--    
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddTagNameFirstChar" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMinTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMaxTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						--%>
					<hpfx:CustomValidator ID="cvTxtTagsToAddValidateTagNames" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToAddValidateTagNames_ServerValidate"
						 />
					<hpfx:CustomValidator ID="cvTxtTagsToAddMaxTagCount" runat="server" ControlToValidate="txtTagsToAdd"
						Enabled="false"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Too many Tags. There is a limit of 100 Tags per group (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToAddMaxTagCount_ServerValidate"
						 />
					<asp:AutoCompleteExtender ID="ajaxextTxtTagsToAddAutoComplete" runat="server" SkinId="AutoComplete2"
						TargetControlID="txtTagsToAdd"
						ServiceMethod="GetTagNameCompletionList"
						DelimiterCharacters=","
						/>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblTagsToRemove" runat="server" Text="Remove Tags:" AssociatedControlID="txtTagsToRemove"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlDataControls_TagsToRemove" runat="server"
					Enabled='<%# this.IsMetadataModificationAllowed() %>'
				>
					<asp:TextBox ID="txtTagsToRemove" runat="server" Columns="100"></asp:TextBox>
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveInvalidTagNameChars" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"
						 />
						 <%-- 
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveTagNameFirstChar" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMinTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMaxTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsToRemoveValidateTagNames" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveJumpstationGroup" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToRemoveValidateTagNames_ServerValidate"
						 />
					<asp:AutoCompleteExtender ID="ajaxextTxtTagsToRemoveAutoComplete" runat="server" SkinId="AutoComplete3"
						TargetControlID="txtTagsToRemove"
						ServiceMethod="GetTagNameCompletionList"
						DelimiterCharacters=","
						/>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
			</td>
			<td>
				<hpfx:CustomValidator ID="cvJumpstationGroupPublishable" runat="server" 
					ValidationGroup="PublishableJumpstationGroup" 
					ErrorMessage="This jumpstation is not ready to be published. Please make sure that all data is complete and valid." 
					OnServerValidate="cvJumpstationGroupPublishable_ServerValidate"
					 />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="saveJumpstationGroup" />
				<asp:ValidationSummary ID="vsPublishableOfferValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="PublishableJumpstationGroup" />
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="saveJumpstationGroup" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
					ValidationGroup="saveJumpstationGroup" OnClick="btnSaveAsNew_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnAbandon" runat="server" Text="Abandon" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnAbandon_Click" />
				<asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the jumpstation validator(s)" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnReadyForValidation_Click" />
				<asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the jumpstation coordinator(s)" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnValidate_Click" />
				<asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the jumpstation owner for modification" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnRework_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnPublish" runat="server" Text="Publish" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnPublish_Click" />
				<asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" 
					CausesValidation="true" ValidationGroup="saveJumpstationGroup" OnClick="btnUnPublish_Click" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Panel ID="pnlJumpstationLog" runat="server" Visible="false">
					<ElementsCPSuc:JumpstationLog ID="ucJumpstationLog" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
