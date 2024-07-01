Downloaded from http://encosia.com/downloads/postback-ritalin/

PostBack Ritalinby: Dave WardPostBack Ritalin is an ASP.NET AJAX server control to help simplify the common task of disabling buttons during partial postbacks.

PostBack Ritalin depends on the ASP.NET AJAX extensions. If you’re using ASP.NET 3.5, you’re all set. If you’re using ASP.NET 2.0, make sure you have the AJAX extensions property installed and configured before attempting to use PostBack Ritalin.

Properties
WaitText (optional): String value specifying replacement text for any Button controls, displayed only during partial postbacks (e.g. “Please Wait”, “Processing…”, etc). If omitted, defaults to “Processing…”.

WaitImage (optional): String value specifying an alternate image URL to be displayed during partial postbacks. If this is not specified, ImageButtons will be functionally disabled, but visibly unchanged during partial postbacks.

PreloadWaitImages (optional): Boolean value which controls preloading of WaitImages. Defaults to true.

MonitoredUpdatePanels (optional): An optional collection of UpdatePanels to monitor. If specified, then only the specified UpdatePanels will activate PostBack Ritalin. If omitted, any UpdatePanel on the page will active PostBack Ritalin. See usage example and sample website for syntax.

MonitoredUpdatePanel Properties
UpdatePanelID: The control ID of the UpdatePanel to monitor.

WaitText (optional): Same functionality as the master WaitText, but specific to the particular UpdatePanel specified by UpdatePanelID.

WaitImage (optional): Same functionality as the master WaitImage, but specific to the particular UpdatePanel specified by UpdatePanelID.

Usage examples
Basic usage. In the following example, when Button1 is clicked, its text will be replaced with “Processing…”, it will be blurred, and it will be disabled. When the partial postback completes, it will revert to its previous state.

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
    <asp:Button ID="Button1" runat="server" 
      Text="Click Me" OnClick="Button_OnClick" />
  </ContentTemplate>
</asp:UpdatePanel>
<encosia:PostBackRitalin runat="server" />Example of using the MonitoredUpdatePanels collection to restrict PostBack Ritalin to only activate on events triggered from certain UpdatePanels. In the following example, only events from UpdatePanel1 will cause Button1 to be disabled during partial postbacks. Events occurring in UpdatePanel2 will be unaffected.

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
    <asp:Button ID="Button1" runat="server" 
      Text="Click Me" OnClick="Button_OnClick" />
  </ContentTemplate>
</asp:UpdatePanel>
 
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
  <ContentTemplate>
    <asp:Button ID="Button2" runat="server" 
      Text="Click Me" OnClick="Button_OnClick" />
  </ContentTemplate>
</asp:UpdatePanel>
 
<encosia:PostBackRitalin ID="PostBackRitalin1" runat="server">
  <MonitoredUpdatePanels>
    <encosia:MonitoredUpdatePanel UpdatePanelID="UpdatePanel1" />
  </MonitoredUpdatePanels>
</encosia:PostBackRitalin>
