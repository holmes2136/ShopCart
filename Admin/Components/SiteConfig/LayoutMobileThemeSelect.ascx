<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayoutMobileThemeSelect.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_LayoutMobileThemeSelect" %>
<%@ Register Src="LayoutCommonSelect.ascx" TagName="LayoutCommonSelect" TagPrefix="uc1" %>
<uc1:LayoutCommonSelect ID="uxCommonSelect" runat="server" PanelCss="ConfigRow" LabelText="Mobile Theme"
    LabelCss="Label" DropDownCss="fl DropDown" ImagePanelCss="ImagePanelSettingControl"
    BoxRadius="8" BoxCorners="All" BoxBorderColor="Black" ConfigName="MobileTheme" />
