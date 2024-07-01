<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/DefaultMaster.master" AutoEventWireup="true" CodeBehind="ApplicationError.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ApplicationError"
	Title="CPS - Application Error" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<h2 class="SecurityNotice">An application error has occurred.</h2>
	<div>
		<span style="font-size: large;">
			An application processing error has occurred.
			<br />
			The error has been logged with the application support team for further review.
			<br />
			<br />
			Please use the browser's Back button to return to the previous page when you are ready to continue.
			<br />
			<br />
			The current time is:
			<%= DateTime.UtcNow.ToString() %>
		</span>
	</div>
</asp:Content>
