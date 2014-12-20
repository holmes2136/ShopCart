<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportCategory.ascx.cs"
    Inherits="Admin_Components_ExportCategory" %>
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
            <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice"
                CssClass="CommonTextTitle" />
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxLanguageLabel" runat="Server" meta:resourcekey="uxLanguageLabel"
                    CssClass="Label" />
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
            <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton"
                CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxGenerateButton_Click"
                OnClickGoTo="top" />
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
