<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionRadioItem.ascx.cs"
    Inherits="Components_OptionRadioItem" %>
<div class="OptionRadioItem">
    <asp:RequiredFieldValidator ID="uxRadioRequiredValid" runat="server" ControlToValidate="uxOptionRadioButtonList"
        ValidationGroup="ValidOption" Display="Dynamic">
            <div class="CommonOptionItemValidator">
            <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" /> Please select
            <div class="OptionValidateDiv"></div></div>
    </asp:RequiredFieldValidator>
    <div class="OptionRadioItemDiv">
        <asp:RadioButtonList ID="uxOptionRadioButtonList" runat="server" ValidationGroup="ValidOption"
            DataTextField="PriceUp" DataValueField="OptionItemID" CssClass="OptionRadioItemRadioButtonList">
        </asp:RadioButtonList>
    </div>
</div>
