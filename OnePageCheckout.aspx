<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="OnePageCheckout.aspx.cs"
    Inherits="OnePageCheckout" Title="[$Title]" %>

<%@ Register Src="Components/CheckoutShippingAddress.ascx" TagName="CheckoutShippingAddress"
    TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutShippingOption.ascx" TagName="CheckoutShippingOption"
    TagPrefix="uc2" %>
<%@ Register Src="Components/CheckoutPaymentMethods.ascx" TagName="CheckoutPaymentMethods"
    TagPrefix="uc3" %>
<%@ Register Src="Components/CheckoutPaymentInfo.ascx" TagName="CheckoutPaymentInfo"
    TagPrefix="uc4" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc5" %>
<%@ Register Src="~/Components/QuickOrderSummaryDetails.ascx" TagName="QuickOrderSummaryDetails" TagPrefix="uc8" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="OnePageCheckout">
        <asp:Panel ID="uxPanelMessage" runat="server">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$ErrorHead]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <uc5:Message ID="uxValidateMessage" runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxPanelAll" runat="server" class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxShippingAddressTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxShippingAddressDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$ShippingAddressHead]</asp:Label>
                <asp:Image ID="uxShippingAddressTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxShippingAddressPanel" runat="server">
                <div class="CommonPageLeft">
                    <div class="CommonPageRight">
                        <uc1:CheckoutShippingAddress ID="uxCheckoutShippingAddress" runat="server" />
                        <div class="CheckoutButtonDiv">
                           <asp:LinkButton ID="uxShippingAddressNextImageButton" runat="server" Text="[$BtnNext]" CssClass="BtnStyle1"
                                OnClick="uxShippingAddressNextImageButton_Click" ValidationGroup="shippingAddress" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonPageTop">
                <asp:Image ID="uxShippingOptionTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxShippingOptionDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$ShippingOptionHead]</asp:Label>
                <asp:Image ID="uxShippingOptionTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxShippingOptionPanel" runat="server">
                <div class="CommonPageLeft">
                    <div class="CommonPageRight">
                        <uc2:CheckoutShippingOption ID="uxCheckoutShippingOption" runat="server" />
                        <div class="ShippingButtonDiv">
                            
                            <asp:LinkButton ID="uxShippingOptionBackImageButton" runat="server" Text="[$BtnBack]" CssClass="BtnStyle2"
                                OnClick="uxShippingOptionBackImageButton_Click" />
                            <asp:LinkButton ID="uxShippingOptionNextImageButton" runat="server" Text="[$BtnNext]" CssClass="BtnStyle1"  
                                OnClick="uxShippingOptionNextImageButton_Click" ValidationGroup="ShippingValid" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonPageTop">
                <asp:Image ID="uxPaymentMethodsTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxPaymentMethodsDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$PaymentMethodsHead]</asp:Label>
                <asp:Image ID="uxPaymentMethodsTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxPaymentMethodsPanel" runat="server">
                <div class="CommonPageLeft">
                    <div class="CommonPageRight">
                        <uc3:CheckoutPaymentMethods ID="uxCheckoutPaymentMethods" runat="server" />
                        <div class="ShippingButtonDiv">
                            <asp:LinkButton ID="uxPaymentMethodsBackImageButton" runat="server" Text="[$BtnBack]" CssClass="BtnStyle2"
                                OnClick="uxPaymentMethodsBackImageButton_Click" />
                            <asp:LinkButton ID="uxPaymentMethodsNextImageButton" runat="server" Text="[$BtnNext]" CssClass="BtnStyle1"  
                                OnClick="uxPaymentMethodsNextImageButton_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonPageTop">
                <asp:Image ID="uxPaymentInfoTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxPaymentInfoDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$PaymentInfoHead]</asp:Label>
                <asp:Image ID="uxPaymentInfoTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxPaymentInfoPanel" runat="server">
                <div class="CommonPageLeft">
                    <div class="CommonPageRight">
                        <uc4:CheckoutPaymentInfo ID="uxCheckoutPaymentInfo" runat="server" />
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="uxShippingCountryHidden" runat="server" />
    <asp:HiddenField ID="uxShippingStateHidden" runat="server" />
</asp:Content>
