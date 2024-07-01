<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DefaultOverlayMaster.master" AutoEventWireup="true" CodeBehind="NoteSummaryOverlay.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.NoteSummaryOverlay"
	Title="CPS - Note Summary" %>
<%@ Register Src="~/UserControls/NoteSummaryDetailPanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
