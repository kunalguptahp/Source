<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/ConfigurationServiceMaster.Master" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupEditUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ConfigurationServiceGroupEditUpdate"
	Title="CPS - Multi-Edit Configuration Service Group" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupEditUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
