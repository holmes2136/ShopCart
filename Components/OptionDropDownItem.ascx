<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionDropDownItem.ascx.cs"
    Inherits="Components_OptionDropDownItem" %>
<div class="OptionDropDownItem">
    <asp:RequiredFieldValidator ID="uxDropRequiredValid" runat="server" ControlToValidate="uxOptionDrop"
        ValidationGroup="ValidOption" Display="Dynamic">
            <div class="CommonOptionItemValidator">
            <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" /> Please select
            <div class="OptionValidateDiv"></div></div>
    </asp:RequiredFieldValidator>
    <asp:DropDownList ID="uxOptionDrop" runat="server" ValidationGroup="ValidOption"
        DataTextField="PriceUp" DataValueField="OptionItemID">
    </asp:DropDownList>
</div>
