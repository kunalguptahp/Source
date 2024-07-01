<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.LogList"
	Title="CPS - Log List" %>
<%@ Register Src="~/UserControls/LogListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="logList" runat="server" />
	</asp:Panel>
</asp:Content>