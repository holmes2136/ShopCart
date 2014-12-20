<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCouponDetailRightMenu.ascx.cs"
    Inherits="Components_GiftCouponDetail" %>
<%@ Register Src="CouponMessageDisplay.ascx" TagName="CouponMessageDisplay" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="GiftCouponDetailRightMenu">
    <div id="uxGiftCertificateTable" runat="server" class="GiftCouponDetailBox">
        <div class="SidebarTop">
            <asp:Label ID="uxGiftCertificateTitle" runat="server" Text="[$GiftCertificateCode]"
                CssClass="SidebarTopTitle" />
        </div>
        <div class="SidebarLeft">
            <div class="SidebarRight">
                <table class="GiftCouponDetailRightMenuTable">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" CssClass="CommonTextBox InputTextBox" />
                            <asp:LinkButton ID="uxVerifyGiftButton" runat="server" OnClick="uxVerifyGiftButton_Click"
                                Text="[$BtnVerifyButton]" CssClass="BtnStyle2 GiftCouponDetailButton" ValidationGroup="ValidGift" />
                            <asp:RequiredFieldValidator ID="uxGiftCodeRequiredValidator" runat="server" ControlToValidate="uxGiftCertificateCodeText"
                                ValidationGroup="ValidGift" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Gift Certificate Code.
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="uxGiftMessageTR" runat="server" visible="false">
                        <td colspan="2">
                            <div id="uxMessageDiv" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxMessage" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr id="uxGiftRemainValueTR" runat="server" visible="false">
                        <td class="GiftCouponDetailLabel">
                            <span>[$RemainingValue] </span>
                        </td>
                        <td class="GiftCouponDetailInput" colspan="2">
                            <asp:Label ID="uxGiftRemainValueLabel" runat="server" />
                        </td>
                    </tr>
                    <tr id="uxGiftExpireDateTR" runat="server" visible="false">
                        <td class="GiftCouponDetailLabel">
                            <span>[$ExpirationDate] </span>
                        </td>
                        <td class="GiftCouponDetailInput">
                            <asp:Label ID="uxGiftExpireDateLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:LinkButton ID="uxClearGiftImageButton" runat="server" Text="[$Clear]" CssClass="CommonHyperLink"
                                OnClick="uxClearGiftImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="uxCouponDiv" runat="server" class="GiftCouponDetailBox">
        <div class="SidebarTop">
            <asp:Label ID="uxCouponCodeTitle" runat="server" Text="[$CouponCode]" CssClass="SidebarTopTitle" />
        </div>
        <div class="SidebarLeft">
            <div class="SidebarRight">
                <table class="GiftCouponDetailRightMenuTable">
                    <tr>
                        <td>
                            <asp:TextBox ID="uxCouponIDText" runat="server" CssClass="CommonTextBox InputTextBox" />
                            <asp:LinkButton ID="uxVerifyCouponButton" runat="server" OnClick="uxVeryfyCouponButton_Click"
                                Text="[$BtnVerifyButton]" CssClass="BtnStyle2 GiftCouponDetailButton" ValidationGroup="ValidCoupon" />
                            <asp:RequiredFieldValidator ID="uxCounponCodeRequiredValidator" runat="server" ControlToValidate="uxCouponIDText"
                                ValidationGroup="ValidCoupon" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Coupon Code.
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="uxCouponErrorMessageDiv" runat="server" visible="false">
                        <td>
                            <div id="Div1" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxCouponMessage" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td id="uxCouponMessageDiv" runat="server" visible="false">
                            <uc2:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="uxClearCouponImageButton" runat="server" Text="[$Clear]" CssClass="CommonHyperLink"
                                OnClick="uxClearCouponImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="uxRewardPointDiv" runat="server" class="GiftCouponDetailBox">
        <div class="SidebarTop">
            <asp:Label ID="uxRewardPointTitle" runat="server" Text="[$RewardPoint]" CssClass="SidebarTopTitle" />
        </div>
        <div class="SidebarLeft">
            <div class="SidebarRight">
                <table class="GiftCouponDetailRightMenuTable">
                    <tr>
                        <td>
                            <asp:TextBox ID="uxRewardPointText" runat="server" CssClass="CommonTextBox InputTextBox" />
                            
                            <asp:LinkButton ID="uxVeryfyRewardPointButton" runat="server" OnClick="uxVeryfyRewardPointButton_Click"
                                Text="[$BtnVerifyButton]" CssClass="BtnStyle2 GiftCouponDetailButton" ValidationGroup="ValidPoint" />
                            <asp:RequiredFieldValidator ID="uxRewardPointRequiredValidator" runat="server" ControlToValidate="uxRewardPointText"
                                ValidationGroup="ValidPoint" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please Enter Reward Point.
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="uxRewardPointCompare" runat="server" ControlToValidate="uxRewardPointText"
                                Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidPoint" Display="Dynamic"
                                CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid Reward Point.
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="uxRewardPointMessageTR" runat="server" visible="false">
                        <td>
                            <div id="Div2" runat="server" class="CommonValidatorText GiftCouponDetailValidatorText">
                                <div class="CommonValidateDiv GiftCouponDetailValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxRewardPointMessage" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="uxRewardPointLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="uxClearRewardPointImageButton" runat="server" Text="[$Clear]"
                                CssClass="CommonHyperLink" OnClick="uxClearRewardPointImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="uxMessageHidden" runat="server" />
</div>
