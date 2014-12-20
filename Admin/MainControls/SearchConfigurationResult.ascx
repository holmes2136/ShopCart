<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchConfigurationResult.ascx.cs"
    Inherits="AdminAdvanced_MainControls_SearchConfigurationResult" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Boxset/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="../Components/SiteConfig/Wholesale.ascx" TagName="Wholesale" TagPrefix="uc9" %>
<%@ Register Src="../Components/Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc4" %>
<%@ Register Src="../Components/SiteConfig/WidgetDetails.ascx" TagName="WidgetConfig"
    TagPrefix="uc30" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" HeaderText="<div class='Title'>Please correct the following errors :</div>"
            ValidationGroup="SiteConfigValid" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="CommonTitle">
            <asp:Label ID="uxSearchTitle" runat="server" />
            <div class="RequiredLabel c6 fr">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
        <div class="Container-Box">
            <asp:Panel ID="uxSearchResultPanel" runat="server">
                <asp:PlaceHolder ID="uxPlaceHolder" runat="server"></asp:PlaceHolder>
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="SiteConfigValid">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxBackButton" OnClick="uxBackButton_Click" meta:resourcekey="uxBackButton" OnClickGoTo="Top"
                    runat="server" CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" ></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
