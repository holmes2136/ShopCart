<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreBlogConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_StoreConfig_StoreBlogConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="../SiteConfig/LayoutBlogThemeSelect.ascx" TagName="DefaultBlogThemeSelect"
    TagPrefix="uc2" %>
<%@ Register Src="../SiteConfig/LayoutBlogListSelect.ascx" TagName="DefaultBlogListSelect"
    TagPrefix="uc3" %>
<%@ Register Src="../SiteConfig/LayoutBlogDetailsSelect.ascx" TagName="DefaultBlogDetailsSelect"
    TagPrefix="uc4" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc5" %>
<div class="CommonConfigTitle  mgt0">
    <asp:Label ID="uxBlogTitle" runat="server" meta:resourcekey="uxBlogTitle" /></div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableBlogHelp" ConfigName="BlogEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableBlogLabel" runat="server" meta:resourcekey="uxEnableBlogLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableBlogDrop" runat="server" CssClass="fl DropDown" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxBlogDetailsPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxEnableBlogCommentHelp" ConfigName="BlogCommentEnabled" runat="server" />
        <div class="Label">
            <asp:Label ID="uxEnableBlogCommentLabel" runat="server" meta:resourcekey="uxEnableBlogCommentLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxEnableBlogCommentDrop" runat="server" CssClass="fl DropDown"
            AutoPostBack="true" OnSelectedIndexChanged="uxEnableBlogCommentDrop_SelectedIndexChanged">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <asp:Panel ID="uxBlogCommentPanel" runat="server">
        <div class="ConfigRow">
            <uc1:HelpIcon ID="uxBlogCommentPerPageHelp" ConfigName="BlogCommentsPerPage" runat="server" />
            <div class="BulletLabel">
                <asp:Label ID="uxBlogCommentPerPageLabel" runat="server" meta:resourcekey="uxBlogCommentPerPageLabel"
                    CssClass="fl" />
            </div>
            <asp:TextBox ID="uxBlogCommentPerPageText" runat="server" CssClass="TextBox" />
            <div class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
            <asp:RequiredFieldValidator ID="uxBlogCommentPerPageRequiredValidator" runat="server"
                ControlToValidate="uxBlogCommentPerPageText" Display="Dynamic" ValidationGroup="SiteConfigValid"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of blog comments per page is required.
                <div class="CommonValidateDiv">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxBlogCommentPerPageCompare" runat="server" ControlToValidate="uxBlogCommentPerPageText"
                Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
                <div class="CommonValidateDiv">
                </div>
            </asp:CompareValidator>
        </div>
        <div class="ConfigRow">
            <uc1:HelpIcon ID="uxBlogCommentsSortOrderHelp" ConfigName="BlogCommentsSortOrder"
                runat="server" />
            <div class="BulletLabel">
                <asp:Label ID="uxBlogCommentsSortOrderLabel" runat="server" meta:resourcekey="uxBlogCommentsSortOrderLabel"
                    CssClass="fl" />
            </div>
            <asp:DropDownList ID="uxBlogCommentsSortOrderDrop" runat="server" CssClass="fl DropDown">
                <asp:ListItem Value="Last to First" Text="Last to First" />
                <asp:ListItem Value="First to Last" Text="First to Last" />
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxBlogPageTitleHelp" ConfigName="BlogPageTitle" runat="server" />
        <div class="Label">
            <asp:Label ID="uxBlogPageTitleLable" runat="server" meta:resourcekey="uxBlogPageTitleLable"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxBlogPageTitleTextbox" runat="server" CssClass="TextBox" />
        <uc5:LanaguageLabelPlus ID="uxPlus5" runat="server" />
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxBlogListItemsPerPageHelp" ConfigName="BlogListItemsPerPage" runat="server" />
        <div class="Label">
            <asp:Label ID="uxBlogListItemsPerPageLabel" runat="server" meta:resourcekey="uxBlogListItemsPerPageLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxBlogListItemsPerPageTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxBlogListItemsPerPageRequiredValidator" runat="server"
            ControlToValidate="uxBlogListItemsPerPageTextbox" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Blog List Items Per Page.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="uxRegularItemPerPageValidator" runat="server"
            ControlToValidate="uxBlogListItemsPerPageTextbox" Display="Dynamic" ValidationExpression="\d{1,3}(,\d{1,3})*"
            ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:RegularExpressionValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDisplayArchivesEnabledHelp" ConfigName="DisplayArchivesEnabled"
            runat="server" />
        <div class="Label">
            <asp:Label ID="uxDisplayArchivesLabel" runat="server" meta:resourcekey="uxDisplayArchivesLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxEnableDisplayArchivesDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDisplayBlogCountHelp" ConfigName="BlogCountInCategoryBox" runat="server" />
        <div class="Label">
            <asp:Label ID="uxDisplayBlogCountLabel" runat="server" meta:resourcekey="uxDisplayBlogCountLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxDisplayBlogCountDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <uc2:DefaultBlogThemeSelect ID="uxBlogThemeSelect" runat="server" />
    <uc3:DefaultBlogListSelect ID="uxBlogListSelect" runat="server" />
    <uc4:DefaultBlogDetailsSelect ID="uxBlogDetailsSelect" runat="server" />
</asp:Panel>
<div class="ConfigRow mgt20">
    <uc1:HelpIcon ID="uxFacebookCommentEnabledHelp" ConfigName="FacebookCommentEnabled"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxFacebookCommentEnabledLabel" runat="server" meta:resourcekey="uxFacebookCommentEnabledLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFacebookCommentEnabledDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxFacebookCommentEnabledDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxFbCommentDetailsPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxFacebookCommentAPIKeyHelp" ConfigName="FacebookCommentAPIKey"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxFacebookCommentAPIKeyLabel" runat="server" meta:resourcekey="uxFacebookCommentAPIKeyLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxFacebookCommentAPIKeyTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxFacebookCommentAPIKeyRequired" runat="server" ControlToValidate="uxFacebookCommentAPIKeyTextbox"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Facebook App ID/API key is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxFacebookCommentNumberOfPostsHelp" ConfigName="FacebookCommentNumberOfPosts"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxFacebookCommentNumberOfPostsLabel" runat="server" meta:resourcekey="uxFacebookCommentNumberOfPostsLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxFacebookCommentNumberOfPostsTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxFacebookCommentNumberOfPostsRequired" runat="server"
            ControlToValidate="uxFacebookCommentNumberOfPostsTextbox" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of post is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxFacebookCommentNumberOfPostsCompare" runat="server" ControlToValidate="uxFacebookCommentNumberOfPostsTextbox"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
</asp:Panel>
