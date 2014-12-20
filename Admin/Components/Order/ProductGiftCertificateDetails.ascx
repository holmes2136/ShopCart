<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductGiftCertificateDetails.ascx.cs"
    Inherits="Admin_Components_Order_ProductGiftCertificateDetails" %>
<asp:Panel ID="uxGiftCertificateComponentsPanel" runat="server">
    <p id="uxNeedPhysicalGCP" runat="server" class="GiftCertificateDetailsCheckParagraph">
        <%-- <asp:CheckBox ID="uxNeedPhysicalGCCheck" Text="Need physical gift certificate" runat="server"
                 OnCheckedChanged = "uxNeedPhysicalGCCheck_OnCheckedChanged"
                    AutoPostBack="true" CssClass="GiftCertificateDetailsNeedPhysicalGCCheck" />--%>
        *If you need physical gift certificate, please enter Recipient and Personal Note
        details.
    </p>
    <asp:Panel ID="uxRecipientTR" CssClass="ProductDetailsRow" runat="server">
        <div class="BulletLabel">
            <asp:Label ID="uxRecipientLabel" runat="server" Text="Recipient" CssClass="fl" />
        </div>
        <asp:TextBox ID="uxRecipientText" runat="server" Width="210px" CssClass="TextBox" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxPersonalNoteTR" CssClass="ProductDetailsRow" runat="server">
        <div class="BulletLabel">
            <asp:Label ID="uxPersonalNoteLabel" runat="server" Text="Personal Note" CssClass="fl" />
        </div>
        <asp:TextBox ID="uxPersonalNoteText" Rows="5" Width="210px" TextMode="MultiLine"
            runat="server" CssClass="TextBox" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxGiftAmountTR" CssClass="ProductDetailsRow" runat="server">
        <div class="BulletLabel">
            <asp:Label ID="uxGiftAmountLabel" runat="server" Text="Gift Amount" CssClass="fl" />
        </div>
        <asp:TextBox ID="uxGiftAmountText" runat="server" Width="210px" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
            <asp:RequiredFieldValidator ID="uxGiftAmountRequired" runat="server" ErrorMessage="Gift Amount is required."
                Display="Dynamic" ControlToValidate="uxGiftAmountText" ValidationGroup="VaildProduct">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxGiftAmountCompare" runat="server" ErrorMessage="Gift Amount must be number."
                Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="uxGiftAmountText"
                ValidationGroup="VaildProduct">
            </asp:CompareValidator>
            <asp:CompareValidator ID="uxGiftAmountDataTypeCheck" runat="server" ControlToValidate="uxGiftAmountText"
                ErrorMessage="Gift Amountis invalid" Operator="DataTypeCheck" Type="Currency"
                ValidationGroup="VaildProduct" Display="Dynamic">
            </asp:CompareValidator>
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <div class="GiftCertificateDetailsGiftCheckDiv">
        <asp:CheckBox ID="uxGiftCheck" runat="server" Checked='<%# Eval( "IsGiftCertificate" ) %>'
            Visible="false" CssClass="GiftCertificateDetailsGiftCheck" />
    </div>
</asp:Panel>
