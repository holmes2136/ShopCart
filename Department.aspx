<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Department.aspx.cs"
    Inherits="DepartmentPage" Title="[$Title]" EnableViewStateMac="false" EnableSessionState="True"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%-- "[EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false"  ViewStateEncryptionMode ="Never"]" is added for 
        "the state information is invalid for this page and might be corrupted" prevention in Firefox --%>
<%@ Register Src="Components/CatalogBreadcrumb.ascx" TagName="CatalogBreadcrumb"
    TagPrefix="uc1" %>
<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="Components/NewArrivalCategory.ascx" TagName="NewArrivalCategory"
    TagPrefix="uc6" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
<uc1:CatalogBreadcrumb ID="uxCatalogBreadcrumb" runat="server" />
    <div class="Department">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                     <asp:Label ID="uxDepartmentNameLabel" runat="server" CssClass="CatalogName" /> 
                    <asp:Panel ID="uxDepartmentControlPanel" runat="server" CssClass="DepartmentControlPanel">
                    </asp:Panel>
                    <asp:Panel ID="uxProductControlPanel" runat="server" CssClass="DepartmentControlPanel">
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
