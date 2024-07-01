<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/UserAdminMaster.master" AutoEventWireup="true" CodeBehind="PersonUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.PersonUpdate"
	Title="CPS - New User" %>
<%@ Register Src="~/UserControls/PersonUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
