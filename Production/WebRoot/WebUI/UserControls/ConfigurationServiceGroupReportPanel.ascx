<%@ Import Namespace="HP.ElementsCPS.Apps.WebUI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationServiceGroupReportPanel.ascx.cs"
    Inherits="HP.ElementsCPS.Apps.WebUI.UserControls.ConfigurationServiceGroupReportPanel" %>
<%@ Import Namespace="Microsoft.Security.Application" %>
<%@ Register Src="~/UserControls/ConfigurationServiceLabelValueReportPanel.ascx"
    TagName="ConfigurationServiceLabelValueReportPanel" TagPrefix="ElementsCPSuc" %>
<%@ Register Src="~/UserControls/ConfigurationServiceGroupSelectorReportPanel.ascx"
    TagName="ConfigurationServiceGroupSelectorReportPanel" TagPrefix="ElementsCPSuc" %>
<asp:Panel ID="pnlEditArea" runat="server">
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="lblId" runat="server" Text="Group Ids:" SkinID="DataFieldLabel" AssociatedControlID="lblIdText"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIdText" runat="server"></asp:Label>
            </td>
        </tr>
        <asp:Repeater ID="repConfigurationServiceGroup" runat="server">
            <HeaderTemplate>
                <tr>
                    <td colspan="2" style="background-color:DeepSkyBlue">&nbsp;</td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblIdLabel" runat="server" Text="Id:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label ID="lblIdValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNameLabel" runat="server" Text="Name:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label ID="lblNameValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescriptionLabel" runat="server" Text="Description:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDescriptionValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblApplicationLabel" runat="server" Text="Application:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblApplicationValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConfigurationServiceApplication")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGroupTypeLabel" runat="server" Text="Group Type:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGroupTypeValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConfigurationServiceGroupType")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValuesLabel" runat="server" Text="Values:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="pnlConfigurationServiceLabelValue" runat="server" >
                            <ElementsCPSuc:ConfigurationServiceLabelValueReportPanel ID="ucConfigurationServiceLabelValueReport" ConfigurationServiceGroupId = '<%#DataBinder.Eval(Container.DataItem, "Id")%>'
                                runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOwnerLabel" runat="server" Text="Owner:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblOwnerValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PersonToOwnerId.Name")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValidationIdLabel" runat="server" Text="Validation Id:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblValidationIdValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValidationId")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProductionIdLabel" runat="server" Text="Production Id:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProductionIdValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductionId")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatusLabel" runat="server" Text="Group Status:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblStatusValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConfigurationServiceGroupState")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreatedOnLabel" runat="server" Text="Created:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCreatedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedOn")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblModifiedOnLabel" runat="server" Text="Last Modified:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblModifiedOnValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedOn")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreatedByLabel" runat="server" Text="Created By:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCreatedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedBy")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblModifiedByLabel" runat="server" Text="Last Modified By:" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblModifiedByValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ModifiedBy")%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGroupSelector" runat="server" Text="Group Selector(s)" SkinID="DataFieldLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="pnlConfigurationServiceGroupSelector" runat="server" >
                            <ElementsCPSuc:ConfigurationServiceGroupSelectorReportPanel ID="ConfigurationServiceGroupSelectorReportPanel" ConfigurationServiceGroupId = '<%#DataBinder.Eval(Container.DataItem, "Id")%>'
                                runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="2" style="background-color:DeepSkyBlue">&nbsp;</td>
                </tr>
            </SeparatorTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
