<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs"
    Inherits="Payment" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutPaymentMethods.ascx" TagName="PaymentMethods"
    TagPrefix="uc2" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" StepID="4" Title="[$Title]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="Payment">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <uc2:PaymentMethods ID="uxPaymentMehods" runat="server" />
                    <div class="PaymentButtonDiv">
                        <asp:LinkButton ID="uxPaymentImageButton" runat="server" Text="[$BtnNext]" CssClass="BtnStyle1"
                            OnClick="uxPaymentImageButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
