using System;
using System.Collections;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using System.Collections.Generic;

public partial class Mobile_Components_RelatedProducts : Vevo.WebUI.Products.BaseProductUserControl
{
    #region Private

    private Product[] GetArrayOfProducts( string[] productIDs )
    {
        ArrayList array = new ArrayList();
        string rootID = DataAccessContext.Configurations.GetValueNoThrow( "RootCategory", StoreContext.CurrentStore );
        for (int i = 0; i < productIDs.Length; i++)
        {
            string productID = productIDs[i].Trim();
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture,
                productID, new StoreRetriever().GetCurrentStoreID() );

            if (!product.IsNull && product.IsParentVisible && product.IsEnabled)
            {
                if (IsProductInsideRoot( product, rootID ))
                    array.Add( product );
            }
        }

        Product[] detailsArray = new Product[array.Count];
        array.CopyTo( detailsArray );

        return detailsArray;
    }

    private bool IsProductInsideRoot( Product product, string rootID )
    {
        IList<string> categoryIDs = product.CategoryIDs;
        foreach (string categoryID in categoryIDs)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, categoryID );
            if (category.RootID == rootID)
                return true;
        }
        return false;
    }

    private void PopulateRelatedProducts()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        string relatedProducts = product.RelatedProducts;

        if (!String.IsNullOrEmpty( relatedProducts ))
        {
            this.Visible = true;

            string[] relatedArray = relatedProducts.Split( ',' );

            uxRelatedList.DataSource = GetArrayOfProducts( relatedArray );
            uxRelatedList.DataBind();
        }
        else
        {
            this.Visible = false;
        }

        if (uxRelatedList.Items.Count == 0)
            P1.Visible = false;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateRelatedProducts();
    }

    protected string GetRelatedProductsImage( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, new StoreRetriever().GetCurrentStoreID() );
        ProductImage imageDetails = product.GetPrimaryProductImage();
        return "~/" + imageDetails.RegularImage;
    }

    #endregion


    #region Public Properties

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    #endregion
}
