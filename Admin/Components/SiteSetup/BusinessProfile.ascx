<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusinessProfile.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteSetup_BusinessProfile" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc5" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyNameHelp" ConfigName="CompanyName" runat="server" />
    <asp:Label ID="lcBusinessName" runat="server" meta:resourcekey="lcBusinessName" CssClass="Label" />
    <asp:TextBox ID="uxCompanyNameText" runat="server" CssClass="TextBox" />
    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyCountryHelp" ConfigName="CompanyCountry" runat="server" />
    <asp:Label ID="lcBusinessCountry" runat="server" meta:resourcekey="lcBusinessCountry"
        CssClass="Label" />
    <asp:TextBox ID="uxCompanyCountryText" runat="server" CssClass="TextBox" />
    <uc5:LanaguageLabelPlus ID="uxPlus5" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyAddressHelp" ConfigName="CompanyAddress" runat="server" />
    <asp:Label ID="lcBusinessAddress" runat="server" meta:resourcekey="lcBusinessAddress"
        CssClass="Label" />
    <asp:TextBox ID="uxCompanyAddressText" runat="server" CssClass="TextBox" />
    <uc5:LanaguageLabelPlus ID="uxPlus2" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyCityHelp" ConfigName="CompanyCity" runat="server" />
    <asp:Label ID="lcBusinessCity" runat="server" meta:resourcekey="lcBusinessCity" CssClass="Label" />
    <asp:TextBox ID="uxCompanyCityText" runat="server" CssClass="TextBox" />
    <uc5:LanaguageLabelPlus ID="uxPlus3" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyStateHelp" ConfigName="CompanyState" runat="server" />
    <asp:Label ID="lcBusinessState" runat="server" meta:resourcekey="lcBusinessState"
        CssClass="Label" />
    <asp:TextBox ID="uxCompanyStateText" runat="server" CssClass="TextBox" />
    <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyZipHelp" ConfigName="CompanyZip" runat="server" />
    <asp:Label ID="lcBusinessZip" runat="server" meta:resourcekey="lcBusinessZip" CssClass="Label" />
    <asp:TextBox ID="uxCompanyZipText" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyPhoneHelp" ConfigName="CompanyPhone" runat="server" />
    <asp:Label ID="lcBusinessPhone" runat="server" meta:resourcekey="lcBusinessPhone"
        CssClass="Label" />
    <asp:TextBox ID="uxCompanyPhoneText" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyFaxHelp" ConfigName="CompanyFax" runat="server" />
    <asp:Label ID="lcBusinessFax" runat="server" meta:resourcekey="lcBusinessFax" CssClass="Label" />
    <asp:TextBox ID="uxCompanyFaxText" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCompanyEmailHelp" ConfigName="CompanyEmail" runat="server" />
    <asp:Label ID="lcBusinessEmail" runat="server" meta:resourcekey="lcBusinessEmail"
        CssClass="Label" />
    <asp:TextBox ID="uxCompanyEmailText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxCompanyEmailText"
        Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
        <div class="CommonValidateDiv">
        </div>         
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
        ControlToValidate="uxCompanyEmailText" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email format.
        <div class="CommonValidateDiv">
        </div>
    </asp:RegularExpressionValidator>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableMapHelp" ConfigName="EnableMap" runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableMapLabel" runat="server" meta:resourcekey="uxEnableMapLabel" CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableMapDrop" AutoPostBack="true" OnSelectedIndexChanged="uxEnableMapDrop_SelectedIndexChanged"
        runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxLatitudeTextDiv" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxLatitudeHelp" ConfigName="Latitude" runat="server" />
    <asp:Label ID="uxLatitudeLabel" runat="server" meta:resourcekey="uxLatitudeLabel" CssClass="Label" />
    <asp:TextBox ID="uxLatitudeText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredLatitudeValidator" runat="server" ControlToValidate="uxLatitudeText"
        Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Latitude is required.
        <div class="CommonValidateDiv">
        </div>         
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxCompareLatitudeValidator" runat="server" Display="Dynamic"
        ControlToValidate="uxLatitudeText" Operator="DataTypeCheck" Type="Double" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> The input should be a number.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</div>
<div id="uxLongitudeTextDiv" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxLongitudeHelp" ConfigName="Longitude" runat="server" />
    <asp:Label ID="uxLongitudeLabel" runat="server" meta:resourcekey="uxLongitudeLabel" CssClass="Label" />
    <asp:TextBox ID="uxLongitudeText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredLongitudeValidator" runat="server" ControlToValidate="uxLongitudeText"
        Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Longitude is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxCompareLongitudeValidator" runat="server" Display="Dynamic"
        ControlToValidate="uxLongitudeText" Operator="DataTypeCheck" Type="Double" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> The input should be a number.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</div>
