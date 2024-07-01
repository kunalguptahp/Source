<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="DataImport.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.DataImport"
	Title="CPS - Data Import" %>
<%@ MasterType TypeName="HP.ElementsCPS.Apps.WebUI.MasterPages.SystemAdminMaster" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<h1 class="SecurityNotice">NOTE: Do NOT use the browser's Refresh button on this page.</h1>

		<h4>Select a file to upload:</h4>

		<asp:FileUpload ID="fuDataFile" runat="server"></asp:FileUpload>

		<br />
		<br />
		<h4 style="color:Red;">NOTE: You will need to re-select the file before each action (e.g. view, preview, import):</h4>

		<br />
		Table: <asp:DropDownList ID="ddlEntity" runat="server">
			<asp:ListItem Value="" Text="-- Please Select --" />
			<asp:ListItem Value="ConfigurationServiceApplication" Text="ConfigurationServiceApplication" />
			<asp:ListItem Value="ConfigurationServiceGroup" Text="ConfigurationServiceGroup" />
			<asp:ListItem Value="ConfigurationServiceGroupConfigurationServiceLabelValue" Text="ConfigurationServiceGroupConfigurationServiceLabelValue" />
			<asp:ListItem Value="ConfigurationServiceGroupSelector" Text="ConfigurationServiceGroupSelector" />
			<asp:ListItem Value="ConfigurationServiceGroupSelectorQueryParameterValue" Text="ConfigurationServiceGroupSelectorQueryParameterValue" />
			<asp:ListItem Value="ConfigurationServiceGroupStatus" Text="ConfigurationServiceGroupStatus" />
			<asp:ListItem Value="ConfigurationServiceGroupTag" Text="ConfigurationServiceGroupTag" />
			<asp:ListItem Value="ConfigurationServiceGroupType" Text="ConfigurationServiceGroupType" />
			<asp:ListItem Value="ConfigurationServiceItem" Text="ConfigurationServiceItem" />
			<asp:ListItem Value="ConfigurationServiceLabel" Text="ConfigurationServiceLabel" />
			<asp:ListItem Value="ConfigurationServiceLabelValue" Text="ConfigurationServiceLabelValue" />
			<asp:ListItem Value="JumpstationApplication" Text="JumpstationApplication" />
			<asp:ListItem Value="JumpstationGroup" Text="JumpstationGroup" />
			<asp:ListItem Value="JumpstationGroupSelector" Text="JumpstationGroupSelector" />
			<asp:ListItem Value="JumpstationGroupSelectorQueryParameterValue" Text="JumpstationGroupSelectorQueryParameterValue" />
			<asp:ListItem Value="JumpstationGroupStatus" Text="JumpstationGroupStatus" />
			<asp:ListItem Value="JumpstationGroupTag" Text="JumpstationGroupTag" />
			<asp:ListItem Value="JumpstationGroupType" Text="JumpstationGroupType" />
			<asp:ListItem Value="JumpstationMacro" Text="JumpstationMacro" />
			<asp:ListItem Value="EntityType" Text="EntityType" />
			<%--<asp:ListItem Value="Log" Text="Log" />--%>
			<asp:ListItem Value="Note" Text="Note" />
			<asp:ListItem Value="NoteType" Text="NoteType" />
			<asp:ListItem Value="Person" Text="Person" />
			<asp:ListItem Value="PersonRole" Text="PersonRole" />
			<asp:ListItem Value="ProxyURL" Text="ProxyURL" />
			<asp:ListItem Value="ProxyURLDomain" Text="ProxyURLDomain" />
			<asp:ListItem Value="ProxyURLGroupType" Text="ProxyURLGroupType" />
			<asp:ListItem Value="ProxyURLQueryParameterValue" Text="ProxyURLQueryParameterValue" />
			<asp:ListItem Value="ProxyURLStatus" Text="ProxyURLStatus" />
			<asp:ListItem Value="ProxyURLTag" Text="ProxyURLTag" />
			<asp:ListItem Value="ProxyURLType" Text="ProxyURLType" />
			<asp:ListItem Value="PublishTemp" Text="PublishTemp" />
			<asp:ListItem Value="QueryParameter" Text="QueryParameter" />
			<asp:ListItem Value="QueryParameterConfigurationServiceGroupType" Text="QueryParameterConfigurationServiceGroupType" />
			<asp:ListItem Value="QueryParameterProxyURLType" Text="QueryParameterProxyURLType" />
			<asp:ListItem Value="QueryParameterValue" Text="QueryParameterValue" />
			<asp:ListItem Value="Role" Text="Role" />
			<asp:ListItem Value="RowStatus" Text="RowStatus" />
			<asp:ListItem Value="Tag" Text="Tag" />
		</asp:DropDownList>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:Button ID="btnImport" Text="Import data" OnClick="btnImport_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<asp:Button ID="btnPreview" Text="Preview data" OnClick="btnPreview_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<asp:Button ID="btnViewFileContent" Text="View raw data" OnClick="btnViewFileContent_Click" runat="server"
			CausesValidation="true"
		>
		</asp:Button>
		<br />
		<HPFx:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="ddlEntity"
			ErrorMessage="Please select a table to import the data into.<br/>" />
		<HPFx:CustomValidator ID="cvDataFileRequired" runat="server" ErrorMessage="Please select a data file."
			OnServerValidate="cvDataFileRequired_ServerValidate" />
		<HPFx:CustomValidator ID="cvDataFileValid" runat="server" ErrorMessage="Invalid data file."
			OnServerValidate="cvDataFileValid_ServerValidate" />

		<hr />
		<asp:Label ID="lblUploadStatus" runat="server" Font-Size="X-Large">
		</asp:Label>

		<br />
		<asp:TextBox ID="txtFileContent" runat="server" TextMode="MultiLine" Rows="20" Columns="250" Wrap="false" Visible="false">
		</asp:TextBox>
		<asp:GridView ID="gvFileData" runat="server" 
			AutoGenerateColumns="true" AllowPaging="false" AllowSorting="false" ShowHeader="true"
		>
			<EmptyDataTemplate>
				<asp:Label ID="lblEmptyData" runat="server" Text="Label">There is no data to display.</asp:Label>
			</EmptyDataTemplate>
		</asp:GridView>
	</div>
</asp:Content>
