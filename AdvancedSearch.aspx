<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AdvancedSearch.aspx.cs"
    Inherits="AdvancedSearch" Title="[$Title]" %>

<%@ Register Src="Components/CategoryCheckList.ascx" TagName="CategoryCheckList"
    TagPrefix="uc1" %>
<%@ Register Src="Components/DepartmentCheckList.ascx" TagName="DepartmentCheckList"
    TagPrefix="uc2" %>
<%@ Register Src="Components/ContentMenuItemCheckList.ascx" TagName="ContentMenuItemCheckList"
    TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AdvancedSearch">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:LinkButton class="HideAdvancedSearchLinkButton" ID="uxHideAdvancedSearchLinkButton"
                    runat="server" OnClick="HideAdvancedSearchLinkButton_OnClick" Visible="true" >
                                    [$Hide]
                </asp:LinkButton>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel id="uxEnhancedSearchPanel" runat="server" Visible="true">
                <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="AdvancedSearchValidationSummary">
                        <asp:ValidationSummary ID="uxValidationSummary" runat="server" HeaderText="Please correct the following errors:"
                            ValidationGroup="ValidSearch" />
                    </div>
                    <div class="AdvancedSearchPanel SearchByKeyword">
                        <div class="AdvancedSearchLabel">
                            [$Name]
                        </div>
                        <div class="AdvancedSearchDrop">
                            <asp:DropDownList ID="uxSearchDrop" runat="server">
                                <asp:ListItem Text="Any words" Value="any" />
                                <asp:ListItem Text="All words" Value="all" />
                                <asp:ListItem Text="Exact phase" Value="exact" />
                            </asp:DropDownList>
                        </div>
                        <div class="AdvancedSearchText SearchTextByKeyword">
                            <asp:TextBox ID="uxKeywordText" runat="server" Width="175px" CssClass="AdvancedSearchTextBox">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="uxNewSearchPanel1" runat="server">
                        <div class="AdvancedSearchPanel SearchByCategory">
                            <div class="AdvancedSearchLabel">
                                [$SearchIn]
                            </div>
                            <div class="AdvancedSearchDrop SearchDropByCategory">
                                <asp:DropDownList ID="uxCategoryDrop" runat="server" class="AdvancedSearchInCategoryDrop">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="AdvancedSearchPanel SearchByCategoryField">
                            <asp:CheckBoxList ID="uxSearchTypeCheckList" CssClass="AdvancedSearchTypeCheckList"
                                runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="AdvancedSearchPanel SearchByPrice">
                        <div class="AdvancedSearchLabel">
                            [$Price]
                        </div>
                        <div class="AdvancedSearchText SearchTextByPrice">
                            <asp:TextBox ID="uxPrice1Text" runat="server" ValidationGroup="ValidSearch" CssClass="AdvancedSearchTextBox">
                            </asp:TextBox>
                            <asp:CompareValidator ID="uxPrice1Compare" runat="server" ControlToValidate="uxPrice1Text"
                                Operator="DataTypeCheck" Type="Currency" ValidationGroup="ValidSearch" Display="Dynamic"
                                CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Price is invalid.
                            </asp:CompareValidator>
                        </div>
                        <div class="AdvancedSearchToLabel">
                            [$To]
                        </div>
                        <div class="AdvancedSearchText SearchTextByPriceTo">
                            <asp:TextBox ID="uxPrice2Text" runat="server" ValidationGroup="ValidSearch" CssClass="AdvancedSearchTextBox">
                            </asp:TextBox>
                            <asp:CompareValidator ID="uxPrice2Compare" runat="server" ControlToValidate="uxPrice2Text"
                                Operator="DataTypeCheck" Type="Currency" ValidationGroup="ValidSearch" Display="Dynamic"
                                CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Price is invalid.
                            </asp:CompareValidator>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div id="uxNewSearchPanel2" runat="server">
                        <div class="AdvancedSearchDotted">
                        </div>
                        <div class="AdvancedSearchHeader">
                            [$MoreSearchOptions]
                        </div>
                        <div class="AdvancedSearchPanel">
                            <div class="AdvancedSearchInLabel">
                                [$AlsoSearchIn]
                            </div>
                        </div>
                        <div class="AdvancedSearchPanel SearchByDepartment">
                            <div id="uxDepartmentDiv" runat="server" visible="false">
                                <div class="AdvancedSearchDepartmentLabel">
                                    [$SearchInDepartment]
                                </div>
                                <div class="AdvancedSearchDrop SearchDropByDepartment ">
                                    <asp:DropDownList ID="uxDepartmentDrop" runat="server" class="AdvancedSearchInDepartmentDrop">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="AdvancedSearchPanel SearchByManufacturer">
                            <div id="uxManufacturerDiv" runat="server" visible="false">
                                <div class="AdvancedSearchManufacturerLabel" id="uxManufactureLabel" runat="server">
                                    [$SearchInManufacturer]
                                </div>
                                <div class="AdvancedSearchDrop SearchDropByManufacturer ">
                                    <asp:DropDownList ID="uxManufacturerDrop" runat="server" class="AdvancedSearchinmanufacturerDrop">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="uxOldAdvancedSearchDiv" visible="false">
                        <table border="0" cellpadding="0" cellspacing="5" class="AdvancedSearchTable">
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="uxCategoryCheckPanel" runat="server">
                                        <div class="AdvancedSearchTitle">
                                            <asp:Label ID="uxDefalutTitleProduct" runat="server">[$HeadCategory]</asp:Label>
                                        </div>
                                        <asp:UpdatePanel ID="uxUpdateAdvancedSearchList" runat="server">
                                            <ContentTemplate>
                                                <asp:DataList ID="uxCategoryList" runat="server" CssClass="AdvancedSearchDataList"
                                                    OnItemDataBound="uxCategoryList_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table cellpadding="3" class="AdvancedSearchDataListTable">
                                                            <tr>
                                                                <td class="AdvancedSearchDataListTableHeaderCheck">
                                                                    <asp:HiddenField ID="uxCategoryIDHidden" runat="server" Value='<%# Eval("CategoryID") %>' />
                                                                    <asp:CheckBox ID="uxCategoryCheck" runat="server" Text='<%# Eval("Name") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="AdvancedSearchDataListTableCategoryCheckList">
                                                                    <uc1:CategoryCheckList ID="uxCategoryCheckList" runat="server" CurrentID='<%# Eval("CategoryID") %>' />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="uxDepartmentCheckPanel" runat="server">
                                        <div class="AdvancedSearchTitle">
                                            <asp:Label ID="uxDepartmentTitle" runat="server">[$HeadDepartment]</asp:Label>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DataList ID="uxDepartmentsList" runat="server" CssClass="AdvancedSearchDataList"
                                                    OnItemDataBound="uxDepartmentsList_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table cellpadding="3" class="AdvancedSearchDataListTable">
                                                            <tr>
                                                                <td class="AdvancedSearchDataListTableHeaderCheck">
                                                                    <asp:HiddenField ID="uxDepartmentIDHidden" runat="server" Value='<%# Eval("DepartmentID") %>' />
                                                                    <asp:CheckBox ID="uxDepartmentCheck" runat="server" Text='<%# Eval("Name") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="AdvancedSearchDataListTableDepartmentCheckList">
                                                                    <uc2:DepartmentCheckList ID="uxDepartmentCheckList" runat="server" CurrentID='<%# Eval("DepartmentID") %>' />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="uxContentCheckPanel" runat="server">
                                        <div class="AdvancedSearchTitle">
                                            <asp:Label ID="Label1" runat="server">[$HeadContent]</asp:Label>
                                        </div>
                                        <asp:DataList ID="uxTopContentMenuItemList" runat="server" CssClass="AdvancedSearchDataList">
                                            <ItemTemplate>
                                                <table cellpadding="3" class="AdvancedSearchDataListTable">
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableHeaderCheck">
                                                            <asp:HiddenField ID="uxTopContentMenuItemIDHidden" runat="server" Value='<%# Eval("ContentMenuItemID") %>' />
                                                            <asp:CheckBox ID="uxTopContentMenuItemCheck" runat="server" Text='<%# Eval("Name") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableCategoryCheckList">
                                                            <uc3:ContentMenuItemCheckList ID="uxTopContentMenuItemCheckList" runat="server" CurrentID='<%# Eval("ContentMenuItemID") %>' />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:DataList ID="uxLeftContentMenuItemList" runat="server" CssClass="AdvancedSearchDataList">
                                            <ItemTemplate>
                                                <table cellpadding="3" class="AdvancedSearchDataListTable">
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableHeaderCheck">
                                                            <asp:HiddenField ID="uxLeftContentMenuItemIDHidden" runat="server" Value='<%# Eval("ContentMenuItemID") %>' />
                                                            <asp:CheckBox ID="uxLeftContentMenuItemCheck" runat="server" Text='<%# Eval("Name") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableCategoryCheckList">
                                                            <uc3:ContentMenuItemCheckList ID="uxLeftContentMenuItemCheckList" runat="server"
                                                                CurrentID='<%# Eval("ContentMenuItemID") %>' />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:DataList ID="uxRightContentMenuItemList" runat="server" CssClass="AdvancedSearchDataList">
                                            <ItemTemplate>
                                                <table cellpadding="3" class="AdvancedSearchDataListTable">
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableHeaderCheck">
                                                            <asp:HiddenField ID="uxRightContentMenuItemIDHidden" runat="server" Value='<%# Eval("ContentMenuItemID") %>' />
                                                            <asp:CheckBox ID="uxRightContentMenuItemCheck" runat="server" Text='<%# Eval("Name") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="AdvancedSearchDataListTableCategoryCheckList">
                                                            <uc3:ContentMenuItemCheckList ID="uxRightContentMenuItemCheckList" runat="server"
                                                                CurrentID='<%# Eval("ContentMenuItemID") %>' />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="AdvancedSearchResetButton">
                        <asp:LinkButton ID="uxResetImageButton" runat="server" Text="[$BtnReset]" CssClass="BtnStyle2"
                            OnClick="uxResetImageButton_Click" />
                    </div>
                    <div class="AdvancedSearchButton">
                        <asp:LinkButton ID="uxSearchImageButton" runat="server" Text="[$BtnSearch]" CssClass="BtnStyle1"
                            OnClick="uxSearchImageButton_Click" ValidationGroup="ValidSearch" />
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
            </asp:Panel>
        </div>
    </div>
    <asp:Panel id="uxEnhancedSearchResultPanel" runat="server" Visible="false">
        <div class="AdvancedSearchResult">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopDeptLeftResult" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultDeptTitleResult" runat="server" CssClass="CommonPageTopTitle"></asp:Label>
                <asp:Image ID="uxTopDeptRightResult" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="uxCatalogControlPanel" runat="server">
                    </asp:Panel>
                    <div id="uxMessageDiv" runat="server" class="CommonGridViewEmptyRowStyle" visible="false">
                        <asp:Label ID="uxMessageLabel" runat="server" Text="[$NoData]" />
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomDeptLeftResult" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomDeptRighResult" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
    <asp:HiddenField ID="uxStartKeywordTextHidden" runat="server" />
</asp:Content>
