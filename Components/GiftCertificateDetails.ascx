<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificateDetails.ascx.cs"
    Inherits="Components_GiftCertificateDetails" %>
<asp:UpdatePanel ID="uxUpdatePanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:Panel ID="uxGiftCertificateComponentsPanel" runat="server" CssClass="GiftCertificateDetailsPanel">
    <div class="GiftCertificateDetailsTop">
        <asp:Label ID="uxGiftCertificateComponentsTitleLabel" runat="server" Text='<%# GetLanguageText( "GiftCertificateTitle" )%>'
            CssClass="GiftCertificateDetailsTitle"></asp:Label>
    </div>
    <div class="GiftCertificateDetailsLeft">
        <div class="GiftCertificateDetailsRight">
            <p id="uxNeedPhysicalGCP" runat="server" class="GiftCertificateDetailsCheckParagraph">
                <asp:CheckBox ID="uxNeedPhysicalGCCheck" Text="Need physical gift certificate" runat="server"
                    AutoPostBack="true" CssClass="GiftCertificateDetailsNeedPhysicalGCCheck" />
            </p>
            <table class="GiftCertificateDetailsTable">
                <tr id="uxRecipientTR" runat="server">
                    <td class="GiftCertificateDetailsLabelColumn">
                        Recipient
                    </td>
                    <td class="GiftCertificateDetailsInputColumn">
                        <asp:TextBox ID="uxRecipientText" runat="server" Width="150px" CssClass="GiftCertificateDetailsTextBox" />
                    </td>
                </tr>
                <tr id="uxPersonalNoteTR" runat="server">
                    <td class="GiftCertificateDetailsLabelColumn">
                        Personal Note
                    </td>
                    <td class="GiftCertificateDetailsInputColumn">
                        <asp:TextBox ID="uxPersonalNoteText" Rows="5" TextMode="MultiLine" runat="server"
                            CssClass="GiftCertificateDetailsTextBox" />
                    </td>
                </tr>
                <tr id="uxGiftAmountTR" runat="server">
                    <td class="GiftCertificateDetailsLabelColumn">
                        Gift Amount
                    </td>
                    <td class="GiftCertificateDetailsInputColumn">
                        <asp:TextBox ID="uxGiftAmountText" runat="server" Width="150px" CssClass="GiftCertificateDetailsTextBox" />
                        <asp:RequiredFieldValidator ID="uxGiftAmountRequired" runat="server" ErrorMessage="Gift Amount is required."
                            Display="Dynamic" ControlToValidate="uxGiftAmountText" ValidationGroup="ValidOption">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxGiftAmountCompare" runat="server" ErrorMessage="Gift Amount must be number."
                            Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="uxGiftAmountText"
                            ValidationGroup="ValidOption">
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="uxGiftAmountDataTypeCheck" runat="server" ControlToValidate="uxGiftAmountText"
                            ErrorMessage="Gift Amountis invalid" Operator="DataTypeCheck" Type="Currency"
                            ValidationGroup="ValidOption" Display="Dynamic">
                        </asp:CompareValidator>
                    </td>
                </tr>
            </table>
            <div class="GiftCertificateDetailsGiftCheckDiv">
                <asp:CheckBox ID="uxGiftCheck" runat="server" Checked='<%# Eval( "IsGiftCertificate" ) %>'
                    Visible="false" CssClass="GiftCertificateDetailsGiftCheck" />
            </div>
        </div>
    </div>
    <div class="GiftCertificateDetailsBottom">
        <div class="GiftCertificateDetailsBottomLeft">
        </div>
        <div class="GiftCertificateDetailsBottomRight">
        </div>
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
