<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/ConfigurationServiceMaster.Master" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ConfigurationServiceGroupList"
	Title="CPS - List Configuration Service Group" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>