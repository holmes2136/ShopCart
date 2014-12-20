<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductGroup.ascx.cs"
    Inherits="Mobile_Components_PromotionProductGroup" %>
<%@ Register Src="PromotionProductItem.ascx" TagName="ProductItem" TagPrefix="uc1" %>
<div class="MobilePromotionProductGroup">
    <asp:DataList ID="uxList" runat="server" HorizontalAlign="Center" RepeatDirection="Horizontal"
        RepeatColumns="1" CssClass="MobilePromotionProductGroupList" OnItemDataBound="uxList_OnItemDataBound">
        <ItemTemplate>
            <uc1:ProductItem ID="uxProductItem" runat="server" PromotionSubGroupID='<%# Eval( "PromotionSubGroupID" )  %>'
                ProductID='<%# Eval( "ProductID" )  %>' OnProductCheckedChanged="uxProductItem_ProductCheckedChanged"
                OnProductOptionChanged="uxProductItem_ProductOptionChanged" />
        </ItemTemplate>
        <ItemStyle VerticalAlign="Middle" />
    </asp:DataList>
    <asp:HiddenField ID="uxSelectedProductHidden" runat="server" />
</div>
<asp:Panel ID="uxErrorMessagePanel" runat="server" CssClass="MessagePanel">
    <div id="uxMessageSelect" runat="server" class="MobileCommonValidatorText" visible="false">
        <div class="MobileCommonValidateDiv MobilePromotionProductGroupValidateDiv">
        </div>
        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
        <asp:Label ID="uxErrorSelectLabel" runat="server" Text="[$ErrorSelect]" />
    </div>
    <div id="uxMessageOption" runat="server" class="MobileCommonValidatorText" visible="false">
        <div class="MobileCommonValidateDiv MobilePromotionProductGroupValidateDiv">
        </div>
        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
        <asp:Label ID="uxErrorOptionLabel" runat="server" Text="[$ErrorOption]" />
    </div>
</asp:Panel>

