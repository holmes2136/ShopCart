<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMessage.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AdminMessage" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxContentTemplate" runat="server" >
    <ContentTemplate>
        <div class="ac">
            <asp:Literal ID="uxMessageLiteral" runat="server" /></div>
        <div class="fr mgt10 mgb10 mgr0 ar w90">
            <vevo:AdvancedLinkButton ID="uxReturnLink" runat="server" Text="Back" Visible="false"
                PageQueryString="" OnClick="ChangePage_Click" CssClassBegin="fl mgl5" CssClassEnd="CssBackLink"
                CssClass="c11 ul"></vevo:AdvancedLinkButton>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
