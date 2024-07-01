<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DataAdminMaster.Master" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupTypeList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ConfigurationServiceGroupTypeList"
	Title="CPS - List Configuration Service Group Type" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupTypeListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>