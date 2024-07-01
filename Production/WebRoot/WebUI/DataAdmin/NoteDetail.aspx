<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI"%>
<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DataAdminMaster.master" AutoEventWireup="true" CodeBehind="NoteDetail.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.NoteDetail"
	Title="CPS - View Note" %>
<%@ Register Src="~/UserControls/NoteDetailPanel.ascx" TagName="DetailPanel"
    TagPrefix="ElementsCPSuc" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
    <asp:Panel ID="pnlDetail" runat="server">
        <ElementsCPSuc:DetailPanel ID="ucDetail" runat="server" />
    </asp:Panel>
</asp:Content>
