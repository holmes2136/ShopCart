<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCartGiftCoupon.ascx.cs"
    Inherits="Components_ShoppingCartGiftCoupon" %>
<%@ Register Src="CouponMessageDisplay.ascx" TagName="CouponMessageDisplay" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="ShoppingCartGiftCouponTable" id="uxGiftCertificateTable" runat="server">
    <div class="GiftCouponDiv">
        <h3>
            [$GiftCertificateCode]
        </h3>
        <div class="ShoppingCartGiftCouponInputCenter">
            <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" CssClass="CommonTextBox ShoppingCartGiftCouponInputCodeTextBox"></asp:TextBox>
            <div class="ShoppingCartGiftCouponButton">
                <asp:LinkButton ID="uxVerifyGiftButton" runat="server" OnClick="uxVerifyGiftButton_Click"
                    Text="[$BtnApply]" CssClass="BtnStyle2 DefaultGreenButtonStyle" ValidationGroup="ValidGift" />
            </div>
        </div>
        <div class="GiftCouponDetailValidatorText">
            <asp:RequiredFieldValidator ID="uxGiftCodeRequiredValidator" runat="server" ControlToValidate="uxGiftCertificateCodeText"
                ValidationGroup="ValidGift" Display="Dynamic" CssClass="CommonValidatorText ShoppingCartGiftCouponValidateText">
                    <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText"></div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Gift Certificate Code.
            </asp:RequiredFieldValidator>
        </div>
        <div id="uxGiftMessageTR" runat="server" visible="false">
            <div id="uxGiftMessageValidateDiv" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText">
                <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div id="uxGiftRemainValueTR" runat="server" visible="false" class="ShoppingCartGiftCouponTextInfo">
            <div class="ShoppingCartGiftCouponLabel">
                [$RemainingValue]
            </div>
            <div class="ShoppingCartGiftCouponInput">
                <asp:Label ID="uxGiftRemainValueLabel" runat="server" />
            </div>
        </div>
        <div id="uxGiftExpireDateTR" runat="server" visible="false" class="ShoppingCartGiftCouponTextInfo">
            <div class="ShoppingCartGiftCouponLabel">
                <span>[$ExpirationDate] </span>
            </div>
            <div class="ShoppingCartGiftCouponInput">
                <asp:Label ID="uxGiftExpireDateLabel" runat="server" />
            </div>
        </div>
        <div class="ShoppingCartClearButton">
            <asp:LinkButton ID="uxClearGiftImageButton" runat="server" Text="[$Clear]" CssClass="CommonHyperLink"
                OnClick="uxClearGiftImageButton_Click" />
        </div>
    </div>
</div>
<div class="ShoppingCartGiftCoupon">
    <div class="ShoppingCartGiftCouponTable" id="uxCouponDiv" runat="server">
        <div class="GiftCouponDiv">
            <h3>
                [$CouponCode]
            </h3>
            <div class="ShoppingCartGiftCouponInputCenter">
                <asp:TextBox ID="uxCouponIDText" runat="server" CssClass="CommonTextBox ShoppingCartGiftCouponInputCodeTextBox"></asp:TextBox>
                <div class="ShoppingCartGiftCouponButton">
                    <asp:LinkButton ID="uxVerifyCouponButton" runat="server" OnClick="uxVeryfyCouponButton_Click"
                        Text="[$BtnApply]" CssClass="BtnStyle2" ValidationGroup="ValidCoupon" />
                </div>
            </div>
            <div class="GiftCouponDetailValidatorText">
                <asp:RequiredFieldValidator ID="uxCounponCodeRequiredValidator" runat="server" ControlToValidate="uxCouponIDText"
                        ValidationGroup="ValidCoupon" Display="Dynamic" CssClass="CommonValidatorText ShoppingCartGiftCouponValidateText">
                        <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText"></div>
                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Coupon Code.
                    </asp:RequiredFieldValidator>
            </div>
            <div id="uxCouponMessageDiv" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText"
                visible="false">
                <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxCouponMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div class="ShoppingCartGiftCouponTextInfo">
            <uc2:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
        </div>
        <div class="ShoppingCartClearButton">
            <asp:LinkButton ID="uxClearCouponImageButton" runat="server" Text="[$Clear]" CssClass="CommonHyperLink"
                OnClick="uxClearCouponImageButton_Click" />
        </div>
    </div>
</div>
<div class="ShoppingCartGiftCouponTable" id="uxRewardPointDiv" runat="server">
    <div class="GiftCouponDiv">
        <h3>
            [$RewardPoint]
        </h3>
        <div class="ShoppingCartGiftCouponInputCenter">
            <asp:TextBox ID="uxRewardPointText" runat="server" CssClass="CommonTextBox ShoppingCartGiftCouponInputCodeTextBox"></asp:TextBox>
            <div class="ShoppingCartGiftCouponButton">
                <asp:LinkButton ID="uxVeryfyRewardPointButton" runat="server" OnClick="uxVeryfyRewardPointButton_Click"
                    Text="[$BtnApply]" CssClass="BtnStyle2" ValidationGroup="ValidPoint" />
            </div>
        </div>
        <div class="GiftCouponDetailValidatorText">
        <asp:RequiredFieldValidator ID="uxRewardPointRequiredValidator" runat="server" ControlToValidate="uxRewardPointText"
                ValidationGroup="ValidPoint" Display="Dynamic" CssClass="CommonValidatorText ShoppingCartGiftCouponValidateText">
                <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Reward Point.
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxRewardPointCompare" runat="server" ControlToValidate="uxRewardPointText"
                Operator="GreaterThan" ValueToCompare="0" Type="Integer" ValidationGroup="ValidPoint"
                Display="Dynamic" CssClass="CommonValidatorText ShoppingCartGiftCouponValidateText">
                <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Reward Point Cannot less than or equal to zero(0)
            </asp:CompareValidator>
        </div>
        <div id="uxRewardPointMessageTR" runat="server" visible="false">
            <div id="uxRewardPointValidateDiv" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText">
                <div class="CommonValidateDiv ShoppingCartGiftCouponValidateText">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxRewardPointMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div class="GiftCouponDiv">
            <asp:Label ID="uxRewardPointLabel" runat="server" CssClass="ShoppingCartGiftCouponPointLabel" />
        </div>
        <div class="ShoppingCartClearButton">
            <asp:LinkButton ID="uxClearRewardPointImageButton" runat="server" Text="[$Clear]"
                CssClass="CommonHyperLink" OnClick="uxClearRewardPointImageButton_Click" />
        </div>
    </div>
</div>
<span style="display: none">
    <asp:Label ID="uxMessageLabel" runat="server"></asp:Label></span>
<asp:HiddenField ID="uxMessageHidden" runat="server" />
