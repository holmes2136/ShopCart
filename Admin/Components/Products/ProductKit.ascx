<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKit.ascx.cs" Inherits="Admin_Components_Products_ProductKit" %>
<%@ Register Src="../MultiProductKitGroup.ascx" TagName="MultiProductKitGroup" TagPrefix="uc1" %>
<div>
    <asp:Panel ID="uxProductKitTR" runat="server" CssClass="ProductDetailsRowTitle mgt10">
        <asp:Label ID="lcProductKitHeader" runat="server" Text="Product Kit" />
    </asp:Panel>
    <asp:Panel ID="uxIsProductKitTR" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="lcIsProductKit" runat="server" Text="Is Product Kit" CssClass="Label" />
            <asp:DropDownList ID="uxIsProductKitDrop" runat="server" AutoPostBack="true" CssClass="fl DropDown">
                <asp:ListItem Value="True">Yes</asp:ListItem>
                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
            </asp:DropDownList>
            <div class="Clear">
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="uxLinkToProductKitGroupTR" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="lcProductKitGroup" runat="server" Text="Product Kit Group" CssClass="Label" />
            <asp:DropDownList ID="uxProductKitGroupDrop" runat="server" Width="255px" Visible="false"
                CssClass="fl DropDown">
            </asp:DropDownList>
            <uc1:MultiProductKitGroup ID="uxMultiProductKitGroup" runat="server" />
            <div class="Clear">
            </div>
        </div>
    </asp:Panel>
</div>
