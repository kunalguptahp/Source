<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/RedirectMaster.Master" AutoEventWireup="true" CodeBehind="ProxyURLMultiEditUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ProxyURLMultiEditUpdate"
	Title="CPS - Multi Update Redirector" %>
<%@ Register Src="~/UserControls/ProxyURLMultiEditUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
