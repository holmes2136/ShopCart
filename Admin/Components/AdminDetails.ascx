<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_AdminDetails" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc1" %>
<%@ Register Src="SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<uc2:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="AdminEdit" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol"></asp:Label></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName"></asp:Label></div>
                <asp:TextBox ID="uxUserName" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <asp:RequiredFieldValidator ID="uxRequireUserNameValid" runat="server" ControlToValidate="uxUserName"
                Display="Dynamic" ValidationGroup="AdminEdit" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Username is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
            </asp:RequiredFieldValidator>
            <asp:Panel ID="uxOldPwd" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcOldPassword" runat="server" meta:resourcekey="lcOldPassword"></asp:Label></div>
                <asp:TextBox ID="uxOldPassword" runat="server" TextMode="Password" Width="150px"
                    CssClass="fl TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword"></asp:Label></div>
                <asp:TextBox ID="uxPassword" runat="server" Width="150px" TextMode="Password" CssClass="fl TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcRePassword" runat="server" meta:resourcekey="lcRePassword"></asp:Label></div>
                <asp:TextBox ID="uxRePassword" runat="server" Width="150px" TextMode="Password" CssClass="fl TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail"></asp:Label></div>
                <asp:TextBox ID="uxEmail" runat="server" Width="266px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequireEmailValid" runat="server" ControlToValidate="uxEmail"
                    Display="Dynamic" ValidationGroup="AdminEdit" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
                            <div class="CommonValidateDiv CommonValidateDivAdminAddLong">
                            </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="uxEmail" Display="Dynamic" CssClass="CommonValidatorText"
                    ValidationGroup="AdminEdit">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is in the wrong format.
                            <div class="CommonValidateDiv CommonValidateDivAdminAddLong">
                            </div>
                </asp:RegularExpressionValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcRegisterDate" runat="server" meta:resourcekey="lcRegisterDate" /></div>
                <uc1:CalendarPopup ID="uxRegisterDateCalendarPopup" runat="server" TextBoxEnabled="false" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="detailsNoLabel1">
                    <div>
                        <asp:Label ID="uxMessagePermissionTable" runat="server" CssClass="fb SearchFilterMessage" />
                    </div>
                    <div class="mgt5" style="width: 90%;">
                        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                            Width="100%" AllowSorting="True" DataKeyNames="MenuPageName" OnSorting="uxGrid_Sorting"
                            OnRowDataBound="uxGrid_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="fl mgl10">
                                            <asp:CheckBox ID="SelectAllCheckBoxView" runat="server" /></div>
                                        <div class="fl" style="margin-top: 3px;">
                                            <asp:Label ID="lcView" runat="server" meta:resourcekey="lcView"></asp:Label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="uxCheckVeiw" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="fl mgl10">
                                            <asp:CheckBox ID="SelectAllCheckBoxModify" runat="server" />
                                        </div>
                                        <div class="fl" style="margin-top: 3px;">
                                            <asp:Label ID="lcModified" runat="server" meta:resourcekey="lcModified"></asp:Label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="uxCheckModify" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MenuName" HeaderText="<%$ Resources:AdminFields, MenuName %>"
                                    SortExpression="MenuName">
                                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MenuPageName" HeaderText="<%$ Resources:AdminFields, MenuPageName %>"
                                    SortExpression="MenuPageName" Visible="False"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="AdminEdit">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="AdminEdit">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminUserControlContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
