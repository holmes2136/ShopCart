<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Invoice.ascx.cs" Inherits="AdminAdvanced_MainControls_Invoice" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<asp:ObjectDataSource ID="uxInvoiceSource" runat="server" SelectMethod="OrdersGetOne"
    TypeName="Vevo.Domain.DataSources.OrdersDataSource"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxOrderItemSource" runat="server" SelectMethod="OrderItemGetByOrderID"
    TypeName="Vevo.Domain.DataSources.OrdersDataSource"></asp:ObjectDataSource>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="Invoice And Packing slip">
    <ButtonEventTemplate>
        <vevo:AdvanceButton ID="uxPrintButton" runat="server" meta:resourcekey="lcPrintButton"
            CssClass="CommonAdminButtonIcon AdminButtonIconPrint fl" CausesValidation="false"
            ShowText="true" />
    </ButtonEventTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <asp:Panel ID="uxEmailTABLE" runat="server">
                <div class="OrderEditRowTitle">
                    <asp:Label ID="uxEmailInformationLabel" runat="server" meta:resourcekey="lcEmailInformation" /></div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxSenderNameLabel" runat="server" meta:resourcekey="lcSenderName"
                        CssClass="Label" />
                    <asp:TextBox ID="uxSenderNameText" runat="server" ValidationGroup="EmailValid" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxSenderRequired" runat="server" ValidationGroup="EmailValid"
                        ControlToValidate="uxSenderNameText" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Sender Name is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxToLabel" runat="server" meta:resourcekey="lcTo" CssClass="Label" />
                    <asp:TextBox ID="uxToText" runat="server" ValidationGroup="EmailValid" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxToRequired" runat="server" ValidationGroup="EmailValid"
                        ControlToValidate="uxToText" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Recipient is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxEmailRegular" runat="server" ValidationGroup="EmailValid"
                        ControlToValidate="uxToText" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email format.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxSubjectLabel" runat="server" meta:resourcekey="lcSubject" CssClass="Label" />
                    <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="EmailValid" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxSubjectRequired" runat="server" ValidationGroup="EmailValid"
                        ControlToValidate="uxSubjectText" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Subject is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <uc1:Message ID="uxMessage" runat="server" />
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="Label1" runat="server" Text="&nbsp;" CssClass="Label" />
                <vevo:AdvanceButton ID="uxEmailButton" runat="server" meta:resourcekey="lcEmailButton"
                    CssClassBegin="fl mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxEmailButton_Click"
                    OnClickGoTo="Top" ValidationGroup="EmailValid"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
            <div class="InvoiceBox">
                <div id="uxBody">
                    <div class="w100p fb c10 ac">
                        <asp:Label ID="uxTitleLabel" runat="server"></asp:Label>
                    </div>
                    <asp:FormView ID="uxInvoiceForm" runat="server" Width="100%" DataSourceID="uxInvoiceSource"
                        CssClass="MainForm">
                        <ItemTemplate>
                            <div class="invoiceHeaderDetails">
                                <div class="invoiceHeaderDetailsLeft">
                                    <span class="InvoiceCompanyName fb">
                                        <%# GetConfigValue( "CompanyName" ) %>
                                    </span>
                                    <br />
                                    <span class="InvoiceCompanyAddress">
                                        <%# GetConfigValue( "CompanyAddress" )%>
                                        <br />
                                        <%# GetConfigValue( "CompanyCity" )%>
                                        <%# String.IsNullOrEmpty( GetConfigValue( "CompanyState" ) ) ? "" : ",&nbsp;" + GetConfigValue( "CompanyState" )%>
                                        <br />
                                        <%# GetConfigValueNoCulture( "CompanyZip" )%>
                                        <br />
                                        <%# GetConfigValue( "CompanyCountry" ) %>
                                        <br />
                                        <%# "Phone : " + GetConfigValueNoCulture( "CompanyPhone" )%>
                                        <br />
                                        <%# "Fax : " + GetConfigValueNoCulture( "CompanyFax" )%>
                                        <br />
                                        <%# "Email : " + GetConfigValueNoCulture( "CompanyEmail" )%>
                                    </span>
                                </div>
                                <div class="invoiceHeaderDetailsRight">
                                    <table border="0" cellpadding="5" cellspacing="0" class="TableOrderAndDate" style="margin: 0px 0px 0px auto;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="uxOrderIDLabel" runat="server" meta:resourcekey="lcOrderID" CssClass="fb" />
                                            </td>
                                            <td class="fb">
                                                <%# CurrentOrderID %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="uxDateLabel" runat="server" meta:resourcekey="lcDate" />
                                            </td>
                                            <td>
                                                <%# Convert.ToDateTime( Eval( "OrderDate" ) ).ToString( "yyyy-MM-dd" ) %>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                            <div class="invoiceBillShipAddress">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="border3 w49p vt">
                                            <div class="mg5">
                                                <div class="w100p fb c10 ac">
                                                    <asp:Label ID="uxBillLabel" runat="server" meta:resourcekey="lcBill" /></div>
                                                <div class="CommonTitle1">
                                                    <asp:Label ID="uxBillAddressLabel" runat="server" meta:resourcekey="lcBillAddress" /></div>
                                            </div>
                                            <div class="mgl10 mgr10">
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="uxFirstNameLabel" runat="server" meta:resourcekey="lcFirstName" />
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.FirstName" ) %>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="uxLastNameLabel" runat="server" meta:resourcekey="lcLastName" />
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.LastName" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcCompany" runat="server" meta:resourcekey="lcCompany" />
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Company" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="uxAddress" runat="server" meta:resourcekey="lcAddress"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Address1" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        &nbsp;
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Address2" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.City" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcState" runat="server" meta:resourcekey="lcState"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.State" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcCountry" runat="server" meta:resourcekey="lcCountry"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Country" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcZip" runat="server" meta:resourcekey="lcZip"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Zip" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcPhone" runat="server" meta:resourcekey="lcPhone"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Phone" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcFax" runat="server" meta:resourcekey="lcFax"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Billing.Fax" )%>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label3 fl">
                                                        <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail"></asp:Label>
                                                    </div>
                                                    <div class="fl">
                                                        <%# Eval( "Email" ) %>
                                                    </div>
                                                    <div class="Clear">
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="border3 w49p vt">
                                            <div class="mg5">
                                                <div class="w100p fb c10 ac">
                                                    <asp:Label ID="uxShipToLabel" runat="server" meta:resourcekey="lcShipTo"></asp:Label></div>
                                                <div class="CommonTitle1">
                                                    <asp:Label ID="uxShippingAddressLabel" runat="server" meta:resourcekey="lcShippingAddress"></asp:Label></div>
                                                <div class="mgl10 mgr10">
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.FirstName" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.LastName" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingCompany" runat="server" meta:resourcekey="lcCompany"></asp:Label></div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Company" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcAddress">:</asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Address1" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            &nbsp;
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Address2" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.City" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingState" runat="server" meta:resourcekey="lcState"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.State" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingCountry" runat="server" meta:resourcekey="lcCountry"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Country" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingZip" runat="server" meta:resourcekey="lcZip"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Zip" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingPhone" runat="server" meta:resourcekey="lcPhone"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Phone" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="label3 fl">
                                                            <asp:Label ID="lcShippingFax" runat="server" meta:resourcekey="lcFax"></asp:Label>
                                                        </div>
                                                        <div class="fl">
                                                            <%# Eval( "Shipping.Fax" ) %>
                                                        </div>
                                                        <div class="Clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="mgt20">
                                <asp:GridView ID="uxItemGrid" runat="server" AutoGenerateColumns="False" DataSourceID="uxOrderItemSource"
                                    CssClass="Gridview1" SkinID="DefaultGridView" OnRowDataBound="uxGrid_RowDataBound"
                                    ShowFooter="false">
                                    <Columns>
                                        <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources:OrdersFields, ProductID %>"
                                            ReadOnly="True" SortExpression="ProductID">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Name %>" SortExpression="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="uxItemName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
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
                                                <%# StoreContext.Currency.FormatPrice( Eval( "UnitPrice" ) ) %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" CssClass="pdr10" />
                                            <HeaderStyle HorizontalAlign="Right" CssClass="pdr10" Width="140px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, SubTotal %>">
                                            <ItemTemplate>
                                                <%# StoreContext.Currency.FormatPrice( String.Format( "{0:n2}", (int) Eval( "Quantity" ) * (decimal) Eval( "UnitPrice" ) ) ) %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" CssClass="pdr10" />
                                            <HeaderStyle HorizontalAlign="Right" CssClass="pdr10" Width="140px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="pdb10 invoiceBottomDetails">
                                <div class="fl invoiceBottomDetailsMerchant">
                                    <div class="BorderSet1 pd1 b6 c4 mgt5 mgr10 mgb5">
                                        <div class="CommonTitle1 ac">
                                            <asp:Label ID="uxMerchantNoteLabel" runat="server" meta:resourcekey="lcMerchantNote"></asp:Label></div>
                                        <div class="mgt10">
                                            <%# Eval( "InvoiceNotes" ) %>
                                        </div>
                                    </div>
                                </div>
                                <div class="fl invoiceBottomDetailsCustomer">
                                    <div class="mg5 BorderSet1 pd1 b6 c4">
                                        <div class="CommonTitle1 ac">
                                            <asp:Label ID="uxcustomerNoteLabel" runat="server" meta:resourcekey="lcCustomerNote"></asp:Label></div>
                                        <div class="mgt10">
                                            <%# Eval("CustomerComments") %>
                                        </div>
                                    </div>
                                </div>
                                <div class="fr" style="width: 332px;">
                                    <div class="fr">
                                        <table id="T_Summary" width="100%" class="GridInsertItemRowStyle" cellspacing="0">
                                            <tr>
                                                <td style="width: 140px;" class="Label">
                                                    <asp:Label ID="uxSubTotalLabel" runat="server" meta:resourcekey="lcSubTotal"></asp:Label>
                                                </td>
                                                <td style="width: 140px;" class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( Eval( "SubTotal" ) ) %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Label">
                                                    <asp:Label ID="lcCouponDiscount" runat="server" meta:resourcekey="lcCouponDiscount"></asp:Label>
                                                </td>
                                                <td class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( DisplayDiscount(Eval( "CouponDiscount" )) )%>
                                                </td>
                                            </tr>
                                            <% if (DisplayPointDiscount())
                                               { %>
                                            <tr>
                                                <td class="Label">
                                                    <asp:Label ID="uxPointDiscount" runat="server" meta:resourcekey="uxPointDiscount"></asp:Label>
                                                </td>
                                                <td class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( DisplayDiscount(Eval( "RedeemPrice" )) )%>
                                                </td>
                                            </tr>
                                            <%} %>
                                            <tr>
                                                <td class="Label">
                                                    <asp:Label ID="lcTax" runat="server" meta:resourcekey="lcTax"></asp:Label>
                                                </td>
                                                <td class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( Eval( "Tax" ) ) %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Label">
                                                    <asp:Label ID="lcShippingCost" runat="server" meta:resourcekey="lcShippingCost"></asp:Label>
                                                </td>
                                                <td class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( Eval( "ShippingCost" ) ) %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Label">
                                                    <asp:Label ID="uxHandlingFee" runat="server" meta:resourcekey="uxHandlingFee"></asp:Label>
                                                </td>
                                                <td class="Value">
                                                    <%# StoreContext.Currency.FormatPrice( Eval( "HandlingFee" ) )%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Label fb">
                                                    <asp:Label ID="uxtotalLabel" runat="server" meta:resourcekey="lctotal"></asp:Label>
                                                </td>
                                                <td class="Value fb">
                                                    <%# StoreContext.Currency.FormatPrice( ((decimal) Eval( "SubTotal" ) + (decimal) Eval( "Tax" ) + (decimal) Eval( "ShippingCost" ) + (decimal) Eval("HandlingFee")) - ((decimal) Eval( "CouponDiscount" ) + (decimal)Eval("RedeemPrice") )) %>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:FormView ID="uxPackingSlipForm" runat="server" DataSourceID="uxInvoiceSource"
                        Width="100%">
                        <ItemTemplate>
                            <div id="uxPacking" class="mgl10 mgr10">
                                <div class="packingFromAddress">
                                    <div class="fl al">
                                        <span class="packingCompanyName">
                                            <%# GetConfigValue( "CompanyName" )%>
                                        </span>
                                        <br />
                                        <span class="packingCompanyAddress">
                                            <%# GetConfigValue( "CompanyAddress" )%>
                                            <br />
                                            <%# GetConfigValue( "CompanyCity" )%>
                                            <%# String.IsNullOrEmpty( GetConfigValue( "CompanyState" ) ) ? "" : ",&nbsp;" + GetConfigValue( "CompanyState" )%>
                                            <br />
                                            <%# GetConfigValueNoCulture( "CompanyZip" )%>
                                            <%# String.IsNullOrEmpty( GetConfigValue( "CompanyCountry" ) ) ? "" : ",&nbsp;" + GetConfigValue( "CompanyCountry" )%>
                                        </span>
                                    </div>
                                    <div class="fr">
                                        <img src='../<%# GetConfigValueNoCulture( "LogoImage" ) %>' height="117px" />
                                    </div>
                                    <div class="Clear">
                                    </div>
                                </div>
                                <div class="mgt10 packingOrderDetails">
                                    <div>
                                        <span>
                                            <asp:Label ID="uxOrderIDLabel" runat="server" meta:resourcekey="lcOrderID" /></span>
                                        <%# CurrentOrderID %>
                                    </div>
                                    <div>
                                        <span>
                                            <asp:Label ID="uxDateLabel" runat="server" meta:resourcekey="lcDate" /></span>
                                        <%# Convert.ToDateTime( Eval( "OrderDate" ) ).ToString( "yyyy-MM-dd" ) %>
                                    </div>
                                </div>
                                <div class="mgt10 packingOrderDestination">
                                    <div class="packingOrderDestinationTitle">
                                        <asp:Label ID="uxDeliveryLabel" runat="server" meta:resourcekey="lcDelivery" /></div>
                                    <div class="packingOrderDestinationAddress">
                                        <div>
                                            <span>
                                                <%# Eval("Shipping.FirstName") %>
                                            </span><span>
                                                <%# Eval("Shipping.LastName") %>
                                            </span>
                                        </div>
                                        <div>
                                            <%# Eval("Shipping.Company") %>
                                        </div>
                                        <div>
                                            <%# Eval("Shipping.Address1") %>
                                        </div>
                                        <div>
                                            <%# Eval("Shipping.Address2") %>
                                        </div>
                                        <div>
                                            <span>
                                                <%# Eval("Shipping.City") %>
                                            </span><span>
                                                <%# (String.IsNullOrEmpty( Convert.ToString( ( Eval("Shipping.State") ) ) ) ) ? "" : Eval("Shipping.State", ",&nbsp;{0}") %>
                                            </span>
                                        </div>
                                        <div>
                                            <span>
                                                <%# Eval( "Shipping.Zip" )%>
                                            </span><span>
                                                <%# (String.IsNullOrEmpty( Convert.ToString( ( Eval( "Shipping.Country" ) ) ) ) ) ? "" : Eval( "Shipping.Country", ",&nbsp;{0}" )%>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
