<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsDetails.ascx.cs" Inherits="AdminAdvanced_Components_NewsDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageSelector ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <asp:Label ID="lcTopic" runat="server" meta:resourcekey="lcTopic" CssClass="Label" />
                <asp:TextBox ID="uxTopicText" runat="server" Width="500px" CssClass="TextBox"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
            </div>
            <asp:Panel ID="uxStorePanel" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcStore" runat="server" meta:resourcekey="lcStore" CssClass="Label" />
                <div class="fl">
                    <asp:DropDownList ID="uxStoreDrop" runat="server" Width="155px" />
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDescription" runat="server" meta:resourcekey="lcDescription" CssClass="Label" />
                <uc6:TextEditor ID="uxDescriptionText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxNewsMetaTitleLabel" runat="server" meta:resourcekey="uxNewsMetaTitleLabel" CssClass="Label" />
                <asp:TextBox ID="uxNewsMetaTitleText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxPlus2" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxNewsMetaKeywordLabel" runat="server" meta:resourcekey="uxNewsMetaKeywordLabel" CssClass="Label" />
                <asp:TextBox ID="uxNewsMetaKeywordText" runat="server" Height="40px" TextMode="MultiLine"
                    Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxPlus3" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxNewsMetaDescriptionLabel" runat="server" meta:resourcekey="uxNewsMetaDescriptionLabel" class="Label" />
                <asp:TextBox ID="uxNewsMetaDescriptionText" runat="server" Width="252px" TextMode="MultiLine"
                    Rows="4" CssClass="TextBox fl"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
            </div>
            <asp:Panel ID="uxImageTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcImages" runat="server" meta:resourcekey="lcImages" CssClass="Label" />
                <asp:TextBox ID="uxFileImageText" runat="server" Width="328px" CssClass="TextBox"></asp:TextBox>
                <asp:LinkButton ID="uxUploadLinkButton" runat="server" OnClick="uxUploadLinkButton_Click"
                    CssClass="fl mgl5">Upload...</asp:LinkButton>
                <div class="Clear">
                </div>
            </asp:Panel>  
            <uc6:Upload ID="uxUpload" runat="server" ShowControl="false" CssClass="CommonRowStyle"
                CheckType="Image" ButtonImage="SelectImages.png" ButtonWidth="105" ButtonHeight="22"
                ShowText="false" />
            <asp:Panel ID="uxDateTimeTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxNewsDateLabel" runat="server" meta:resourcekey="lcDate" CssClass="Label" />
                <uc4:CalendarPopup ID="uxNewDateCalendarPopup" runat="server" TextBoxEnabled="false" />
                <asp:TextBox ID="uxDateText" runat="server" Width="328px" CssClass="TextBox"></asp:TextBox>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcHotNews" runat="server" meta:resourcekey="lcHotNews" />
                    <asp:Label ID="lcShow" runat="server" meta:resourcekey="lcShow" /></div>
                <asp:CheckBox ID="uxIsHotNewsCheck" runat="server" CssClass="fl" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcIsEnabledNews" runat="server" meta:resourcekey="lcIsEnabledNews" />
                </div>
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" CssClass="fl" />
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
