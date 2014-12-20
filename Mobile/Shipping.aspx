<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Shipping.aspx.cs" Inherits="Mobile_Shipping" %>

<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <div id="uxFixedShippingMessageDiv" runat="server" visible="false" class="MobileShoppingCartMessage MobileCommonBox">
        [$MessageFixedShippingCost3]
    </div>
    <asp:Panel ID="uxRealTimeMessagePanel" runat="server" CssClass="MobileShoppingCartMessage MobileCommonBox"
        Visible="false">
        <asp:Label ID="uxRealTimeMessageLabel" runat="server" ForeColor="Red" />
    </asp:Panel>
    <div class="MobileCommonBox">
        <uc1:Message ID="uxMessage" runat="server" />
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="ShippingValid"
            CssClass="MobileCommonValidateText" />
        <asp:RadioButtonList ID="uxShippingRadioList" runat="server" ValidationGroup="ShippingValid"
            CssClass="MobileRadioList">
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="uxShippingMethodRequired" runat="server" ErrorMessage="Please select shipping method."
            ControlToValidate="uxShippingRadioList" Display="None" ValidationGroup="ShippingValid">*</asp:RequiredFieldValidator>
        <div class="MobileShippingRestrictions">
            <asp:Literal ID="uxRestrictionsLiteral" runat="server"></asp:Literal>
        </div>
        <div class="MobileShippingRecurringWarring">
            <asp:Label ID="uxRecurringWarringLabel" runat="server"></asp:Label>
        </div>
        <table class="MobileShoppingCartTable" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MobileShoppingCartButtonDiv">
                    <asp:LinkButton ID="uxCancelButton" runat="server" Text="[$Cancel]" OnClick="uxCancelButton_Click"
                        CssClass="MobileButton MobileShoppingCartButton" ValidationGroup="ShoppingCartValid" />
                </td>
                <td class="MobileShoppingCartButtonDiv">
                    <asp:LinkButton ID="uxNextButton" runat="server" Text="[$Next]" OnClick="uxNextButton_Click"
                        CssClass="MobileButton MobileShoppingCartButton" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
