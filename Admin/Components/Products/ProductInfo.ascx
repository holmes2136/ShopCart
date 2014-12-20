<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductInfo.ascx.cs" Inherits="AdminAdvanced_Components_Products_ProductInfo" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<%@ Register Src="../MultiCatalog.ascx" TagName="MultiCatalog" TagPrefix="uc3" %>
<div class="ProductDetailsRow">
    <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" CssClass="Label" />
    <asp:TextBox ID="uxNameText" runat="server" Width="250px" ValidationGroup="VaildProduct"
        CssClass="TextBox" />
    <uc1:LanaguageLabelPlus ID="LanaguageLabelPlus3" runat="server" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Name is required.
        <div class="CommonValidateDiv CommonValidateDivProductName">
        </div>
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcShortDescription" runat="server" meta:resourcekey="lcShortDescription"
        CssClass="Label" />
    <asp:TextBox ID="uxShortDescriptionText" runat="server" Width="250px" TextMode="MultiLine"
        Rows="3" MaxLength="255" CssClass="TextBox" />
    <uc1:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcLongDescription" runat="server" meta:resourcekey="lcLongDescription"
        CssClass="Label" />
    <uc2:TextEditor ID="uxLongDescriptionText" runat="Server" PanelClass="freeTextBox1 fl"
        TextClass="TextBox" Height="300px" />
    <uc1:LanaguageLabelPlus ID="LanaguageLabelPlus2" runat="server" />
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcCategory" runat="server" meta:resourcekey="lcCategory" CssClass="Label" />
    <asp:DropDownList ID="uxCategoryDrop" runat="server" Width="255px" Visible="false"
        CssClass="fl DropDown">
    </asp:DropDownList>
    <uc3:MultiCatalog ID="uxMultiCatalog" runat="server" />
    <div class="Clear">
    </div>
</div>
<div class="Clear">
</div>
