<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="AdminAdvanced_Components_Menu_AdminMenu" %>
<%@ Register Src="../Common/AdminItemBox.ascx" TagName="AdminItemBox" TagPrefix="uc1" %>
<div id="innerMenu" class="AdminMenu">
    <div id="MenuShow" class="AdminMenuItem">
        <div class="AdminMenuItemList">
            <uc1:AdminItemBox ID="uxAdminItemBox" runat="server" />
            <div class="AdminMenuBottomSpace">&nbsp;</div>        </div>
    </div>
    <div id="MenuShowA" class="fl" style="position: relative;">
        <div class="Clear">
        </div>
        <asp:Image ID="uxHideMenuImage" runat="server" SkinID="FrameLeft" /></div>
    <div id="MenuHide" style="display: none;" class="fl vt">
        <asp:Image ID="uxShowMenuTextImage" runat="server" SkinID="FrameText" />
    </div>
    <div id="MenuHideA" style="display: none;" class="fl">
        <asp:Image ID="uxShowMenuImage" runat="server" SkinID="FrameRight" /></div>
    <div class="Clear">
    </div>
</div>
