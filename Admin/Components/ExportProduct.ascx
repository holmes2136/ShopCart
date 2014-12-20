<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportProduct.ascx.cs"
    Inherits="Admin_Components_ExportProduct" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="ExportFilter"
    TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="ExportNotice">
            <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice" CssClass="CommonTextTitle" />
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxStoreLabel" runat="Server" meta:resourcekey="uxStoreLabel" CssClass="Label" />
                <asp:DropDownList ID="uxStoreDrop" runat="server" CssClass="fl DropDown" />
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxLanguageLabel" runat="Server" meta:resourcekey="uxLanguageLabel" CssClass="Label" />
                <asp:DropDownList ID="uxLanguageDrop" runat="server" DataSourceID="uxCultureSource"
                    DataTextField="DisplayName" DataValueField="CultureID" CssClass="fl DropDown">
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxSortByLabel" runat="Server" meta:resourcekey="uxSortByLabel" CssClass="Label" />
                <asp:DropDownList ID="uxSortByDrop" runat="server" CssClass="DropDown" />
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <uc2:ExportFilter ID="uxExportFilter" runat="server" IsExportFilter="true" />
        </div>
        <div>
            <vevo:AdvanceButton ID="uxGenProductKitItemButton" runat="server" meta:resourcekey="uxGenProductKitItemButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenProductKitItemButton_Click" OnClickGoTo="top" />
            <vevo:AdvanceButton ID="uxGenSpecValueButton" runat="server" meta:resourcekey="uxGenSpecValueButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenSpecValueButton_Click" OnClickGoTo="top" />
            <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenerateButton_Click" OnClickGoTo="top" />
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
