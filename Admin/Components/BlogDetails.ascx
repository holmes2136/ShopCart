<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogDetails.ascx.cs" Inherits="Admin_Components_BlogDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="MultiStoreList.ascx" TagName="MultiStoreList" TagPrefix="uc7" %>
<%@ Register Src="BlogCategoryItemList.ascx" TagName="BlogCategoryItemList" TagPrefix="uc8" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidBlog" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogTitleLabel" runat="server" meta:resourcekey="uxBlogTitleLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxBlogTitleText" runat="server" Width="252px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxTitleRequiredValidator" runat="server" ControlToValidate="uxBlogTitleText"
                    ValidationGroup="ValidBlog" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Blog Title is required.
                            <div class="CommonValidateDiv CommonValidateDivContentNameLong">
                            </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div id="uxBlogUrlSettingTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxBlogUrlLabel" runat="server" meta:resourcekey="uxBlogUrlLabel"></asp:Label></div>
                <%--<asp:Label ID="uxBlogShowUrlLabel" runat="server"></asp:Label>--%>
                <asp:HyperLink ID="uxBlogUrlLink" runat="server" Target="_blank"></asp:HyperLink>
            </div>
            <asp:Panel ID="uxStorePanel" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcStore" runat="server" meta:resourcekey="lcStore" CssClass="Label" />
                <div class="fl">
                    <uc7:MultiStoreList ID="uxMultiStoreList" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="uxBlogCategoryPanel" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcBlogCategory" runat="server" meta:resourcekey="lcBlogCategory" CssClass="Label" />
                <div class="fl">
                    <uc8:BlogCategoryItemList ID="uxBlogCategoryList" runat="server" />
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxShortContentLabel" runat="server" meta:resourcekey="uxShortContentLabel"
                    CssClass="Label" />
                <uc6:TextEditor ID="uxShortContentText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogContentLabel" runat="server" meta:resourcekey="uxBlogContentLabel"
                    CssClass="Label" />
                <uc6:TextEditor ID="uxBlogContentText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcImage" runat="server" meta:resourcekey="lcImage" CssClass="Label" />
                <asp:TextBox ID="uxImageText" runat="server" Width="250px" CssClass="TextBox" />
                <asp:LinkButton ID="uxImageBlogLinkButton" runat="server" OnClick="uxImageBlogLinkButton_Click"
                    CssClass="fl mgl5">Upload...</asp:LinkButton>
                <uc6:Upload ID="uxImageBlogUpload" runat="server" ShowControl="false" CheckType="Image"
                    CssClass="CommonRowStyle" ButtonImage="SelectImages.png" ButtonWidth="105" ButtonHeight="22"
                    ShowText="false" />
            </div>    
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxIsEnabledLabel" runat="server" meta:resourcekey="uxIsEnabledLabel" />
                </div>
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" CssClass="fl" Checked="true" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogTagsLabel" runat="server" meta:resourcekey="uxBlogTagsLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxBlogTagsText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogMetaTitleLabel" runat="server" meta:resourcekey="uxBlogMetaTitleLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxBlogMetaTitleText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogMetaKeywordLabel" runat="server" meta:resourcekey="uxBlogMetaKeywordLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxBlogMetaKeywordText" runat="server" Height="40px" TextMode="MultiLine"
                    Width="252px" CssClass="TextBox fl"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlogMetaDescriptionLabel" runat="server" meta:resourcekey="uxBlogMetaDescriptionLabel"
                    class="Label" />
                <asp:TextBox ID="uxBlogMetaDescriptionText" runat="server" Width="252px" TextMode="MultiLine"
                    Rows="4" CssClass="TextBox fl"></asp:TextBox>
            </div>
            <asp:Panel ID="uxCreateDatePanel" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxCreateDateLabel" runat="server" meta:resourcekey="uxCreateDateLabel"
                    CssClass="Label" />
                <uc4:CalendarPopup ID="uxCreateDateCalendarPopup" runat="server" TextBoxEnabled="false" />
                <asp:TextBox ID="uxCreateDateText" runat="server" Width="328px" CssClass="TextBox"></asp:TextBox>
            </asp:Panel>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidBlog" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidBlog" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
