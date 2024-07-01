<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupCopyPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupCopyPanel" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblId" runat="server" Text="Group Ids:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblIdText"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdText" runat="server"></asp:Label>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblName" runat="server" Text="Name: *" SkinID="DataFieldLabel" AssociatedControlID="txtName"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
				<HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
					ErrorMessage="Please enter a name.<br/>" />
				<HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
					SkinID="MinLength5Validator" ErrorMessage="Name is too short.</br>" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
			<td>
    	    	<asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="2" TextMode="MultiLine"></asp:TextBox>
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
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToAddInvalidTagNameChars" runat="server"
                        ControlToValidate="txtTagsToAdd" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names must begin wtih letter,and its string length is between 5 and 256 ,and contain only letters, numbers, comma,and underscores (ex. tag_01, tag_02)." 
						ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" />
						<%--
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToAddTagNameFirstChar" runat="server"
                        ControlToValidate="txtTagsToAdd" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)."
                        ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToAddMinTagNameLength" runat="server"
                        ControlToValidate="txtTagsToAdd" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToAddMaxTagNameLength" runat="server"
                        ControlToValidate="txtTagsToAdd" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)" />
                        --%>
                    <HPFx:CustomValidator ID="cvTxtTagsToAddValidateTagNames" runat="server" ControlToValidate="txtTagsToAdd"
                        ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Invalid Tag name(s)." OnServerValidate="cvTxtTagsToAddValidateTagNames_ServerValidate" />
                    <HPFx:CustomValidator ID="cvTxtTagsToAddMaxTagCount" runat="server" ControlToValidate="txtTagsToAdd"
                        Enabled="false" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Too many Tags. There is a limit of 100 Tags per redirector (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsToAddMaxTagCount_ServerValidate" />
                    <asp:AutoCompleteExtender ID="ajaxextTxtTagsToAddAutoComplete" runat="server" SkinID="AutoComplete2"
                        TargetControlID="txtTagsToAdd" ServiceMethod="GetTagNameCompletionList" DelimiterCharacters="," />
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
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToRemoveInvalidTagNameChars" runat="server"
                        ControlToValidate="txtTagsToRemove" ValidationGroup="saveConfigurationServiceGroup"ErrorMessage="Tag names must begin wtih letter,and its string length is between 5 and 256 ,and contain only letters, numbers, comma,and underscores (ex. tag_01, tag_02)." 
						ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" />
                       <%--  
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToRemoveTagNameFirstChar" runat="server"
                        ControlToValidate="txtTagsToRemove" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)."
                        ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToRemoveMinTagNameLength" runat="server"
                        ControlToValidate="txtTagsToRemove" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsToRemoveMaxTagNameLength" runat="server"
                        ControlToValidate="txtTagsToRemove" ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)" />
                        --%>
                    <HPFx:CustomValidator ID="cvTxtTagsToRemoveValidateTagNames" runat="server" ControlToValidate="txtTagsToRemove"
                        ValidationGroup="saveConfigurationServiceGroup" ErrorMessage="Invalid Tag name(s)." OnServerValidate="cvTxtTagsToRemoveValidateTagNames_ServerValidate" />
                    <asp:AutoCompleteExtender ID="ajaxextTxtTagsToRemoveAutoComplete" runat="server"
                        SkinID="AutoComplete3" TargetControlID="txtTagsToRemove" ServiceMethod="GetTagNameCompletionList"
                        DelimiterCharacters="," />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="saveConfigurationServiceGroup" />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"  ValidationGroup="saveConfigurationServiceGroup" />
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    SkinID="CancelChanges" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Panel ID="pnlCPSLog" runat="server">
                    <ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
