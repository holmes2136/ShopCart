<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitGroupItemDetails.ascx.cs"
    Inherits="Admin_Components_ProductKitGroupItemDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="ProductKitProductList.ascx" TagName="ProductKitProductList"
    TagPrefix="uc2" %>
<%@ Register Src="ProductKitGroupItemSeletedList.ascx" TagName="ProductKitGroupItemSeletedList"
    TagPrefix="uc3" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageSelector ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <ajaxToolkit:TabContainer ID="uxPromotionSubGroupTab" runat="server" CssClass="DefaultTabContainer"
                AutoPostBack="true">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" CssClass="DefaultTabPanel">
                    <ContentTemplate>
                        <uc3:ProductKitGroupItemSeletedList ID="uxProductKitGroupItemSeletedList" runat="server" />
                    </ContentTemplate>
                    <HeaderTemplate>
                        Product Kit Group Item</HeaderTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" CssClass="DefaultTabPanel">
                    <ContentTemplate>
                        <uc2:ProductKitProductList ID="uxProductKitProductList" runat="server" />
                    </ContentTemplate>
                    <HeaderTemplate>
                        Product Lists</HeaderTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
