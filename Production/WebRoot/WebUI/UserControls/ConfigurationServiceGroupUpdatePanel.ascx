<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupUpdatePanel" %>
<%@ Register Src="~/UserControls/ConfigurationServiceLabelValueUpdatePanel.ascx"
    TagName="ConfigurationServiceLabelValueUpdatePanel" TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupSelectorListPanel.ascx"
    TagName="ConfigurationServiceGroupSelectorListPanel" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Group Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblValidationId" runat="server" Text="Validation Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblValidationIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValidationIdValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPublicationId" runat="server" Text="Publication Id:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblPublicationIdValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPublicationIdValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCreatedByValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedOnValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedOnValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedOnValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblCreatedByValue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByValue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel"
                    AssociatedControlID="lblModifiedByValue"></asp:Label>
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
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Please enter a name.<br/>" />
    				<HPFx:RegularExpressionValidator ID="revNameMinLength" runat="server" ControlToValidate="txtName"
	    				SkinID="MinLength3Validator" ErrorMessage="Name is too short.</br>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDescription" runat="server" Text="Description:" SkinID="DataFieldLabel"
                    AssociatedControlID="txtDescription"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Description" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="2" MaxLength="512" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceApplication" runat="server" Text="Application: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlConfigurationServiceApplication"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ConfigurationServiceApplication" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlConfigurationServiceApplication" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConfigurationServiceApplication_SelectedIndexChanged">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvConfigurationServiceApplication" runat="server"
                        ControlToValidate="ddlConfigurationServiceApplication" ValidationGroup="SaveConfigurationServiceGroup"
                        ErrorMessage="Please select a application.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceGroupType" runat="server" Text="Group Type: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlConfigurationServiceGroupType"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ConfigurationServiceGroupType" runat="server" Enabled='<%# this.IsDataModificationAllowed() && !this.HasGroupSelector() %>'>
                    <asp:DropDownList ID="ddlConfigurationServiceGroupType" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlConfigurationServiceGroupType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblConfigurationServiceGroupTypeHint" runat="server" Text="Note: To change type, you must delete all selector groups."
                        SkinID="DataFieldLabel" Visible='<%# this.HasGroupSelector() %>'></asp:Label>
                    <HPFx:RequiredFieldValidator ID="rfvConfigurationServiceGroupType" runat="server"
                        ControlToValidate="ddlConfigurationServiceGroupType" ValidationGroup="SaveConfigurationServiceGroup"
                        ErrorMessage="Please select a group type.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfigurationServiceLabelValue" runat="server" Text="Values: "
                    SkinID="DataFieldLabel" AssociatedControlID="ucConfigurationServiceLabelValue"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ConfigurationServiceLabelValue" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <ElementsCPSuc:ConfigurationServiceLabelValueUpdatePanel ID="ucConfigurationServiceLabelValue" IsMultipleUpdate="false"
                        runat="server" />
                </asp:Panel>
                <HPFx:CustomValidator ID="cvQueryParameterValueRequired" runat="server" ErrorMessage="Please select or enter required label value(s)."
                    ValidationGroup="SaveConfigurationServiceGroup" OnServerValidate="cvLabelValueRequired_ServerValidate" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOwner" runat="server" Text="Owner: *" SkinID="DataFieldLabel" AssociatedControlID="ddlOwner"></asp:Label>
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
                <asp:Label ID="lblConfigurationServiceGroupStatus" runat="server" Text="Group Status: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlConfigurationServiceGroupStatus"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Status" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlConfigurationServiceGroupStatus" runat="server" Enabled="false">
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        
        <tr>
        <td>
        <asp:Label ID="lblAppClient" runat="Server" Text="App Client: *" SkinID="DataFieldLabel" AssociatedControlID="ddlAppClient"></asp:Label>
        </td>
        <td>
        <asp:Panel ID="pnlDataControls_AppClient" runat="Server" Enabled='<%# this.IsDataModificationAllowed() %>'>
        <asp:DropDownList  ID="ddlAppClient" runat="server" AutoPostBack="true"  ></asp:DropDownList>
       <HPFx:RequiredFieldValidator ID="rfvAppClient" runat="server" ControlToValidate="ddlAppClient"
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Please select a App Client.<br/>" />
        </asp:Panel>
        </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblTags" runat="server" Text="Tags:" AssociatedControlID="txtTags"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Tags" runat="server" Enabled='<%# this.IsMetadataModificationAllowed() %>'>
                    <asp:TextBox ID="txtTags" runat="server" Columns="100"></asp:TextBox>
                    <HPFx:RegularExpressionValidator ID="revTxtTagsInvalidTagNameChars" runat="server"
                        ControlToValidate="txtTags" ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$"  />
                    <%-- 
                   <HPFx:RegularExpressionValidator ID="revTxtTagsTagNameFirstChar" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)."
                        ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMinTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMaxTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)" />
                        --%>
                    <HPFx:CustomValidator ID="cvTxtTagsValidateTagNames" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsValidateTagNames_ServerValidate" />
                    <HPFx:CustomValidator ID="cvTxtTagsMaxTagCount" runat="server" ControlToValidate="txtTags"
                        Enabled="false" ValidationGroup="SaveConfigurationServiceGroup" ErrorMessage="Too many Tags. There is a limit of 100 Tags per group (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsMaxTagCount_ServerValidate" />
                    <asp:AutoCompleteExtender ID="ajaxextTxtTagsAutoComplete" runat="server" SkinID="AutoComplete2"
                        TargetControlID="txtTags" ServiceMethod="GetTagNameCompletionList" DelimiterCharacters="," />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
                <HPFx:CustomValidator ID="cvConfigurationServiceGroupReplacement" runat="server"
                    ValidationGroup="ConfigurationServiceGroupReplacement" ErrorMessage="This group is in the process of being replaced and therefore cannot be unpublished.<br>"
                    OnServerValidate="cvConfigurationServiceGroupReplacement_ServerValidate" />
                <HPFx:CustomValidator ID="cvConfigurationServiceGroupPublishable" runat="server"
                    ValidationGroup="PublishableConfigurationServiceGroup" ErrorMessage="This group is not ready to be published. Please make sure there is at least one group selector.<br>"
                    OnServerValidate="cvConfigurationServiceGroupPublishable_ServerValidate" />
                <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="SaveConfigurationServiceGroup" />
                <asp:ValidationSummary ID="vsPublishableOfferValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="PublishableConfigurationServiceGroup" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"
                    ValidationGroup="SaveConfigurationServiceGroup" />
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    SkinID="CancelChanges" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
                    ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnSaveAsNew_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnAbandon" runat="server" Text="Abandon" CausesValidation="true"
                    ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnAbandon_Click" />
                <asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the Configuration Service Group Validator(s)"
                    CausesValidation="true" ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnReadyForValidation_Click" />
                <asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the Configuration Service Group Coordinator(s)"
                    CausesValidation="true" ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnValidate_Click" />
                <asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the Configuration Service Group Owner for modification"
                    CausesValidation="true" ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnRework_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnPublish" runat="server" Text="Publish" CausesValidation="true"
                    ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnPublish_Click" />
                <asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" CausesValidation="true"
                    ValidationGroup="SaveConfigurationServiceGroup" OnClick="btnUnPublish_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td>
            <asp:Panel ID="pnlConfigurationServiceGroupSelectorListPanel" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                <div class="ChildGridTitle">
                    List Configuration Service Selector Group</div>
                <ElementsCPSuc:ConfigurationServiceGroupSelectorListPanel ID="ucConfigurationServiceGroupSelectorList"
                    runat="server" />
            </asp:Panel>
        </td>
    </tr>
</table>
