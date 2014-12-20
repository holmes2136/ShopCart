<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Manufacturer.aspx.cs"
    Inherits="Manufacturer" Title="[$Title]" EnableViewStateMac="false" EnableSessionState="True"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%@ Register Src="Components/CatalogBreadcrumb.ascx" TagName="CatalogBreadcrumb"
    TagPrefix="uc1" %>
<%@ Register Src="Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
   <uc1:CatalogBreadcrumb ID="uxCatalogBreadcrumb" runat="server" />
    <div class="Manufacturer">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Label ID="uxManufacturerNameLabel" runat="server" CssClass="CatalogName" /> 
                    <asp:Panel ID="uxManufacturerControlPanel" runat="server" CssClass="ManufacturerControlPanel">
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
