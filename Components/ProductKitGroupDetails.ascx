<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitGroupDetails.ascx.cs"
    Inherits="Components_ProductKitGroupDetails" %>
<%@ Register Src="ProductKitItemDetails.ascx" TagName="ProductKitItemDetails" TagPrefix="uc1" %>
<div class="OptionGroupDetails">
    <asp:Panel ID="uxTitlePanel" runat="server" CssClass="OptionGroupDetailsTitleLeft">
    </asp:Panel>
    <asp:DataList ID="uxOptionDataList" runat="server" OnItemDataBound="uxProductKitDataList_ItemDataBound"
        CssClass="OptionGroupDetailsDatalist">
        <ItemTemplate>
            <uc1:ProductKitItemDetails ID="uxProductKitItemDetails" runat="server" ProductKitGroupID='<%# Eval("ProductKitGroupID") %>' />
        </ItemTemplate>
        <ItemStyle CssClass="OptionGroupDetailsDataListItemStyle" />
    </asp:DataList>
</div>
