<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSpecial.ascx.cs"
    Inherits="Mobile_Components_ProductSpecial" %>
<div class="MobileProductSpecial">
    <div class="Top MobileTitleNoBackground">
       <asp:Label ID="uxProductSpecialTitle" runat="server" Text="[$Today]"></asp:Label>
    </div>
    <div class="Left">
        <div class="Right">
            <div class="ProductSpecialImage">
                <asp:Repeater ID="uxRepeater" runat="server">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxLinkLink" runat="server" NavigateUrl='<%# GetUrl(Eval("ProductID"),Eval("UrlName")) %>'>
                            <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("ImageSecondary"))%>' />
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
