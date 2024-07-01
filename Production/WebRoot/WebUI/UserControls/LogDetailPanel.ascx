<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogDetailPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.LogDetailPanel" %>
<asp:Panel ID="pnlEditArea" runat="server"
	Visible='<%# !this.DataItem.IsNew %>'
>
	<table border="1">
		<tr>
			<td>
				<asp:Label ID="lblIdLabel" runat="server" Text="Log Id :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblIdValue" runat="server" Text='<%# this.DataItem.Id %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblCreatedAt" runat="server" Text="CreatedAt :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblCreatedAtValue" runat="server" Text='<%# this.DataItem.CreatedAt %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblDateValue" runat="server" Text='<%# this.DataItem.DateX %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblUtcDate" runat="server" Text="UtcDate :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblUtcDateValue" runat="server" Text='<%# this.DataItem.UtcDate %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblSeverity" runat="server" Text="Severity :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblSeverityValue" runat="server" Text='<%# this.DataItem.Severity %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblUserIdentity" runat="server" Text="UserIdentity :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblUserIdentityValue" runat="server" Text='<%# this.DataItem.UserIdentity %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblUserName" runat="server" Text="UserName :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblUserNameValue" runat="server" Text='<%# this.DataItem.UserName %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblUserWebIdentity" runat="server" Text="UserWebIdentity :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblUserWebIdentityValue" runat="server" Text='<%# this.DataItem.UserWebIdentity %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblLogger" runat="server" Text="Logger :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblLoggerValue" runat="server" Text='<%# this.DataItem.Logger %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblLocationValue" runat="server" Text='<%# this.DataItem.Location %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblWebSessionId" runat="server" Text="WebSessionId :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblWebSessionIdValue" runat="server" Text='<%# this.DataItem.WebSessionId %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProcessThread" runat="server" Text="ProcessThread :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblProcessThreadValue" runat="server" Text='<%# this.DataItem.ProcessThread %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblMachineName" runat="server" Text="MachineName :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblMachineNameValue" runat="server" Text='<%# this.DataItem.MachineName %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProcessorCount" runat="server" Text="ProcessorCount :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblProcessorCountValue" runat="server" Text='<%# this.DataItem.ProcessorCount %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblOSVersion" runat="server" Text="OSVersion :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblOSVersionValue" runat="server" Text='<%# this.DataItem.OSVersion %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblClrVersion" runat="server" Text="ClrVersion :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblClrVersionValue" runat="server" Text='<%# this.DataItem.ClrVersion %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblAllocatedMemory" runat="server" Text="AllocatedMemory :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblAllocatedMemoryValue" runat="server" Text='<%# this.DataItem.AllocatedMemory %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblWorkingMemory" runat="server" Text="WorkingMemory :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblWorkingMemoryValue" runat="server" Text='<%# this.DataItem.WorkingMemory %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProcessUser" runat="server" Text="ProcessUser :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblProcessUserValue" runat="server" Text='<%# this.DataItem.ProcessUser %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProcessUserInteractive" runat="server" Text="ProcessUserInteractive :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblProcessUserInteractiveValue" runat="server" Text='<%# this.DataItem.ProcessUserInteractive %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblProcessUptime" runat="server" Text="ProcessUptime :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblProcessUptimeValue" runat="server" Text='<%# this.DataItem.ProcessUptime %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblMessage" runat="server" Text="Message :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblMessageValue" runat="server" Text='<%# this.DataItem.Message %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblException" runat="server" Text="Exception :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblExceptionValue" runat="server" Text='<%# this.DataItem.Exception %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblStackTrace" runat="server" Text="StackTrace :"></asp:Label>
			</td>
			<td>
				<asp:Label ID="lblStackTraceValue" runat="server" Text='<%# this.DataItem.StackTrace %>'></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table class="table.layoutTable">
					<tr>
						<td>
							<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2">
				<asp:HyperLink ID="lnkEdit" runat="server" SkinID="EditRecordLink"
					Visible='<%# !this.DataItem.IsNew %>'
					NavigateUrl='<%# Global.GetLogDetailPageUri((int)this.DataItem.Id) %>'
					/>
			</td>
		</tr>
	</table>
</asp:Panel>
