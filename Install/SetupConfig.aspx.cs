using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;
using Vevo.WebUI.Widget;

public partial class Install_SetupConfig : System.Web.UI.Page
{
    # region Private

    private bool _success = false;
    private const int _keyMaxLength = 255;
    private const int _vevoPayPathMaxLength = 255;
    private Store _defaultStore;
    private string _bussinessName = "eCommerce Store";

    private string GetDefaultPageID()
    {
        return PageAccess.GetIDByPath( "/default.aspx" );
    }

    private void UpdateBusinessProfile()
    {
        Culture culture = DataAccessContext.CultureRepository.GetOne( CultureUtilities.DefaultCultureID );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            culture,
            DataAccessContext.Configurations["CompanyName"],
            _bussinessName,
            _defaultStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            culture,
            DataAccessContext.Configurations["SiteName"],
            _bussinessName,
            _defaultStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            culture,
            DataAccessContext.Configurations["Title"],
            _bussinessName,
            _defaultStore );
    }

    private void CreateStore()
    {
        string storefrontUrl = UrlPath.GetStoreFrontUrlWithoutWWWandHTTP();
        _defaultStore = DataAccessContext.StoreRepository.GetOne(
            DataAccessContext.StoreRepository.GetStoreIDByUrlName( storefrontUrl ) );

        if (_defaultStore.IsNull)
        {
            _defaultStore = new Store();
            _defaultStore.StoreName = _bussinessName;
            _defaultStore.UrlName = storefrontUrl;
            _defaultStore.IsEnabled = true;
            DataAccessContext.StoreRepository.Save( _defaultStore );
            _defaultStore.CreateStoreConfigCollection( _defaultStore.StoreID );

            WidgetDirector widgetDirector = new WidgetDirector();
            foreach (string widgetConfigName in widgetDirector.WidgetConfigurationNameAll)
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations[widgetConfigName],
                DataAccessContext.Configurations.GetValue( widgetConfigName, Store.Null ),
                    _defaultStore );
            }
        }
        else
        {
            DataAccessContext.StoreRepository.Save( _defaultStore );
        }
    }

    private void CreateRootCategory()
    {
        Culture culture = DataAccessContext.CultureRepository.GetOne( CultureUtilities.DefaultCultureID );

        IList<Category> rootList = DataAccessContext.CategoryRepository.GetRootCategory( culture, "CategoryID", BoolFilter.ShowAll );
        if (rootList.Count == 0)
        {
            Category rootCategory = new Category( culture );
            rootCategory.Name = "RootCategory";
            rootCategory = DataAccessContext.CategoryRepository.Save( rootCategory );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["RootCategory"],
                rootCategory.CategoryID,
                _defaultStore );
        }
    }

    private string GetFullPath( string url )
    {
        if (!url.Substring( 0, "http://".Length ).ToLower().Contains( "http://" )
            || !url.Substring( 0, "https://".Length ).ToLower().Contains( "https://" ))
            url = string.Format( "{0}{1}", "http://", url );

        if (!url.Substring( url.Length - 1, 1 ).ToLower().Contains( "/" ))
        {
            url = string.Format( "{0}/", url );
        }
        return url;
    }

    private void Update()
    {
        try
        {
            CreateStore();
            CreateRootCategory();
            UpdateBusinessProfile();
            _success = true;
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( Server.HtmlEncode( ex.Message ) );
        }
    }
    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxTimer_Tick( object sender, EventArgs e )
    {
        Update();
        if (_success == true)
        {
            Response.Redirect( "finished.aspx", true );
        }
    }
    #endregion
}
