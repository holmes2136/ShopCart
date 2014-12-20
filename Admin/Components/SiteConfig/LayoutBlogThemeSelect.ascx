<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayoutBlogThemeSelect.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_LayoutBlogThemeSelect" %>
<%@ Register Src="LayoutCommonSelect.ascx" TagName="LayoutCommonSelect" TagPrefix="uc1" %>
<uc1:LayoutCommonSelect ID="uxCommonSelect" runat="server" PanelCss="ConfigRow" LabelText="Blog Theme"
    LabelCss="Label" DropDownCss="fl DropDown" ImagePanelCss="ImagePanelSettingControl"
    BoxRadius="8" BoxCorners="All" BoxBorderColor="Black" ConfigName="BlogTheme" />
