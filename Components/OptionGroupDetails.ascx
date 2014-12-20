<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionGroupDetails.ascx.cs"
    Inherits="Components_OptionGroupDetails" %>
<%@ Register Src="OptionItemDetails.ascx" TagName="OptionItemDetails" TagPrefix="uc1" %>
<div class="OptionGroupDetails">
    <asp:Panel ID="uxTitlePanel" runat="server" CssClass="OptionGroupDetailsTitleLeft">
        <div class="OptionGroupDetailsTitleRight">
            <asp:Label ID="uxTitleLabel" runat="server" Text='<%# GetLanguageText( "Title" )%>' ></asp:Label>
        </div>
    </asp:Panel>
    <asp:DataList ID="uxOptionDataList" runat="server" OnItemDataBound="uxOptionDataList_ItemDataBound"
        CssClass="OptionGroupDetailsDatalist">
        <ItemTemplate>
            <uc1:OptionItemDetails ID="uxOptionItemDetails" runat="server" OptionGroupID='<%# Eval("OptionGroupID") %>'
                ProductID="<%# ProductID %>" />
        </ItemTemplate>
        <ItemStyle CssClass="OptionGroupDetailsDataListItemStyle" />
    </asp:DataList>
</div>
