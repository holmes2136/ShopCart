<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentAppUrl.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_PaymentAppUrl" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxPaymentAppUrlHelp" ConfigName="PaymentAppUrl" runat="server" />
    <div class="Label">
        <asp:Label ID="lcPaymentAppUrl" CssClass="fl" runat="server" meta:resourcekey="lcPaymentAppUrl"></asp:Label>
    </div>
    <asp:TextBox ID="uxPaymentAppUrlText" runat="server" CssClass="TextBox" Width="200px">
    </asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxPaymentAppUrlTextRequired" runat="server" ControlToValidate="uxPaymentAppUrlText"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText"><img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Payment Application Url is required.
                    <div class="CommonValidateDiv CommonValidateDivPaymentUrl"></div></asp:RequiredFieldValidator>
</div>
<div class="ConfigRow">
    <div class="Label fr">
        &nbsp;
    </div>
    <div style="color: Red; float: left; margin-left: 20px; width: 250px;">
        (e.g. vevopayment.[YourWebsiteUrl].com, www.[YourWebsiteUrl].com/vevopayment)
    </div>
</div>
