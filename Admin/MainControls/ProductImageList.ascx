<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductImageList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ProductImageList" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Products/ImageList.ascx" TagName="ProductImage" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxProductDetailLink" runat="server" meta:resourcekey="lcProductDetail"
            ShowText="true" PageName="ProductAdd.ascx" CommandName="Edit" OnClick="ChangePage_Click" StatusBarText="Product Detail"
            CssClass="CommonAdminButtonIcon AdminButtonIconEdit fl" />
        <vevo:AdvancedLinkButton ID="uxReviewLink" runat="server" meta:resourcekey="uxReviewLink"
            ShowText="true" PageName="ProductReviewList.ascx" OnClick="ChangePage_Click"
            StatusBarText="Review List" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <PlainContentTemplate>
        <uc2:ProductImage ID="uxProductImage" runat="server" />
        <div class="mgt10">
            <vevo:AdvanceButton ID="uxOKImageButton" runat="server" meta:resourcekey="uxUpdateButton"
                CssClassBegin="fr mgt10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxOKImageButton_Click" OnClickGoTo="Top" /></div>
    </PlainContentTemplate>
</uc1:AdminContent>
