<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/JumpstationMaster.Master" AutoEventWireup="true" CodeBehind="JumpstationGroupByQueryParameterList.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.JumpstationGroupByQueryParameterList"
	Title="CPS - List Jumpstation By Query Parameter" %>
<%@ Register Src="~/UserControls/JumpstationGroupByQueryParameterListPanel.ascx" TagName="ListPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<asp:Panel ID="pnlList" runat="server">
		<ElementsCPSuc:ListPanel ID="ucList" runat="server" />
	</asp:Panel>
</asp:Content>