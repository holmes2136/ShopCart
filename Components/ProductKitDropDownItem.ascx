<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitDropDownItem.ascx.cs"
    Inherits="Components_ProductKitDropDownItem" %>
<%@ Register Src="Common/TooltippedText.ascx" TagName="TooltippedText" TagPrefix="uc2" %>
<div id="uxMessageDiv" class="CommonOptionItemValidator" visible="false" runat="server">
    <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" />
    <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label>
    <div class="ProductKitValidateDiv"></div>
</div>
<div class="ProductKitGroupItem">
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
        <tr>
            <td class="quantityTD">
                <asp:Label ID="uxQuantityLabel" runat="server" CssClass="quantityLabel"></asp:Label>
                <asp:TextBox ID="uxQuantityText" runat="server" CssClass="ProductKitGroupItemQuantityText"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="uxKitItemDrop" runat="server" ValidationGroup="ValidOption"
                    DataTextField="DisplayName" DataValueField="ProductID" OnSelectedIndexChanged="uxKitItemDrop_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="uxKitGroupIDHidden" runat="server" />
