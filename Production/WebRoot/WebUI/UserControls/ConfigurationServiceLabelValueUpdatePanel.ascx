<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceLabelValueUpdatePanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceLabelValueUpdatePanel" %>
<asp:Panel ID="pnlEditArea" runat="server">
    <asp:PlaceHolder ID="plcLabelValue" runat="server"></asp:PlaceHolder>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Panel>
<asp:Panel ID="pnlPopupArea" runat="server" SkinId="PopupControlExtenderPopupControl">
	<asp:Panel ID="pnlPopupAreaDynamicContent" runat="server" SkinId="PopupControlExtenderPopupControlDynamicContent"
		Width="250" Height="250">
		<!--Dynamic content will appear here-->
	</asp:Panel>
</asp:Panel>


