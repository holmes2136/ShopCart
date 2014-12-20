<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataFeedDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_DataFeedDetails" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc2" %>
<%@ Register Src="StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc3" %>
<div class="mgl5 mgr5">
    <div>
        <asp:Label ID="uxTitleLabel" runat="server" meta:resourcekey="uxTitleLabel" CssClass="CommonTextTitle"></asp:Label>
    </div>
    <asp:Panel ID="uxDataFeedTitlePanel" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxDataFeedTitleLabel" runat="server" meta:resourcekey="uxDataFeedTitleLabel"></asp:Label></div>
        <asp:TextBox ID="uxDataFeedTitleText" runat="server" CssClass="fl TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxDataFeedDescriptionPanel" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxDataFeedDescriptionLabel" runat="server" meta:resourcekey="uxDataFeedDescriptionLabel"></asp:Label>
        </div>
        <asp:TextBox ID="uxDataFeedDescriptionText" runat="server" CssClass="fl TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </asp:Panel>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxUploadDirectoryLabel" runat="server" meta:resourcekey="uxUploadDirectoryLabel"></asp:Label>
        </div>
        <div class="fl">
            <asp:Label ID="uxDirectoryLabel" runat="server" CssClass="fl"></asp:Label>
            <asp:TextBox ID="uxFileNameText" runat="server" Width="200px" CssClass="TextBox mgl5"></asp:TextBox>
            <asp:Label ID="uxFileExtensionLabel" runat="server" CssClass="fl mgl5"></asp:Label>
        </div>
        <div class="Clear">
        </div>
    </div>
    <asp:Panel ID="uxShippingMethodTR" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxShippingMethodLabel" runat="server" Text="Shipping Method"></asp:Label></div>
        <asp:DropDownList ID="uxShippingMethodDrop" runat="server" CssClass="fl DropDown">
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxMediumTR" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxMediumLabel" runat="server" Text="Medium (Required for Music and Video products only)"></asp:Label></div>
        <asp:DropDownList ID="uxMediumDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="None" Text="None"></asp:ListItem>
            <asp:ListItem Value="CD" Text="CD"></asp:ListItem>
            <asp:ListItem Value="Cassette" Text="Cassette"></asp:ListItem>
            <asp:ListItem Value="MiniDisc" Text="MiniDisc"></asp:ListItem>
            <asp:ListItem Value="LP" Text="LP"></asp:ListItem>
            <asp:ListItem Value="EP" Text="EP"></asp:ListItem>
            <asp:ListItem Value="45" Text="45"></asp:ListItem>
            <asp:ListItem Value="DVD" Text="DVD"></asp:ListItem>
            <asp:ListItem Value="VHS" Text="VHS"></asp:ListItem>
            <asp:ListItem Value="VCD" Text="VCD"></asp:ListItem>
            <asp:ListItem Value="Beta" Text="Beta"></asp:ListItem>
            <asp:ListItem Value="8mm" Text="8mm"></asp:ListItem>
            <asp:ListItem Value="Laser Disc" Text="Laser Disc"></asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxProductConditionTR" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxProductConditionLabel" runat="server" Text="Product Condition"></asp:Label></div>
        <asp:DropDownList ID="uxProductConditionDrop" runat="server" CssClass="DropDown fl">
            <asp:ListItem Value="None" Text="None"></asp:ListItem>
            <asp:ListItem Value="New" Text="New"></asp:ListItem>
            <asp:ListItem Value="Like New" Text="Like New"></asp:ListItem>
            <asp:ListItem Value="Generic" Text="Generic"></asp:ListItem>
            <asp:ListItem Value="Refurbished" Text="Refurbished"></asp:ListItem>
            <asp:ListItem Value="Used" Text="Used"></asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxGoogleProductConditionTR" runat="server" CssClass="CommonRowStyle"
        Visible="false">
        <div class="Label">
            <asp:Label ID="uxGoogleProductConditionLabel" runat="server" Text="Product Condition"></asp:Label>
        </div>
        <asp:DropDownList ID="uxGoogleProductConditionDrop" runat="server" CssClass="DropDown fl">
            <asp:ListItem Value="New" Text="New"></asp:ListItem>
            <asp:ListItem Value="Used" Text="Used"></asp:ListItem>
            <asp:ListItem Value="Refurbished" Text="Refurbished"></asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
        <div class="Label">
            <asp:Label ID="uxGoogleCountryLabel" runat="server" Text="Target Country"></asp:Label>
        </div>
        <div class="CountrySelect fl">
            <asp:DropDownList ID="uxGoogleCountryDrop" runat="server" CssClass="DropDown fl">
                <asp:ListItem Value="AU" Text="Australia" />
                <asp:ListItem Value="BR" Text="Brazil" />
                <asp:ListItem Value="CN" Text="China" />
                <asp:ListItem Value="CZ" Text="Czech Republic" />
                <asp:ListItem Value="FR" Text="France" />
                <asp:ListItem Value="DE" Text="Germany" />
                <asp:ListItem Value="IT" Text="Italy" />
                <asp:ListItem Value="JP" Text="Japan" />
                <asp:ListItem Value="NL" Text="Netherlands" />
                <asp:ListItem Value="ES" Text="Spain" />
                <asp:ListItem Value="CH" Text="Switzerland" />
                <asp:ListItem Value="GB" Text="United Kingdom" />
                <asp:ListItem Value="US" Text="United States" Selected="True" />
            </asp:DropDownList>
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxStockDescriptionTR" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxStockDescriptionLabel" runat="server" Text="Stock Description"></asp:Label></div>
        <asp:TextBox ID="uxStockDescriptionText" runat="server" MaxLength="21" CssClass="fl TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </asp:Panel>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxOutOfStockLabel" runat="server" Text="Include out-of-stock product"></asp:Label></div>
        <asp:CheckBox ID="uxOutOfStockCheck" runat="server" CssClass="fl CheckBox" />
        <div class="Clear">
        </div>
    </div>
    <asp:Panel ID="uxGoogleProductCategoryPanel" runat="server" CssClass="CommonRowStyle"
        Visible="false">
        <div class="Label">
            <asp:Label ID="uxGoogleProductCategory" runat="server" Text="Google Category"></asp:Label></div>
        <asp:CheckBox ID="uxGoogleProductCategoryCheck" runat="server" Checked="false" CssClass="fl CheckBox"
            OnCheckedChanged="uxGoogleProductCategoryCheck_CheckedChanged" AutoPostBack="true" />
        <div class="Clear">
        </div>
        <div id="uxMainCategoryDiv" runat="server" visible="false">
            <asp:Label ID="uxMaingCategoryLabel" runat="server" Text="Main Category" CssClass="Label"></asp:Label>
            <asp:DropDownList ID="uxGoogleProductCategoryDrop" runat="server" CssClass="DropDown fl"
                OnSelectedIndexChanged="uxGoogleProductCategoryDrop_SelectIndexChanged" AutoPostBack="true"
                Width="600px" />
        </div>
        <div class="Clear">
        </div>
        <div id="uxSubCategoryDiv" runat="server" visible="false">
            <asp:Label ID="uxSubCategoryLabel" runat="server" Text="Selected Category" CssClass="Label"></asp:Label>
            <asp:DropDownList ID="uxGoogleSubCategoryDrop" runat="server" CssClass="DropDown fl"
                Width="600px" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxStoreFilterPanel" runat="server" CssClass="CommonRowStyle">
        <div class="Label">
            <asp:Label ID="uxStoreFilterLabel" runat="server" Text="Store"></asp:Label></div>
        <uc3:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" DisplayLabel="false" />
        <div class="Clear">
        </div>
    </asp:Panel>
</div>
<div class="mgt20">
    <asp:GridView ID="uxGridCategory" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
        AllowSorting="false" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxCheck')">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="uxCategoryIDHidden" Value='<%# Bind("CategoryID") %>' runat="server" />
                    <asp:CheckBox ID="uxCheck" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:CategoryDataFeedFields, Name %>">
                <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                <ItemTemplate>
                    <asp:Label ID="uxCategoryName" runat="server" Text='<% # Eval("Name").ToString()%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
</div>
