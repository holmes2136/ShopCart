using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using System.IO;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.Domain.ImportExport;
using Vevo.Domain.Stores;
using System.Collections;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class Admin_Components_ExportProduct : AdminAdvancedBaseUserControl
{
    #region Private
    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        uxStoreDrop.Items.Add( new ListItem( "Default Value", Store.Null.StoreID ) );
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[index].StoreName, storeList[index].StoreID ) );
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ProductRepository.GetTableSchema();
        uxExportFilter.SetUpSchema( list );
        SetUpSortDrop( list );
    }

    private void SetUpSortDrop( IList<TableSchemaItem> list )
    {
        uxSortByDrop.Items.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            uxSortByDrop.Items.Add( list[i].ColumnName );
        }
    }

    private void TieGenerateButton()
    {
        uxExportFilter.TieGenerateTextBox( uxGenerateButton );
    }


    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            InsertStoreInDropDownList();
            SetUpSearchFilter();
        }

        TieGenerateButton();

        ScriptManager uxScriptManager = ScriptManager.GetCurrent( Page );
        uxScriptManager.RegisterPostBackControl( uxGenerateButton );
        uxScriptManager.RegisterPostBackControl( uxGenSpecValueButton );
        uxScriptManager.RegisterPostBackControl( uxGenProductKitItemButton );
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();

        uxExportFilter.GetSearchFilterObj( e );

        int howMayItems;
        int count = DataAccessContext.ProductRepository.GetAllProductCount();
        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue.ToString() );
        string storeID = uxStoreDrop.SelectedValue.ToString();
        IList<Product> list = DataAccessContext.ProductRepository.SearchProduct(
            culture, "", uxSortByDrop.SelectedValue.ToString(), uxExportFilter.SearchFilterObj,
            0, count, out howMayItems, storeID, DataAccessContext.Configurations.GetValue( "RootCategory", DataAccessContext.StoreRepository.GetOne( storeID ) ) );
        if (list.Count > 0)
        {
            ProductExporter exporter = new ProductExporter();
            MemoryStream memStream = exporter.ExportToStream( list, culture, storeID );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportProduct.csv", out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    protected void uxGenSpecValueButton_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();
        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue.ToString() );
        string storeID = uxStoreDrop.SelectedValue.ToString();
        IList<ProductSpecification> list = DataAccessContext.ProductRepository.GetAllProductSpecifications();

        if (list.Count > 0)
        {
            ProductSpecificationExporter exporter = new ProductSpecificationExporter();
            MemoryStream memStream = exporter.ExportToStream( list, culture, storeID );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportProductSpecification.csv", out ex);
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    protected void uxGenProductKitItemButton_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();
        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue.ToString() );
        string storeID = uxStoreDrop.SelectedValue.ToString();
        IList<ProductKitItem> list = DataAccessContext.ProductKitGroupRepository.GetAllProductKitItems();

        if (list.Count > 0)
        {
            ProductKitGroupItemExporter exporter = new ProductKitGroupItemExporter();
            MemoryStream memStream = exporter.ExportToStream( list, culture, storeID );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportProductKitItem.csv",out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    #endregion

    #region Public Methods

    public void HideProductGenerateButton()
    {
        uxGenerateButton.Visible = false;
    }

    #endregion
}
