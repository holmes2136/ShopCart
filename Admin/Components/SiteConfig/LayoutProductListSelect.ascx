<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayoutProductListSelect.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_LayoutProductListSelect" %>
<%@ Register Src="LayoutCommonSelect.ascx" TagName="LayoutCommonSelect" TagPrefix="uc1" %>
<uc1:LayoutCommonSelect ID="uxCommonSelect" runat="server" PanelCss="ConfigRow mgt20"
    LabelText="Product List Default layouts" LabelCss="Label" DropDownCss="fl DropDown"
    ImagePanelCss="ImagePanelSettingControl" BoxRadius="8" BoxCorners="All" BoxBorderColor="Black"
    ConfigName="DefaultProductListLayout" />
