<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerTab.ascx.cs" 
Inherits="Components_ManufacturerTab" %>
<%@ Register Src="ManufacturerTabItem.ascx" TagName="ManufacturerTabItem" TagPrefix="uc3" %>

<div class="ManufacturerPanel">
    <div class="List">
        <asp:DataList ID="uxList" CssClass="ManufacturerTabDefaultDataList" runat="server" >
            <ItemTemplate>
                <uc3:ManufacturerTabItem ID="uxManufacturerList" runat="server" />
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>