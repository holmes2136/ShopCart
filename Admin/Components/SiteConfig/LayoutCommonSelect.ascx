<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayoutCommonSelect.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_LayoutCommonSelect" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxPanel" runat="server">
    <uc1:HelpIcon ID="uxHelp" runat="server" />
    <asp:Panel ID="uxLabelPanel" runat="server">
        <asp:Label ID="uxLabel" runat="server" CssClass="fl" />
    </asp:Panel>
    <asp:DropDownList ID="uxItemDrop" runat="server">
    </asp:DropDownList>
    <div class="Clear">
    </div>
    <asp:Label ID="uxImageLabel" runat="server" />
    <div class="LayoutImagePanelDiv">
        <asp:Panel ID="uxImagePanel" runat="server" CssClass="LayoutImagePanel">
        </asp:Panel>
    </div>
    <%--    <ajaxToolkit:RoundedCornersExtender ID="uxImagePanelRoundedCornersExtender" runat="server"
        TargetControlID="uxImagePanel">
    </ajaxToolkit:RoundedCornersExtender>--%>
    <div class="Clear">
    </div>
</asp:Panel>
