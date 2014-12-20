<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminUserControlContent.ascx.cs"
    Inherits="AdminAdvanced_Components_Template_AdminUserControlContent" %>
<%@ Register Src="../BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<asp:Panel ID="uxMessagePanel" runat="server" CssClass="CssAdminContentMessage">
    <asp:PlaceHolder ID="uxMessagePlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxValidationSummaryPanel" runat="server" CssClass="CssAdminContentMessage">
    <asp:PlaceHolder ID="uxValidationSummaryPlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxButtonEventPanel" runat="server" CssClass="CssAdminContentButtonEvent">
    <asp:PlaceHolder ID="uxButtonEventPlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxLanguageControlPanel" runat="server" CssClass="CssAdminUserControlContentLanguage">
    <asp:PlaceHolder ID="uxLanguageControlPlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxValidationDenotePanel" runat="server" CssClass="CssAdminContentValidationDenote">
    <asp:PlaceHolder ID="uxValidationDenotePlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<uc2:BoxSet ID="uxTopContentBoxSet" runat="server" CssClass="CssAdminContentBoxSet">
    <ContentTemplate>
        <asp:PlaceHolder ID="uxTopContentBoxPlaceHolder" runat="server"></asp:PlaceHolder>
    </ContentTemplate>
</uc2:BoxSet>
<asp:Panel ID="uxContentPanel" runat="server" CssClass="CssAdminContentPanel">
    <asp:PlaceHolder ID="uxContentPlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxPlainContentPanel" runat="server" CssClass="CssAdminPlainContentPanel">
    <asp:PlaceHolder ID="uxPlainContentPlaceHolder" runat="server"></asp:PlaceHolder>
    <div class="Clear">
    </div>
</asp:Panel>
<div class="Clear">
</div>
