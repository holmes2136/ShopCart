using System;
using System.Collections.Generic;
using System.Web;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Products;

public partial class Manufacturer : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string pageTitle = String.Empty;
    private string ManufacturerID
    {
        get
        {
            if (ViewState["ManufacturerID"] == null)
                return "0";
            else
                return (string) ViewState["ManufacturerID"];
        }
        set
        {
            ViewState["ManufacturerID"] = value;
        }
    }

    private string CurrentManufacturerID
    {
        get
        {
            string id = Request.QueryString["ManufacturerID"];
            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else
            {
                Vevo.Domain.Products.Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentManufacturerName );

                return manufacturer.ManufacturerID;
            }
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

    private string ManufacturerNameTitle
    {
        get
        {
            if ((CurrentManufacturerName == String.Empty) && (CurrentManufacturerID == "0"))
                return "[$Manufacturer]";
            else
                return pageTitle;
        }
        set
        {
            pageTitle = value;
        }
    }
    private void PopulateTitleAndMeta( DynamicPageElement element )
    {
        Vevo.Domain.Products.Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne(
            StoreContext.Culture, CurrentManufacturerID );
        element.SetUpTitleAndMetaTags(
            manufacturer.Name,
            manufacturer.GetMetaDescription( StoreContext.Culture ),
            manufacturer.GetMetaKeyword( StoreContext.Culture ) );
    }

    private void PopulateControls()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        if (CurrentManufacturerID == "0")
        {
            element.SetUpTitleAndMetaTags( "[$Title]", NamedConfig.SiteDescription );
        }
        else
        {
            Vevo.Domain.Products.Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne(
            StoreContext.Culture, CurrentManufacturerID );

            if (manufacturer.IsEnabled)
            {
                PopulateTitleAndMeta( element );
            }
            else
                Response.Redirect( "~/Error404.aspx" );
        }
    }

    private void PopulateUserControl()
    {
        if (CurrentManufacturerID == "0")
        {
            BaseManufacturerListControl manufacturerControl = new BaseManufacturerListControl();
            manufacturerControl = LoadControl( String.Format( "{0}{1}",
                            SystemConst.LayoutManufacturerListPath, "ManufacturerListDefault.ascx" ) )
                        as BaseManufacturerListControl;
            uxManufacturerControlPanel.Controls.Add( manufacturerControl );
        }
        else
        {
            BaseProductListControl manufacturerListControl = new BaseProductListControl();
            manufacturerListControl = LoadControl( String.Format(
                    "{0}{1}",
                    SystemConst.LayoutProductListPath,
                    DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) )
                        as BaseProductListControl;

            manufacturerListControl.ID = "uxProductList";
            manufacturerListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            manufacturerListControl.UserDefinedParameters = new object[] { CurrentManufacturerID };
            uxManufacturerControlPanel.Controls.Add( manufacturerListControl );
        }
    }

    private void SetUpBreadcrumb()
    {
        if (CurrentManufacturerID == "0" && CurrentManufacturerName == String.Empty)
        {
            uxCatalogBreadcrumb.Visible = false;
            uxManufacturerNameLabel.CssClass = "CatalogName CatalogRoot";
        }
        else
        {
            uxCatalogBreadcrumb.SetupManufacturerSitemap( CurrentManufacturerID );
            uxCatalogBreadcrumb.Refresh();
        }
    }

    private void RefreshTitle()
    {
        Vevo.Domain.Products.Manufacturer manufacturerItem = DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, ManufacturerID );
        pageTitle = manufacturerItem.Name;
        uxManufacturerNameLabel.Text = manufacturerItem.Name;
    }

    private void Manufacturer_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Vevo.Domain.Products.Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne(
                    StoreContext.Culture, ManufacturerID );
        string newURL = UrlManager.GetManufacturerUrl( ManufacturerID, manufacturer.UrlName );

        Response.Redirect( newURL );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        Response.Cache.SetCacheability( HttpCacheability.NoCache );
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader( "Pragma", "no-cache" );

        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Manufacturer_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
            ManufacturerID = CurrentManufacturerID;
        }

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpBreadcrumb();
        RefreshTitle();
    }

    public static IList<Product> GetProductList(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        string sort = sortBy;
        if (sort.Contains( "SortOrder" )) sort = sort.Replace( "SortOrder", "Name" );
        return DataAccessContext.ProductRepository.GetByManufacturerID(
            StoreContext.Culture,
            (string) userDefined[0],
            sortBy,
            startIndex,
            endIndex,
            BoolFilter.ShowTrue,
            out howManyItems,
            new StoreRetriever().GetCurrentStoreID() );
    }
}