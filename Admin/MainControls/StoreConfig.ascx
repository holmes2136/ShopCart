<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreConfig.ascx.cs" Inherits="Admin_MainControls_StoreConfig" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Boxset/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/StoreConfig/StoreLayoutConfig.ascx" TagName="StoreLayoutConfig"
    TagPrefix="uc11" %>
<%@ Register Src="../Components/StoreConfig/StoreDisplayConfig.ascx" TagName="StoreDisplayConfig"
    TagPrefix="uc8" %>
<%@ Register Src="../Components/StoreConfig/StoreProfileConfig.ascx" TagName="StoreProfileConfig"
    TagPrefix="uc14" %>
<%@ Register Src="../Components/SiteSetup/EmailSetup.ascx" TagName="EmailSetup" TagPrefix="uc10" %>
<%@ Register Src="../Components/StoreConfig/StoreFacebookConfig.ascx" TagName="StoreFacebookConfig"
    TagPrefix="uc12" %>
<%@ Register Src="../Components/StoreConfig/StoreBlogConfig.ascx" TagName="StoreBlogConfig"
    TagPrefix="uc13" %>
<%@ Register Src="../Components/StoreConfig/StorePointSystemConfig.ascx" TagName="PointSystemConfig"
    TagPrefix="uc14" %>
<%@ Register Src="../Components/StoreConfig/StoreSeoConfig.ascx" TagName="SeoConfig"
    TagPrefix="uc15" %>
<uc1:admincontent id="uxContentTemplate" runat="server" headertext="<%$ Resources:lcHeader %>">
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
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
        </div>
        <asp:Label ID="lcFieldMutipleLanguage" runat="server" meta:resourcekey="lcFieldMutipleLanguage"
            CssClass="Label" />
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageControl ID="uxLanguageControl" runat="server" />
    </LanguageControlTemplate>
    <PlainContentTemplate>
        <uc2:BoxSet ID="uxBoxSetMain" runat="server" CssClass="SiteConfigBoxSet">
            <ContentTemplate>
                <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer">
                    <ajaxToolkit:TabPanel ID="uxProfileTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcProfileHeader" runat="server" meta:resourcekey="lcProfileHeader" />
                            </div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonConfigTitle mgt0">
                                <asp:Label ID="uxBusinessProfileNoteLabel" runat="server" meta:resourcekey="lcBusinessProfileNote" />
                            </div>
                            <div class="CommonAdminBorder">
                                <uc14:StoreProfileConfig ID="uxStoreProfileConfig" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxDisplaySettingTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcStoreLayoutDisplay" runat="server" meta:resourcekey="lcStoreLayoutDisplay" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                            <div class="CommonConfigTitle mgt0">
                                <asp:Label ID="lcDisplayFollowing" runat="server" meta:resourcekey="lcDisplayFollowing" /></div>
                                <uc11:StoreLayoutConfig ID="uxStoreLayoutConfig" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxStoreConfigTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcDisplayHeader" runat="server" meta:resourcekey="lcDisplayHeader" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc8:StoreDisplayConfig ID="uxDisplay" runat="server" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxEmailSettingTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcEmailSettingHeader" runat="server" meta:resourcekey="lcEmailSettingHeader" /></div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc10:EmailSetup ID="uxEmailSetup" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxFaceBookSettingTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcFaceBookSettingHeader" runat="server" meta:resourcekey="lcFaceBookSettingHeader" />
                            </div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder ">
                                <uc12:StoreFacebookConfig ID="uxFacebook" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxBlogSettingTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="lcBlogSettingHeader" runat="server" meta:resourcekey="lcBlogSettingHeader" />
                            </div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc13:StoreBlogConfig ID="uxBlogConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxPointSystemTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="uxPointSystemHeader" runat="server" Text="Point System" />
                            </div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc14:PointSystemConfig ID="uxPointSystemConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="uxSeoConfigTab" runat="server" CssClass="DefaultTabPanel">
                        <HeaderTemplate>
                            <div>
                                <asp:Label ID="uxSeoConfigHeader" runat="server" Text="SEO Setting" />
                            </div>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="CommonAdminBorder">
                                <uc15:SeoConfig ID="uxSeoConfig" runat="server" ValidationGroup="SiteConfigValid" />
                                <div class="Clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <div class="ConfigUpdetButton">
                    <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                        CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="SiteConfigValid">
                    </vevo:AdvanceButton>
                    <div class="Clear">
                    </div>
                </div>
            </ContentTemplate>
        </uc2:BoxSet>
    </PlainContentTemplate>
</uc1:admincontent>
