<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponMessageDisplay.ascx.cs"
    Inherits="Components_CouponMessageDisplay" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="CouponMessageDisplay">
    <div id="uxCouponAmountDiv" runat="server" visible="false">
        <label class="CouponMessageDisplayLabel">
            [$DiscountAmount]
        </label>
        <asp:Label ID="uxCouponAmountLabel" runat="server" CssClass="CouponMessageDisplayData" />
        <div class="Clear"></div>
    </div>
    <div id="uxCouponExpireDateDiv" runat="server" visible="false">
        <label class="CouponMessageDisplayLabel">
            [$ExpirationDate]
        </label>
        <asp:Label ID="uxCouponExpireDateLabel" runat="server" CssClass="CouponMessageDisplayData" />
        <div class="Clear"></div>
    </div>
    <div id="uxAvailableItemHeaderListDiv" runat="server" class="CouponMessageDisplayAvaliableMessageHeader"
        visible="false">
        <asp:Label ID="uxAvailableItemHeaderLabel" runat="server" />
    </div>
    <div id="uxAvailableItemListDiv" runat="server" class="CouponMessageDisplayAvaliableMessageList"
        visible="false">
        <asp:Literal ID="uxAvailableItemLabel" runat="server" />
    </div>
    <div id="uxPromotionWarningDiv" runat="server" class="CouponMessageDisplayAvaliableMessageList"
        visible="false">
        <label class="CouponPromotionWarningDisplayLabel">
            [$PromotionCouponWarning]
        </label>
    </div>
    <div id="uxErrorMessageDiv" runat="server" class="CouponMessageDisplayError" visible="false">
        <uc1:Message ID="uxErrorMessage" runat="server" />
    </div>
</div>
<div class="clear">
</div>
