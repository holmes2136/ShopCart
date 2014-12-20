<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImportCustomerList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ImportCustomerList" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="admin" runat="server">
    <MessageTemplate>
    </MessageTemplate>
    <ContentTemplate>
        <ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer Container-Box1">
            <ajaxToolkit:TabPanel ID="uxCustomerImportTab" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <div>
                        <asp:Label ID="lcCustomerImport" runat="server" Text="Customer Import" /></div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="ExportNotice">
                        <asp:Label ID="uxMessageNotice" runat="server" Text="Customer Import" CssClass="CommonTextTitle" />
                    </div>
                    <asp:Panel ID="uxImportCustomerMessagePanel" runat="server">
                    </asp:Panel>
                    <div class="Container-Row">
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Type in File Name</div>
                            <asp:TextBox Width="300" ID="uxCustomerCsvFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;</div>
                            <asp:Label ID="uxSampleCustomerCsvLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
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
                                <vevo:AdvancedLinkButton ID="uxCustomerCsvFileNameUploadLinkButton" runat="server"
                                    OnClick="uxCustomerCsvFileNameUploadLinkButton_Click" Text="Upload A File" CssClassBegin="fl"
                                    CssClassEnd="Button1Right" CssClass="ButtonOrange" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <uc3:Upload ID="uxCustomerCsvFileUpload" runat="server" ShowControl="false" PathDestination="Import\"
                            CheckType="Csv" CssClass="CommonRowStyle" MaxFileSize="20 MB" ButtonImage="Browse.png"
                            ButtonWidth="72px" ButtonText="Browse..." ShowText="false" />
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Import Mode</div>
                            <asp:RadioButtonList ID="uxCustomerImportModeRadioList" runat="server" RepeatDirection="Horizontal"
                                CssClass="fl DropDown">
                                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;
                            </div>
                            <div class="fl">
                                * Please note that newly imported Customer will use their zip code as password.
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <vevo:AdvanceButton ID="uxCustomerImportButton" runat="server" meta:resourcekey="uxCustomerImportButton"
                            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxCustomerImportButton_Click"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="uxShippingAddressImportTab" runat="server" CssClass="DefaultTabPanel">
                <HeaderTemplate>
                    <div>
                        <asp:Label ID="lcShippingAddressImport" runat="server" Text="Shipping Address Import" /></div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="ExportNotice">
                        <asp:Label ID="uxMessageNotice2" runat="server" Text="Shipping Address Import" CssClass="CommonTextTitle" />
                    </div>
                    <asp:Panel ID="uxImportShippingAddressMessagePanel" runat="server">
                    </asp:Panel>
                    <div class="Container-Row">
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Type in File Name</div>
                            <asp:TextBox Width="300" ID="uxShippingAddressCsvFileNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label">
                                &nbsp;</div>
                            <asp:Label ID="uxSampleCsvLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
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
                                <vevo:AdvancedLinkButton ID="uxShippingAddressCsvFileNameUploadLinkButton" runat="server"
                                    OnClick="uxShippingAddressCsvFileNameUploadLinkButton_Click" Text="Upload A File"
                                    CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                        <uc3:Upload ID="uxShippingAddressCsvFileUpload" runat="server" ShowControl="false"
                            PathDestination="Import\" CheckType="Csv" CssClass="CommonRowStyle" MaxFileSize="20 MB"
                            ButtonImage="Browse.png" ButtonWidth="72px" ButtonText="Browse..." ShowText="false" />
                        <div class="CommonRowStyle">
                            <div class="Label">
                                Import Mode</div>
                            <asp:RadioButtonList ID="uxShippingAddressImportModeRadioList" runat="server" RepeatDirection="Horizontal"
                                CssClass="fl DropDown">
                                <%--<asp:ListItem Value="Purge" Selected="True">Purge All</asp:ListItem>--%>
                                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="Clear">
                            </div>
                        </div>
                        <vevo:AdvanceButton ID="uxShippingAddressImportButton" runat="server" meta:resourcekey="uxShippingAddressImportButton"
                            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxShippingAddressImportButton_Click"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </ContentTemplate>
</uc1:AdminContent>
