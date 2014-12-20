<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductBulkImport.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ProductBulkImport" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="admin" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
    </MessageTemplate>
    <ContentTemplate>
        <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer Container-Box1">
            <ajaxToolkit:TabPanel ID="uxProductImportTabPanel" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <div>
                        <asp:Label ID="lcProductImport" runat="server" Text="Product Import" /></div>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="uxMessagePanel" runat="server" CssClass="CommonRowStyle">
                    </asp:Panel>
                    <div class="Container-Row">
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Type in File Name</div>
                            <asp:TextBox Width="300" ID="uxFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;</div>
                            <asp:Label ID="uxSampleLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label" style="margin: 10px 0px">
                                Or
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle mgb10 mgt10">
                            <div class="Label">
                                <vevo:AdvancedLinkButton ID="uxFileNameUploadLinkButton" runat="server" OnClick="uxFileNameUploadLinkButton_Click"
                                    Text="Upload A File" CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <uc6:Upload ID="uxFileUpload" runat="server" ShowControl="false" PathDestination="Import\"
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
                            <asp:RadioButtonList ID="uxModeRadioList" runat="server" RepeatDirection="Horizontal"
                                CssClass="fl DropDown">
                                <asp:ListItem Value="Purge" Selected="True">Purge All</asp:ListItem>
                                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Update Images and Thumbnails
                            </div>
                            <asp:CheckBox ID="uxImageProcessCheck" runat="server" CssClass="fl CheckBox" OnCheckedChanged="uxImageProcessCheck_OnCheckedChanged"
                                AutoPostBack="true" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;
                            </div>
                            <div class="fl">
                                * If "No", image-related fields from CSV will be ignored. This can speed up the
                                Bulk Import process.
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Skip Image Processing
                            </div>
                            <asp:CheckBox ID="uxSkipImageProcessCheck" runat="server" CssClass="fl CheckBox" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;
                            </div>
                            <div class="fl">
                                * If this check box is checked, Image file path will be added to database but image
                                processing will be skipped.
                                <br />
                                it needs to upload regular, thumbnail and large image files manually.
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <vevo:AdvanceButton ID="uxImportButton" runat="server" meta:resourcekey="uxImportButton"
                            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxImportButton_Click"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="uxSpecificationImportTabPanel" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <div>
                        <asp:Label ID="lcSpecificationImport" runat="server" Text="Other Import" /></div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="ExportNotice">
                        <span>Product Specification Import</span></div>
                    <asp:Panel ID="uxImportSpecificationMessagePanel" runat="server">
                    </asp:Panel>
                    <div class="Container-Row">
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Type in File Name</div>
                            <asp:TextBox Width="300" ID="uxSpecificationCsvFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;</div>
                            <asp:Label ID="uxSampleSpecificationCsvLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label" style="margin: 10px 0px">
                                Or
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle mgb10 mgt10">
                            <div class="Label">
                                <vevo:AdvancedLinkButton ID="uxSpecificationCsvFileNameUploadLinkButton" runat="server"
                                    OnClick="uxSpecificationCsvFileNameUploadLinkButton_Click" Text="Upload A File" CssClassBegin="fl"
                        CssClassEnd="Button1Right" CssClass="ButtonOrange" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <uc6:Upload ID="uxSpecificationCsvFileUpload" runat="server" ShowControl="false"
                            PathDestination="Import\" CheckType="Csv" CssClass="CommonRowStyle" MaxFileSize="20 MB"
                            ButtonImage="Browse.png" ButtonWidth="72px" ButtonText="Browse..." ShowText="false" />
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Import Mode</div>
                            <asp:RadioButtonList ID="uxSpecificationImportModeRadioList" runat="server" RepeatDirection="Horizontal"
                                CssClass="fl DropDown">
                                <asp:ListItem Value="Purge" Selected="True">Purge All</asp:ListItem>
                                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="Clear">
                            </div>
                        </div>
                        <vevo:AdvanceButton ID="uxSpecificationImportButton" runat="server" meta:resourcekey="uxImportButton"
                            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSpecificationImportButton_Click"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="ExportNotice" style="margin-top: 15px;">
                        <span>Product Kit Item Import</span></div>

                    <asp:Panel ID="uxImportProductKitItemMessagePanel" runat="server" CssClass="CommonRowStyle">
                    </asp:Panel>
                    <div class="Container-Row">
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Type in File Name</div>
                            <asp:TextBox Width="300" ID="uxProductKitItemCsvFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;</div>
                            <asp:Label ID="uxSampleProductKitItemCsvLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label" style="margin: 10px 0px">
                                Or
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle mgb10 mgt10">
                            <div class="Label">
                                <vevo:AdvancedLinkButton ID="uxProductKitItemCsvFileNameUploadLinkButton" runat="server"
                                    OnClick="uxProductKitItemCsvFileNameUploadLinkButton_Click" Text="Upload A File" 
                                    CssClassEnd="Button1Right" CssClass="ButtonOrange" CssClassBegin="fl" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <uc6:Upload ID="uxProductKitItemCsvFileUpload" runat="server" ShowControl="false"
                            PathDestination="Import\" CheckType="Csv" CssClass="CommonRowStyle" MaxFileSize="20 MB"
                            ButtonImage="Browse.png" ButtonWidth="72px" ButtonText="Browse..." ShowText="false" />
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Import Mode</div>
                            <asp:RadioButtonList ID="uxProductKitItemImportModeRadioList" runat="server" RepeatDirection="Horizontal"
                                CssClass="fl DropDown">
                                <asp:ListItem Value="Purge" Selected="True">Purge All</asp:ListItem>
                                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="Clear">
                            </div>
                        </div>
                        <vevo:AdvanceButton ID="uxProductKitItemImportButton" runat="server" meta:resourcekey="uxImportButton"
                            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxProductKitItemImportButton_Click"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </ContentTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
