<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DataAdminMaster.master" AutoEventWireup="true" CodeBehind="TenantGroupList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.TenantGroupList"
	Title="CPS - List Tenant " %>
<%@ Register Src="~/UserControls/TenantGroupListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	
    <ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	
</asp:Content>