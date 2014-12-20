<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Register.aspx.cs"
    Inherits="Register" Title="[$Title]" %>

<%@ Register Src="Components/CustomerRegister.ascx" TagName="CustomerRegister" TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" Title="[$Head]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc2:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <div id="uxRegisterDiv" runat="server">
        <div class="CommonPage">
            <div class="CommonPageTop" id="uxTopPage" runat="server">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxRegisterTitle" runat="server" Text="[$Head]">
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <uc1:CustomerRegister ID="uxCustomerRegister" runat="server" />
                    <div class="Clear">
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
