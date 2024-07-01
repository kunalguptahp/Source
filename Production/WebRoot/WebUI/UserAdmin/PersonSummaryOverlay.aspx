<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DefaultOverlayMaster.master" AutoEventWireup="true" CodeBehind="PersonSummaryOverlay.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.PersonSummaryOverlay"
	Title="CPS - Person Summary" %>
<%@ Register Src="~/UserControls/PersonSummaryDetailPanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
