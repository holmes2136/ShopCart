using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo;
using Vevo.Domain.Stores;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Products;

public partial class Mobile_Product : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string GetProductName( string productName )
    {
        string[] productNameList = productName.Split( ',' );
        return productNameList[0];
    }

    private string ProductID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["ProductID"] ))
            {
                return Request.QueryString["ProductID"];
            }
            else
            {
                return DataAccessContext.ProductRepository.GetProductIDFromUrlName( GetProductName( ProductName ) );
            }
        }
    }

    private string ProductName
    {
        get
        {
            if (Request.QueryString["ProductName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ProductName"];
        }
    }

    private string Name
    {
        get
        {
            if (ViewState["Name"] == null)
                return String.Empty;
            else
                return (String) ViewState["Name"];
        }
        set
        {
            ViewState["Name"] = value;
        }
    }

    private string ShortDescription
    {
        get
        {
            if (ViewState["ShortDescription"] == null)
                return String.Empty;
            else
                return (String) ViewState["ShortDescription"];
        }
        set
        {
            ViewState["ShortDescription"] = value;
        }
    }

    private void Refresh()
    {
        uxProductFormView.DataBind();
    }

    private void PopulateTitleAndMeta()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, storeID );

        element.SetUpTitleAndMetaTags(
            product.Name,
            product.GetMetaDescription( StoreContext.Culture, storeID ),
            product.GetMetaKeyword( StoreContext.Culture, storeID ) );
    }

    private void IsProductAvailable()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, storeID );
        IList<String> storeList = product.GetAvailableStoreIDList();
        IList<String> categoryList = product.CategoryIDs;
        IList<String> departmentList = product.DepartmentIDs;
        bool hasCategoryAvailable = false;
        bool hasDepartmentAvailable = false;

        foreach (string categoryID in categoryList)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, categoryID );

            if (category.IsEnabled)
            {
                hasCategoryAvailable = true;
                break;
            }

        }

        foreach (string departmentID in departmentList)
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, departmentID );

            if (department.IsEnabled)
            {
                hasDepartmentAvailable = true;
                break;
            }
        }

        if (product.IsNull || !product.IsEnabled || !storeList.Contains( storeID ) || (!hasCategoryAvailable && !hasDepartmentAvailable))
            Response.Redirect( "~/default.aspx" );
    }

    private void RefreshTitle()
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID );
        this.Page.Title = product.Name;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        IsProductAvailable();

        if (!String.IsNullOrEmpty( Name ))
        {
            PopulateTitleAndMeta();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshTitle();
    }

    protected void uxProductDetailsSource_Selecting( object sender, ObjectDataSourceSelectingEventArgs e )
    {
        e.InputParameters["culture"] = StoreContext.Culture;
        e.InputParameters["productID"] = ProductID;
        e.InputParameters["storeID"] = new StoreRetriever().GetCurrentStoreID();
    }

    protected void uxProductFormView_DataBinding( object sender, EventArgs e )
    {
    }

    protected void uxProductFormView_DataBound( object sender, EventArgs e )
    {
        FormView formView = (FormView) sender;

        // FormView's DataItem property is valid only in DataBound event. 
        // Most of the time it is null.
        if (String.IsNullOrEmpty( Name ))
        {
            Name = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "Name" ) );
            ShortDescription = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "ShortDescription" ) );
            PopulateTitleAndMeta();
        }
        IList<String> categoryIDs = (IList<String>) DataBinder.Eval( formView.DataItem, "CategoryIDs" );

        string categoryID = ConvertUtilities.ToString( categoryIDs[0] );
        foreach (string catID in categoryIDs)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, catID );
            if (category.IsCategoryAvailableStore( StoreContext.CurrentStore.StoreID ) && (category.IsParentsEnable()))
            {
                categoryID = catID;
                break;
            }
        }
    }

    protected void ProductItemCreate( object sender, EventArgs e )
    {
        FormView formView = (FormView) sender;
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        BaseProductDetails productDetailsControl = new BaseProductDetails();

        productDetailsControl = LoadControl( "Components/ProductDetails.ascx" ) as BaseProductDetails;

        productDetailsControl.CurrentProduct = product;
        productDetailsControl.DiscountGroupID = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "DiscountGroupID" ) );
        formView.Controls.Add( productDetailsControl );
    }
}
