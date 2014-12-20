<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_AffiliateConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliateEnabledHelp" ConfigName="AffiliateEnabled" runat="server" />
    <asp:Label ID="lcAffiliateEnabled" runat="server" meta:resourcekey="lcAffiliateEnabled"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxAffiliateEnabledDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
        <asp:ListItem Value="False" Text="No"></asp:ListItem>
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliateAutoApproveHelp" ConfigName="AffiliateAutoApprove" runat="server" />
    <asp:Label ID="lcAffiliateAutoApprove" runat="server" meta:resourcekey="lcAffiliateAutoApprove"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxAffiliateAutoApproveDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
        <asp:ListItem Value="False" Text="No"></asp:ListItem>
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliateReferenceHelp" ConfigName="AffiliateReference" runat="server" />
    <asp:Label ID="lcAffiliateReference" runat="server" meta:resourcekey="lcAffiliateReference"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxAffiliateReferenceDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="First" Text="Always use first affiliate"></asp:ListItem>
        <asp:ListItem Value="Last" Text="use latest affiliate"></asp:ListItem>
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliateExpirePeriodHelp" ConfigName="AffiliateExpirePeriod"
        runat="server" />
    <asp:Label ID="lcAffiliateExpirePeriod" runat="server" meta:resourcekey="lcAffiliateExpirePeriod"
        CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxAffiliateExpirePeriodText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireAffiliateExpireValidator" runat="server"
        Display="Dynamic" ControlToValidate="uxAffiliateExpirePeriodText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Cookie Expire Period is required.
                    <div class="CommonValidateDiv"></div></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxAffiliateExpirePeriodCompareWithZero" runat="server"
        Type="Integer" Display="Dynamic" ControlToValidate="uxAffiliateExpirePeriodText"
        Operator="GreaterThan" ValueToCompare="0" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Expire Period must be an Integer and greater than zero(0).
                    <div class="CommonValidateDiv"></div></asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliateCommissionRateHelp" ConfigName="AffiliateCommissionRate"
        runat="server" />
    <asp:Label ID="lcAffiliateCommissionRate" runat="server" meta:resourcekey="lcAffiliateCommissionRate"
        CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxAffiliateCommissionRateText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredAffiliateCommisionRateValidator" runat="server"
        Display="Dynamic" ControlToValidate="uxAffiliateCommissionRateText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Affiliate commission rate is required.
                    <div class="CommonValidateDiv"></div></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxAffiliateCommissionRateCompare" runat="server" Display="Dynamic"
        ControlToValidate="uxAffiliateCommissionRateText" Operator="DataTypeCheck" Type="Double"
        ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Affiliate commission rate must be a Double.
                    <div class="CommonValidateDiv"></div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAffiliatePaidBalanceHelp" ConfigName="AffiliateDefaultPaidBalance"
        runat="server" />
    <asp:Label ID="lcAffiliatePaidBalance" runat="server" meta:resourcekey="lcAffiliatePaidBalance"
        CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxAffiliatePaidBalanceText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredAffiliatePaidBalanceValidator" runat="server"
        Display="Dynamic" ControlToValidate="uxAffiliatePaidBalanceText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Default paid balance is required.
                    <div class="CommonValidateDiv"></div></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxAffiliatePaidBalanceCompareWithZero" runat="server" ControlToValidate="uxAffiliatePaidBalanceText"
        ErrorMessage="Your default paid balnce must greater than 0" Operator="GreaterThan"
        ValueToCompare="0" Type="Double" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Default paid balance must be a Double and greater than zero(0).
                    <div class="CommonValidateDiv"></div>  </asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
