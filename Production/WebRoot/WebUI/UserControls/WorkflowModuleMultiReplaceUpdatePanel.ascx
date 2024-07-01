<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleMultiReplaceUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleMultiReplaceUpdatePanel" %>
<%@ Register Src="~/UserControls/WorkflowModuleDescriptionMultiReplaceUpdatePanel.ascx" TagName="WorkflowModuleDescriptionMultiReplaceUpdatePanel"
	TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblWorkflows" runat="server" Text="Modules:" SkinID="DataFieldLabel"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlQueryParameterValueMultiUpdatePanel" runat="server">
					<ElementsCPSuc:WorkflowModuleDescriptionMultiReplaceUpdatePanel ID="ucWorkflowModuleDescriptionMultiReplaceUpdate" runat="server" />
				</asp:Panel>
            </td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblOwner" runat="server" Text="Owner: " SkinID="DataFieldLabel" AssociatedControlID="ddlOwner"></asp:Label>
			</td>
			<td>
				<asp:DropDownList ID="ddlOwner" runat="server" Enabled="false">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvOwner" runat="server" ControlToValidate="ddlOwner"
					ErrorMessage="Please select an Owner.<br/>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblTagsToAdd" runat="server" Text="Add Tags:" AssociatedControlID="txtTagsToAdd"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlDataControls_TagsToAdd" runat="server">
					<asp:TextBox ID="txtTagsToAdd" runat="server" Columns="100"></asp:TextBox>
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddInvalidTagNameChars" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="SaveWorkflowModule" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" 
						 />
						 <%-- 
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddTagNameFirstChar" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="SaveWorkflowModule" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMinTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="SaveWorkflowModule" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMaxTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="SaveWorkflowModule" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsToAddValidateTagNames" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="SaveWorkflowModule" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToAddValidateTagNames_ServerValidate"
						 />
					<hpfx:CustomValidator ID="cvTxtTagsToAddMaxTagCount" runat="server" ControlToValidate="txtTagsToAdd"
						Enabled="false"
						ValidationGroup="SaveWorkflowModule" 
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
				<asp:Panel ID="pnlDataControls_TagsToRemove" runat="server">
					<asp:TextBox ID="txtTagsToRemove" runat="server" Columns="100"></asp:TextBox>
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveInvalidTagNameChars" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="SaveWorkflow" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" 
						 />
						 <%--
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveTagNameFirstChar" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="SaveWorkflow" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMinTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="SaveWorkflow" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMaxTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="SaveWorkflow" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsToRemoveValidateTagNames" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="SaveWorkflow" 
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
			<td colspan="2">
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveWorkflowModule" />
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"  ValidationGroup="SaveWorkflowModule" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Panel ID="pnlCPSLog" runat="server">
					<ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
