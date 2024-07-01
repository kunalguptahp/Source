<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/UserAdminMaster.master" AutoEventWireup="true" CodeBehind="PersonDetail.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.PersonDetail"
	Title="CPS - Person Detail" %>
<%@ Register Src="~/UserControls/PersonDetailPanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
