<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="StoreSiteMap.aspx.cs"
    Inherits="StoreSiteMap" Title="[$Title]" %>

<%@ Register Src="~/Components/ContentSiteMap.ascx" TagName="ContentSiteMap" TagPrefix="uc1" %>
<%@ Register Src="~/Components/CategorySiteMap.ascx" TagName="CategorySiteMap" TagPrefix="uc2" %>
<%@ Register Src="~/Components/DepartmentSiteMap.ascx" TagName="DepartmentSiteMap"
    TagPrefix="uc3" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="StoreSiteMap">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="uxInformationPanel" runat="server" CssClass="StoreSiteMapInformationPanel">
                        <div class="StoreSiteMapInformationTop">
                            <asp:Image ID="uxInformationTopLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapInformationTopLeft.gif"
                                runat="server" CssClass="StoreSiteMapInformationTopImgLeft" />
                            <asp:Label ID="uxInformationTitleLabel" runat="server" Text="[$InformationHead]"
                                CssClass="StoreSiteMapInformationTopTitle"></asp:Label>
                            <asp:Image ID="uxInformationTopRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapInformationTopRight.gif"
                                runat="server" CssClass="StoreSiteMapInformationTopImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="StoreSiteMapInformationLeft">
                            <div class="StoreSiteMapInformationRight">
                                <uc1:ContentSiteMap ID="uxContentNavList" runat="server" />
                            </div>
                        </div>
                        <div class="StoreSiteMapInformationBottom">
                            <asp:Image ID="uxInformationBottomLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapInformationBottomLeft.gif"
                                runat="server" CssClass="StoreSiteMapInformationBottomImgLeft" />
                            <asp:Image ID="uxInformationBottomRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapInformationBottomRight.gif"
                                runat="server" CssClass="StoreSiteMapInformationBottomImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxProductPanel" runat="server" CssClass="StoreSiteMapProductPanel">
                        <uc2:CategorySiteMap ID="uxCategoryNavList" runat="server" />
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                    <asp:Panel ID="uxDepartmentPanel" runat="server" CssClass="StoreSiteMapProductPanel">
                        <uc3:DepartmentSiteMap ID="uxDepartmentNavList" runat="server" />
                    </asp:Panel>
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
