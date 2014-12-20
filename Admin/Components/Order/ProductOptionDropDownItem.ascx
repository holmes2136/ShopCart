<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionDropDownItem.ascx.cs"
    Inherits="Admin_Components_Order_ProductOptionDropDownItem" %>
<div class="OptionDropDownItem">
    <asp:RequiredFieldValidator ID="uxDropRequiredValid" runat="server" ControlToValidate="uxOptionDrop"
        ValidationGroup="ValidOption" Display="Dynamic">
        <div class="CommonValidatorTextProductOption">
        <img src="../Images/Design/Bullet/RequiredFillBullet_Down.gif" /> Please select
        <div class="OptionValidateDiv"></div>
        </div>
    </asp:RequiredFieldValidator>
    <asp:DropDownList ID="uxOptionDrop" runat="server" ValidationGroup="ValidOption"
        DataTextField="PriceUp" DataValueField="OptionItemID">
    </asp:DropDownList>
</div>
