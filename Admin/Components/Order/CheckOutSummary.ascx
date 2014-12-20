<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckOutSummary.ascx.cs"
    Inherits="Admin_Components_Order_CheckOutSummary" %>
<%@ Register Src="../Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Common/StateList.ascx" TagName="StateList" TagPrefix="uc1" %>
<%@ Register Src="../Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain.Orders" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ContentTemplate>
        <div class="CommonTitle">
            <asp:Label ID="lcHeaderTitle" runat="server" meta:resourcekey="lcHeaderTitle" />
        </div>
        <div id="uxApplyCouponDiv" runat="server" class="OrderSummaryApplyCouponDiv">
            <ul class="OrderSummaryApplyCouponList">
                <li class="OrderSummaryApplyCouponItemList">
                    <%--  <asp:HyperLink ID="uxApplyCouponAndGiftLink" runat="server" Text="Apply coupon or gift certificate"
                            NavigateUrl="CouponAndGift.aspx?IsSummaryPage=true" CssClass="OrderSummaryApplyCouponLink">
                        </asp:HyperLink>--%>
                </li>
            </ul>
        </div>
        <div class="invoiceBillShipAddress mgt0">
            <asp:FormView ID="uxShippingForm" runat="server" CssClass="MainForm" Width="100%"
                BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <ItemTemplate>
                    <div class="Container-Box">
                        <div class="CommonTextTitle1 mgt0 mgb10">
                            <asp:Label ID="lcShippingLabel" runat="server" meta:resourcekey="lcShippingLabel" />
                        </div>
                        <div class="mgl10 mgr10">
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.FirstName" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.LastName" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingCompany" runat="server" meta:resourcekey="lcCompany"></asp:Label></div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Company" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcAddress">:</asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Address1" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    &nbsp;
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Address2" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.City" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingState" runat="server" meta:resourcekey="lcState"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.State" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingCountry" runat="server" meta:resourcekey="lcCountry"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Country" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingZip" runat="server" meta:resourcekey="lcZip"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Zip" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingPhone" runat="server" meta:resourcekey="lcPhone"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Phone" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="CommonRowStyle">
                                <div class="Label">
                                    <asp:Label ID="lcShippingFax" runat="server" meta:resourcekey="lcFax"></asp:Label>
                                </div>
                                <div class="Label fl">
                                    <%# Eval( "ShippingAddress.Fax" ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <RowStyle CssClass="OrderSummaryShippingFromFormViewRowStyle" />
            </asp:FormView>
            <div class="mgt20">
                <asp:GridView ID="uxGridCart" runat="server" AutoGenerateColumns="False" CssClass="Gridview1"
                    SkinID="DefaultGridView" ShowFooter="false">
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources:OrdersFields, ProductID %>"
                            ReadOnly="True" SortExpression="ProductID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Name %>" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label ID="uxProductNameLabel" runat="server" Text='<%# GetItemName( Container.DataItem ) %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources:OrdersFields, Quantity %>"
                            SortExpression="Quantity">
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr10" />
                            <HeaderStyle HorizontalAlign="Right" CssClass="pdr10" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, UnitPrice %>">
                            <ItemTemplate>
                                <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetUnitPriceText( (ICartItem) Container.DataItem ) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr10" />
                            <HeaderStyle HorizontalAlign="Right" CssClass="pdr10" Width="140px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, SubTotal %>">
                            <ItemTemplate>
                                <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetSubtotalText( (ICartItem) Container.DataItem ) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr10" />
                            <HeaderStyle HorizontalAlign="Right" CssClass="pdr10" Width="140px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="pdb10 invoiceBottomDetails">
                <div class="fl invoiceBottomDetailsCustomer">
                    <div class="mg5 BorderSet1 pd1 b6 c4" id="uxOrderSummaryCommentTD" runat="server">
                        <div class="CommonTitle1 ac">
                            <asp:Label ID="uxcustomerNoteLabel" runat="server" meta:resourcekey="lcCustomerNote"></asp:Label></div>
                        <div class="mgt10">
                            <asp:Label ID="uxOrderCommentLabel" runat="server">                                </asp:Label>&nbsp;
                        </div>
                    </div>
                </div>
                <div class="fl invoiceBottomDetailsCustomer">
                    <div class="mg5 BorderSet1 pd1 b6 c4" id="uxPointEarnedTD" runat="server">
                        <div class="CommonTitle1 ac">
                            <asp:Label ID="uxPointEarnedLabel" runat="server" Text="Point Earned"></asp:Label></div>
                        <div class="mgt10">
                            <asp:Label ID="uxPointEarnedValueLabel" runat="server">                                </asp:Label>&nbsp;
                        </div>
                    </div>
                </div>
                <div class="fr" style="width: 306px;">
                    <table cellpadding="0" cellspacing="0" class="OrderEditTotalBox1" id="T_Summary">
                        <tr>
                            <td>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxProductCostLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="uxSubTotalLabel" runat="server" meta:resourcekey="lcSubTotal" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxDiscountLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lcDiscountCost" runat="server" meta:resourcekey="lcDiscountCost" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxPointDiscountCostLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="uxPointDiscountLabel" runat="server" Text="Point Discount" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxTaxLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lcTax" runat="server" meta:resourcekey="lcTax" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxShippingCostLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lcShippingCost" runat="server" meta:resourcekey="lcShippingCost" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow" id="uxHandlingFeeTR" runat="server">
                                    <asp:Label ID="uxHandlingFeeLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="SummaryRow" id="uxGiftCertificateTR" runat="server">
                                    <asp:Label ID="uxGiftCertificateLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lcGiftCertificate" runat="server" meta:resourcekey="lcGiftCertificate"
                                        CssClass="Label fb" /><div class="Clear">
                                        </div>
                                </div>
                                <div class="SummaryRow">
                                    <asp:Label ID="uxTotalLabel" runat="server" CssClass="Value fb" />
                                    <asp:Label ID="lctotal" runat="server" meta:resourcekey="lctotal" CssClass="Label fb" />
                                    <div class="Clear">
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="uxMessageDiv" runat="server" class="OrderSummaryMessageDiv" visible="false">
                <asp:Literal ID="uxMessageLiteral" runat="server" />
            </div>
            <div id="uxWarningMessageDiv" runat="server" class="OrderSummaryWarningMessageDiv"
                visible="false">
                <asp:Literal ID="uxWarningMessageLiteral" runat="server" />
            </div>
            <div id="uxStockMessageDiv" runat="server" visible="false" class="OrderSummaryStockMessageDiv">
                <asp:Literal ID="uxStockMessageLiteral" runat="server" />
            </div>
            <div id="uxQuantityMessageDiv" runat="server" visible="false" class="OrderSummaryQuantityMessageDiv">
                <asp:Literal ID="uxQuantityMessageLiteral" runat="server" />
            </div>
            <div class="Clear">
            </div>
            <div class="mgt10">
                <div class="CommonRowStyle">
                    <vevo:AdvanceButton ID="uxNextButton" runat="server" meta:resourcekey="uxNextButton"
                        CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxNextButton_Click"
                        OnClickGoTo="Top" ValidationGroup="ValidDetails"></vevo:AdvanceButton>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="Clear">
            </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
