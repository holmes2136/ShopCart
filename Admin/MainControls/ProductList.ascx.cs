using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ProductList : AdminAdvancedBaseListControl
{
    #region Private
    private const int ProductIDColumnIndex = 1;
    private const int StockColumnIndex = 5;
    private const int RetailColumnIndex = 7;
    private const int ReviewColumnIndex = 9;
    private string _sortPage = "ProductSorting.ascx";

    private string RootCategoryValue
    {
        get
        {
            if (!KeyUtilities.IsMultistoreLicense())
                return DataAccessContext.Configurations.GetValue(
                    "RootCategory",
                    DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            else
                return uxCategoryFilterDrop.SelectedRootValue;
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ProductRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "UrlName", 
            "UseDefaultMetaKeyword", "UseDefaultMetaDescription", 
            "UseDefaultPrice", "UseDefaultRetailPrice", 
            "UseDefaultWholeSalePrice", "UseDefaultWholeSalePrice2", "UseDefaultWholeSalePrice3" );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGridProduct.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            uxeBayListing.Visible = false;
            DeleteVisible( false );
        }

        uxSortButton.Visible = IsAdminViewable( _sortPage );

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxeBayListing.Visible = false;
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private bool IsAnyOptionOutOfStock( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            uxLanguageControl.CurrentCulture, productID, new StoreRetriever().GetCurrentStoreID() );
        return product.IsOutOfStock();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.ProductItemsPerPage;
            SetUpSearchFilter();
            uxCategoryFilterDrop.CultureID = uxLanguageControl.CurrentCultureID;
        }

        RegisterGridView( uxGridProduct, "ProductID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterCategoryFilterDrop( uxCategoryFilterDrop );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxCategoryFilterDrop.BubbleEvent += new EventHandler( uxCategoryFilterDrop_BubbleEvent );
    }
    #endregion


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        ProductImageData.Clear();
        MainContext.RedirectMainControl( "ProductAdd.ascx" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridProduct.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ProductIDColumnIndex].Text.Trim();
                    DataAccessContext.ProductRepository.Delete( id );
                    DataAccessContextDeluxe.PromotionProductRepository.DeleteByProductID( id );
                    DataAccessContextDeluxe.ProductSubscriptionRepository.DeleteByProductID( id );
                    deleted = true;
                }
            }

            uxStatusHidden.Value = "Deleted";

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridProduct.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxSortButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( _sortPage );
    }

    protected void uxeBayListing_Click( object sender, EventArgs e )
    {
        try
        {
            string queryStringID = String.Empty;
            ArrayList selectedIsKit = new ArrayList();

            foreach (GridViewRow row in uxGridProduct.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string productID = row.Cells[ProductIDColumnIndex].Text.Trim();
                    if (DataAccessContext.ProductRepository.CheckIsProductKitByProductID( productID ))
                        selectedIsKit.Add( productID );

                    queryStringID += productID + ",";
                }
            }

            if (selectedIsKit.Count > 0)
            {
                string errorMsg = "Cannot list to eBay because the following Product(s) is Product Kit:<br/>";

                for (int i = 0; i < selectedIsKit.Count; i++)
                {
                    errorMsg += selectedIsKit[i];
                    if ((i + 1) != selectedIsKit.Count)
                        errorMsg += ", ";
                }

                uxMessage.DisplayError( errorMsg );
                return;
            }

            if(!String.IsNullOrEmpty(queryStringID))
                MainContext.RedirectMainControl( "EBayListingReview.ascx", "ProductID=" + queryStringID );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxGridProduct_DataBound( object sender, EventArgs e )
    {
        GridView grid = (GridView) sender;

        if (!CatalogUtilities.IsRetailMode)
        {
            grid.Columns[RetailColumnIndex].Visible = false;
        }

        if (!DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
            grid.Columns[StockColumnIndex].Visible = false;
    }

    protected void uxGridProduct_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string productID = e.Row.Cells[ProductIDColumnIndex].Text.Trim();
            int result;
            if (DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
            {
                //Product product = (Product) e.Row.DataItem;
                //if (product.IsOutOfStock())
                //{
                //    foreach (TableCell cell in e.Row.Cells)
                //    {
                //        cell.Style.Add( "color", "#ff0000" );
                //    }
                //}

                if (int.TryParse( productID, out result ))
                {
                    if (IsAnyOptionOutOfStock( productID ))
                    {
                        foreach (TableCell cell in e.Row.Cells)
                        {
                            cell.Style.Add( "color", "#ff0000" );
                        }
                    }
                }
            }

            if (e.Row.RowIndex > -1)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    // Even
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                }
                else
                {
                    // Odd
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                }
            }
        }
    }

    protected string GetStockText( string sumStock, object isGiftCertificate, object useInventory )
    {
        if (!ConvertUtilities.ToBoolean( useInventory ))
            return "-";
        else
            return sumStock;
    }

    protected decimal GetPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, StoreContext.CurrentStore.StoreID );
        ProductPrice productPrice = product.GetProductPrice( StoreContext.CurrentStore.StoreID );

        return productPrice.Price;
    }

    protected decimal GetRetailPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, StoreContext.CurrentStore.StoreID );
        ProductPrice productPrice = product.GetProductPrice( StoreContext.CurrentStore.StoreID );

        return productPrice.RetailPrice;
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        string storeID = new StoreRetriever().GetCurrentStoreID();

        uxGridProduct.DataSource = DataAccessContext.ProductRepository.SearchProduct(
            uxLanguageControl.CurrentCulture,
            uxCategoryFilterDrop.SelectedValue,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems,
            storeID,
            RootCategoryValue );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridProduct.DataBind();
    }

}
