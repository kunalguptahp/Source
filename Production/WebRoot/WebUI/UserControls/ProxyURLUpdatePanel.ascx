<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProxyURLUpdatePanel.ascx.cs"
	Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ProxyURLUpdatePanel" %>
<%@ Register Src="~/UserControls/QueryParameterValueEditUpdatePanel.ascx" TagName="QueryParameterValueEditUpdatePanel"
	TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Redirector Id:" SkinID="DataFieldLabel" AssociatedControlID="lblIdValue"></asp:Label>
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
				<asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel" AssociatedControlID="txtDescription"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="2" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
		<tr>
			<td>
				<asp:Label ID="lblURL" runat="server" Text="Target URL: *" SkinID="DataFieldLabel" AssociatedControlID="txtURL"></asp:Label>
			</td>
            <td>
                <asp:Panel ID="pnlDataControls_URL" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtURL" runat="server" MaxLength="512" Columns="150"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvURL" runat="server" ControlToValidate="txtURL"
                        ValidationGroup="SaveProxyURL" ErrorMessage="Please enter a target URL.<br/>" />
                    <%--<HPFx:CustomValidator ID="cvURL" runat="server" ControlToValidate="txtURL" ValidationGroup="SaveProxyURL"
                        ErrorMessage="Please enter a valid URL.</br>" OnServerValidate="cvURL_ServerValidate" />--%>
                </asp:Panel>
            </td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProxyURLType" runat="server" Text="Redirector Type: *" SkinID="DataFieldLabel" AssociatedControlID="ddlProxyURLType"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlDataControls_ProxyURLType" runat="server"
					Enabled='<%# this.IsDataModificationAllowed() %>'>
				<asp:DropDownList ID="ddlProxyURLType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProxyURLType_SelectedIndexChanged">
				</asp:DropDownList>
				<HPFx:RequiredFieldValidator ID="rfvProxyURLType" runat="server" ControlToValidate="ddlProxyURLType"
					ValidationGroup="SaveProxyURL" 
					ErrorMessage="Please select a Redirector type.<br/>" />
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblParameter" runat="server" Text="Parameters:" SkinID="DataFieldLabel"></asp:Label>
			</td>
			<td>
                <asp:Panel ID="pnlQueryParameterValueEditUpdate" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <ElementsCPSuc:QueryParameterValueEditUpdatePanel ID="ucQueryParameterValueEditUpdatePanel"
                        runat="server"  Enabled='<%# this.IsDataModificationAllowed() %>'/>
                <HPFx:CustomValidator ID="cvQueryParameterValueRequired" runat="server" ErrorMessage="Please select parameter value(s)."
                    ValidationGroup="SaveProxyURL" OnServerValidate="cvQueryParameterValueRequired_ServerValidate" />
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
				<asp:Label ID="lblProxyURLStatus" runat="server" Text="Redirector Status: *" SkinID="DataFieldLabel" AssociatedControlID="ddlProxyURLStatus"></asp:Label>
			</td>
			<td>
    			<asp:Panel ID="pnlDataControls_Status" runat="server"
				Enabled='<%# this.IsDataModificationAllowed() %>'>
				<asp:DropDownList ID="ddlProxyURLStatus" runat="server" Enabled="false">
				</asp:DropDownList>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblTags" runat="server" Text="Tags:" AssociatedControlID="txtTags"></asp:Label>
			</td>
			<td>
				<asp:Panel ID="pnlDataControls_Tags" runat="server"
					Enabled='<%# this.IsMetadataModificationAllowed() %>'>					
						<asp:TextBox ID="txtTags" runat="server" Columns="100"></asp:TextBox>
					<hpfx:RegularExpressionValidator ID="revTxtTagsInvalidTagNameChars" runat="server" ControlToValidate="txtTags"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"   />
                        <%-- 
					<hpfx:RegularExpressionValidator ID="revTxtTagsTagNameFirstChar" runat="server" ControlToValidate="txtTags"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)." 
						ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsMinTagNameLength" runat="server" ControlToValidate="txtTags"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)"
						 />
					<hpfx:RegularExpressionValidator ID="revTxtTagsMaxTagNameLength" runat="server" ControlToValidate="txtTags"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)." 
						ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)"
						 />
						 --%>
					<hpfx:CustomValidator ID="cvTxtTagsValidateTagNames" runat="server" ControlToValidate="txtTags"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsValidateTagNames_ServerValidate"
						 />
					<hpfx:CustomValidator ID="cvTxtTagsMaxTagCount" runat="server" ControlToValidate="txtTags"
						Enabled="false"
						ValidationGroup="SaveProxyURL" 
						ErrorMessage="Too many Tags. There is a limit of 100 Tags per redirector (ex. tag_01, tag_02)." 
						OnServerValidate="cvTxtTagsMaxTagCount_ServerValidate"
						 />
					<asp:AutoCompleteExtender ID="ajaxextTxtTagsAutoComplete" runat="server" SkinId="AutoComplete2"
						TargetControlID="txtTags"
						ServiceMethod="GetTagNameCompletionList"
						DelimiterCharacters=","
						/>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
				<asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
				<hpfx:CustomValidator ID="cvProxyURLPublishable" runat="server" 
					ValidationGroup="PublishableProxyURL" 
					ErrorMessage="This redirector is not ready to be published. Please make sure that all redirector data is complete and valid.<br>" 
					OnServerValidate="cvProxyURLPublishable_ServerValidate"
					 />
				<hpfx:CustomValidator ID="cvProxyURLDuplidate" runat="server" 
					ValidationGroup="DuplicateProxyURL" 
					ErrorMessage="Redirector cannot move to ready to validate when another redirector is already ready to validate, validated or published with the same parameters.<br>" 
					OnServerValidate="cvProxyURLDuplicate_ServerValidate"
					 />
				<hpfx:CustomValidator ID="cvProxyURLReplacement" runat="server" 
					ValidationGroup="ReplacementProxyURL" 
					ErrorMessage="Redirector cannot be published or unpublished because another redirector is replacing this one.<br>" 
					OnServerValidate="cvProxyURLReplacement_ServerValidate"
					 />
				<asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="SaveProxyURL" />
				<asp:ValidationSummary ID="vsPublishableOfferValidationSummary" runat="server" HeaderText="Invalid Input:" ValidationGroup="PublishableProxyURL" />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2" >
				<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges" ValidationGroup="SaveProxyURL" />
				<%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
				<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
					SkinID="CancelChanges" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
					ValidationGroup="SaveProxyURL" OnClick="btnSaveAsNew_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnAbandon" runat="server" Text="Abandon" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnAbandon_Click" />
				<asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the Redirector Validator(s)" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnReadyForValidation_Click" />
				<asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the Redirector Coordinator(s)" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnValidate_Click" />
				<asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the Redirector Owner for modification" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnRework_Click" />
				&nbsp;
				&nbsp;
				&nbsp;
				<asp:Button ID="btnPublish" runat="server" Text="Publish" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnPublish_Click" />
				<asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" 
					CausesValidation="true" ValidationGroup="SaveProxyURL" OnClick="btnUnPublish_Click" />
			</td>
		</tr>
	</table>
</asp:Panel>
