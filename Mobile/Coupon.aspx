<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Coupon.aspx.cs" Inherits="Mobile_Coupon" %>

<%@ Register Src="~/Mobile/Components/MobileCouponMessageDisplay.ascx" TagName="CouponMessageDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc2" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <uc2:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
    <div class="MobileCommonBox">
        <asp:Panel ID="uxCouponSubmitPanel" runat="server">
            <asp:Label ID="uxVerifyCouponTitle" runat="server" Text="[$Coupon]" CssClass="MobileCouponLabel"></asp:Label>
            <asp:TextBox ID="uxCouponIDText" runat="server" MaxLength="50" CssClass="MobileCouponText" />
            <div class="MobileUserLoginControlPanel">
                <asp:LinkButton ID="uxCouponButton" runat="server" Text="[$VerifyCouponSubmit]" OnClick="uxCouponButton_Click"
                    CssClass="MobileButton MobileCouponButton" />
            </div>
        </asp:Panel>
        <asp:Label ID="uxCouponLiteral" runat="server" Text="" CssClass="MobileCouponMessage MobileCouponCode" />
        <uc1:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
    </div>
</asp:Content>
