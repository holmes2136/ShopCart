<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCouponDetail.ascx.cs"
    Inherits="Components_GiftCouponDetail" %>
<%@ Register Src="CouponMessageDisplay.ascx" TagName="CouponMessageDisplay" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="GiftCouponDetail">
    <table class="GiftCouponDetailTable" id="uxGiftCertificateTable" runat="server">
        <tr>
            <td class="GiftCouponDetailSearchTermText">
                <span>[$GiftCertificateCode] </span>
            </td>
            <td class="GiftCouponDetailInputCenter">
                <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" CssClass="CommonTextBox GiftCouponDetailInputCodeTextBox"></asp:TextBox>
            </td>
            <td class="GiftCouponDetailButton">
                <asp:LinkButton ID="uxVerifyGiftButton" runat="server" OnClick="uxVerifyGiftButton_Click"
                    Text="[$BtnVerify]" CssClass="BtnStyle2" />
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
            <td colspan="2" class="GiftCouponDetailInput">
                <asp:Label ID="uxGiftExpireDateLabel" runat="server" />
            </td>
        </tr>
        <tr id="uxGiftMessageTR" runat="server" visible="false">
            <td colspan="3" class="GiftCouponDetailMessage">
                <uc1:Message ID="uxMessage" runat="server" />
            </td>
        </tr>
    </table>
    <div class="GiftCouponDetailTable" id="uxCouponDiv" runat="server">
        <table>
            <tr>
                <td class="GiftCouponDetailSearchTermText">
                    <span>[$CouponCode]</span>
                </td>
                <td class="GiftCouponDetailInputCenter">
                    <asp:TextBox ID="uxCouponIDText" runat="server" CssClass="CommonTextBox GiftCouponDetailInputCodeTextBox"></asp:TextBox>
                </td>
                <td class="GiftCouponDetailButton">
                    <asp:LinkButton ID="uxVerifyCouponButton" runat="server" OnClick="uxVeryfyCouponButton_Click"
                        Text="[$BtnVerify]" CssClass="BtnStyle2" />
                </td>
            </tr>
            <tr id="uxCouponMessageDiv" runat="server" visible="false">
                <td colspan="3" class="GiftCouponDetailMessage">
                    <uc1:Message ID="uxCouponMessage" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <uc2:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="GiftCouponDetailTable" id="uxRewardPointDiv" runat="server">
        <table>
            <tr>
                <td class="GiftCouponDetailSearchTermText">
                    <span>[$RewardPoint]</span>
                </td>
                <td class="GiftCouponDetailInputCenter">
                    <asp:TextBox ID="uxRewardPointText" runat="server" CssClass="CommonTextBox GiftCouponDetailInputCodeTextBox"></asp:TextBox>
                </td>
                <td class="GiftCouponDetailButton">
                    <asp:LinkButton ID="uxVeryfyRewardPointButton" runat="server" OnClick="uxVeryfyRewardPointButton_Click"
                        Text="[$BtnVerify]" CssClass="BtnStyle2" />
                </td>
            </tr>
            <tr>
                <td class="GiftCouponDetailSearchTermText">
                </td>
                <td colspan="2">
                    <asp:Label ID="uxRewardPointLabel" runat="server" />
                </td>
            </tr>
            <tr id="uxRewardPointMessageTR" runat="server" visible="false">
                <td colspan="3" class="GiftCouponDetailMessage">
                    <uc1:Message ID="uxRewardPointMessage" runat="server" NumberOfNewLines="1" />
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="2" cellspacing="0" class="GiftCouponDetailSpecialRequestTable">
        <tr>
            <td class="GiftCouponDetailSearchTermText">
                [$Special Request]
            </td>
            <td class="GiftCouponDetailSpecialRequestInput">
                <asp:TextBox ID="uxCustomerComments" runat="server" Height="50px" TextMode="MultiLine"
                    Width="280px" CssClass="GiftCouponDetailSpecialRequestTextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <span style="display: none">
        <asp:Label ID="uxMessageLabel" runat="server"></asp:Label></span>
    <asp:HiddenField ID="uxMessageHidden" runat="server" />
</div>
