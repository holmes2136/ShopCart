<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryImport.ascx.cs"
    Inherits="Admin_MainControls_CategoryImport" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="admin" runat="server">
    <ContentTemplate>
        <div class="ExportNotice">
            <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice"
                CssClass="CommonTextTitle" />
        </div>
        <asp:Panel ID="uxImportCategoryMessagePanel" runat="server" CssClass="CommonRowStyle">
        </asp:Panel>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <div class="Label">
                    Type in File Name</div>
                <asp:TextBox Width="300" ID="uxCategoryCsvFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    &nbsp;</div>
                <asp:Label ID="uxSampleCategoryCsvLabel" runat="server" Text="<i>( Example : import\xxxxx.csv )</i>"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <div class="Label" style="margin: 10px 0px">
                    Or
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="mgb10 mgt10 CommonRowStyle">
                <div class="Label">
                    <vevo:AdvancedLinkButton ID="uxCategoryCsvFileNameUploadLinkButton" runat="server"
                        OnClick="uxCategoryCsvFileNameUploadLinkButton_Click"
                         Text="Upload A File" CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <uc3:Upload ID="uxCategoryCsvFileUpload" runat="server" ShowControl="false" PathDestination="Import\"
                CheckType="Csv" CssClass="CommonRowStyle" MaxFileSize="20 MB" ButtonImage="Browse.png"
                ButtonWidth="72px" ButtonText="Browse..." ShowText="false" />
            <div class="CommonRowStyle">
                <div class="Label">
                    Language
                </div>
                <asp:DropDownList ID="uxLanguageDrop" runat="server" DataSourceID="uxCultureSource"
                    DataTextField="DisplayName" DataValueField="CultureID" CssClass="fl DropDown">
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxStorePanel" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    Store
                </div>
                <asp:DropDownList ID="uxStoreDrop" runat="server" CssClass="fl DropDown" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    Import Mode</div>
                <asp:RadioButtonList ID="uxCategoryImportModeRadioList" runat="server" RepeatDirection="Horizontal"
                    CssClass="fl DropDown">
                    <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                </asp:RadioButtonList>
                <div class="Clear">
                </div>
            </div>
            <vevo:AdvanceButton ID="uxCategoryImportButton" runat="server" Text="Import" CssClassBegin="mgt10 fr"
                CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxCategoryImportButton_Click"
                OnClickGoTo="Top"></vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
