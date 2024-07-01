<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/SystemAdminMaster.master" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="HP.ElementsCPS.Apps.WebUI.Pages.SystemLog"
	Title="CPS - System Log" EnableEventValidation="false" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentArea" runat="server">
	<div>
		<h3>System Log:</h3>
		<SubSonic:QuickTable runat="server" TableName="Log" 
			PageSize="20"
			SortBy="Id"
			SortDirection="DESC"
			ColumnList="Id,UtcDate,Severity,MachineName,UserIdentity,Logger,Message,Exception"
			LinkToPage="/SystemAdmin/ViewLog.aspx" 
			/>
	</div>
</asp:Content>
