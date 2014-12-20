<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReviewDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_ProductReviewDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildProduct" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc2:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcProductNameLabel" runat="server" meta:resourcekey="lcProductName"></asp:Label>
                </div>
                <div class="AdminName">
                    <asp:Label ID="uxProductNameLabel" runat="server" />
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName"></asp:Label>
                </div>
                <asp:TextBox ID="uxCustomerID" runat="server" Width="252px" CssClass="fl TextBox"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcReviewRating" runat="server" meta:resourcekey="lcReviewRating"></asp:Label>
                </div>
                <asp:TextBox ID="uxReviewRating" runat="server" Width="94px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxReviewRatingRequiredValidator" runat="server" ControlToValidate="uxReviewRating"
                    ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" />
                    <asp:Label ID="uxReviewRatingRequiredLabel" runat="server" meta:resourcekey="uxReviewRatingRequiredLabel"></asp:Label>
                    <div class="CommonValidateDiv CommonValidateDivReviewRating">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxReviewRatingCompare" runat="server" ControlToValidate="uxReviewRating"
                    Operator="GreaterThan" ValueToCompare="0" Type="Integer" ValidationGroup="VaildProduct"
                    Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Natural Number and greater than zero(0).
                    <div class="CommonValidateDiv CommonValidateDivReviewRating">
                    </div>
                </asp:CompareValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcSubject" runat="server" meta:resourcekey="lcSubject"></asp:Label>
                </div>
                <asp:TextBox ID="uxSubject" runat="server" Width="251px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxSubjectRequiredValidator" runat="server" ControlToValidate="uxSubject"
                    ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" />
                    <asp:Label ID="uxSubjectRequiredLabel" runat="server" meta:resourcekey="uxSubjectRequiredLabel"></asp:Label>
                    <div class="CommonValidateDiv CommonValidateDivReviewSubject">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcBody" runat="server" meta:resourcekey="lcBody"></asp:Label>
                </div>
                <uc6:TextEditor ID="uxLongDescriptionText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
            </div>
            <asp:Panel ID="uxDateTimeTR" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcDateTime" runat="server" meta:resourcekey="lcDateTime"></asp:Label>
                </div>
                <uc4:CalendarPopup ID="uxCalendar" runat="server" TextBoxEnabled="false" />
                <asp:TextBox ID="uxDateText" runat="server" Width="328px" CssClass="fl TextBox"></asp:TextBox>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcEnabled" runat="server" meta:resourcekey="lcEnabled"></asp:Label>
                </div>
                <asp:CheckBox ID="uxReviewCheck" runat="server" CssClass="fl" />
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    ValidationGroup="VaildProduct" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
                <vevo:AdvanceButton ID="uxEditButton" runat="server" meta:resourcekey="uxEditButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    ValidationGroup="VaildProduct" OnClick="uxEditButton_Click" OnClickGoTo="Top" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
