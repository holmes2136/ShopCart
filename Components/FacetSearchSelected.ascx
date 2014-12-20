<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchSelected.ascx.cs"
    Inherits="Components_FacetSearchSelected" %>
<div class="FacetedSearchSelected" id="uxFacetedSearchSelectedDiv" runat="server">
    <div class="FacetedSelectedBox">
        <asp:DataList ID="uxFacetSearchList" runat="server" ShowFooter="false">
            <ItemTemplate>
                <asp:Label ID="uxGroupLabel" runat="server" Text='<%# GetGroupName( Container.DataItem ) %>'
                    CssClass="GroupLabel" />
                <asp:Label ID="uxSelectedLabel" runat="server" Text='<%# GetDisplayText(Container.DataItem ) %>' />
                <asp:HyperLink ID="uxDeleteLink" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                    NavigateUrl='<%# GetRemovePostBackUrl(Container.DataItem) %>'></asp:HyperLink>
            </ItemTemplate>
            <HeaderStyle />
        </asp:DataList>
    </div>
    <asp:HyperLink ID="hyperLink" runat="server" Text='[$ClearAll]' NavigateUrl='<%# GetNavUrl() %>'></asp:HyperLink>
</div>
<div class="Clear">
</div>
