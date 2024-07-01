<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/JumpstationMaster.Master" AutoEventWireup="true" CodeBehind="JumpstationGroupUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.JumpstationGroupUpdate"
	Title="CPS - Update Jumpstation" %>
<%@ Register Src="~/UserControls/JumpstationGroupUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
