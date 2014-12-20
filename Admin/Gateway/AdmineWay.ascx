<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdmineWay.ascx.cs" Inherits="AdminAdvanced_Gateway_AdmineWay" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcCustomerID" runat="server" meta:resourcekey="lcCustomerID" /></div>
    <asp:TextBox ID="uxCustomerIDText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcUseCvn" runat="server" meta:resourcekey="lcUseCvn" /></div>
    <asp:DropDownList ID="uxUseCvnDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcTestMode" runat="server" meta:resourcekey="lcTestMode" /></div>
    <asp:DropDownList ID="uxTestModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcTestErrorCode" runat="server" meta:resourcekey="lcTestErrorCode" /></div>
    <asp:TextBox ID="uxTestErrorCodeText" runat="server" Width="100px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="eWay" />
