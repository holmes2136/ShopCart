<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VerifyCoupon.ascx.cs"
    Inherits="Components_VerifyCoupon" %>
<%@ Register Src="CouponMessageDisplay.ascx" TagName="CouponMessageDisplay" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
 <div id="uxCouponDiv" runat="server" class="GiftCouponDetailBox">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/VerifyCouponTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxVerifyCouponTitle" runat="server" Text="[$Coupon]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/VerifyCouponTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
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
   <%-- <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/VerifyCouponBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/VerifyCouponBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>--%>
</div>
