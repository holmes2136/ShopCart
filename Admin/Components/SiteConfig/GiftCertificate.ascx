<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificate.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_GiftCertificate" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxGiftCertificateEnabledHelp" ConfigName="GiftCertificateEnabled"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGiftCertificateEnabled" runat="server" meta:resourcekey="lcGiftCertificateEnabled"
            CssClass="fl">        
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxGiftCertificateEnabledDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes">
        </asp:ListItem>
        <asp:ListItem Value="False" Text="No">
        </asp:ListItem>
    </asp:DropDownList>
</div>
<div class="ConfigRow" id="uxOnlyProductDiv" runat="server">
    <uc1:HelpIcon ID="uxOnlyProductHelp" ConfigName="GiftRedeemProductOnly" runat="server" />
    <div class="Label">
        <asp:Label ID="uxOnlyProductLabel" runat="server" meta:resourcekey="lcOnlyProduct"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxOnlyProductDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes">
        </asp:ListItem>
        <asp:ListItem Value="False" Text="No">
        </asp:ListItem>
    </asp:DropDownList>
</div>
