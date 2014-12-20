<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCouponDetail.ascx.cs"
    Inherits="Mobile_Components_GiftCouponDetail" %>
<%@ Register Src="MobileCouponMessageDisplay.ascx" TagName="CouponMessageDisplay" TagPrefix="uc2" %>
<%@ Register Src="MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="MobileGiftCouponDetail">
    <table class="MobileGiftCouponDetailTable" id="uxGiftCertificateTable" runat="server">
        <tr>
            <td class="MobileGiftCouponDetailSearchTermText">
                <span>[$GiftCertificateCode] </span>
            </td>
            <td class="MobileGiftCouponDetailInputCenter">
                <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" CssClass="CommonTextBox MobileGiftCouponDetailInputCodeTextBox"></asp:TextBox>
            </td>
            <td class="MobileGiftCouponDetailButton">
                <vevo:ImageButton ID="uxVerifyGiftButton" runat="server" OnClick="uxVerifyGiftButton_Click"
                    ThemeImageUrl="[$GiftCertificateVerifyButton]" />
            </td>
        </tr>
        <tr id="uxGiftRemainValueTR" runat="server" visible="false">
            <td class="MobileGiftCouponDetailLabel">
                <span>[$RemainingValue] </span>
            </td>
            <td class="MobileGiftCouponDetailInput" colspan="2">
                <asp:Label ID="uxGiftRemainValueLabel" runat="server" />
            </td>
        </tr>
        <tr id="uxGiftExpireDateTR" runat="server" visible="false">
            <td class="MobileGiftCouponDetailLabel">
                <span>[$ExpirationDate] </span>
            </td>
            <td colspan="2" class="MobileGiftCouponDetailInput">
                <asp:Label ID="uxGiftExpireDateLabel" runat="server" />
            </td>
        </tr>
        <tr id="uxGiftMessageTR" runat="server" visible="false">
            <td colspan="3" class="MobileGiftCouponDetailMessage">
                <uc1:Message ID="uxMessage" runat="server" />
            </td>
        </tr>
    </table>
    <div class="MobileGiftCouponDetailTable" id="uxCouponDiv" runat="server">
        <table>
            <tr>
                <td class="MobileGiftCouponDetailSearchTermText">
                    <span>[$CouponCode]</span>
                </td>
                <td class="MobileGiftCouponDetailInputCenter">
                    <asp:TextBox ID="uxCouponIDText" runat="server" CssClass="CommonTextBox MobileGiftCouponDetailInputCodeTextBox"></asp:TextBox>
                </td>
                <td class="MobileGiftCouponDetailButton">
                    <vevo:ImageButton ID="uxVerifyCouponButton" runat="server" OnClick="uxVeryfyCouponButton_Click"
                        ThemeImageUrl="[$CouponVerifyButton]" />
                </td>
            </tr>
        </table>
        <div class="MobileGiftCouponDetailTextInfo">
            <div id="uxCouponMessageDiv" runat="server" class="MobileGiftCouponDetailMessage" visible="false">
                <uc1:Message ID="uxCouponMessage" runat="server" />
            </div>
            <uc2:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
        </div>
    </div>
    <table cellpadding="2" cellspacing="0" class="MobileGiftCouponDetailSpecialRequestTable">
        <tr>
            <td class="MobileGiftCouponDetailSpecialRequestLabel">
                [$Special Request]
            </td>
            <td class="MobileGiftCouponDetailSpecialRequestInput">
                <asp:TextBox ID="uxCustomerComments" runat="server" Height="50px" TextMode="MultiLine"
                    Width="280px" CssClass="MobileGiftCouponDetailSpecialRequestTextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <span style="display: none">
        <asp:Label ID="uxMessageLabel" runat="server"></asp:Label></span>
    <asp:HiddenField ID="uxMessageHidden" runat="server" />
</div>
