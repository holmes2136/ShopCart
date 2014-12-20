<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderMenu.ascx.cs" Inherits="Components_HeaderMenu" %>
<%@ Register Src="../Components/ContentMenuNavList.ascx" TagName="ContentMenuNavList"
    TagPrefix="uc9" %>
<%@ Register Src="../Components/CategoryDynamicDropDownMenu.ascx" TagName="CategoryDynamicDropDownMenu"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/DepartmentDynamicDropDownMenu.ascx" TagName="DepartmentDynamicDropDownMenu"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/ManufacturerDynamicDropDownMenu.ascx" TagName="ManufacturerDynamicDropDownMenu"
    TagPrefix="uc3" %>
<div class="HeaderMenu">
    <div class="HeaderMenuLeft">
        <div class="HeaderMenuRight">
            <ul>
                <li>
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxHomeLink" runat="server" NavigateUrl="~/Default.aspx" class="HyperLink">[$Home]</asp:HyperLink></div>
                    </div>
                </li>
                <li id="uxCatelogMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxCatalogLink" runat="server" NavigateUrl="~/Catalog.aspx" class="HyperLink">[$Products]</asp:HyperLink></div>
                    </div>
                </li>
                <li id="uxCatelogDropDownMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <uc1:CategoryDynamicDropDownMenu ID="uxCategoryDynamicDropDownMenu" runat="server" />
                        </div>
                    </div>
                </li>
                <li id="uxManufacturerMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxManufactureLink" runat="server" class="HyperLink" NavigateUrl="~/Manufacturer.aspx">[$Manufacturer]</asp:HyperLink></div>
                    </div>
                </li>
                <li id="uxManufacturerDropDownMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <uc3:ManufacturerDynamicDropDownMenu ID="uxManufacturerDynamicDropDownMenu" runat="server" />
                        </div>
                    </div>
                </li>
                <li id="uxDepartmentMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxDepartmentLink" runat="server" NavigateUrl="~/Department.aspx"
                                class="HyperLink">[$Department]</asp:HyperLink></div>
                    </div>
                </li>
                <li id="uxDepartmentDropDownMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <uc2:DepartmentDynamicDropDownMenu ID="uxDepartmentDynamicDropDownMenu" runat="server" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxMyAccountLink" runat="server" 
                                class="HyperLink">[$My Account]</asp:HyperLink></div>
                    </div>
                </li>
                <li>
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxContactUsLink" runat="server" NavigateUrl="~/ContactUs.aspx"
                                class="HyperLink">[$Contact Us]</asp:HyperLink></div>
                    </div>
                </li>
                <li id="uxContentMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <uc9:ContentMenuNavList ID="uxContentMenuNavList" runat="server" />
                        </div>
                    </div>
                </li>
                <li id="uxBlogMenu" runat="server">
                    <div class="HeaderMenuNavItemLeft">
                        <div class="HeaderMenuNavItemRight">
                            <asp:HyperLink ID="uxBlogLink" runat="server" class="HyperLink" NavigateUrl="~/Blog/Default.aspx">[$Blog]</asp:HyperLink></div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
