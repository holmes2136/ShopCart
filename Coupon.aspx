<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Coupon.aspx.cs"
    Inherits="CouponPage" Title="[$Title]" %>

<%@ Register Src="Components/CouponMessageDisplay.ascx" TagName="CouponMessageDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="Coupon">
        <uc2:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div id="uxNoCouponDiv" runat="server" visible="false">
                        <div class="MessageNormal">
                            [$InputCouponMessage]</div>
                        <div class="CouponPanel">
                            <asp:Label ID="uxCouponLabel" runat="server" Text="[$CouponCode]" CssClass="CommonFormLabel" />
                            <asp:TextBox ID="uxCouponIDText" runat="server" MaxLength="50" CssClass="CommonTextBox" />
                            <asp:LinkButton ID="uxCouponImageButton" runat="server" OnClick="uxCouponButton_Click"
                                Text="[$BtnVerifyButton]" CssClass="BtnStyle2" />
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <p class="CouponCodeParagraph">
                        <asp:Label ID="uxCouponLiteral" runat="server" Text="" CssClass="CouponCodeLabel">
                        </asp:Label>
                    </p>
                    <uc1:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
                    <div class="CouponBackButton">
                        <asp:HyperLink ID="uxBackLink" runat="server" CssClass="CommonHyperLink" Text="[$GoHome]"
                            NavigateUrl="~/Default.aspx" />
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
