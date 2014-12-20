<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericError.aspx.cs" Inherits="Advanced_GenericError"
    ViewStateEncryptionMode="Never" ValidateRequest="false" EnableViewStateMac="false"
    EnableSessionState="True" EnableEventValidation="false" Title="Administrator Panel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Components/Menu/AdminMenu.ascx" TagName="uxLeftMenu" TagPrefix="uc2" %>
<%@ Register Src="Components/Menu/AdminTopMenu.ascx" TagName="uxTopMenu" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="uxForm" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="uxScriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:PlaceHolder ID="uxTrialWarningPlaceHolder" runat="server">
        <div class="WarningMessage">
            <span>VevoCart 6.0 Deluxe Trial Version</span> : Licensed to use in localhost environment
            for 30 days.
        </div>
    </asp:PlaceHolder>
    <nStuff:UpdateHistory runat="server" ID="uxUpdateHistory" OnNavigate="OnUpdateHistoryNavigate" />
    <div id="AdminArea" class="AdminArea">
            <div id="adminMainBorder" class="AdminMainBorder">
                    <div id="adminHeader" class="DefaultHeader">
                        <div class="DefaultHeaderLogo">
                            <asp:HyperLink ID="uxHyperLink" runat="server" NavigateUrl="Default.aspx">
                                <asp:Image ID="uxLogoImage" runat="server" SkinID="VevoLogoImageDefault" />
                            </asp:HyperLink>
                        </div>
                        <div class="DefaultHeaderLink">
                            <div class="login-box">
                        <asp:Panel ID="uxUserPanel" runat="server" CssClass="login-box-panel">
                            <asp:Label ID="uxAdminProgramVersion" runat="server" CssClass="version"><%= SystemConst.CurrentVevoCartVersionNumber() %></asp:Label>
                            <asp:Label ID="uxAdminLogin" runat="server" CssClass="login-text">Logged in as :</asp:Label>
                            <asp:LoginName ID="uxAdminName" runat="server" CssClass="AdminName" />
                        </asp:Panel>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="quick-menu">
                        <asp:Panel ID="uxQuickMenuPanel" runat="server" CssClass="quick-menu-panel">
                            <ul class="quick-menu-link">
                                <li>
                                    <asp:HyperLink ID="uxPaymentHyperLink" runat="server" Target="_blank" CssClass="payment"
                                        Text="Payment application" />
                                </li>
                                <li class="help">
                                    <asp:Menu ID="uxQuickMenu" runat="server" CssClass="help-menu ContentMenuNavMenuList"
                                        StaticEnableDefaultPopOutImage="false" DynamicEnableDefaultPopOutImage="false"
                                        Orientation="Horizontal">
                                        <StaticHoverStyle CssClass="ContentMenuNavMenuListStaticHover" />
                                        <StaticMenuStyle CssClass="ContentMenuNavMenuListStaticMenuStyle" />
                                        <StaticMenuItemStyle CssClass="ContentMenuNavListStaticMenuItem" ItemSpacing="0" />
                                        <StaticSelectedStyle CssClass="ContentMenuNavMenuListStaticSelectItem" />
                                        <DynamicHoverStyle CssClass="ContentMenuNavMenuListDynamicHover" />
                                        <DynamicMenuStyle CssClass="ContentMenuNavMenuListDynamicMenuStyle" />
                                        <DynamicMenuItemStyle CssClass="ContentMenuNavMenuListDynamicMenuItem" />
                                        <DynamicSelectedStyle CssClass="ContentMenuNavMenuListDynamicSelectItem" />
                                        <Items>
                                            <asp:MenuItem Text="Help" ImageUrl="" NavigateUrl="">
                                                <asp:MenuItem NavigateUrl="http://www.vevocart.com/document.aspx" Text="VevoCart Document"
                                                    Target="_blank" />
                                                <asp:MenuItem NavigateUrl="http://forum.vevocart.com/" Text="VevoCart Community"
                                                    Target="_blank" />
                                                <asp:MenuItem NavigateUrl="http://support.vevocart.com/" Text="Support Ticket" Target="_blank" />
                                            </asp:MenuItem>
                                        </Items>
                                    </asp:Menu>
                                </li>
                                <li>
                                    <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="login-status" LogoutText="Logout" />
                                </li>
                            </ul>
                        </asp:Panel>
                        <div class="Clear">
                        </div>
                    </div>
                </div>
                <div class="Clear">
                </div>
            </div>
            <uc1:uxTopMenu ID="uxAdminTopMenu" runat="server" />
            <%--//adminbody Set Height with javascript--%>
            <div id="adminBody" class="AdminBody">
                <div id="adminBodyMenu" class="fl AdminBodyMenu">
                    <uc2:uxLeftMenu ID="uxLeftMenu" runat="server" MenuModeUse="Image" />
                </div>
                <div id="adminBodyContent" class="fl AdminBodyContent">
                    <asp:UpdatePanel ID="uxContentUpdatePanel" runat="server" UpdateMode="Conditional"
                        RenderMode="Block">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="uxContentPlaceHolder" runat="server"></asp:PlaceHolder>
                            <div style="display: none">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                            <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" />
                            <div class="Clear">
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
    <asp:UpdateProgress DynamicLayout="false" ID="uxUpdateProgress" runat="server">
        <ProgressTemplate>
            <div class="AjaxUpdateProgress">
                <asp:Image ID="uxImageLoading" runat="server" SkinID="VevoAjaxLoaderWaiting" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:AlwaysVisibleControlExtender ID="uxAlwaysVisibleControlExtender" runat="server"
        TargetControlID="uxUpdateProgress" HorizontalSide="Center" VerticalSide="Middle"
        HorizontalOffset="5" VerticalOffset="5">
    </ajaxToolkit:AlwaysVisibleControlExtender>
    <asp:HiddenField ID="scrollBarLeft" runat="server" Value="0" />
    <asp:HiddenField ID="scrollBarTop" runat="server" Value="0" />
    </form>
</body>
</html>

