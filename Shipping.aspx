<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Shipping.aspx.cs"
    Inherits="ShippingPage" Title="Shipping" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutShippingOption.ascx" TagName="CheckoutShippingOption"
    TagPrefix="uc2" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" StepID="3" Title="[$Head]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="Shipping">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <uc2:CheckoutShippingOption ID="uxShippingDetails" runat="server" />
                    <div class="ShippingButtonDiv">
                        <asp:LinkButton ID="uxCancelImageButton" runat="server" Text="[$BtnCancel]" CssClass="BtnStyle2"
                            OnClick="uxCancelImageButton_Click" />
                        <asp:LinkButton ID="uxNextImageButton" runat="server" Text="[$BtnNext]" CssClass="BtnStyle1"
                            OnClick="uxNextImageButton_Click" ValidationGroup="ShippingValid" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
