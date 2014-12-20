<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerNavNormalList.ascx.cs" Inherits="Components_ManufacturerNavNormalList" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="ManufacturerNavNormalList">
    <HeaderStyle />
    <ItemTemplate>
        <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetManufacturerUrl( Eval( "ManufacturerID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
            Text='<%# GetNavName( Container.DataItem ) %>' ><%--ToolTip='<%# Eval("ShortDescription") %>'--%>
        </asp:HyperLink>
    </ItemTemplate>
</asp:DataList>