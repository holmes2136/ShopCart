using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using System.Globalization;
using Vevo.Domain.Products;
using System.IO;

public partial class Components_NewArrivalCategory : Vevo.WebUI.Products.BaseProductListControl
{
    private string CurrentCategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"];
        }
    }

    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (id != null)
            {
                return id;
            }
            else
            {
                return DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
            }
        }
    }

    private Category CurrentCategory
    {
        get
        {
            return DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, CurrentCategoryID );
        }
    }

    private string CurrentDepartmentName
    {
        get
        {
            if (Request.QueryString["DepartmentName"] == null)
                return String.Empty;
            else
                return Request.QueryString["DepartmentName"];
        }
    }

    private string CurrentDepartmentID
    {
        get
        {
            string id = Request.QueryString["DepartmentID"];
            if (id != null)
            {
                return id;
            }
            else
            {
                return DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
            }
        }
    }

    private Department CurrentDepartment
    {
        get
        {
            return DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, CurrentDepartmentID );
        }
    }

    private string CurrentManufacturerName
    {
        get
        {
            if (Request.QueryString["ManufacturerName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ManufacturerName"];
        }
    }

    private string CurrentManufacturerID
    {
        get
        {
            string id = Request.QueryString["ManufacturerID"];
            if (id != null)
            {
                return id;
            }
            else
            {
                return "0";
            }
        }
    }

    private Manufacturer CurrentManufacturer
    {
        get
        {
            return DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, CurrentManufacturerID );
        }
    }

    private bool IsInCategoryPage()
    {
        if (!String.IsNullOrEmpty( CurrentCategoryName ))
        {
            return true;
        }
        else if (!String.IsNullOrEmpty( CurrentDepartmentName ))
        {
            return false;
        }
        else if (!String.IsNullOrEmpty( CurrentManufacturerName ))
        {
            return false;
        }
        else
        {
            string filename = Path.GetFileName( Request.Path );
            if (filename.ToLower() == "catalog.aspx")
                return true;
            else if (filename.ToLower() == "department.aspx")
                return false;
            else if (filename.ToLower() == "manufacturer.aspx")
                return false;

            string rootCatID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
            if (CurrentCategoryID != rootCatID)
                return true;
            else
                return false;
        }
    }

    private bool IsInDepartmentPage()
    {
        if (!String.IsNullOrEmpty( CurrentDepartmentName ))
        {
            return true;
        }
        else if (!String.IsNullOrEmpty( CurrentCategoryName ))
        {
            return false;
        }
        else if (!String.IsNullOrEmpty( CurrentManufacturerName ))
        {
            return false;
        }
        else
        {
            string filename = Path.GetFileName( Request.Path );
            if (filename.ToLower() == "catalog.aspx")
                return false;
            else if (filename.ToLower() == "department.aspx")
                return true;
            else if (filename.ToLower() == "manufacturer.aspx")
                return false;

            string rootDepID = DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
            if (CurrentDepartmentID != rootDepID)
                return true;
            else
                return false;
        }
    }

    private void PopulateControls()
    {
        string name = string.Empty;
        string catName;

        IList<string> catList = new List<string>();
        List<string> categoryCollection = new List<string>();
        IList<string> depList = new List<string>();
        List<string> departmentCollection = new List<string>();
        string currentManID = "0";
        int maxCount = 9;
        string currentDeptID = "0";
        string currentCatID = "0";

        Store store = new StoreRetriever().GetStore();
        string rootCatID = DataAccessContext.Configurations.GetValue( "RootCategory", store );
        string rootDepID = DataAccessContext.Configurations.GetValue( "RootDepartment", store );

        bool isManufacturer = false;
        if (IsInCategoryPage())
        {
            if (!String.IsNullOrEmpty( CurrentCategoryName ))
            {
                Category category = DataAccessContext.CategoryRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentCategoryName );
                catName = category.Name;
                currentCatID = category.CategoryID;
            }
            else
            {
                catName = CurrentCategory.Name;
                currentCatID = CurrentCategoryID;
            }
            Category cat = DataAccessContext.CategoryRepository.GetOne(
                    StoreContext.Culture, currentCatID );
            if (!cat.IsShowNewArrival)
            {
                uxNewArrivalCategory.Visible = false;
                return;
            }

            IList<string> categoryIDs = DataAccessContext.CategoryRepository.GetLeafFromCategoryID(
                currentCatID, catList );

            foreach (string categoryItem in categoryIDs)
            {
                categoryCollection.Add( categoryItem );
            }
            if (cat.CategoryID == rootCatID)
                maxCount = DataAccessContext.Configurations.GetIntValue( "ProductNewArrivalNumber", new StoreRetriever().GetStore() );
            else
                maxCount = cat.NewArrivalAmount;
        }
        else if (IsInDepartmentPage())
        {
            if (!String.IsNullOrEmpty( CurrentDepartmentName ))
            {
                Department department = DataAccessContext.DepartmentRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentDepartmentName );
                catName = department.Name;
                currentDeptID = department.DepartmentID;
            }
            else
            {
                catName = CurrentDepartment.Name;
                currentDeptID = CurrentDepartmentID;
            }
            Department dept = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, currentDeptID );
            if (!dept.IsShowNewArrival)
            {
                uxNewArrivalCategory.Visible = false;
                return;
            }

            IList<string> departmentIDs = DataAccessContext.DepartmentRepository.GetLeafFromDepartmentID(
                currentDeptID, depList );

            foreach (string departmentItem in departmentIDs)
            {
                departmentCollection.Add( departmentItem );
            }

            if (dept.DepartmentID == rootDepID)
                maxCount = DataAccessContext.Configurations.GetIntValue( "ProductNewArrivalNumber", new StoreRetriever().GetStore() );
            else
                maxCount = dept.NewArrivalAmount;
        }
        else
        {
            if (!String.IsNullOrEmpty( CurrentManufacturerName ))
            {
                Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentManufacturerName );
                catName = manufacturer.Name;
                currentManID = manufacturer.ManufacturerID;
            }
            else
            {
                catName = CurrentManufacturer.Name;
                currentManID = CurrentManufacturerID;
            }
            isManufacturer = true;
        }

        if ((currentCatID == rootCatID) || (currentDeptID == rootDepID) || (catName == "RootManufacturer") || (catName == String.Empty))
        {
            uxNewArrivalCategoryName.Text = string.Empty;
        }
        else
            uxNewArrivalCategoryName.Text = "-&nbsp;" + char.ToUpper( catName[0] ) + catName.Substring( 1 );

        IList<Product> newProducts;
        if (isManufacturer == false)
        {
            newProducts = DataAccessContext.ProductRepository.GetNewArrivalByCategoryDepartmentID(
                StoreContext.Culture, categoryCollection.ToArray(), departmentCollection.ToArray(), maxCount, new StoreRetriever().GetCurrentStoreID() );
        }
        else
        {
            if (currentManID == "0")
            {
                newProducts = DataAccessContext.ProductRepository.GetAllByNewArrival( StoreContext.Culture, DataAccessContext.Configurations.GetIntValue( "ProductNewArrivalNumber", new StoreRetriever().GetStore() ), DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ), true, new StoreRetriever().GetCurrentStoreID(), DataAccessContext.Configurations.GetValue( "RootCategory" ) );
            }
            else
            {
                Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, currentManID );
                if (manufacturer.IsShowNewArrival)
                    newProducts = DataAccessContext.ProductRepository.GetNewArrivalByManufacturerID(
                        StoreContext.Culture,
                        currentManID,
                        manufacturer.NewArrivalAmount,
                        new StoreRetriever().GetCurrentStoreID() );
                else
                    newProducts = new List<Product>();
            }
        }

        if (newProducts.Count > 0)
        {
            uxNewArrivalList.DataSource = newProducts;
            uxNewArrivalList.DataBind();
        }
        else
        {
            uxNewArrivalCategory.Visible = false;
        }
    }

    private void NewArrivalProduct_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void NewArrivalProduct_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( NewArrivalProduct_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( NewArrivalProduct_StoreCurrencyChanged );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
            PopulateControls();
    }
    protected string GetFormattedPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID.ToString(), StoreContext.CurrentStore.StoreID );

        decimal price = product.GetDisplayedPrice( StoreContext.WholesaleStatus );
        return StoreContext.Currency.FormatPrice( price );
    }
}
