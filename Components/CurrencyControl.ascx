<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyControl.ascx.cs"
    Inherits="Components_CurrencyControl" %>
<div class="CurrencyControl">
    <div class="CurrencyControlTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CurrencyControlTopLeft.gif"
            runat="server" CssClass="CurrencyControlTopImgLeft" />
        <asp:Label ID="uxCurrencyControlTitle" runat="server" Text="[$Currency]" CssClass="CurrencyControlTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CurrencyControlTopRight.gif"
            runat="server" CssClass="CurrencyControlTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CurrencyControlLeft">
        <div class="CurrencyControlRight">
            <div class="CurrencySwitch">
                <asp:Menu ID="uxCurrencyDataList" runat="server" CssClass="CommonTopDynamicDropdownList"
                    OnMenuItemClick="uxCurrencyDataList_OnMenuItemClick" Orientation="horizontal"
                    StaticEnableDefaultPopOutImage="false" DynamicEnableDefaultPopOutImage="false"
                    StaticHoverStyle-CssClass="CommonTopDynamicDropdownListStaticHover" StaticMenuItemStyle-CssClass="CommonTopDynamicDropdownListStaticMenuItem"
                    StaticSelectedStyle-CssClass="CommonTopDynamicDropdownListStaticSelectItem" StaticMenuStyle-CssClass="CommonTopDynamicDropdownListStaticMenuStyle"
                    DynamicHoverStyle-CssClass="CommonTopDynamicDropdownListDynamicHover" DynamicMenuItemStyle-CssClass="CommonTopDynamicDropdownListDynamicMenuItem"
                    DynamicSelectedStyle-CssClass="CommonTopDynamicDropdownListDynamicSelectItem" DynamicMenuStyle-CssClass="CommonTopDynamicDropdownListDynamicMenuStyle">
                </asp:Menu>
            </div>
        </div>
    </div>
    <div class="CurrencyControlBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CurrencyControlBottomLeft.gif"
            runat="server" CssClass="CurrencyControlBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CurrencyControlBottomRight.gif"
            runat="server" CssClass="CurrencyControlBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
