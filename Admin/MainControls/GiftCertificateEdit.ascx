<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificateEdit.ascx.cs"
    Inherits="AdminAdvanced_MainControls_GiftCertificateEdit" %>
<%@ Register Src="../Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxContentTemplate" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildGifCertificate" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" CssClass="Label" />
                <asp:Label ID="uxNameLabel" runat="server" CssClass="fl"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcGiftCertificateCode" runat="server" meta:resourcekey="lcGiftCertificateCode"
                    CssClass="Label" />
                <asp:Label ID="uxGiftCertificateCodeLabel" runat="server" CssClass="fl fb"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcGiftValue" runat="server" meta:resourcekey="lcGiftValue" CssClass="Label" />
                <asp:TextBox ID="uxGiftValueText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:CompareValidator ID="uxGiftValueCompare" runat="server" ControlToValidate="uxGiftValueText"
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildGifCertificate"
                    Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Gift Value is invalid.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:CompareValidator>
                <asp:RequiredFieldValidator ID="uxGiftValueRequired" runat="server" ControlToValidate="uxGiftValueText"
                    ValidationGroup="VaildGifCertificate" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Gift Value is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcRemainValue" runat="server" meta:resourcekey="lcRemainValue" CssClass="Label" />
                <asp:TextBox ID="uxRemainValueText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:CompareValidator ID="uxRemainValueCompare" runat="server" ControlToValidate="uxRemainValueText"
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildGifCertificate"
                    Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Remaining Value is invalid.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:CompareValidator>
                <asp:RequiredFieldValidator ID="uxRemainValueRequired" runat="server" ControlToValidate="uxRemainValueText"
                    ValidationGroup="VaildGifCertificate" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Remaining Value is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcRecipient" runat="server" meta:resourcekey="lcRecipient" CssClass="Label" />
                <asp:TextBox ID="uxRecipientText" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcPersonalNote" runat="server" meta:resourcekey="lcPersonalNote" CssClass="Label" />
                <asp:TextBox ID="uxPersonalNote" TextMode="MultiLine" Rows="5" Style="width: 40%;"
                    runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsExpire" runat="server" meta:resourcekey="lcIsExpire" CssClass="Label" />
                <asp:CheckBox ID="uxIsExpireCheck" runat="server" AutoPostBack="true" CssClass="fl CheckBox" />
            </div>
            <asp:Panel ID="uxExpireDateTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcExpireDate" runat="server" meta:resourcekey="lcExpireDate" CssClass="Label"></asp:Label>
                <div class="fl">
                    <uc2:CalendarPopup ID="uxDateCalendarPopup" TextBoxEnabled="false" runat="server" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxDateRequiredValidator" runat="server" ControlToValidate="uxDateCalendarPopup"
                    ValidationGroup="VaildGifCertificate" Display="Dynamic" EnableClientScript="false"
                    CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Date is required.
                <div class="CommonValidateDiv">
                </div>
                </asp:RequiredFieldValidator>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcNeedPhysical" runat="server" meta:resourcekey="lcNeedPhysical" CssClass="Label" />
                <asp:CheckBox ID="uxNeedPhysicalCheck" runat="server" CssClass="fl CheckBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsActive" runat="server" meta:resourcekey="lcIsActive" CssClass="Label" />
                <asp:CheckBox ID="uxIsActiveCheck" runat="server" CssClass="fl CheckBox" />
            </div>
            <div class="CommonRowStyle mgt10">
                <vevo:AdvanceButton ID="uxPrintButton" runat="server" meta:resourcekey="uxPrintButton" CssClassBegin="fr"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange" 
                    OnLoad="uxPrintButton_Load" OnClickGoTo="None" CausesValidation="false"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="VaildGifCertificate">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
