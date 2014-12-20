<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_SystemConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxMoreAjaxErrorMessageTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxMoreAjaxErrorMessageHelp" ConfigName="ShowDetailAjaxErrorMessage" runat="server" />
    <div class="Label">
        <asp:Label ID="uxMoreAjaxErrorMessage" runat="server" meta:resourcekey="lcMoreAjaxErrorMessage"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxMoreAjaxErrorMessageDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="ThubLogEnabledTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxThubLogEnabledHelp" ConfigName="ThubLogEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcThubLogEnabled" runat="server" meta:resourcekey="lcThubLogEnabled"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxThubLogEnabledDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
