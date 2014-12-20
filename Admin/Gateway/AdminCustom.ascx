<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminCustom.ascx.cs" Inherits="AdminAdvanced_Gateway_AdminCustom" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcCustomList" runat="server" meta:resourcekey="lcCustomList" /></div>
    <asp:TextBox ID="uxCustomListText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="Custom" />
