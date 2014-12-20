<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitRadioItem.ascx.cs"
    Inherits="Components_ProductKitRadioItem" %>
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
        <tr class="quantityTR">
            <td class="quantityTD">
                <asp:Label ID="uxQuantityLabel" runat="server" CssClass="quantityLabelRadio"></asp:Label>
                <asp:TextBox ID="uxQuantityText" runat="server" CssClass="ProductKitGroupItemQuantityTextRadio"></asp:TextBox>
            </td>
            <td>
                <asp:RadioButtonList ID="uxKitItemRadioButtonList" runat="server" ValidationGroup="ValidOption"
                    DataTextField="DisplayName" DataValueField="ProductID" CssClass="OptionRadioItemRadioButtonList"
                    OnSelectedIndexChanged="uxKitItemRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="uxKitGroupIDHidden" runat="server" />
