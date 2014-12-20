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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_ProductBulkUpdate : AdminAdvancedBaseListControl
{
    #region Private
    private const int ProductIDColumn = 1;

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
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.ProductItemsPerPage;
            SetUpSearchFilter();
            uxCategoryFilterDrop.CultureID = uxLanguageControl.CurrentCultureID;
        }

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterCategoryFilterDrop( uxCategoryFilterDrop );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxCategoryFilterDrop.BubbleEvent += new EventHandler( uxCategoryFilterDrop_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }
    #endregion


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxStoreView.Visible = false;
        }

        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();

        if (!IsAdminModifiable())
            uxUpdateButton.Visible = false;
    }

    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected decimal GetPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, uxStoreList.CurrentSelected );
        ProductPrice productPrice = product.GetProductPrice( uxStoreList.CurrentSelected );

        return productPrice.Price;
    }

    protected bool IsUseDefaultValue( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, uxStoreList.CurrentSelected );
        ProductPrice productPrice = product.GetProductPrice( uxStoreList.CurrentSelected );

        if (uxStoreList.CurrentSelected == "0")
            return false;
        else
            return productPrice.UseDefaultPrice;
    }

    private IList<Product> _allProductShowInPage = new List<Product>();

    protected void uxGridProduct_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.Cells.Count > 1)
        {
            if (e.Row.RowIndex > -1)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    // Even

                    if (e.Row.RowIndex == 0)
                    {
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                        e.Row.CssClass = "DefaultGridRowStyle";
                    }
                    else if (e.Row.RowIndex == _allProductShowInPage.Count)
                    {
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                        e.Row.CssClass = "DefaultGridRowStyle";
                    }
                    else
                    {
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                    }
                }
                else
                {
                    // Odd
                    if (e.Row.RowIndex == _allProductShowInPage.Count - 1)
                    {
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                        e.Row.CssClass = "DefaultGridAlternatingRowStyle";
                    }
                    else
                    {
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                    }
                }
            }
        }

    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        int countrow = uxGridProduct.Rows.Count;

        foreach (GridViewRow r in uxGridProduct.Rows)
            if (IsRowModified( r )) { uxGridProduct.UpdateRow( r.RowIndex, false ); }

        RefreshGrid();

        uxStatusHidden.Value = "Updated";
    }

    private Product SetProductPrice( Product product, string storeID, decimal price )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        if (storeID == "0")
        {
            product.SetProductPrice( "0", price );

            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

            foreach (Store store in storeList)
            {
                ProductPrice storeProductPrice = product.GetProductPrice( store.StoreID );
                if (storeProductPrice.UseDefaultPrice)
                    product.SetProductPrice( store.StoreID, price );
            }
        }
        else
        {
            if (!productPrice.UseDefaultPrice)
            {
                product.SetProductPrice( storeID, price );
            }
        }

        return product;
    }

    protected void uxGridProduct_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string storeID = uxStoreList.CurrentSelected;
            GridViewRow rowGrid = uxGridProduct.Rows[e.RowIndex];

            string productID = ((HiddenField) rowGrid.Cells[0].FindControl( "uxProductIDHidden" )).Value;

            Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, productID, storeID );

            product.Sku = ((TextBox) rowGrid.Cells[0].FindControl( "uxSkuText" )).Text;
            product.Name = ((TextBox) rowGrid.Cells[1].FindControl( "uxProductNameText" )).Text;
            product.ShortDescription = ((TextBox) rowGrid.Cells[2].FindControl( "uxShortDescriptionText" )).Text;
            TextBox productStock = (TextBox) rowGrid.Cells[3].FindControl( "uxStockText" );

            product = SetProductPrice( product, storeID, ConvertUtilities.ToDecimal( ((TextBox) rowGrid.Cells[4].FindControl( "uxPriceText" )).Text ) );

            if (!HasOptionStock( productID ))
                product.SetStock( ConvertUtilities.ToInt32( productStock.Text ) );

            DataAccessContext.ProductRepository.Save( product );

            uxMessage.DisplayMessage( Resources.ProductMessages.BulkUpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }


    protected bool IsRowModified( GridViewRow r )
    {
        int productID;
        string productName;
        string shortDescription;
        string sku;
        int stock;
        decimal price;

        productID = Convert.ToInt32( ((HiddenField) r.Cells[0].FindControl( "uxProductIDHidden" )).Value );

        productName = ((TextBox) r.FindControl( "uxProductNameText" )).Text;

        sku = ((TextBox) r.FindControl( "uxSkuText" )).Text;
        shortDescription = ((TextBox) r.FindControl( "uxShortDescriptionText" )).Text;
        stock = ConvertUtilities.ToInt32( ((TextBox) r.FindControl( "uxStockText" )).Text );
        price = ConvertUtilities.ToDecimal( ((TextBox) r.FindControl( "uxPriceText" )).Text );

        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, productID.ToString(), new StoreRetriever().GetCurrentStoreID() );

        if (!productName.Equals( product.Name )) { return true; }
        if (!sku.Equals( product.Sku )) { return true; }
        if (!shortDescription.Equals( product.ShortDescription )) { return true; }
        if (!stock.Equals( product.SumStock )) { return true; }
        if (!price.Equals( product.GetProductPrice( new StoreRetriever().GetCurrentStoreID() ).Price )) { return true; }

        return false;
    }

    private Product SelectProuduct( IList<Product> productList, string productID )
    {
        foreach (Product product in productList)
        {
            if (product.ProductID == productID)
                return product;
        }
        return new Product( uxLanguageControl.CurrentCulture );
    }

    protected bool HasOptionStock( string productID )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, productID, storeID );

        if (product.ProductStocks.Count == 1 && product.ProductStocks[0].OptionCombinationID == "0")
            return false;
        else
            return true;
    }

    protected bool ShowStock( object useInventory, object HasOptionStock )
    {
        if (!ConvertUtilities.ToBoolean( useInventory ) || ConvertUtilities.ToBoolean( HasOptionStock ))
            return false;
        else
            return true;
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        string storeID = new StoreRetriever().GetCurrentStoreID();
        _allProductShowInPage = DataAccessContext.ProductRepository.SearchProduct(
            uxLanguageControl.CurrentCulture,
            uxCategoryFilterDrop.SelectedValue,
            "ProductID",
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems,
            storeID,
            RootCategoryValue
            );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridProduct.DataSource = _allProductShowInPage;
        uxGridProduct.DataBind();
    }

}
