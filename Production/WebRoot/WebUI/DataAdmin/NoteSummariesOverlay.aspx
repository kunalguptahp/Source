<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>

<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DefaultOverlayMaster.master"
	AutoEventWireup="true" CodeBehind="NoteSummariesOverlay.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.NoteSummariesOverlay"
	Title="CPS - List Notes" %>
<%@ MasterType TypeName="HP.ElementsCPS.Apps.WebUI.MasterPages.DefaultOverlayMaster" %>

<%@ Register Src="~/UserControls/NoteSummariesListPanel.ascx" TagName="ListPanel" TagPrefix="ElementsCPSuc" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>
