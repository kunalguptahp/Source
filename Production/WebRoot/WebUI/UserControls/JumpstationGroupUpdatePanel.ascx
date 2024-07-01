<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="JumpstationGroupUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.JumpstationGroupUpdatePanel" %>
<%@ Register Src="~/UserControls/JumpstationGroupSelectorListPanel.ascx" TagName="JumpstationGroupSelectorListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server" DefaultButton="btnSave">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblIdLabel" runat="server" Text="Jumpstation Id:" SkinID="DataFieldLabel"
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
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" Columns="100"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please enter a name.<br/>" />
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
                    <asp:TextBox ID="txtDescription" runat="server" Columns="150" Rows="2" MaxLength="512"
                        TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTargetURL" runat="server" Text="Target URL: *" SkinID="DataFieldLabel"
                    AssociatedControlID="txtTargetURL"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_URL" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtTargetURL" runat="server" Columns="150" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvURL" runat="server" ControlToValidate="txtTargetURL"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please enter a target URL.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOrder" runat="server" Text="Order *" SkinID="DataFieldLabel" AssociatedControlID="txtOrder"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlOrder" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:TextBox ID="txtOrder" runat="server" SkinID="IntValidator" Columns="5"></asp:TextBox>
                    <HPFx:RequiredFieldValidator ID="rfvOrder" runat="server" ControlToValidate="txtOrder"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please enter an Order Value.<br/>" />
                    <HPFx:RegularExpressionValidator ID="rgxOrder" ControlToValidate="txtOrder" runat="server"
                        SkinID="IntValidator" ErrorMessage="Must be an integer." ValidationGroup="SaveJumpstationGroup"
                        ValidationExpression="^\d*$" />
                    <asp:RangeValidator ID="rvOrder" runat="server" ErrorMessage="Please enter a valid integer (0 - 2147483647)."
                        MinimumValue="0" MaximumValue="2147483647" ControlToValidate="txtOrder" ValidationGroup="SaveJumpstationGroup"
                        Type="Integer">
                    </asp:RangeValidator>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJumpstationApplication" runat="server" Text="Application: *" SkinID="DataFieldLabel"
                    AssociatedControlID="ddlJumpstationApplication"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_JumpstationApplication" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlJumpstationApplication" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlJumpstationApplication_SelectedIndexChanged">
                    </asp:DropDownList>
                    <HPFx:RequiredFieldValidator ID="rfvJumpstationApplication" runat="server" ControlToValidate="ddlJumpstationApplication"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please select a application.<br/>" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJumpstationGroupType" runat="server" Text="Jumpstation Type: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationGroupType"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_JumpstationGroupType" runat="server" Enabled='<%# this.IsDataModificationAllowed() && !this.HasGroupSelector() %>'>
                    <asp:DropDownList ID="ddlJumpstationGroupType" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlJumpstationGroupType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblJumpstationTypeHint" runat="server" Text="Note: To change type, you must delete all selectors."
                        SkinID="DataFieldLabel" Visible='<%# this.HasGroupSelector() %>'></asp:Label>
                    <HPFx:RequiredFieldValidator ID="rfvJumpstationGroupType" runat="server" ControlToValidate="ddlJumpstationGroupType"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please select a jumpstation type.<br/>" />
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
                <asp:Label ID="lblJumpstationGroupStatus" runat="server" Text="Jumpstation Status: *"
                    SkinID="DataFieldLabel" AssociatedControlID="ddlJumpstationGroupStatus"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlDataControls_Status" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                    <asp:DropDownList ID="ddlJumpstationGroupStatus" runat="server" Enabled="false">
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
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Please select a App Client.<br/>" />
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
                        ControlToValidate="txtTags" ValidationGroup="SaveJumpstationGroup" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|([ ]*,[ ]*[\S]*)|([_])){4,255}$" />
                    <%--  <HPFx:RegularExpressionValidator ID="revTxtTagsTagNameFirstChar" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Tag names must begin with a letter (ex. tag_01, tag_02)."
                        
                        ValidationExpression="(\s*)[a-zA-Z][^,\s]*((\s*[,\s]\s*)[a-zA-Z][^,\s]*)*(\s*)" 
                        Display="Dynamic" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMinTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Tag names shorter than 5 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{5,})((\W+)([^,\s]{5,}))*)?(\W*)" />
                    <HPFx:RegularExpressionValidator ID="revTxtTagsMaxTagNameLength" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Tag names longer than 256 characters are not allowed (ex. tag_01, tag_02)."
                        ValidationExpression="(\W*)(([^,\s]{1,256})((\W+)([^,\s]{1,256}))*)?(\W*)" 
                        Display="Dynamic" />  --%>
                    <HPFx:CustomValidator ID="cvTxtTagsValidateTagNames" runat="server" ControlToValidate="txtTags"
                        ValidationGroup="SaveJumpstationGroup" ErrorMessage="Every tag's name must begin wtih letter,and its string length is between 5 and 256 ,and contains only letters, numbers, and underscores (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsValidateTagNames_ServerValidate"  />
                    <HPFx:CustomValidator ID="cvTxtTagsMaxTagCount" runat="server" ControlToValidate="txtTags"
                        Enabled="false" ValidationGroup="SaveJumpstationGroup" ErrorMessage="Too many Tags. There is a limit of 100 Tags per group (ex. tag_01, tag_02)."
                        OnServerValidate="cvTxtTagsMaxTagCount_ServerValidate"  />
                    <asp:AutoCompleteExtender ID="ajaxextTxtTagsAutoComplete" runat="server" SkinID="AutoComplete2"
                        TargetControlID="txtTags" ServiceMethod="GetTagNameCompletionList" DelimiterCharacters="," />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
                <HPFx:CustomValidator ID="cvJumpstationGroupReplacement" runat="server" ValidationGroup="JumpstationGroupReplacement"
                    ErrorMessage="This jumpstation is in the process of being replaced and therefore cannot be unpublished.<br>"
                    OnServerValidate="cvJumpstationGroupReplacement_ServerValidate" />
                <HPFx:CustomValidator ID="cvJumpstationGroupPublishable" runat="server" ValidationGroup="PublishableJumpstationGroup"
                    ErrorMessage="This jumpstation is not ready to be published. Please make sure there is at least one group selector.<br>"
                    OnServerValidate="cvJumpstationGroupPublishable_ServerValidate" />
                <asp:ValidationSummary ID="vsDefaultValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="SaveJumpstationGroup" />
                <asp:ValidationSummary ID="vsPublishableOfferValidationSummary" runat="server" HeaderText="Invalid Input:"
                    ValidationGroup="PublishableJumpstationGroup" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" SkinID="DeleteItem" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="SaveChanges"
                    ValidationGroup="SaveJumpstationGroup" />
                <%= Global.CreateHtml_ResetFormButton("btnUndoAllChanges") %>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    SkinID="CancelChanges" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSaveAsNew" runat="server" SkinID="SaveChanges" Text="Save As New"
                    ValidationGroup="SaveJumpstationGroup" OnClick="btnSaveAsNew_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnAbandon" runat="server" Text="Abandon" CausesValidation="true"
                    ValidationGroup="SaveJumpstationGroup" OnClick="btnAbandon_Click" />
                <asp:Button ID="btnReadyForValidation" runat="server" Text="Request Validation" ToolTip="Submit to the jumpstation validator(s)"
                    CausesValidation="true" ValidationGroup="SaveJumpstationGroup" OnClick="btnReadyForValidation_Click" />
                <asp:Button ID="btnValidate" runat="server" Text="Validate" ToolTip="Submit to the jumpstation coordinator(s)"
                    CausesValidation="true" ValidationGroup="SaveJumpstationGroup" OnClick="btnValidate_Click" />
                <asp:Button ID="btnRework" runat="server" Text="Rework" ToolTip="Return to the jumpstation owner for modification"
                    CausesValidation="true" ValidationGroup="SaveJumpstationGroup" OnClick="btnRework_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnPublish" runat="server" Text="Publish" CausesValidation="true"
                    ValidationGroup="SaveJumpstationGroup" OnClick="btnPublish_Click" />
                <asp:Button ID="btnUnPublish" runat="server" Text="UnPublish" CausesValidation="true"
                    ValidationGroup="SaveJumpstationGroup" OnClick="btnUnPublish_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td>
            <asp:Panel ID="pnlJumpstationGroupSelectorListPanel" runat="server" Enabled='<%# this.IsDataModificationAllowed() %>'>
                <div class="ChildGridTitle">
                    List Jumpstation Selector</div>
                <ElementsCPSuc:JumpstationGroupSelectorListPanel ID="ucJumpstationGroupSelectorList"
                    runat="server" />
            </asp:Panel>
        </td>
    </tr>
</table>
