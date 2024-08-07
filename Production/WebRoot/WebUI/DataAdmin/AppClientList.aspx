﻿<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DataAdminMaster.master" AutoEventWireup="true" CodeBehind="AppClientList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.AppClientList"
	Title="CPS - List App Clients" %>
<%@ Register Src="~/UserControls/AppClientListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>