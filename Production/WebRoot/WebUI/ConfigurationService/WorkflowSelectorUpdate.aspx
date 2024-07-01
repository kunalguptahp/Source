<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/ConfigurationServiceMaster.Master" AutoEventWireup="true" CodeBehind="WorkflowSelectorUpdate.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.WorkflowSelectorUpdate"
	Title="CPS - Update Workflow Selector" %>
<%@ Register Src="~/UserControls/WorkflowSelectorUpdatePanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" WorkflowEnabled="false" />
    </asp:Panel>
</asp:Content>
