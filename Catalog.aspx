<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Catalog.aspx.cs"
    Inherits="Catalog" Title="[$Title]" EnableViewStateMac="false" EnableSessionState="True"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%-- "[EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false"  ViewStateEncryptionMode ="Never"]" is added for 
        "the state information is invalid for this page and might be corrupted" prevention in Firefox --%>
<%@ Register Src="Components/CatalogBreadcrumb.ascx" TagName="CatalogBreadcrumb"
    TagPrefix="uc1" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:CatalogBreadcrumb ID="uxCatalogBreadcrumb" runat="server" />
    <div class="Catalog">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Label ID="uxCatalogNameLabel" runat="server" CssClass="CatalogName" />                    
                    <asp:Panel ID="uxCatalogControlPanel" runat="server" CssClass="CatalogControlPanel">
                    </asp:Panel>
                    <asp:Panel ID="uxProductControlPanel" runat="server" CssClass="ProductItemControlPanel">
                    </asp:Panel>
                    <asp:HiddenField ID="uxStatusHidden" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
