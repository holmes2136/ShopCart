<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchSpecification.ascx.cs"
    Inherits="Components_FacetSearchSpecification" %>
<%@ Register Src="~/Components/FacetSearchSpecificationValue.ascx" TagName="FacetSearchSpecificationValue"
    TagPrefix="uc1" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="FacetedSearchNavList">
    <HeaderStyle />
    <ItemTemplate>
        <uc1:FacetSearchSpecificationValue ID="uxFacetSearchSpecificationValue" runat="server" />
    </ItemTemplate>
</asp:DataList>
