<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteConfig.ascx.cs" Inherits="AdminAdvanced_MainControls_SiteConfig" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Boxset/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/SiteConfig/TaxConfiguration.ascx" TagName="TaxConfig"
    TagPrefix="uc7" %>
<%@ Register Src="../Components/SiteConfig/DisplayConfig.ascx" TagName="DisplayConfig"
    TagPrefix="uc8" %>
<%@ Register Src="../Components/SiteConfig/Wholesale.ascx" TagName="Wholesale" TagPrefix="uc9" %>
<%@ Register Src="../Components/SiteConfig/RatingReview.ascx" TagName="RatingReview"
    TagPrefix="uc10" %>
<%@ Register Src="../Components/SiteConfig/GiftCertificate.ascx" TagName="GiftCertificate"
    TagPrefix="uc12" %>
<%@ Register Src="../Components/SiteConfig/ProductImageSizeConfig.ascx" TagName="ProductImageSizeConfig"
    TagPrefix="uc13" %>
<%@ Register Src="../Components/SiteConfig/EmailNotification.ascx" TagName="EmailNotification"
    TagPrefix="uc15" %>
<%@ Register Src="../Components/SiteConfig/UploadConfig.ascx" TagName="UploadConfig"
    TagPrefix="uc16" %>
<%@ Register Src="../Components/SiteConfig/Seo.ascx" TagName="Seo" TagPrefix="uc17" %>
<%@ Register Src="../Components/SiteConfig/SystemConfig.ascx" TagName="SystemConfig"
    TagPrefix="uc18" %>
<%@ Register Src="../Components/SiteConfig/AffiliateConfig.ascx" TagName="AffiliateConfig"
    TagPrefix="uc19" %>
<%@ Register Src="../Components/SiteConfig/ShippingTracking.ascx" TagName="ShippingTracking"
    TagPrefix="uc22" %>
<%@ Register Src="../Components/SiteConfig/EBayConfig.ascx" TagName="eBayConfig"
    TagPrefix="uc26" %>
<%@ Register Src="../Components/Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc23" %>
<%@ Register Src="../Components/Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc24" %>
<%@ Register Src="../Components/SiteConfig/DownloadCountConfig.ascx" TagName="DownloadCount"
    TagPrefix="uc25" %>
<uc1:AdminContent ID="uxContentTemplate" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <asp:Panel ID="SiteConfigSearchPanel" runat="server">
            <div class="SiteConfigSearch">
                <asp:Label ID="lcSearchConfig" runat="server" meta:resourcekey="lcSearchConfig" CssClass="Label"></asp:Label>
                <asp:TextBox ID="uxSearchText" runat="server" CssClass="TextBox" />
                <vevo:AdvanceButton ID="uxSearchButton" runat="server" meta:resourcekey="uxSearchButton"
                    CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSearchButton_Click" /></div>
        </asp:Panel>
    </FilterTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" HeaderText="<div class='Title'>Please correct the following errors :</div>"
            ValidationGroup="SiteConfigValid" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <span class="Asterisk">*</span>
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
    </ValidationDenotesTemplate>
    <PlainContentTemplate>
        <uc2:BoxSet ID="uxBoxSetMain" runat="server" CssClass="SiteConfigBoxSet">
            <ContentTemplate>
                <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer">
                    <ajaxToolkit:TabPanel ID="uxDisplayTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcDisplayHeader" runat="server" meta:resourcekey="lcDisplayHeader" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc8:DisplayConfig ID="uxDisplay" runat="server" />
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcProductImage" runat="server" meta:resourcekey="lcProductImage" /></div>
                                <uc13:ProductImageSizeConfig ID="uxProductImageSizeConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxRatingTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcReviewRatingHeader" runat="server" meta:resourcekey="lcReviewRatingHeader" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc10:RatingReview ID="uxRatingReview" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxTaxTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcTaxHeader" runat="server" meta:resourcekey="lcTaxHeader" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc7:TaxConfig ID="uxTaxConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxAffiliateTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcAffiliateConfig" runat="server" meta:resourcekey="lcAffiliateConfig" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc19:AffiliateConfig ID="uxAffiliateConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxShippingTrackingTab" runat="server" CssClass="DefaultTabPanel">
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc22:ShippingTracking ID="uxShippingTracking" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcShippingTrackingConfig" runat="server" meta:resourcekey="lcShippingTrackingConfig" /></div>
                        </HeaderTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxeBayConfigTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lceBayConfig" runat="server" meta:resourcekey="lceBayConfig" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc26:eBayConfig ID="uxeBayConfig" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxOtherTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcOtherConfig" runat="server" meta:resourcekey="lcOtherConfig" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <div class="CommonConfigTitle  mgt0">
                                    <asp:Label ID="lcGiftCertificate" runat="server" meta:resourcekey="lcGiftCertificate" /></div>
                                <uc12:GiftCertificate ID="uxGiftCertificate" runat="server" />
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcWholesaleHead" runat="server" meta:resourcekey="lcWholesaleHead" /></div>
                                <uc9:Wholesale ID="uxWholesale" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcEmailNotification" runat="server" meta:resourcekey="lcEmailNotification" /></div>
                                <uc15:EmailNotification ID="uxEmailNotification" runat="server" />
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcUploadSetting" runat="server" meta:resourcekey="lcUploadSetting" /></div>
                                <uc16:UploadConfig ID="uxUploadConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                                <asp:Panel ID="uxSiteMaintenancePanel" runat="server">
                                    <div class="CommonConfigTitle">
                                        <asp:Label ID="uxSiteMaintenance" runat="server" meta:resourcekey="lcSystemMaintenance" /></div>
                                    <div class="ConfigRow">
                                        <uc24:HelpIcon ID="uxDeleteFileTempHelp" HelpKeyName="DeleteAllFileInTempFolder"
                                            runat="server" />
                                        <asp:Label ID="uxDeleteFileTemp" runat="server" meta:resourcekey="lcDeleteFileTemp"
                                            CssClass="Label" />
                                        <vevo:AdvanceButton ID="uxDeleteFileTempButton" runat="server" meta:resourcekey="uxDeleteFileTempButton"
                                            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxDeleteFileTempButton_Click"
                                            OnClickGoTo="Top"></vevo:AdvanceButton>
                                        <asp:Button ID="uxDummyDeleteFileTempButton" runat="server" Text="" CssClass="dn" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="uxDeleteConfirmButton" runat="server" TargetControlID="uxDeleteFileTempButton"
                                            ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
                                        </ajaxToolkit:ConfirmButtonExtender>
                                        <ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxDeleteFileTempButton"
                                            CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
                                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                                            SkinID="ConfirmPanel">
                                            <div class="ConfirmTitle">
                                                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:SiteConfigMessages, ConfirmDeleteTempFile %>"></asp:Label></div>
                                            <div class="ConfirmButton mgt10">
                                                <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                                                </vevo:AdvanceButton>
                                                <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                                                </vevo:AdvanceButton>
                                                <div class="Clear">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="ConfigRow">
                                        <uc24:HelpIcon ID="uxDeleteSearchHelp" HelpKeyName="DeleteAllSearchInDatabase" runat="server" />
                                        <asp:Label ID="lcDeleteSearch" runat="server" meta:resourcekey="lcDeleteSearch" CssClass="Label" />
                                        <vevo:AdvanceButton ID="uxDeleteSearchButton" runat="server" meta:resourcekey="uxDeleteSearchButton"
                                            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxDeleteSearchButton_Click"
                                            OnClickGoTo="Top"></vevo:AdvanceButton>
                                        <asp:Button ID="uxDummyDeleteSearchButton" runat="server" Text="" CssClass="dn" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="uxDeleteSearchConfirmButton" runat="server"
                                            TargetControlID="uxDeleteSearchButton" ConfirmText="" DisplayModalPopupID="uxSearchModalPopup">
                                        </ajaxToolkit:ConfirmButtonExtender>
                                        <ajaxToolkit:ModalPopupExtender ID="uxSearchModalPopup" runat="server" TargetControlID="uxDeleteSearchButton"
                                            CancelControlID="uxSearchCancelButton" OkControlID="uxSearchOkButton" PopupControlID="uxSearchConfirmPanel"
                                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="uxSearchConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                                            SkinID="ConfirmPanel">
                                            <div class="ConfirmTitle">
                                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:SiteConfigMessages, ConfirmDeleteSearch %>"></asp:Label></div>
                                            <div class="ConfirmButton mgt10">
                                                <vevo:AdvanceButton ID="uxSearchOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                                                </vevo:AdvanceButton>
                                                <vevo:AdvanceButton ID="uxSearchCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                                                </vevo:AdvanceButton>
                                                <div class="Clear">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="Clear">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcSeoSetting" runat="server" meta:resourcekey="lcSeoSetting" /></div>
                                <uc17:Seo ID="uxSeo" runat="server" />
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="lcSystemConfig" runat="server" meta:resourcekey="lcSystemConfig" /></div>
                                <uc18:SystemConfig ID="uxSystemConfig" runat="server" />
                                <asp:Panel ID="uxPaymentTimeZoneSettingPanel" runat="server">
                                    <div class="CommonConfigTitle">
                                        <asp:Label ID="uxPaymentTimeZone" runat="server" meta:resourcekey="uxPaymentTimeZoneLabel" /></div>
                                    <div class="ConfigRow">
                                        <uc24:HelpIcon ID="uxUseTimeZoneHelp" ConfigName="UseCustomTimeZone" runat="server" />
                                        <div class="Label">
                                            <asp:Label ID="uxUseTimeZoneLabel" runat="server" meta:resourcekey="lcUseTimeZoneLabel"
                                                CssClass="fl" />
                                        </div>
                                        <asp:DropDownList ID="uxUseTimeZoneDrop" runat="server" CssClass="fl DropDown">
                                            <asp:ListItem Value="True" Text="Yes" />
                                            <asp:ListItem Value="False" Text="No" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="ConfigRow">
                                        <uc24:HelpIcon ID="uxTimeZoneHelp" ConfigName="CustomTimeZone" runat="server" />
                                        <div class="BulletLabel">
                                            <asp:Label ID="uxTimeZoneLabel" runat="server" meta:resourcekey="lcTimeZoneLabel"
                                                CssClass="fl" />
                                        </div>
                                        <asp:DropDownList ID="uxCustomTimeZoneDrop" runat="server" CssClass="fl DropDown">
                                            <asp:ListItem Value="-12.00" Text="(GMT -12:00) Eniwetok, Kwajalein" />
                                            <asp:ListItem Value="-11.00" Text="(GMT -11:00) Midway Island, Samoa" />
                                            <asp:ListItem Value="-10.00" Text="(GMT -10:00) Hawaii" />
                                            <asp:ListItem Value="-9.00" Text="(GMT -9:00) Alaska" />
                                            <asp:ListItem Value="-8.00" Text="(GMT -8:00) Pacific Time (US &amp; Canada)" />
                                            <asp:ListItem Value="-7.00" Text="(GMT -7:00) Mountain Time (US &amp; Canada)" />
                                            <asp:ListItem Value="-6.00" Text="(GMT -6:00) Central Time (US &amp; Canada), Mexico City" />
                                            <asp:ListItem Value="-5.00" Text="(GMT -5:00) Eastern Time (US &amp; Canada), Bogota, Lima" />
                                            <asp:ListItem Value="-4.00" Text="(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz" />
                                            <asp:ListItem Value="-3.30" Text="(GMT -3:30) Newfoundland" />
                                            <asp:ListItem Value="-3.00" Text="(GMT -3:00) Brazil, Buenos Aires, Georgetown" />
                                            <asp:ListItem Value="-2.00" Text="(GMT -2:00) Mid-Atlantic" />
                                            <asp:ListItem Value="-1.00" Text="(GMT -1:00 hour) Azores, Cape Verde Islands" />
                                            <asp:ListItem Value="0.00" Text="(GMT) Western Europe Time, London, Lisbon, Casablanca" />
                                            <asp:ListItem Value="1.00" Text="(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris" />
                                            <asp:ListItem Value="2.00" Text="(GMT +2:00) Kaliningrad, South Africa" />
                                            <asp:ListItem Value="3.00" Text="(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg" />
                                            <asp:ListItem Value="3.30" Text="(GMT +3:30) Tehran" />
                                            <asp:ListItem Value="4.00" Text="(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi" />
                                            <asp:ListItem Value="4.30" Text="(GMT +4:30) Kabul" />
                                            <asp:ListItem Value="5.00" Text="(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent" />
                                            <asp:ListItem Value="5.30" Text="(GMT +5:30) Bombay, Calcutta, Madras, New Delhi" />
                                            <asp:ListItem Value="5.45" Text="(GMT +5:45) Kathmandu" />
                                            <asp:ListItem Value="6.00" Text="(GMT +6:00) Almaty, Dhaka, Colombo" />
                                            <asp:ListItem Value="7.00" Text="(GMT +7:00) Bangkok, Hanoi, Jakarta" />
                                            <asp:ListItem Value="8.00" Text="(GMT +8:00) Beijing, Perth, Singapore, Hong Kong" />
                                            <asp:ListItem Value="9.00" Text="(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk" />
                                            <asp:ListItem Value="9.30" Text="(GMT +9:30) Adelaide, Darwin" />
                                            <asp:ListItem Value="10.00" Text="(GMT +10:00) Eastern Australia, Guam, Vladivostok" />
                                            <asp:ListItem Value="11.00" Text="(GMT +11:00) Magadan, Solomon Islands, New Caledonia" />
                                            <asp:ListItem Value="12.00" Text="(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka" />
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="uxDownloadExpireLabel" runat="server" meta:resourcekey="uxDownloadExpireLabel" /></div>
                                <div id="uxDownloadExpireSettingTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxDownloadExpirePeriodHelp" ConfigName="DownloadExpirePeriod"
                                        runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="uxDownloadExpirePeriodLabel" runat="server" meta:resourcekey="uxDownloadExpirePeriodLabel"
                                            CssClass="fl" />
                                    </div>
                                    <asp:TextBox ID="uxDownloadExpirePeriodText" runat="server" CssClass="TextBox" />
                                    <div class="validator1 fl">
                                        <span class="Asterisk">*</span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="uxDownloadExpiredPeriodRequired" runat="server" ControlToValidate="uxDownloadExpirePeriodText"
                                        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
                                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" />
                                        Download Link Expiration (day) is required.
                                    <div class="CommonValidateDiv">
                                    </div>
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="uxDownloadExpirePeriodCompareWithZero" runat="server" ControlToValidate="uxDownloadExpirePeriodText"
                                        Display="Dynamic" Type="Integer" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="SiteConfigValid"
                                        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> 
                Download Link Expiration (day) must be an Integer and greater than zero(0).
        <div class="CommonValidateDiv">
        </div>
                                    </asp:CompareValidator>
                                    <uc25:DownloadCount ID="uxDownloadCount" runat="server" />
                                </div>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="uxSettingAnonymousCheckoutLabel" runat="server" meta:resourcekey="uxSettingAnonymousCheckoutLabel" /></div>
                                <div id="uxAnonymousCheckoutSettingTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxAllowAnonymousCheckoutHelp" ConfigName="AnonymousCheckoutAllowed"
                                        runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="uxAllowAnonymousCheckoutLabel" runat="server" meta:resourcekey="uxAllowAnonymousCheckoutLabel"
                                            CssClass="fl" />
                                    </div>
                                    <asp:DropDownList ID="uxAllowAnonymousCheckoutDropDown" runat="server" CssClass="fl DropDown">
                                        <asp:ListItem Value="True" Text="Yes" />
                                        <asp:ListItem Value="False" Text="No" />
                                    </asp:DropDownList>
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="uxShippingSettingLabel" runat="server" meta:resourcekey="lcShippingSettingLabel" /></div>
                                <div id="uxShippingSettingTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxShippingMerchantHelp" ConfigName="ShippingMerchantCountry" runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="uxShippingMerchantLabel" runat="server" meta:resourcekey="lcShippingMerchantLabel"
                                            CssClass="fl" />
                                    </div>
                                    <div class="ConfigCountry">
                                        <uc23:CountryList ID="uxMerchantCountryList" runat="server" />
                                    </div>
                                </div>
                                <div id="uxShippingIncludeDiscountTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxShippingIncludeDiscountHelp" ConfigName="ShippingIncludeDiscount"
                                        runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="lcShippingIncludeDiscount" runat="server" meta:resourcekey="lcShippingIncludeDiscount"
                                            CssClass="fl">
                                        </asp:Label>
                                    </div>
                                    <asp:DropDownList ID="uxShippingIncludeDiscountDrop" runat="server" CssClass="fl DropDown">
                                        <asp:ListItem Value="True" Text="Yes" />
                                        <asp:ListItem Value="False" Text="No" />
                                    </asp:DropDownList>
                                </div>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="uxCurrencySettingLabel" runat="server" Text="Currency" /></div>
                                <div id="uxCurrencySettingTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxSetCurrencyDefaultHelp" ConfigName="BaseWebsiteCurrency" runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="uxSetCurrencyDefault" runat="server" Text="Set Default Currency" CssClass="fl"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="uxSymbolDrop" runat="server" DataTextField="Name" DataValueField="CurrencyCode"
                                        CssClass="DropDown">
                                    </asp:DropDownList>
                                </div>
                                <div class="CommonConfigTitle">
                                    <asp:Label ID="uxWebServiceSettingLabel" runat="server" Text="Web Service" /></div>
                                <div id="uxWebServiceSettingTR" runat="server" class="ConfigRow">
                                    <uc24:HelpIcon ID="uxWebServiceAdminUserHelp" ConfigName="WebServiceAdminUser" runat="server" />
                                    <div class="Label">
                                        <asp:Label ID="uxWebServiceAdminUserLabel" runat="server" Text="Web Service Admin Account"
                                            CssClass="fl"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="uxWebServiceAdminUserText" runat="server" CssClass="TextBox" />
                                </div>
                                <div class="Clear">
                                </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <div class="ConfigUpdetButton">
                    <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                        CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                        OnClickGoTo="Top" ValidationGroup="SiteConfigValid"></vevo:AdvanceButton>
                    <div class="Clear">
                    </div>
                </div>
            </ContentTemplate>
        </uc2:BoxSet>
    </PlainContentTemplate>
</uc1:AdminContent>
