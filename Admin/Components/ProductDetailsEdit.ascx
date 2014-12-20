<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetailsEdit.ascx.cs"
    Inherits="AdminAdvanced_Components_ProductDetailsEdit" %>
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
<%@ Register Src="Products/ProductInfo.ascx" TagName="ProductInfo" TagPrefix="uc20" %>
<%@ Register Src="Products/ProductSeo.ascx" TagName="ProductSeo" TagPrefix="uc21" %>
<%@ Register Src="Products/DepartmentInfo.ascx" TagName="DepartmentInfo" TagPrefix="uc25" %>
<%@ Register Src="Products/ImageList.ascx" TagName="ProductImage" TagPrefix="uc22" %>
<%@ Register Src="Products/Recurring.ascx" TagName="Recurring" TagPrefix="uc23" %>
<%@ Register Src="Products/GiftCertificate.ascx" TagName="GiftCertificate" TagPrefix="uc24" %>
<%@ Register Src="Products/ProductAttributes.ascx" TagName="ProductAttributes" TagPrefix="uc24" %>
<%@ Register Src="StoreDropDownList.ascx" TagName="StoreDropDownList" TagPrefix="uc10" %>
<%@ Register Src="Products/ProductLink.ascx" TagName="ProductLink" TagPrefix="uc26" %>
<%@ Register Src="Products/ProductSubscription.ascx" TagName="ProductSubscription"
    TagPrefix="uc25" %>
<%@ Register Src="Products/ProductKit.ascx" TagName="ProductKit" TagPrefix="uc27" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildProduct" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxReviewLink" runat="server" meta:resourcekey="uxReviewLink"
            OnClick="ChangePage_Click" StatusBarText="Review Products" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <ButtonCommandTemplate>
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
            </div>
        </asp:Panel>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click" />
        <asp:Button ID="uxDummyButton1" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxDeleteConfirmButton" runat="server" TargetControlID="uxDeleteButton"
            ConfirmText="" DisplayModalPopupID="uxConfirmDeleteModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxConfirmDeleteModalPopup" runat="server" TargetControlID="uxDeleteButton"
            CancelControlID="uxCancelDeleteButton" OkControlID="uxOkDeleteButton" PopupControlID="uxConfirmDeletePanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxConfirmDeletePanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ProductMessages, DeleteConfirmation %>"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxOkDeleteButton" runat="server" meta:resourcekey="uxOkDeleteButton"
                    CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                    OnClickGoTo="Top" />
                <vevo:AdvanceButton ID="uxCancelDeleteButton" runat="server" meta:resourcekey="uxCancelDeleteButton"
                    CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                    OnClickGoTo="None" />
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <asp:Label ID="uxStoreViewLabel" runat="server" meta:resourcekey="uxStoreViewLabel"></asp:Label>
        <uc10:StoreDropDownList ID="uxStoreList" runat="server" AutoPostBack="True" OnBubbleEvent="uxStoreList_RefreshHandler"
            FirstItemText="Default Value" FirstItemValue="0" />
    </PageNumberTemplate>
    <TopContentBoxTemplate>
        <div id="uxProductIDTR" runat="server" class="fl">
            <asp:Label ID="lcProductID" runat="server" meta:resourcekey="lcProductID"></asp:Label>
            <%=CurrentID %>
        </div>
        <div class="fr">
            <div class="ProductDetailsValidationDenote">
                <div class="Label">
                    <asp:Label ID="lcFieldMutipleLanguage" runat="server" meta:resourcekey="lcFieldMutipleLanguage" /></div>
                <div class="RequiredLabel c6">
                    <span class="Asterisk">*</span>
                    <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
            </div>
        </div>
    </TopContentBoxTemplate>
    <BottomContentBoxTemplate>
        <uc1:BoxSet ID="uxBoxSetMain" runat="server" CssClass="SiteConfigBoxSet">
            <ContentTemplate>
                <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer">
                    <ajaxToolkit:TabPanel ID="uxProductInfoTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductInfo" runat="server" Text="Product Information" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc20:ProductInfo ID="uxProductInfo" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductAttributeTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductAttribute" runat="server" meta:resourcekey="lcProductAttribute" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc24:ProductAttributes ID="uxProductAttributes" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductImagesTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductImage" runat="server" Text="Images" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc22:ProductImage ID="uxProductImageList" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductDepartmentTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcDepartmentInfo" runat="server" Text="Department" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc25:DepartmentInfo ID="uxDepartmentInfo" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductSeoTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductSeo" runat="server" Text="SEO" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc21:ProductSeo ID="uxProductSeo" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductKitTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductKit" runat="server" Text="Product Kit" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc27:ProductKit ID="uxProductKit" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxProductOthersTabPanel" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProductOthers" runat="server" Text="Others" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div id="ucProductSubscriptionTR" runat="server">
                                <uc25:ProductSubscription ID="uxProductSubscription" runat="server" />
                            </div>
                            <uc23:Recurring ID="uxRecurring" runat="server" />
                            <uc24:GiftCertificate ID="uxGiftCertificate" runat="server" />
                            <uc26:ProductLink ID="uxProductLink" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </ContentTemplate>
        </uc1:BoxSet>
        <div class="Clear" />
        <div class="mgt10">
            <vevo:AdvanceButton ID="uxEditButton" runat="server" meta:resourcekey="uxEditButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxEditButton_Click" OnClickGoTo="Top" ValidationGroup="VaildProduct" />
        </div>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
