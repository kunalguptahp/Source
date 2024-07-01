<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowModuleUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.WorkflowModuleUpdatePanel" %>
<%@ Register Src="~/UserControls/WorkflowConditionEditListUpdatePanel.ascx" TagName="WorkflowConditionEditListUpdatePanel"
    TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/WorkflowListPanel.ascx" TagName="WorkflowListPanel"
    TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/WorkflowURLReportPanel.ascx" TagName="WorkflowURLReportPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Module Id:" SkinID="DataFieldLabel"
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
                    <asp:TextBox ID="txtName" runat="server" MaxLength="25"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a name.<br/>" />
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
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" MaxLength="512" Rows="2"
                        TextMode="MultiLine"></asp:TextBox>
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
                    <HPFx:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a title.<br/>" />
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
                    <asp:DropDownList ID="ddlModuleCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModuleCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvModuleCategory" runat="server" ControlToValidate="ddlModuleCategory"
                        ErrorMessage="Please select a Module Category.<br/>" />
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
                    <asp:DropDownList ID="ddlModuleSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModuleSubCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvModuleSubCategory" runat="server" ControlToValidate="ddlModuleSubCategory"
                        ErrorMessage="Please select a Module Sub-Category.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblVersionMajor" runat="server" Text="Major Version: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlVersionMajor"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_VersionMajor" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlVersionMajor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVersionMajor_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnIncrementVersionMajor" runat="server" CausesValidation="False"
                        OnClick="btnIncrementVersionMajor_Click" Text="+" Enabled='<%# this.IsDataModificationAllowed() %>' />
                    <a href="/ConfigurationService/WorkflowVersionReport.aspx" target="_blank">List</a>
                    <HPFx:RequiredFieldValidator ID="rfvVersionMajor" runat="server" ControlToValidate="ddlVersionMajor"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a major version.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblVersionMinor" runat="server" Text="Minor Version: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlVersionMinor"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_VersionMinor" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlVersionMinor" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                    <asp:Button ID="btnIncrementVersionMinor" runat="server" CausesValidation="False"
                        OnClick="btnIncrementVersionMinor_Click" Text="+" Enabled='<%# this.IsDataModificationAllowed() %>' />
                    <a href="/ConfigurationService/WorkflowVersionReport.aspx" target="_blank">List</a>
                    <HPFx:RequiredFieldValidator ID="rfvVersionMinor" runat="server" ControlToValidate="ddlVersionMinor"
                        ValidationGroup="saveWorkflow" ErrorMessage="Please enter a minor version.<br/>" />
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
                    <HPFx:RequiredFieldValidator ID="rfvFilename" runat="server" ControlToValidate="txtFilename"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a filename.<br/>" />
                    <HPFx:RegularExpressionValidator ID="revFilename" runat="server" ControlToValidate="txtFilename"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Please enter a valid filename (e.g. module.htm, module1.html)"
                        ValidationExpression="(.*?)\.(htm|html)$" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblValidationModuleUrl" runat="server" Text="Validation Module Url:"
                    SkinID="DataFieldLabel" AssociatedControlID="ucWorkflowURLReportValidationPanel"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_ValidationModuleUrl" runat="server">
                    <ElementsCPSuc:WorkflowURLReportPanel ID="ucWorkflowURLReportValidationPanel" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPublicationModuleUrl" runat="server" Text="Publication Module Url:"
                    SkinID="DataFieldLabel" AssociatedControlID="ucWorkflowURLReportPublicationPanel"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_PublicationModuleUrl" runat="server">
                    <ElementsCPSuc:WorkflowURLReportPanel ID="ucWorkflowURLReportPublicationPanel" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWorkflowConditionEditListUpdate" runat="server" Text="Conditions: "
                    SkinID="DataFieldLabel" AssociatedControlID="ucWorkflowConditionEditListUpdate"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_WorkflowConditionEditListUpdate" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <ElementsCPSuc:WorkflowConditionEditListUpdatePanel ID="ucWorkflowConditionEditListUpdate"
                        runat="server" PersistenceControlsVisible="True" />
                </asp:Panel>
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
                <asp:Label ID="lblTags" runat="server" Text="Tags:" AssociatedControlID="txtTags"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Tags" runat="server" Enabled='<%# this.IsMetadataModificationAllowed() %>'>
                    <asp:TextBox ID="txtTags" runat="server" Columns="100"></asp:TextBox>
                    <HPFx:RegularExpressionValidator ID="revTxtTagsInvalidTagNameChars" runat="server"
                        ControlToValidate="txtTags" ValidationGroup="saveWorkflowModule" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" />
                    <%-- 
                    <HPFx:RegularExpressionValidator ID="revTxtTagsTagNameFirstChar" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)."
                        ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMinTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMaxTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)" />
                        --%>
                    <HPFx:CustomValidator ID="cvTxtTagsValidateTagNames" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="saveWorkflowModule" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsValidateTagNames_ServerValidate" />
                    <HPFx:CustomValidator ID="cvTxtTagsMaxTagCount" runat="server" ControlToValidate="txtTags"
                        Enabled="false" ValidationGroup="saveWorkflowModule" ErrorMessage="Too many Tags. There is a limit of 100 Tags per module (ex. tag_01, tag_02)."
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
                <HPFx:CustomValidator ID="cvWorkflowModuleReplacement" runat="server" ValidationGroup="WorkflowModuleReplacement"
                    ErrorMessage="This workflow module is in the process of being replaced and therefore cannot be unpublished.<br>"
                    OnServerValidate="cvWorkflowModuleReplacement_ServerValidate" />
                <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="saveWorkflowModule" />
                <asp:ValidationSummary ID="vsPublishableWorkflowModuleValidationSummary" runat="server"
                    HeaderText="Invalid Input:" ValidationGroup="PublishableWorkflowModule" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"
                    ValidationGroup="saveWorkflowModule" />
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    SkinID="CancelChanges" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
                    ValidationGroup="saveWorkflowModule" OnClick="btnSaveAsNew_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnAbandon" runat="server" Text="Abandon" CausesValidation="true"
                    ValidationGroup="saveWorkflowModule" OnClick="btnAbandon_Click" />
                <asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the Module Validator(s)"
                    CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnReadyForValidation_Click" />
                <asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the Module Coordinator(s)"
                    CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnValidate_Click" />
                <asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the Module Owner for modification"
                    CausesValidation="true" ValidationGroup="saveWorkflowModule" OnClick="btnRework_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnPublish" runat="server" Text="Publish" CausesValidation="true"
                    ValidationGroup="saveWorkflowModule" OnClick="btnPublish_Click" />
                <asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" CausesValidation="true"
                    ValidationGroup="saveWorkflowModule" OnClick="btnUnPublish_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td colspan="2">
            <asp:Panel ID="pnlWorkflowListPanel" runat="server">
                <div class="ChildGridTitle">
                    Workflow(s) attached to Modules</div>
                <ElementsCPSuc:WorkflowListPanel ID="ucWorkflowList" runat="server" />
            </asp:Panel>
        </td>
    </tr>
</table>
