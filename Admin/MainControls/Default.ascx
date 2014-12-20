<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="AdminAdvanced_MainControls_Default" %>
<%@ Register Src="../Components/SnapShot/ProductSummary.ascx" TagName="ProductSummary"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContentDefault.ascx" TagName="AdminContent"
    TagPrefix="uc3" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc4" %>
<%@ Register Src="../Components/SnapShot/GraphicView.ascx" TagName="GraphicView"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/SnapShot/TabularView.ascx" TagName="TabularView"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/StoreDropDownList.ascx" TagName="StoreDropDownList"
    TagPrefix="uc7" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc3:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <ContentTemplate>
        <div class="ac">
            <asp:LinkButton ID="uxSecurityWarningLink" runat="server" Visible="False" meta:resourcekey="uxSecurityWarningLink"
                CssClass="fb c6 fb"></asp:LinkButton></div>
        <asp:Panel ID="uxWarningPanel" runat="server" CssClass="mgl20">
            <div>
                <div class="mgt20">
                    <span class="c6">Please delete the following files from your system. </span>
                </div>
                <div class="mgl20">
                    <asp:Label ID="uxMessageLabel" runat="server" ForeColor="red"></asp:Label></div>
            </div>
        </asp:Panel>
        <div class="AdminTabBox" id="Div3" runat="server">
            <div class="AdminTabBoxSidebarTop">
                <div class="fr">
                    <asp:Label ID="uxStoreViewLabel" runat="server" CssClass="StoreViewLabel" Visible="false">Store:</asp:Label>
                    <uc7:StoreDropDownList ID="uxStoreList" runat="server" AutoPostBack="True" OnBubbleEvent="uxStoreList_RefreshHandler"
                        FirstItemText="-- All Store --" FirstItemValue="0" Visible="false" />
                </div>
            </div>
            <div class="SidebarLeft">
                <div class="SidebarRight">
                    <ajaxToolkit:TabContainer ID="uxTabContainerReport" runat="server" CssClass="DefaultTabContainer"
                        SkinID="DefaultTab">
                        <ajaxToolkit:TabPanel ID="uxTabPanelTabularReport" runat="server" CssClass="DefaultTabPanel"
                            TabIndex="0">
                            <ContentTemplate>
                                <uc6:TabularView ID="uxTabularView" runat="server" />
                            </ContentTemplate>
                            <HeaderTemplate>
                                <div>
                                    <asp:Label ID="uxTabularTitle" runat="server">Tabular View</asp:Label></div>
                            </HeaderTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="uxTabPanelGraphicReport" runat="server" CssClass="DefaultTabPanel"
                            TabIndex="1">
                            <ContentTemplate>
                                <%--Add report graph here--%>
                                <uc5:GraphicView ID="uxGraphicView" runat="server" />
                            </ContentTemplate>
                            <HeaderTemplate>
                                <div>
                                    <asp:Label ID="Label1" runat="server">Graphic View</asp:Label></div>
                            </HeaderTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
        <div class="AdminTabBox" id="Div1" runat="server">
            <div class="SidebarLeft">
                <div class="SidebarRight">
                    <ajaxToolkit:TabContainer ID="uxTabContainerList" runat="server" CssClass="DefaultTabContainer"
                        SkinID="DefaultTab">
                        <ajaxToolkit:TabPanel ID="uxTabLastestOrder" runat="server" CssClass="DefaultTabPanel"
                            TabIndex="0">
                            <ContentTemplate>
                                <asp:GridView ID="uxLastestOrderGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                                    ShowFooter="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Order Date">
                                            <ItemTemplate>
                                                <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" Text='<%# ConvertUtilities.ToString( Eval("OrderDate") ) %>'
                                                    PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                                                    OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Orders {0} Edit", Eval("OrderID") )%>'
                                                    CssClass="UnderlineDashed" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="uxTotalLabel" runat="server" Text='<%# AdminUtilities.FormatPrice( ConvertUtilities.ToDecimal( Eval("Total") ) ) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="ac" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Processed?">
                                            <ItemTemplate>
                                                <asp:Label ID="uxTotalLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("Processed")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="ac" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                                            <HeaderStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Complete">
                                            <ItemTemplate>
                                                <asp:Label ID="uxTotalLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("PaymentComplete")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="ac" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                                            <HeaderStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <vevo:AdvancedLinkButton ID="uxCustomerLink" runat="server" Text='<%# String.Format( "{0}", Eval("UserName") ) %>'
                                                    PageName="CustomerEdit.ascx" PageQueryString='<%# String.Format( "CustomerID={0}", Eval("CustomerID") ) %>'
                                                    OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("UserName") ) %>'
                                                    CssClass="UnderlineDashed" Visible='<%# ( Eval("CustomerID").ToString() != "0" )? true:false  %>' />
                                                <asp:Label ID="uxCustomerNameLabel" runat="server" Text='<%# String.Format( "{0}", Eval("UserName") ) %>'
                                                    CssClass="" Visible='<%# ( Eval("CustomerID").ToString() == "0" )? true:false  %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="No Data" />
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <vevo:AdvanceButton ID="uxViewAllOrder" runat="server" meta:resourcekey="uxViewAllOrder"
                                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                                    OnClick="uxViewAllOrder_Click" OnClickGoTo="Top" ValidationGroup="AdminEdit"
                                    Text="aaa"></vevo:AdvanceButton>
                            </ContentTemplate>
                            <HeaderTemplate>
                                <div>
                                    <asp:Label ID="Label2" runat="server">Lastest Orders</asp:Label></div>
                            </HeaderTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="uxTabBestSeller" runat="server" CssClass="DefaultTabPanel"
                            TabIndex="1">
                            <ContentTemplate>
                                <asp:GridView ID="uxBestSellerGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                                    ShowFooter="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <vevo:AdvancedLinkButton ID="uxEditHyperLink" runat="server" ToolTip="<%$ Resources:ProductFields,  CommandEdit %>"
                                                    PageName="ProductEdit.ascx" PageQueryString='<%# String.Format( "ProductID={0}", Eval("ProductID") ) %>'
                                                    OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                           
                                            <asp:Label runat="server" ID="uxProductNameLabel" Text='<%# String.Format( "{0}", Eval("Name") ) %>' />
                                                </vevo:AdvancedLinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="uxPriceLabel" Text='<%# AdminUtilities.FormatPrice( ConvertUtilities.ToDecimal( Eval("ProductPrice") ) ) %>'/>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity Ordered">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="uxQuantityLabel" Text='<%# String.Format( "{0}",Eval("ProductQuantity")) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="No Data" />
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <vevo:AdvanceButton ID="uxViewAllProduct" runat="server" meta:resourcekey="uxViewAllProduct"
                                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                                    OnClick="uxViewAllProduct_Click" OnClickGoTo="Top" ValidationGroup="AdminEdit"
                                    Text="ccc"></vevo:AdvanceButton>
                            </ContentTemplate>
                            <HeaderTemplate>
                                <div>
                                    <asp:Label ID="Label3" runat="server">Best Seller</asp:Label></div>
                            </HeaderTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="uxTabNewCustomers" runat="server" CssClass="DefaultTabPanel"
                            TabIndex="2">
                            <ContentTemplate>
                                <asp:GridView ID="uxGridCustomer" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                                    ShowFooter="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Username">
                                            <ItemTemplate>
                                                <vevo:AdvancedLinkButton ID="uxUserNameEditLink" runat="server" ToolTip="Edit" PageName="CustomerEdit.ascx"
                                                    PageQueryString='<%# String.Format( "CustomerID={0}", Eval("CustomerID") ) %>'
                                                    StatusBarText='<%# String.Format( "Edit {0}", Eval("UserName" ) ) %>' OnClick="ChangePage_Click">
                                                  <asp:Label ID="uxFirstNameLabel" runat="server" Text='<%# Eval("BillingAddress.FirstName").ToString() %>' />
                                                </vevo:AdvancedLinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:Label ID="uxFirstNameLabel" runat="server" Text='<%# Eval("BillingAddress.FirstName").ToString() %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name">
                                            <ItemTemplate>
                                                <asp:Label ID="uxLastNameLabel" runat="server" Text='<%# Eval("BillingAddress.LastName").ToString() %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Register Date">
                                            <ItemTemplate>
                                                <asp:Label ID="uxRegisterDate" runat="server" Text='<%# ConvertUtilities.ToString( Eval("RegisterDate") ) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="No Data" />
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <vevo:AdvanceButton ID="uxViewAllCustomer" runat="server" meta:resourcekey="uxViewAllCustomer"
                                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                                    OnClick="uxViewAllCustomer_Click" OnClickGoTo="Top" ValidationGroup="AdminEdit"
                                    Text="bbb"></vevo:AdvanceButton>
                            </ContentTemplate>
                            <HeaderTemplate>
                                <div>
                                    <asp:Label ID="Label4" runat="server">New Customers</asp:Label></div>
                            </HeaderTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
        <div class="Clear">
        </div>
    </ContentTemplate>
</uc3:AdminContent>
