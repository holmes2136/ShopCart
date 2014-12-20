<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayoutBlogListSelect.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_LayoutBlogListSelect" %>
<%@ Register Src="LayoutCommonSelect.ascx" TagName="LayoutCommonSelect" TagPrefix="uc1" %>
<uc1:LayoutCommonSelect ID="uxCommonSelect" runat="server" PanelCss="ConfigRow mgt20"
    LabelText="Blog List Default layouts" LabelCss="Label" DropDownCss="fl DropDown"
    ImagePanelCss="ImagePanelSettingControl" BoxRadius="8" BoxCorners="All" BoxBorderColor="Black"
    ConfigName="DefaultBlogListLayout" />
