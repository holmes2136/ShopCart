<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminGoogleCheckout.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminGoogleCheckout" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="Google Checkout" />
<p class="CommonRowStyle">
    To use Google Checkout, please set MerchantKey, MerchantID, and Environment in your
    Web.config file.
</p>
<div class="Clear">
</div>