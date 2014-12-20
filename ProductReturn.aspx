<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ProductReturn.aspx.cs"
    Inherits="ProductReturn" Title="[$Title]" EnableEventValidation="False" %>

<%@ Import Namespace="Vevo.WebUI" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:Message ID="uxErrorMessage" runat="server" />
    <div class="ProductReturn">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:ValidationSummary ID="uxValidationSummary" runat="server" HeaderText="Please correct the following errors:<br/>"
                        ValidationGroup="ValidReturn" />
                    <asp:GridView ID="uxProductReturnGrid" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" GridLines="None" CssClass="CommonGridView ProductReturnGridView"
                        OnRowDataBound="uxProductReturnGrid_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="uxSelectBox" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="ProductReturnSelectItem" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Name]">
                                <ItemTemplate>
                                    <asp:Label ID="uxNameProductLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Quantity]">
                                <ItemTemplate>
                                    <asp:Label ID="uxQuantityLabel" runat="server" Text='<%# "[$Max]: " + Eval("Quantity") %>' /><br />
                                    <asp:TextBox ID="uxQuantityText" runat="server" Width="70" />
                                    <asp:RangeValidator ID="uxQuantityRange" runat="server" ErrorMessage="[$InvalidQuantity]"
                                        ForeColor="Red" ControlToValidate="uxQuantityText" Display="Dynamic" MinimumValue="1"
                                        MaximumValue='<%# Eval("Quantity") %>' Type="Integer" ValidationGroup="ValidReturn">*</asp:RangeValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                                <HeaderStyle CssClass="ProductReturnQuantityItem" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$UnitPrice]">
                                <ItemTemplate>
                                    <asp:Label ID="uxUnitPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( Eval("UnitPrice") ) )%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ProductReturnUnitPriceItem" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="CommonGridViewRowStyle" />
                        <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                        <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                        <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                    </asp:GridView>
                    <asp:CustomValidator ID="uxGridValidator" runat="server" ErrorMessage="[$RequireProduct]"
                        ValidationGroup="ValidReturn" Display="None"></asp:CustomValidator>
                    <div class="ProductReturnDiv">
                        <div class="CommonFormLabel">
                            [$Reason]</div>
                        <div class="ProductReturnData">
                            <asp:TextBox ID="uxReasonText" runat="server" Rows="10" TextMode="MultiLine" CssClass="ProductReturnTextBox" />
                            <asp:RequiredFieldValidator ID="uxResonValidator" runat="server" ControlToValidate="uxReasonText"
                                ForeColor="Red" ValidationGroup="ValidReturn" Display="Dynamic" CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ProductReturnValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$RequireReason]
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="CommonFormLabel">
                            [$Return]</div>
                        <div class="ProductReturnData">
                            <asp:DropDownList ID="uxReturnDrop" runat="server" CssClass="ProductReturnDropDown" />
                            <asp:RequiredFieldValidator ID="uxReturnValidator" runat="server" ControlToValidate="uxReturnDrop"
                                InitialValue="[$Select]" ForeColor="Red" ValidationGroup="ValidReturn" Display="Dynamic"
                                CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ProductReturnValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$RequireReturn]
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="CommonFormLabel">
                            [$Note]</div>
                        <div class="ProductReturnData">
                            <asp:TextBox ID="uxNoteText" runat="server" Rows="10" TextMode="MultiLine" CssClass="ProductReturnTextBox" />
                        </div>
                    </div>
                    <div class="ProductReturnAddButtonDiv">
                        <asp:LinkButton ID="uxProductReturnButton" runat="server" Text="[$BtnSubmit]" CssClass="BtnStyle1"
                            ValidationGroup="ValidReturn" OnClick="uxProductReturnButton_Click" />
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
