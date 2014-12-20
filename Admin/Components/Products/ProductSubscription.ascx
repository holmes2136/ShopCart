<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSubscription.ascx.cs"
    Inherits="Admin_Components_Products_ProductSubscription" %>
<div>
    <asp:Panel ID="uxProductSubscriptionTR" runat="server" CssClass="ProductDetailsRowTitle mgt10">
        <asp:Label ID="lcProductSubscriptionHeader" runat="server" meta:resourcekey="lcProductSubscriptionHeader" />
    </asp:Panel>
    <asp:Panel ID="uxIsSubscriptionProductTR" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="lcIsSubscription" runat="server" meta:resourcekey="lcIsSubscription"
                CssClass="Label" />
            <asp:DropDownList ID="uxIsSubscriptionProduct" runat="server" AutoPostBack="true"
                CssClass="fl DropDown">
                <asp:ListItem Value="True">Yes</asp:ListItem>
                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
            </asp:DropDownList>
            <div class="Clear">
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="uxSubscriptionProductInfoTR" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="lcSubscriptionLevel" runat="server" meta:resourcekey="lcSubscriptionLevel"
                CssClass="Label" />
            <asp:DropDownList ID="uxSubscriptionLevel" runat="server" CssClass="fl DropDown"
                DataTextField="Name" DataValueField="SubscriptionLevelID">
            </asp:DropDownList>
            <div class="Clear">
            </div>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="uxSubscriptionRangeLabel" runat="server" meta:resourcekey="lcSubscriptionRange"
                CssClass="Label" />
            <asp:TextBox ID="uxSubscriptionRangeText" runat="server" CssClass="TextBox"></asp:TextBox>
            <div class="validator1 fl">
                <span class="Asterisk">* </span>
            </div>
            <asp:RequiredFieldValidator ID="uxSubscriptionRangeRequired" runat="server" ControlToValidate="uxSubscriptionRangeText"
                ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Range is required.
                <div class="CommonValidateDiv CommonValidateDivProductSubscription">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxSubscriptionRangeCompare" runat="server" ErrorMessage="Subscription Range must be greater than zero."
                ControlToValidate="uxSubscriptionRangeText" Operator="GreaterThan" ValueToCompare="0"
                Type="Integer" Display="Dynamic" ValidationGroup="VaildProduct" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Range must be an Integer and greater than zero(0).
                <div class="CommonValidateDiv CommonValidateDivProductSubscription">
                </div>
            </asp:CompareValidator>
            <div class="Clear">
            </div>
        </div>
    </asp:Panel>
</div>
