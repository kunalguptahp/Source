<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/UserAdminMaster.master" AutoEventWireup="true" CodeBehind="UserAdminDefault.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.UserAdminDefault"
	Title="CPS - User Admin" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<h1 class="SecurityNotice">Use of this area is restricted to User Administrators only.</h1>
</asp:Content>
