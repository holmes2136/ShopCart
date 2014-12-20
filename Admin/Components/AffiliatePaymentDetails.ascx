<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePaymentDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_AffiliatePaymentDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidPayment" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" />
                </div>
                <div class="label fl">
                    <asp:Label ID="uxOrderIDLabel" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcAmount" runat="server" meta:resourcekey="lcAmount" />
                </div>
                <asp:TextBox ID="uxAmountText" runat="server" ValidationGroup="ValidPayment" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredAmountValidator" runat="server" ControlToValidate="uxAmountText"
                        meta:resourcekey="uxRequiredAmountValidator" ValidationGroup="ValidPayment"><--</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxAmountCompare" runat="server" ControlToValidate="uxAmountText"
                        ErrorMessage="Amount invalid." Operator="DataTypeCheck" Type="Double" ValidationGroup="ValidPayment"><--</asp:CompareValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcPaidDate" runat="server" meta:resourcekey="lcPaidDate" />
                </div>
                <uc4:CalendarPopup ID="uxCalendarPopup" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcNote" runat="server" meta:resourcekey="lcNote" />
                </div>
                <asp:TextBox ID="uxNoteText" runat="server" Width="300px" TextMode="MultiLine" Rows="5"
                    CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidPayment" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidPayment" />
                <vevo:AdvanceButton ID="uxSendMailButton" runat="server" meta:resourcekey="uxSendMailButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxSendMailButton_Click" OnClickGoTo="Top" ValidationGroup="ValidPayment" />
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
