<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages._Default" 
	Title="CPS - Default Page" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<a href="App_Data/App_Configs/appSettings.config"></a>
		The current time is:
		<%= DateTime.UtcNow.ToString() %>
	</div>
</asp:Content>
