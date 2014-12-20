<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerSubscriptionDetails.ascx.cs"
    Inherits="Admin_Components_CustomerSubscriptionDetails" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildCustomerSubscription" CssClass="ValidationStyle" />
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
                <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" CssClass="Label" />
                <asp:Label ID="uxUserNameLabel" runat="server" CssClass="fl"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcSubscriptionLevel" runat="server" meta:resourcekey="lcSubscriptionLevel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxSubscriptionLevel" runat="server" CssClass="fl DropDown"
                    DataTextField="Name" DataValueField="SubscriptionLevelID">
                </asp:DropDownList>
                <asp:HiddenField ID="uxSubscriptionLevelHidden" runat="server" />
            </div>
            <asp:Panel ID="uxStartDateTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcStartDate" runat="server" meta:resourcekey="lcStartDate" CssClass="Label"></asp:Label>
                <div class="fl">
                    <uc2:CalendarPopup ID="uxStartDateCalendarPopup" TextBoxEnabled="false" runat="server" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxStartDateRequireValidator" runat="server" ControlToValidate="uxStartDateCalendarPopup"
                    meta:resourcekey="uxStartDateRequireValidator" ValidationGroup="VaildCustomerSubscription"
                    Display="Dynamic" EnableClientScript="false">
                        <--
                </asp:RequiredFieldValidator>
            </asp:Panel>
            <asp:Panel ID="uxExpireDateTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcExpireDate" runat="server" meta:resourcekey="lcExpireDate" CssClass="Label"></asp:Label>
                <div class="fl">
                    <uc2:CalendarPopup ID="uxExpireDateCalendarPopup" TextBoxEnabled="false" runat="server" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxExpireDateRequireValidator" runat="server" ControlToValidate="uxExpireDateCalendarPopup"
                    meta:resourcekey="uxExpireDateRequireValidator" ValidationGroup="uxExpireDateRequireValidator"
                    Display="Dynamic" EnableClientScript="false">
                        <--
                </asp:RequiredFieldValidator>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsActive" runat="server" meta:resourcekey="lcIsActive" CssClass="Label" />
                <asp:CheckBox ID="uxIsActiveCheck" runat="server" CssClass="fl CheckBox" />
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildCustomerSubscription" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="VaildCustomerSubscription" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
