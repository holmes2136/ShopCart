<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponAndGiftDatails.ascx.cs"
    Inherits="Admin_Components_Order_CouponAndGiftDatails" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcGiftCode" runat="server" meta:resourcekey="lcGiftCode"></asp:Label></div>
    <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
    <vevo:AdvanceButton ID="uxVerifyGiftButton" runat="server" meta:resourcekey="uxVerifyGiftButton"
        CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxVerifyGiftButton_Click"
        OnClickGoTo="None" ValidationGroup="ValidGift"></vevo:AdvanceButton>
    <vevo:AdvanceButton ID="uxClearGiftButton" runat="server" Text="Clear" Visible="false"
        CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonGrey" OnClick="uxClearGiftButton_Click"
        OnClickGoTo="None" />
    <asp:RequiredFieldValidator ID="uxGiftCertificateCodeRequired" runat="server" ControlToValidate="uxGiftCertificateCodeText"
        ValidationGroup="ValidGift" Display="Dynamic" CssClass="CommonValidatorText CommonValidatorTextOrderCouponGift">
        <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
        </div>
        <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Gift Certificate Code is required.
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxGiftDetailsPanel" runat="server" Visible="false">
    <div class="CommonRowStyle" id="uxGiftRemainValueTR" visible="false" runat="server">
        <asp:Label ID="uxRemainingLabel" runat="server" meta:resourcekey="uxRemainingLabel"
            CssClass="BulletLabel"></asp:Label>
        <asp:Label ID="uxGiftRemainValueLabel" runat="server" CssClass="Label fl"></asp:Label>
    </div>
    <div class="CommonRowStyle" id="uxGiftExpireDateTR" visible="false" runat="server">
        <asp:Label ID="uxGiftExpirationDateLabel" runat="server" meta:resourcekey="uxGiftExpirationDateLabel"
            CssClass="BulletLabel"></asp:Label>
        <asp:Label ID="uxGiftExpireDateLabel" runat="server" CssClass="Label fl"></asp:Label>
    </div>
    <div id="uxGiftErrorMessageDiv" runat="server" class="CommonValidatorText CommonValidatorTextOrderCouponGift"
        visible="false">
        <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
        </div>
        <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" />
        <asp:Label ID="uxGiftErrorMessage" runat="server"></asp:Label>
    </div>
</asp:Panel>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcCouponCode" runat="server" meta:resourcekey="lcCouponCode"></asp:Label></div>
    <asp:TextBox ID="uxCouponIDText" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
    <vevo:AdvanceButton ID="uxVerifyCouponButton" runat="server" meta:resourcekey="uxVerifyCouponButton"
        CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxVerifyCouponButton_Click"
        OnClickGoTo="None" ValidationGroup="ValidCoupon"></vevo:AdvanceButton>
    <vevo:AdvanceButton ID="uxClearCouponButton" runat="server" Text="Clear" Visible="false"
        CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonGrey" OnClick="uxClearCouponButton_Click"
        OnClickGoTo="None" />
    <asp:RequiredFieldValidator ID="uxCouponRequired" runat="server" ControlToValidate="uxCouponIDText"
        ValidationGroup="ValidCoupon" Display="Dynamic" CssClass="CommonValidatorText CommonValidatorTextOrderCouponGift">
        <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
        </div>
        <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Coupon Code is required.
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxCouponDetailsPanel" runat="server">
    <div class="CommonRowStyle BulletLabel" id="uxCouponMessagePanel" runat="server">
        <uc1:Message ID="uxCouponMessage" runat="server" />
    </div>
    <div class="CommonRowStyle" id="uxCouponAmountDiv" runat="server" visible="false">
        <asp:Label ID="uxDiscountAmountLabel" runat="server" meta:resourcekey="uxDiscountAmountLabel"
            CssClass="BulletLabel"></asp:Label>
        <asp:Label ID="uxCouponAmountLabel" runat="server" CssClass="Label fl" />
    </div>
    <div class="CommonRowStyle" id="uxCouponExpireDateDiv" runat="server" visible="false">
        <asp:Label ID="uxExpirationDateLabel" runat="server" meta:resourcekey="uxExpirationDateLabel"
            CssClass="BulletLabel"></asp:Label>
        <asp:Label ID="uxCouponExpireDateLabel" runat="server" CssClass="Label fl" />
    </div>
    <div id="uxAvailableItemHeaderListDiv" runat="server" class="CommonRowStyle" visible="false">
        <asp:Label ID="uxAvailableItemHeaderLabel" runat="server" CssClass="BulletLabel" />
    </div>
    <div id="uxAvailableItemListDiv" runat="server" class="CommonRowStyle" visible="false">
        <asp:Literal ID="uxAvailableItemLabel" runat="server" />
    </div>
    <div id="uxCouponErrorMessageDiv" runat="server" class="CommonValidatorText CommonValidatorTextOrderCouponGift"
        visible="false">
        <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
        </div>
        <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" />
        <asp:Label ID="uxCouponErrorMessage" runat="server"></asp:Label>
    </div>
</asp:Panel>
<div class="CommonRowStyle" id="uxRewardPointDiv" runat="server">
    <div class="Label">
        <asp:Label ID="uxRewardPointLabel" runat="server" meta:resourcekey="uxRewardPointLabel"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="uxRewardPointText" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
        <vevo:AdvanceButton ID="uxVerifyRewardPointButton" runat="server" meta:resourcekey="uxVerifyRewardPointButton"
            CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxVerifyRewardPointButton_Click"
            OnClickGoTo="None" ValidationGroup="ValidPoint" />
        <vevo:AdvanceButton ID="uxClearRewardPointButton" runat="server" Text="Clear" Visible="false"
            CssClassBegin="mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonGrey" OnClick="uxClearRewardPointButton_Click"
            OnClickGoTo="None" />
        <div>
            <asp:RequiredFieldValidator ID="uxRewardPointRequired" runat="server" ControlToValidate="uxRewardPointText"
                ValidationGroup="ValidPoint" Display="Dynamic" CssClass="CommonValidatorText CommonValidatorTextOrderCouponGift">
            <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
            </div>
            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Reward Point is required.
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxRewardPointCompare" runat="server" ControlToValidate="uxRewardPointText"
                Operator="DataTypeCheck" Type="Currency" ValidationGroup="ValidPoint" Display="Dynamic"
                CssClass="CommonValidatorText CommonValidatorTextOrderCouponGift">
            <div class="CommonValidateDiv CommonValidateDivOrderCouponGift">
            </div>
            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Reward Point is invalid.
            </asp:CompareValidator>
        </div>
    </div>
    <div id="uxRewardPointPanel" runat="server" class="CommonRowStyle" visible="false">
        <asp:Label ID="uxRewardPointMessageLabel" runat="server" Text="You are using" CssClass="BulletLabel"></asp:Label>
        <asp:Label ID="uxRewardPointMessage" runat="server" CssClass="Label fl"></asp:Label>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            &nbsp;</div>
        <asp:Label ID="uxRewardPointDescriptionLabel" runat="server" />
    </div>
    <div class="Clear">
    </div>
</div>
