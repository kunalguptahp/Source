<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/RedirectMaster.Master" AutoEventWireup="true" CodeBehind="ProxyURLList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ProxyURLList"
	Title="CPS - List Redirector" %>
<%@ Register Src="~/UserControls/ProxyURLListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>