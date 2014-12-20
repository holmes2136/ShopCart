<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleSitemaps.ascx.cs"
    Inherits="AdminAdvanced_MainControls_GoogleSitemaps" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
        <div class="CommonDownloadLink">
            <asp:HyperLink ID="uxFileNameLink" runat="server"></asp:HyperLink>
        </div>
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ConfigValid" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <PlainContentTemplate>
        <div class="CommonConfigTitle mgt0">
            <asp:Label ID="uxTitleLabel" runat="server" meta:resourcekey="uxTitleLabel"></asp:Label></div>
    </PlainContentTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <asp:Panel ID="uxStoreFilterPanel" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxStoreFilterLabel" runat="server" meta:resourcekey="uxStoreFilterLabel"></asp:Label></div>
                <asp:DropDownList ID="uxStoreDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxStoreDrop_SelectedIndexChanged"
                    CssClass="DropDown">
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxSiteMapIncludesCategoryLabel" CssClass="Label" runat="server" meta:resourcekey="uxSiteMapIncludesCategoryLabel"></asp:Label>
                <asp:DropDownList ID="uxSiteMapIncludesCategoriesDropDown" runat="server" CssClass="fl DropDown"
                    AutoPostBack="true" OnSelectedIndexChanged="uxSiteMapIncludesCategoriesDropDown_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle" id="uxCategoryRangePanel" runat="server">
                <div class="BulletLabel">
                    Include Categories:</div>
                <div class="fl">
                    <div class="fl">
                        From</div>
                    <asp:TextBox ID="uxCategoryStartText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    <div class="fl">
                        &nbsp;&nbsp;To</div>
                    <asp:TextBox ID="uxCategoryEndText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    &nbsp;of&nbsp;
                    <asp:Label ID="uxAllCategoryLabel" runat="server"></asp:Label>
                    &nbsp;categories.
                    <asp:CompareValidator ID="uxCategoryStartCompareValidator" runat="server" ErrorMessage="Category Start Index must be integer"
                        ControlToValidate="uxCategoryStartText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:CompareValidator ID="uxCategoryEndCompareValidator" runat="server" ErrorMessage="Category End Index must be integer"
                        ControlToValidate="uxCategoryEndText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:RangeValidator ID="uxCategoryStartValidator" runat="server" ControlToValidate="uxCategoryStartText"
                        Display="None" ErrorMessage="Number of Category Start Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                    <asp:RangeValidator ID="uxCategoryEndValidator" runat="server" ControlToValidate="uxCategoryEndText"
                        Display="None" ErrorMessage="Number of Category End Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxSiteMapIncludesProductLabel" CssClass="Label" runat="server" meta:resourcekey="uxSiteMapIncludesProductLabel"></asp:Label>
                <asp:DropDownList ID="uxSiteMapIncludesProductsDropDown" runat="server" CssClass="fl DropDown"
                    AutoPostBack="true" OnSelectedIndexChanged="uxSiteMapIncludesProductsDropDown_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle" id="uxProductRangePanel" runat="server">
                <div class="BulletLabel">
                    Include Products:</div>
                <div class="fl">
                    <div class="fl">
                        From</div>
                    <asp:TextBox ID="uxProductStartText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    <div class="fl">
                        &nbsp;&nbsp;To</div>
                    <asp:TextBox ID="uxProductEndText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    &nbsp;of&nbsp;
                    <asp:Label ID="uxAllProductLabel" runat="server"></asp:Label>
                    &nbsp;products.
                    <asp:CompareValidator ID="uxProductStartCompareValidator" runat="server" ErrorMessage="Product Start Index must be integer"
                        ControlToValidate="uxProductStartText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:CompareValidator ID="uxProductEndCompareValidator" runat="server" ErrorMessage="Product End Index must be integer"
                        ControlToValidate="uxProductEndText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:RangeValidator ID="uxProductStartValidator" runat="server" ControlToValidate="uxProductStartText"
                        Display="None" ErrorMessage="Number of Product Start Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                    <asp:RangeValidator ID="uxProductEndValidator" runat="server" ControlToValidate="uxProductEndText"
                        Display="None" ErrorMessage="Number of Product End Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxSiteMapIncludesContentLabel" CssClass="Label" runat="server" meta:resourcekey="uxSiteMapIncludesContentLabel"></asp:Label>
                <asp:DropDownList ID="uxSiteMapIncludesContentsDropDown" runat="server" CssClass="fl DropDown"
                    AutoPostBack="true" OnSelectedIndexChanged="uxSiteMapIncludesContentsDropDown_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle" id="uxContentRangePanel" runat="server">
                <div class="BulletLabel">
                    Include Content Pages:</div>
                <div class="fl">
                    <div class="fl">
                        From</div>
                    <asp:TextBox ID="uxContentStartText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    <div class="fl">
                        &nbsp;&nbsp;To</div>
                    <asp:TextBox ID="uxContentEndText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    &nbsp;of&nbsp;
                    <asp:Label ID="uxAllContentLabel" runat="server"></asp:Label>
                    &nbsp;pages.
                    <asp:CompareValidator ID="uxContentStartCompareValidator" runat="server" ErrorMessage="Content Pages Start Index must be integer"
                        ControlToValidate="uxContentStartText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:CompareValidator ID="uxContentEndCompareValidator" runat="server" ErrorMessage="Content Pages End Index must be integer"
                        ControlToValidate="uxContentEndText" Display="None" Type="integer" Operator="DataTypeCheck"
                        ValidationGroup="ConfigValid"></asp:CompareValidator>
                    <asp:RangeValidator ID="uxContentStartValidator" runat="server" ControlToValidate="uxContentStartText"
                        Display="None" ErrorMessage="Number of  Content Pages Start Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                    <asp:RangeValidator ID="uxContentEndValidator" runat="server" ControlToValidate="uxContentEndText"
                        Display="None" ErrorMessage="Number of Content Pages End Index Column must be greater than Zero."
                        MaximumValue="99999" MinimumValue="1" ValidationGroup="ConfigValid">
                    </asp:RangeValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxDefaultChangeFreqLabel" CssClass="Label" runat="server" meta:resourcekey="uxDefaultChangeFreqLabel"></asp:Label>
                <asp:DropDownList ID="uxDefaultChangeFreqDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="always" Text="always"></asp:ListItem>
                    <asp:ListItem Value="hourly" Text="hourly"></asp:ListItem>
                    <asp:ListItem Value="daily" Text="daily"></asp:ListItem>
                    <asp:ListItem Value="weekly" Text="weekly"></asp:ListItem>
                    <asp:ListItem Value="monthly" Text="monthly"></asp:ListItem>
                    <asp:ListItem Value="yearly" Text="yearly"></asp:ListItem>
                    <asp:ListItem Value="never" Text="never"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxDefaultPriorityLabel" CssClass="Label" runat="server" meta:resourcekey="uxDefaultPriorityLabel"></asp:Label>
                <asp:TextBox ID="uxDefaultPriorityText" runat="server" CssClass="fl TextBox" ValidationGroup="ConfigValid"></asp:TextBox>
                <div class="validator1 fl">
                    <asp:RequiredFieldValidator ID="uxDefaultPriorityRequiredField" runat="server" ControlToValidate="uxDefaultPriorityText"
                        Display="Dynamic" ValidationGroup="ConfigValid" ErrorMessage="Default URL Priority must fill value"><--
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="uxTaxPercentageIncludedInPriceRange" runat="server" ControlToValidate="uxDefaultPriorityText"
                        Display="Dynamic" ErrorMessage="Your Default URL Priority must be between 0.0 to 1.0"
                        MaximumValue="1.0" MinimumValue="0" ValidationGroup="ConfigValid" Type="Double"><--</asp:RangeValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyleButton mgt10">
                <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton"
                    CssClassBegin="fr " CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxGenerateButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ConfigValid"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxSaveSettingButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSaveSettingButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ConfigValid"></vevo:AdvanceButton></div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
