<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/ConfigurationServiceMaster.Master" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupSelectorUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ConfigurationServiceGroupSelectorUpdate"
	Title="CPS - Update Configuration Service Group Selector" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupSelectorUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" ConfigurationServiceGroupEnabled="false" />
    </asp:Panel>
</asp:Content>
