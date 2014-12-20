<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductImageSizeConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_ProductImageSizeConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxRegularImageWidthHelp" ConfigName="RegularImageWidth" runat="server" />
    <div class="Label">
        <asp:Label ID="lcRegularImageWidth" runat="server" meta:resourcekey="lcRegularImageWidth"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxRegularImageWidthText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireRegularImageWidthValidator" runat="server"
        Display="Dynamic" ControlToValidate="uxRegularImageWidthText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Regular Image Width is required.
                    <div class="CommonValidateDiv"></div></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxRegularImageWidthCompare" runat="server" ControlToValidate="uxRegularImageWidthText"
        Display="Dynamic" Type="Integer" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Regular Image Width must be an Integer and greater than zero(0).
                    <div class="CommonValidateDiv"></div></asp:CompareValidator>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSecondaryImageWidthHelp" ConfigName="SecondaryImageWidth" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSecondaryImageWidth" runat="server" meta:resourcekey="lcSecondaryImageWidth"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxSecondaryImageWidthText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireSecondaryImageWidthValidator" runat="server"
        Display="Dynamic" ControlToValidate="uxSecondaryImageWidthText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Secondary Image Width is required.
                    <div class="CommonValidateDiv"></div></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxSecondaryImageWidthCompare" runat="server" ControlToValidate="uxSecondaryImageWidthText"
        Display="Dynamic" Type="Integer" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Secondary Image Width must be an Integer and greater than zero(0).
                    <div class="CommonValidateDiv"></div></asp:CompareValidator>
</div>
