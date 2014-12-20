<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wholesale.ascx.cs" Inherits="AdminAdvanced_Components_SiteConfig_Wholesale" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxWholesaleTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxWholesaleModeHelp" ConfigName="WholesaleMode" runat="server" />
    <div class="Label">
        <asp:Label ID="uxWholesaleModeLabel" runat="server" meta:resourcekey="lcWholesaleMode"
            CssClass="fl">         
        </asp:Label></div>
    <asp:DropDownList ID="uxWholesaleModeDrop" runat="server" OnSelectedIndexChanged="uxWholesaleModeDrop_SelectedIndexChanged"
        AutoPostBack="true" CssClass="fl DropDown">
        <asp:ListItem Value="0" Text="No" />
        <asp:ListItem Value="1" Text="Enter Wholesale Price for each product" />
        <asp:ListItem Value="2" Text="Use Global Discount" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxNumberOfWholesaleLevelTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfWholesaleLevelHelp" ConfigName="WholesaleLevel" runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfWholesaleLevelLabel" runat="server" meta:resourcekey="lcNumberOfWholesaleLevel"
            CssClass="fl" /></div>
    <asp:DropDownList ID="uxNumberOfWholesaleDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxNumberOfWholesaleDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="0" Text="-- select --" />
        <asp:ListItem Value="1" Text="1" />
        <asp:ListItem Value="2" Text="2" />
        <asp:ListItem Value="3" Text="3" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxDiscountLevel1TR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxDiscountLevelHelp" ConfigName="DiscountLevel1" runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxDiscountLevelLabel" runat="server" meta:resourcekey="lcDiscountLevel1"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxDiscountLevel1Text" Style="width: 50px" runat="server" CssClass="TextBox">
    </asp:TextBox>
    <div class="fl">
        %</div>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxDiscountLevel1Required" runat="server" ControlToValidate="uxDiscountLevel1Text"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount Level 1 is required.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxDiscountLevel1Validator" runat="server" ControlToValidate="uxDiscountLevel1Text"
        Display="Dynamic" MaximumValue="100" MinimumValue="1" ValidationGroup="SiteConfigValid"
        Type="Integer" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount must be an Integer and must be between 1 and 100.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<asp:Panel ID="uxDiscountLevel2TR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxDiscountLevel2Help" ConfigName="DiscountLevel2" runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="lcDiscountLevel2" runat="server" meta:resourcekey="lcDiscountLevel2"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxDiscountLevel2Text" Style="width: 50px" runat="server" CssClass="TextBox">
    </asp:TextBox>
    <div class="fl">
        %</div>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxDiscountLevel2Required" runat="server" ControlToValidate="uxDiscountLevel2Text"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount Level 2 is required.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxDiscountLevel2Validator" runat="server" ControlToValidate="uxDiscountLevel2Text"
        Display="Dynamic" MaximumValue="100" MinimumValue="1" ValidationGroup="SiteConfigValid"
        Type="Integer" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount must be an Integer and must be between 1 and 100.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<asp:Panel ID="uxDiscountLevel3TR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxDiscountLevel3Help" ConfigName="DiscountLevel3" runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="lcDiscountLevel3" runat="server" meta:resourcekey="lcDiscountLevel3"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxDiscountLevel3Text" Style="width: 50px" runat="server" CssClass="TextBox">
    </asp:TextBox>
    <div class="fl">
        %</div>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxDiscountLevel3Required" runat="server" ControlToValidate="uxDiscountLevel3Text"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount Level 3 is required.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxDiscountLevel3Validator" runat="server" ControlToValidate="uxDiscountLevel3Text"
        Display="dynamic" MaximumValue="100" MinimumValue="1" ValidationGroup="SiteConfigValid"
        Type="Integer" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Discount must be an Integer and must be between 1 and 100.
        <div class="CommonValidateDiv CommonValidateDivWholesaleDiscount">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<asp:Panel ID="uxWholesaleWorningTR" runat="server" CssClass="ConfigRow">
    <asp:Label ID="uxWholesaleLevelWarningLabel" runat="server" Visible="false" meta:resourcekey="uxWholesaleLevelWarningLabel"
        CssClass="pdl15">
    </asp:Label>
</asp:Panel>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
