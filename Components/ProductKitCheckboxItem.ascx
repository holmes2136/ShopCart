<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitCheckboxItem.ascx.cs"
    Inherits="Components_ProductKitCheckboxItem" %>
<%@ Register Src="Common/TooltippedText.ascx" TagName="TooltippedText" TagPrefix="uc2" %>
<div id="uxMessageDiv" class="CommonOptionItemValidator" visible="false" runat="server">
    <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" />
    <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label>
    <div class="ProductKitValidateDiv"></div>
</div>
<div>
    <asp:DataList ID="uxInputDataList" runat="server" CssClass="OptionInputListItemDataList"
        OnItemDataBound="uxInputDataList_ItemDataBound">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        <div class="ProductKitItemDetailsTop">
                            <asp:Label ID="uxQtyLabel" runat="server" CssClass="OptionDisplayText" Text="Quantity"
                                Style="padding-right: 5px"></asp:Label>
                        </div>
                    </td>
                    <td class="ProductKitGroupItemStyle">
                        <div class="ProductKitItemDetailsTop">
                            <uc2:TooltippedText ID="uxTooltippedText" runat="server" />
                        </div>
                        <asp:Label ID="uxRequireLabel" runat="server" Text="*" ForeColor="Red" Font-Bold="true"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <HeaderStyle CssClass="ProductKitGroupItemStyle" />
        <ItemTemplate>
            <table>
                <tr valign="top">
                    <td class="quantityTD">
                        <asp:Label ID="uxQuantityLabel" runat="server" CssClass="quantityLabel" Text='<%# Eval( "Quantity" ) %>' Visible='<%# ShowHideQuantity( Eval( "IsUserDefinedQuantity" ) )  %>'></asp:Label>
                        <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Eval( "Quantity" ) %>' CssClass="ProductKitGroupItemQuantityText"
                            Visible='<%# Eval( "IsUserDefinedQuantity" )  %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="uxKitItemCheck" runat="server" Checked='<%# Eval ("IsDefault") %>' />
                        <asp:Label ID="uxInputLabel" runat="server" Text='<%# Eval( "DisplayName" ) %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle CssClass="ProductKitGroupItem" />
    </asp:DataList>
</div>
<asp:HiddenField ID="uxKitGroupIDHidden" runat="server" />
