﻿<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/MyInfoMaster.Master" AutoEventWireup="true" CodeBehind="PersonDetailForMyInfo.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.PersonDetailForMyInfo"
	Title="CPS - My Info" %>
<%@ Register Src="~/UserControls/PersonDetailPanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlDetail" runat="server">
		<ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" DataSourceId='<%# HP.ElementsCPS.Data.SubSonicClient.PersonController.GetCurrentUserId() %>' />
	</asp:Panel>
</asp:Content>