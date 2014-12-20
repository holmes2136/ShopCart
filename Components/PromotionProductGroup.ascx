<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductGroup.ascx.cs"
    Inherits="Components_PromotionProductGroup" %>
<%@ Register Src="PromotionProductItem.ascx" TagName="ProductItem" TagPrefix="uc1" %>
<div class="PromotionProductGroup">
    <asp:DataList ID="uxList" runat="server" HorizontalAlign="Center" RepeatDirection="Horizontal"
        RepeatColumns="6" OnItemDataBound="uxList_ItemDataBound" CssClass="PromotionProductGroupDataList">
        <AlternatingItemTemplate>
            <div>
                <asp:Image ID="uxOrImage" runat="server" ImageUrl="~/Images/Design/Icon/PromotionOr3.gif"
                    ImageAlign="Middle" CssClass="AlternatingItem" />
            </div>
        </AlternatingItemTemplate>
        <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ProductGroupCombineItem"/>
        <ItemTemplate>
            <uc1:ProductItem ID="uxProductItem" runat="server" PromotionSubGroupID='<%# Eval( "PromotionSubGroupID" )  %>'
                ProductID='<%# Eval( "ProductID" )  %>' OnProductCheckedChanged="uxProductItem_ProductCheckedChanged"
                OnProductOptionChanged="uxProductItem_ProductOptionChanged" CssClass="ProductGroupProductItem"/>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  CssClass="ProductGroupProductItemStyle"/>
    </asp:DataList>
    <asp:Panel ID="uxErrorMessagePanel" runat="server" CssClass="MessagePanel">
        <asp:Label ID="uxErrorSelectLabel" runat="server" Text="[$ErrorSelect]" Visible="false" />
        <asp:Label ID="uxErrorOptionLabel" runat="server" Text="[$ErrorOption]" Visible="false" />
    </asp:Panel>
    <asp:HiddenField ID="uxSelectedProductHidden" runat="server" />
</div>
