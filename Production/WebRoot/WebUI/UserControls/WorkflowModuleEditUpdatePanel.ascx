<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleEditUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleEditUpdatePanel" %>
<%@ Register Src="~/UserControls/CPSLogPanel.ascx" TagName="CPSLog"
	TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/WorkflowConditionEditListUpdatePanel.ascx"
    TagName="WorkflowConditionEditListUpdatePanel" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblId" runat="server" Text="Workflow Ids:" SkinID="DataFieldLabel" AssociatedControlID="lblIdText"></asp:Label>
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
                    <asp:TextBox ID="txtName" runat="server" MaxLength="25"></asp:TextBox>
                    <HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
                        SkinID="MinLength3Validator" ErrorMessage="Name is too short.</br>" />
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text="Title: *" SkinID="DataFieldLabel" AssociatedControlID="txtTitle"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Title" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="30"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModuleCategory" runat="server" Text="Module Category: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlModuleCategory"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ModuleCategory" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlModuleCategory" runat="server" AutoPostBack="true" >
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModuleSubCategory" runat="server" Text="Module Sub-Category: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlModuleSubCategory"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ModuleSubCategory" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlModuleSubCategory" runat="server" AutoPostBack="true" >
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFilename" runat="server" Text="Filename: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtFilename"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Filename" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtFilename" runat="server" MaxLength="256" Columns="100"></asp:TextBox>
                    <HPFx:RegularExpressionValidator ID="revFilename" runat="server" ControlToValidate="txtFilename"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a valid filename (e.g. module.htm, module1.html)"
                        ValidationExpression="(.*?)\.(htm|html)$" />
                </asp:Panel>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblWorkflowConditionEditListUpdate" runat="server" Text="Conditions: "
                    SkinID="DataFieldLabel" AssociatedControlID="ucWorkflowConditionEditListUpdate"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_WorkflowConditionEditListUpdate" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>' >
                    <ElementsCPSuc:WorkflowConditionEditListUpdatePanel ID="ucWorkflowConditionEditListUpdate" runat="server" />
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
                <asp:Label ID="lblWorkflowModuleStatus" runat="server" Text="Module Status: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlWorkflowModuleStatus"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Status" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlWorkflowModuleStatus" runat="server" Enabled="false">
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
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names must begin wtih letter,and its string length is between 5 and 256 ,and contain only letters, numbers, comma,and underscores (ex. tag_01, tag_02)." 
						ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"
						 />
						 <%-- 
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddTagNameFirstChar" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMinTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToAddMaxTagNameLength" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsToAddValidateTagNames" runat="server" ControlToValidate="txtTagsToAdd"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToAddValidateTagNames_ServerValidate"
						 />
					<hpfx:CustomValidator ID="cvTxtTagsToAddMaxTagCount" runat="server" ControlToValidate="txtTagsToAdd"
						Enabled="false"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Too many Tags. There is a limit of 100 Tags per workflow (ex. tag_01, tag_02)." 
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
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"
						 />
						 <%-- 
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveTagNameFirstChar" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMinTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsToRemoveMaxTagNameLength" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsToRemoveValidateTagNames" runat="server" ControlToValidate="txtTagsToRemove"
						ValidationGroup="saveWorkflow" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsToRemoveValidateTagNames_ServerValidate"
						 />
					<asp:AutoCompleteExtender ID="ajaxextTxtTagsToRemoveAutoComplete" runat="server" SkinId="AutoComplete3"
						TargetControlID="txtTagsToRemove"
						ServiceMethod="GetTagNameCompletionList"
						DelimiterCharacters="," />
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="saveWorkflow" />
				<asp:ValidationSummary ID="vsPublishableWorkflowModuleValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="PublishableWorkflowModule" />
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
				<br />
				<hpfx:CustomValidator ID="cvWorkflowModulePublishable" runat="server" 
					ValidationGroup="PublishableWorkflowModule" 
					ErrorMessage="This workflow module is not ready to be published. Please make sure that all offer data is complete and valid." 
					OnServerValidate="cvWorkflowModulePublishable_ServerValidate" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="saveWorkflowModule" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
					ValidationGroup="saveWorkflowModule" OnClick="btnSaveAsNew_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnAbandon" runat="server" Text="Abandon" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnAbandon_Click" />
				<asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the Workflow Validator(s)" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnReadyForValidation_Click" />
				<asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the Workflow Coordinator(s)" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnValidate_Click" />
				<asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the Workflow Owner for modification" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnRework_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnPublish" runat="server" Text="Publish" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnPublish_Click" />
				<asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" 
					CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnUnPublish_Click" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Panel ID="pnlCPSLog" runat="server" Visible="false">
					<ElementsCPSuc:CPSLog ID="ucCPSLog" runat="server" />
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Panel>
