<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DataAdminMaster.Master" AutoEventWireup="true" CodeBehind="ProxyURLGroupTypeUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ProxyURLGroupTypeUpdate"
	Title="CPS - New Domain" %>
<%@ Register Src="~/UserControls/ProxyURLGroupTypeUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
