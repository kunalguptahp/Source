<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/UserAdminMaster.master" AutoEventWireup="true" CodeBehind="PersonList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.PersonList"
	Title="CPS - List Users" %>
<%@ Register Src="~/UserControls/PersonListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>
