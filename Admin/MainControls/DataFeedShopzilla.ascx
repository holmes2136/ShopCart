<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataFeedShopzilla.ascx.cs"
    Inherits="AdminAdvanced_MainControls_DataFeedShopzilla" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/DataFeedDetails.ascx" TagName="DataFeedDetails" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<uc1:AdminContent ID="admin" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
        <div class="CommonDownloadLink">
        <asp:HyperLink ID="uxFileNameLink" runat="server"></asp:HyperLink></div>
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="ProductFeedLanguage ar">
            <div class="w100p">
                <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
            </div>
        </div>
        <div class="ProductFeedNotice">
            <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice" />
        </div>
        <div class="mg5">
            <uc2:DataFeedDetails ID="uxDataFeedDetails" runat="server" />
            <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton"
                CssClassBegin="fr mgt10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenerateButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
