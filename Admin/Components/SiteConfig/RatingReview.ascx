<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RatingReview.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_RatingReview" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxStarRatingAmountTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxStarRatingAmountHelp" ConfigName="StarRatingAmount" runat="server" />
    <asp:Label ID="lcStarRatingAmount" runat="server" meta:resourcekey="lcStarRatingAmount"
        CssClass="Label">
    </asp:Label>
    <asp:TextBox ID="uxRatingStarAmountText" runat="server" Width="49px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireRatingStarValidator" runat="server" Display="Dynamic"
        ControlToValidate="uxRatingStarAmountText" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of Stars is required.
        <div class="CommonValidateDiv CommonValidateDivRatingStar"></div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxAffiliateExpirePeriodCompareWithZero" runat="server"
        Type="Integer" Display="Dynamic" ControlToValidate="uxRatingStarAmountText" Operator="GreaterThan"
        ValueToCompare="0" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of Stars must be an Integer and greater than zero(0).
        <div class="CommonValidateDiv CommonValidateDivRatingStar"></div>
    </asp:CompareValidator>
</asp:Panel>
<asp:Panel ID="uxMerchantRatingTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxMerchantRatingHelp" ConfigName="MerchantRating" runat="server" />
    <asp:Label ID="lcMerchantRating" runat="server" meta:resourcekey="lcMerchantRating"
        CssClass="Label">
    </asp:Label>
    <asp:DropDownList ID="uxMerchantRatingDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxCustomerRatingTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxCustomerRatingHelp" ConfigName="CustomerRating" runat="server" />
    <asp:Label ID="lcCustomerRating" runat="server" meta:resourcekey="lcCustomerRating"
        CssClass="Label">    
    </asp:Label>
    <asp:DropDownList ID="uxCustomerRatingDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxRatingRequireLoginTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxRatingRequireLoginHelp" ConfigName="RatingRequireLogin" runat="server" />
    <asp:Label ID="lcRatingRequireLogin" runat="server" meta:resourcekey="lcRatingRequireLogin"
        CssClass="Label">
    </asp:Label>
    <asp:DropDownList ID="RatingRequireLoginDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxCustomerReviewTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxCustomerReviewHelp" ConfigName="CustomerReview" runat="server" />
    <asp:Label ID="lcCustomerReview" runat="server" meta:resourcekey="lcCustomerReview"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxCustomerReviewDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxReviewRequireLoginTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxReviewRequireLoginHelp" ConfigName="ReviewRequireLogin" runat="server" />
    <asp:Label ID="lcReviewRequireLogin" runat="server" meta:resourcekey="lcReviewRequireLogin"
        CssClass="Label">
    </asp:Label>
    <asp:DropDownList ID="ReviewRequireLoginDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxCustomerDisplayTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxCustomerDisplayHelp" ConfigName="ReviewDisplayNameBy" runat="server" />
    <asp:Label ID="lcCustomerDisplayBy" runat="server" meta:resourcekey="lcCustomerDisplayBy"
        CssClass="Label">
    </asp:Label>
    <asp:DropDownList ID="uxCustomerDispalyByDrop" runat="server" Width="119px" CssClass="fl DropDown">
        <asp:ListItem Value="UserName">UserName</asp:ListItem>
        <asp:ListItem Value="FullName">Full Name</asp:ListItem>
        <asp:ListItem Value="FirstName">First Name</asp:ListItem>
        <asp:ListItem Value="LastName">Last Name</asp:ListItem>
    </asp:DropDownList>
</asp:Panel>
