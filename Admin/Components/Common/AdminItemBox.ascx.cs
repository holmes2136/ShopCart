using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo;
using System.Collections.Generic;
using System.Collections.Specialized;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Orders;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Users;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_Common_AdminItemBox : AdminAdvancedBaseUserControl
{
    private const string _orderPage = "OrdersList.ascx";
    private const string _customerPage = "CustomerList.ascx";
    private const string _productPage = "ProductList.ascx";
    private const string _categoryPage = "CategoryList.ascx";
    private const string _departmentPage = "DepartmentList.ascx";

    private void PopulateControl()
    {
        IList<Order> list = DataAccessContext.OrderRepository.GetPaidOrdersByDateTime( DateTimeUtilities.GetFirstDayOfTheYear( DateTime.Now ), DateTime.Now );
        decimal subtotal = 0;
        for (int i = 0; i < list.Count; i++)
        {
            subtotal += list[i].Total;
        }
        uxSaleYearLabel.Text = AdminUtilities.FormatPrice( subtotal );

        list = DataAccessContext.OrderRepository.GetPaidOrdersByDateTime( DateTimeUtilities.GetFirstDayOfTheMonth( DateTime.Now ), DateTime.Now );
        subtotal = 0;
        for (int i = 0; i < list.Count; i++)
        {
            subtotal += list[i].Total;
        }
        uxSaleMonthLabel.Text = AdminUtilities.FormatPrice( subtotal );

        list = DataAccessContext.OrderRepository.GetPaidOrdersByDateTime( DateTime.Now, DateTime.Now );
        subtotal = 0;
        for (int i = 0; i < list.Count; i++)
        {
            subtotal += list[i].Total;
        }
        uxSaleTodayLabel.Text = AdminUtilities.FormatPrice( subtotal );
        uxCustomerLabel.Text = DataAccessContext.CustomerRepository.GetAll( "CustomerID" ).Count.ToString();

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControl();
            IList<TableSchemaItem> list = DataAccessContext.CustomerRepository.GetTableSchema();
            NameValueCollection renameList = new NameValueCollection();

            uxCustomerSearchBox.SetUpSchema( list, "CustomerID", "RegisterDate", "Company", "Address1", "Address2", "City", "State", "Zip", "Country", "Phone", "Fax", "UseBillingAsShipping", "ShippingFirstName", "ShippingLastName", "ShippingCompany", "ShippingAddress1", "ShippingAddress2", "ShippingCity", "ShippingState", "ShippingZip", "ShippingCountry", "ShippingPhone", "ShippingFax", "MerchantNotes", "IsWholesale", "WholesaleLevel", "IsEnabled", "ShippingResidential", "IsTaxExempt", "QBListID", "IsQBExported" );

            list = DataAccessContext.OrderRepository.GetTableSchema();
            renameList.Add( "OrderID", "Order No" );
            uxOrderSearchBox.SetUpSchema( list, renameList, "OrderItemID", "OrderDate", "CustomerID", "Company", "Address1", "Address2", "City", "State", "Zip", "Country", "Phone", "Fax", "ShippingFirstName", "ShippingLastName", "ShippingCompany", "ShippingAddress1", "ShippingAddress2", "ShippingCity", "ShippingState", "ShippingZip", "ShippingCountry", "ShippingPhone", "ShippingFax", "CustomerComments", "PaymentMethod", "ShippingMethod", "Status", "IPAddress", "Subtotal", "Tax", "ShippingCost", "CouponID", "CouponDiscount", "BaseCurrencyCode", "UserCurrencyCode", "UserConversionRate", "InvoiceNotes", "ShippingID", "GiftCertificateCode", "GiftCertificate", "TrackingNumber", "TrackingMethod", "GiftRegistryID", "ShowShippingAddress", "HandlingFee", "QbPostDate", "QbReferenceCode", "ContainsRecurring", "ParentID", "PaymentToken", "Total", "ShippingResidential", "AvsAddrStatus", "AvsZipStatus", "CvvStatus", "StoreID", "QBTxnID", "IsQBExported" );

            uxCustomerSearchBox.ResultPage = "CustomerList.ascx";
            uxOrderSearchBox.ResultPage = "OrdersList.ascx";

            SetupDisplayBox();

            uxAllProductLink.Text = DataAccessContext.ProductRepository.GetAllProductCount().ToString();
            uxAllCategoryLink.Text = DataAccessContext.CategoryRepository.GetCountAllWithoutRootCategory().ToString();
            uxAllDepartmentLink.Text = DataAccessContext.DepartmentRepository.GetCountAllWithoutRootDepartment().ToString();
            uxAllProductOnLowStockLink.Text = DataAccessContext.ProductRepository.GetAllProductInLowStockCount(
                DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ),
                DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ) ).ToString();
            uxAllProductOnLowStockLink.PageQueryString =
            "Type=ValueRange&FieldName=Stock&FieldValue=Stock&Value1=&Value2="
            + DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );
            uxAllProductInactiveLink.Text = DataAccessContext.ProductRepository.GetAllProductInactiveCount().ToString();
        }
    }

    private void SetupDisplayBox()
    {
        AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
        if (!admin.CanViewPage( _orderPage ))
        {
            Div1.Visible = false;
            Div3.Visible = false;
        }

        if (!admin.CanViewPage( _customerPage ))
        {
            Div2.Visible = false;
        }

        if (!admin.CanViewPage( _productPage ))
        {
            Div4.Visible = false;
        }
        if (!admin.CanViewPage( _categoryPage ))
        {
            Div4.Visible = false;
        }
        if (!admin.CanViewPage( _departmentPage ))
        {
            Div4.Visible = false;
        }

    }
}
