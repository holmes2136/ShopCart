<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CatagoryList.ascx.cs"
    Inherits="Mobile_Components_CatagoryList" %>
<%@ Register Src="MobilePagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<div class="MobileListDiv">
    <asp:DataList ID="uxMobileList" CssClass="MobileMenuList" runat="server" ShowFooter="False" 
        OnItemDataBound="uxMobileList_OnItemDataBound">
        <ItemStyle CssClass="MobileMenu" />
        <ItemTemplate>
            <asp:HyperLink ID="uxNameLink" CssClass="MobileHyperLink" runat="server" NavigateUrl='<%# GetCatUrl(Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString()) %>'>
            <%# Eval("Name") %>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:DataList>
</div>
<div class="MobilePagingControlMainDiv">
    <table cellpadding="0" cellspacing="0" class="MobilePagingControl">
        <tr>
            <uc3:PagingControl ID="uxMobilePagingControl" runat="server" />
        </tr>
    </table>
</div>
