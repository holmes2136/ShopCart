<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayCheckCondition.ascx.cs"
    Inherits="AdminAdvanced_Components_EBay_EBayCheckCondition" %>
<div>
    <asp:Panel ID="uxCheckConditionTop" runat="server" CssClass="ProductDetailsRowTitle mgt0">
        <asp:Label ID="lcCheckCondition" runat="server" meta:resourcekey="lcCheckCondition" />
    </asp:Panel>
    <div class="EBayListingProcessRow">
        <div class="fl">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lcIsProductVariationEnabled" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lcIsPayPalRequired" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lcIsReturnPolycyEnabled" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lcMinimunReservePrice" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lcIsLotSizeEnabled" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="EBayListingProcessRow">
        <asp:Label ID="lcMessage" runat="server" CssClass="Label" ForeColor="Red" />
    </div>
</div>
