using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain.Products;

public partial class ComparisonList : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ErrorID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["ErrorID"] ))
                return Request.QueryString["ErrorID"];
            else
                return "0";
        }
    }

    private bool IsExitsInCurrentStore( Product product )
    {
        string rootID = DataAccessContext.Configurations.GetValue(
                "RootCategory",
                DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() ) );

        return product.IsAvailable( rootID );
    }

    private IList<Product> GetProductItemList()
    {
        ArrayList list = StoreContext.ProductIDsCompareList;
        if (UserUtilities.GetCurrentCustomerID() != "0")
        {
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            list = currentUser.GetProductIDsCompare();
            if (StoreContext.ProductIDsCompareList.Count > 0)
            {
                ArrayList sessionList = StoreContext.ProductIDsCompareList;
                foreach (string id in sessionList)
                {
                    if (!list.Contains( id ))
                        list.Add( id );
                }
            }


            currentUser.SetProductIDsCompare( list );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }

        int itemShow = DataAccessContext.Configurations.GetIntValue( "CompareProductShow" );
        IList<Product> displayList = new List<Product>();

        int itemCount = 0;

        foreach (string id in list)
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, id, new StoreRetriever().GetStore().StoreID );

            if (!product.IsNull && IsExitsInCurrentStore( product ) && itemCount < itemShow)
            {
                displayList.Add( product );
                itemCount++;
            }
            else
            {
                return displayList;
            }
        }

        return displayList;

    }

    private bool IsPhysicalGiftCertificate( Product product )
    {
        return product.IsGiftCertificate &&
            !((GiftCertificateProduct) product).IsElectronic;
    }


    private void RefreshGrid()
    {
        uxCompareListGrid.DataSource = GetProductItemList();
        uxCompareListGrid.DataBind();
        if (uxCompareListGrid.Rows.Count == 0)
        {
            uxCompareButton.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessage.Visible = false;

        if (ErrorID != "0")
        {
            uxMessage.Text = "[$ErrorCompareLimit] " + DataAccessContext.Configurations.GetIntValue( "CompareProductShow" ) + "<br><br>";
            uxMessage.Visible = true;
            uxMessage.ForeColor = System.Drawing.Color.Red;
        }

        uxCompareButton.Attributes.Add( "onclick", String.Format( "window.open('{0}');", "ComparisonListPopup.aspx" ) );

        if (!IsAuthorizedToViewPrice())
        {
            uxCompareListGrid.Columns[2].Visible = false;
            uxCompareListGrid.Columns[3].Visible = false;
        }
    }


    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxCompareListGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        StoreContext.ProductIDsCompareList.Remove( uxCompareListGrid.DataKeys[e.RowIndex]["ProductID"].ToString() );

        if (UserUtilities.GetCurrentCustomerID() != "0")
        {
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            ArrayList compareList = currentUser.GetProductIDsCompare();
            compareList.Remove( uxCompareListGrid.DataKeys[e.RowIndex]["ProductID"].ToString() );
            currentUser.SetProductIDsCompare( compareList );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }

        RefreshGrid();

        uxStatusHidden.Value = "Deleted";
    }

    protected bool IsBuyButtonVisible( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
                   StoreContext.Culture, (string) productID, new StoreRetriever().GetCurrentStoreID() );
        if (!CatalogUtilities.IsCatalogMode()
            && !Vevo.CatalogUtilities.IsOutOfStock( product.SumStock, product.UseInventory )
            && IsAuthorizedToViewPrice( product.IsCallForPrice ))
            return true;

        return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        return IsAuthorizedToViewPrice( false );
    }

    protected bool IsAuthorizedToViewPrice( object isCallForPrice )
    {
        bool show = true;

        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
        {
            show = false;
        }

        return show;
    }

    protected void uxAddToCartImageButton_Command( object sender, CommandEventArgs e )
    {
        string productID = e.CommandArgument.ToString();
        string urlName = e.CommandName.ToString();

        Product product = DataAccessContext.ProductRepository.GetOne(
                   StoreContext.Culture,
                   productID, new StoreRetriever().GetCurrentStoreID() );

        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (StoreContext.Customer.IsNull)
            {
                string returnUrl = "AddtoCart.aspx?ProductID=" + product.ProductID;
                Response.Redirect( "~/UserLogin.aspx?ReturnUrl=" + returnUrl );
            }
        }

        if (!StoreContext.ShoppingCart.CheckCanAddItemToCart( product ))
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + product.ProductID );
        }

        if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx" );
        }
        else
        {
            if ((DataAccessContext.OptionGroupRepository.ProductIsOptionGroup( StoreContext.Culture, productID )) || (product.IsProductKit))
            {
                Response.Redirect( UrlManager.GetProductUrl( productID, urlName ) );
            }
            else
            {
                if ((IsPhysicalGiftCertificate( product ) || !product.IsFixedPrice || product.RequiresUserInput() || product.IsCustomPrice))
                {
                    Response.Redirect( UrlManager.GetProductUrl( productID, urlName ) );
                }
                else
                {
                    StoreContext.ShoppingCart.AddItem( product, product.MinQuantity );

                    bool enableNotification = ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", StoreContext.CurrentStore ) );
                    if (UrlManager.IsMobileClient())
                    {
                        enableNotification = false;
                    }
                    if (enableNotification)
                    {
                        uxAddToCartNotification.Show( product, product.MinQuantity );
                    }
                    else
                    {
                        Response.Redirect( "ShoppingCart.aspx" );
                    }
                }
            }
        }
    }
    protected string GetFormattedPrice( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID.ToString(), StoreContext.CurrentStore.StoreID );

        decimal price = product.GetDisplayedPrice( StoreContext.WholesaleStatus );
        return StoreContext.Currency.FormatPrice( price );
    }
}
