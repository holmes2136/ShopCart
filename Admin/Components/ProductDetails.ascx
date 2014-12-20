<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_ProductDetails" %>
<%@ Register Src="~/Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc6" %>
<%@ Register Src="QuantityDiscount.ascx" TagName="QuantityDiscount" TagPrefix="uc5" %>
<%@ Register Src="MultiCatalog.ascx" TagName="MultiCatalog" TagPrefix="uc1" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Products/ImageList.ascx" TagName="ProductImage" TagPrefix="uc22" %>
<%@ Register Src="Products/ProductInfo.ascx" TagName="ProductInfo" TagPrefix="uc20" %>
<%@ Register Src="Products/Recurring.ascx" TagName="Recurring" TagPrefix="uc22" %>
<%@ Register Src="Products/GiftCertificate.ascx" TagName="GiftCertificate" TagPrefix="uc23" %>
<%@ Register Src="Products/ProductAttributes.ascx" TagName="ProductAttributes" TagPrefix="uc24" %>
<%@ Register Src="Products/ProductSubscription.ascx" TagName="ProductSubscription"
    TagPrefix="uc25" %>
<%@ Register Src="Products/ProductKit.ascx" TagName="ProductKit" TagPrefix="uc26" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildProduct" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxLastAddLink" runat="server" meta:resourcekey="lcLastAddProduct"
            OnClick="ChangePage_Click" StatusBarText="Last Product Add." CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
        <vevo:AdvancedLinkButton ID="uxReviewLink" runat="server" meta:resourcekey="uxReviewLink"
            OnClick="ChangePage_Click" StatusBarText="Review Products" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
        <vevo:AdvancedLinkButton ID="uxImageListLink" runat="server" CssClass="CommonAdminButtonIcon AdminButtonIconView fl"
            meta:resourcekey="lcProductImageList" OnClick="ChangePage_Click" StatusBarText="Image List" /><div
                class="Clear">
            </div>
    </ButtonEventTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <span class="Asterisk">*</span>
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
        </div>
        <asp:Label ID="lcFieldMutipleLanguage" runat="server" meta:resourcekey="lcFieldMutipleLanguage"
            CssClass="Label" /><div class="Clear">
            </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div>
            <vevo:AdvanceButton ID="uxCopyButton" runat="server" meta:resourcekey="uxCopyButton"
                CssClass="AdminButtonCopy CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
                ShowText="true" OnClick="uxCopyButton_Click" OnClickGoTo="Top" ValidationGroup="VaildProduct" />
            <asp:Button ID="uxDummyButton" runat="server" Text="" CssClass="dn" />
            <ajaxToolkit:ConfirmButtonExtender ID="uxCopyConfirmButton" runat="server" TargetControlID="uxCopyButton"
                ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
            </ajaxToolkit:ConfirmButtonExtender>
            <ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxCopyButton"
                CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
                BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                SkinID="ConfirmPanel">
                <div class="ConfirmTitle">
                    <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductMessages, ProductCopy %>"
                        CssClass="Label" />
                    <div class="ConfirmButton mgt10">
                        <vevo:AdvanceButton ID="uxOkButton" runat="server" meta:resourcekey="uxOkButton"
                            CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            OnClickGoTo="Top" />
                        <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCancelButton"
                            CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            OnClickGoTo="None" />
                    </div>
            </asp:Panel>
        </div>
        <div id="uxProductIDTR" runat="server" class="CommonTextTitle mgt10">
            <asp:Label ID="lcProductID" runat="server" meta:resourcekey="lcProductID"></asp:Label>
            <%=CurrentID %>
        </div>
        <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer Container-Box1">
            <ajaxToolkit:TabPanel ID="uxProductInfoTabPanel" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <asp:Label ID="lcProductInformation" runat="server" Text="Product Information" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <uc20:ProductInfo ID="uxProductInfo" runat="server" />
                    </div>
                    <div>
                        <uc26:ProductKit ID="uxProductKit" runat="server" />
                    </div>
                    <div id="ucProductSubscriptionTR" runat="server">
                        <uc25:ProductSubscription ID="uxProductSubscription" runat="server" />
                    </div>
                    <div>
                        <uc22:Recurring ID="uxRecurring" runat="server" />
                    </div>
                    <div>
                        <uc23:GiftCertificate ID="uxGiftCertificate" runat="server" />
                    </div>
                    <div>
                        <uc24:ProductAttributes ID="uxProductAttributes" runat="server" />
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="uxProductImagesTabPanel" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <div>
                        <asp:Label ID="lcProductImage" runat="server" Text="Images" />
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <uc22:ProductImage ID="uxProductImageList" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <div class="mgt10">
            <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildProduct">
            </vevo:AdvanceButton>
            <vevo:AdvanceButton ID="uxEditButton" runat="server" meta:resourcekey="uxEditButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxEditButton_Click" OnClickGoTo="Top" ValidationGroup="VaildProduct">
            </vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
