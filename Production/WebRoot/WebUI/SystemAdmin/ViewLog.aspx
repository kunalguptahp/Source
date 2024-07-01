<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="ViewLog.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.ViewLog"
	Title="CPS - System Log Details" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<asp:DetailsView ID="dvLog" runat="server" 
			Caption="Log Event Details"
			>
		</asp:DetailsView>
	</div>
</asp:Content>
