using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Users;

public partial class AdminAdvanced_Components_Snapshot_ProductSummary : AdminAdvancedBaseUserControl
{
    private const string _productPage = "ProductList.ascx";
    private const string _categoryPage = "CategoryList.ascx";
    private const string _departmentPage = "DepartmentList.ascx";

    protected void Page_Load( object sender, EventArgs e )
    {
        uxProductLiteral.Text = String.Format( "<span class='ProductSummaryValue'>{0}</span>", DataAccessContext.ProductRepository.GetAllProductCount() );

        int categoryCount = DataAccessContext.CategoryRepository.GetCountAllWithoutRootCategory();
        int departentCount = DataAccessContext.DepartmentRepository.GetCountAllWithoutRootDepartment();

        uxCategoryLiteral.Text = String.Format( "<span class='ProductSummaryValue'>{0}</span>", categoryCount );
        uxDepartmentLiteral.Text = String.Format( "<span class='ProductSummaryValue'>{0}</span>", departentCount );

        uxOutOfStockLiteral.Text = String.Format( "<span class='ProductSummaryValue'>{0}</span>",
            DataAccessContext.ProductRepository.GetAllProductInLowStockCount(
                DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ),
                DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ) ) );

        uxAllProductOnLowStockLink.PageQueryString =
            "Type=ValueRange&FieldName=Stock&FieldValue=Stock&Value1=&Value2="
            + DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );

        uxProductInactiveLiteral.Text = String.Format( "<span class='ProductSummaryValue'>{0}</span>", DataAccessContext.ProductRepository.GetAllProductInactiveCount() );

        if (!MainContext.IsPostBack)
        {
            SetUpDisplayInformation();
        }

        if (KeyUtilities.IsMultistoreLicense())
            uxAllCategoryLink.PageName = "RootCategoryList.ascx";
        else
            uxAllCategoryLink.PageName = "CategoryList.ascx";

        if (KeyUtilities.IsMultistoreLicense())
            uxAllDepartmentLink.PageName = "RootDepartmentList.ascx";
        else
            uxAllDepartmentLink.PageName = "DepartmentList.ascx";
    }

    private void SetUpDisplayInformation()
    {
        AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
        bool canViewProduct = admin.CanViewPage( _productPage );
        bool canViewCategory = admin.CanViewPage( _categoryPage );
        bool canViewDepartment = admin.CanViewPage( _departmentPage );

        if (!canViewProduct)
        {
            uxProductLiteral.Visible = false;
            uxAllProductLink.Visible = false;
            uxOutOfStockLiteral.Visible = false;
            uxAllProductOnLowStockLink.Visible = false;
            uxProductInactiveLiteral.Visible = false;
            uxAllProductInactiveLink.Visible = false;
        }
        if (!canViewCategory)
        {
            uxCategoryLiteral.Visible = false;
            uxAllCategoryLink.Visible = false;
        }
        if (!canViewDepartment)
        {
            uxDepartmentLiteral.Visible = false;
            uxAllDepartmentLink.Visible = false;
        }
        if (!canViewProduct && !canViewCategory && !canViewDepartment)
        {
            uxNoPermissionLabel.Text = "You do not have permission to view this section";
        }
    }
}
