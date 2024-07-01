<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="DevScaffold.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.DevScaffold"
	Title="CPS - Data Admin Scaffold" EnableEventValidation="false"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<SubSonic:Scaffold ID="AutoScaffold" runat="server" ScaffoldType="Auto">
		</SubSonic:Scaffold>
	</div>
</asp:Content>
