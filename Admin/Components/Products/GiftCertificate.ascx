<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificate.ascx.cs"
    Inherits="AdminAdvanced_Components_Products_GiftCertificate" %>
<%@ Register Src="~/Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc6" %>
<div>
    <asp:Panel ID="uxGiftCertificateTR" runat="server" CssClass="ProductDetailsRowTitle mgt10">
        <asp:Label ID="lcGiftCertificateHeader" runat="server" meta:resourcekey="lcGiftCertificateHeader" />
    </asp:Panel>
    <asp:Panel ID="uxIsGiftCertificateTR" runat="server" CssClass="ProductDetailsRow">
        <asp:Label ID="lcIsGiftCertificate" runat="server" meta:resourcekey="lcGiftCertificate"
            CssClass="Label" />
        <asp:DropDownList ID="uxIsGiftCertificateDrop" runat="server" AutoPostBack="true"
            CssClass="fl DropDown">
            <asp:ListItem Value="True">Yes</asp:ListItem>
            <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
        </asp:DropDownList>
        <asp:HiddenField ID="uxGiftCertificateStatusHidden" runat="server" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxIsFixedPriceTR" runat="server" CssClass="ProductDetailsRow">
        <asp:Label ID="lcFixedPrice" runat="server" meta:resourcekey="lcFixedPrice" CssClass="Label" />
        <asp:DropDownList ID="uxIsFixedPriceDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxIsFixedPriceDrop_SelectedIndexChanged"
            CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Fixed Value"></asp:ListItem>
            <asp:ListItem Value="False" Text="Enter by Customer"></asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxGiftAmountTR" runat="server" CssClass="ProductDetailsRow">
        <asp:Label ID="lcGiftAmount" runat="server" meta:resourcekey="lcGiftAmount" CssClass="Label" />
        <asp:TextBox ID="uxGiftAmountText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:CompareValidator ID="uxGiftAmountCompare" runat="server" ControlToValidate="uxGiftAmountText"
            Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Gift Amount is invalid.
            <div class="CommonValidateDiv CommonValidateDivProductGiftCertificate">
            </div>  
        </asp:CompareValidator>
        <asp:RequiredFieldValidator ID="uxGiftAmountRequired" runat="server" ControlToValidate="uxGiftAmountText"
            ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Gift Amount is required.
            <div class="CommonValidateDiv CommonValidateDivProductGiftCertificate">
            </div>  
        </asp:RequiredFieldValidator>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxExpirationTypeTR" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="lcExpirationType" runat="server" meta:resourcekey="lcExpirationType"
                CssClass="Label" />
            <div class="fl">
                <div>
                    <asp:RadioButton ID="uxNoExpirationRadio" meta:resourcekey="lcNoExpiration" runat="server"
                        GroupName="ExpireTypeGroup" Checked="true" AutoPostBack="true" /><div class="Clear">
                        </div>
                </div>
                <div>
                    <asp:RadioButton ID="uxFixedDateRadio" meta:resourcekey="lcFixedDateExpire" runat="server"
                        GroupName="ExpireTypeGroup" AutoPostBack="true" />
                    <asp:Panel ID="uxCalendarTD" runat="server" CssClass="validator1 fr">
                        <uc6:CalendarPopup ID="uxFixedDateCalendarPopup" runat="server" />
                        <span class="Asterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxFixedDateRequiredValidator" runat="server" ControlToValidate="uxFixedDateCalendarPopup"
                            ValidationGroup="VaildProduct" Display="Dynamic" EnableClientScript="false" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Date is required.
                            <div class="CommonValidateDiv CommonValidateDivProductGiftCertificate">
                            </div>  
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxFixedDateCompareValidator" runat="server" ControlToValidate="uxFixedDateCalendarPopup"
                            Operator="DataTypeCheck" Type="Date" ValidationGroup="VaildProduct" Display="Dynamic"
                            EnableClientScript="false" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Date is invalid.
                            <div class="CommonValidateDiv CommonValidateDivProductGiftCertificate">
                            </div>  
                        </asp:CompareValidator>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </div>
                <div>
                    <asp:RadioButton ID="uxRollingDateRadio" meta:resourcekey="lcRollingDate" runat="server"
                        AutoPostBack="true" GroupName="ExpireTypeGroup" />
                    <asp:TextBox ID="uxNumberOfDayText" runat="server" Style="float: none; margin-left: 5px;"
                        CssClass="TextBox" Width="100px" />
                    <asp:CompareValidator ID="uxNumberOfDayCompare" runat="server" ControlToValidate="uxNumberOfDayText"
                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="VaildProduct" Display="Dynamic"
                        CssClass="CommonValidatorText CommonValidatorTextProductGiftCert">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of Expire Date must be a Natural Number.
                        <div class="CommonValidateDiv CommonValidateDivProductGiftCert">
                        </div>  
                    </asp:CompareValidator>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="uxIsElectronicTR" runat="server" CssClass="ProductDetailsRow">
        <asp:Label ID="lcIsElectronic" runat="server" meta:resourcekey="lcIsElectronic" CssClass="Label" />
        <asp:CheckBox ID="uxIsElectronicCheck" runat="server" CssClass="fl" />
        <div class="Clear">
        </div>
    </asp:Panel>
</div>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
