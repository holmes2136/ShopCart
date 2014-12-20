using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using System.Collections.Generic;

public partial class GiftRegistryItemPage : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private string GiftRegistryID
    {
        get
        {
            return Request.QueryString["GiftRegistryID"];
        }
    }

    private void PopulateControls()
    {
        GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID );
        uxEventNameLable.Text = giftRegistry.EventName;
        uxEventDateLabel.Text = giftRegistry.EventDate.ToShortDateString();

        if ((DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated) || (CatalogUtilities.IsCatalogMode()))
        {
            uxGrid.Visible = false;
            uxAddToCartImageButton.Visible = false;
        }

        uxGrid.DataSource = DataAccessContextDeluxe.GiftRegistryItemRepository.GetAllByGiftRegistryID( GiftRegistryID );
        uxGrid.DataBind();

        CheckCannotBuyItem();
    }

    private void AddToCart()
    {
        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string giftRegistryItemID = uxGrid.DataKeys[rowIndex].Value.ToString();
                int wantQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxWantQuantityLabel" )).Text.Trim() );
                int hasQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxHasQuantityLabel" )).Text.Trim() );
                string quantityText = ((TextBox) row.FindControl( "uxQuantityText" )).Text.Trim();
                int quantity;
                int totalItem = wantQuantity - hasQuantity;
                if (int.TryParse( quantityText, out quantity ))
                {
                    if (quantity <= totalItem && quantity > 0)
                    {
                        AddGiftRegistryItemToCart( giftRegistryItemID, quantity );
                    }
                }
            }
        }
    }

    private void AddGiftRegistryItemToCart( string giftRegistryItemID, int quantity )
    {
        ICartItem cartItem = GiftRegistryItem.AddGiftRegistryItem( StoreContext.ShoppingCart,
                           StoreContext.Culture,
                           giftRegistryItemID,
                           quantity );

        StoreContext.CheckoutDetails.CartItemIDToGiftRegistryIDMap[cartItem.CartItemID] =
            giftRegistryItemID;
    }

    private bool CheckAddToCartByFreeShipping(out bool freeShipping)
    {
        int rowIndex;
        IList<Product> FreeShippingCostProducts = new List<Product>();
        IList<Product> HasShippingCostProducts = new List<Product>();
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string productID = uxGrid.DataKeys[rowIndex]["ProductID"].ToString();
                string giftRegistryItemID = uxGrid.DataKeys[rowIndex].Value.ToString();
                int quantity = ConvertUtilities.ToInt32( ((TextBox) row.FindControl( "uxQuantityText" )).Text.Trim() );
                if (quantity <= 0)
                    continue;

                Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );

                if (product.FreeShippingCost)
                    FreeShippingCostProducts.Add( product );
                else
                    HasShippingCostProducts.Add( product );
            }
        }

        if ((FreeShippingCostProducts.Count == 0) && (HasShippingCostProducts.Count > 0))
        {
            freeShipping = false;
            return true;
        }
        else if ((FreeShippingCostProducts.Count > 0) && (HasShippingCostProducts.Count == 0))
        {
            freeShipping = true;
            return true;
        }
        else
        {
            freeShipping = false;
            return false;
        }
        
    }

    private string CheckErrorItem()
    {
        int rowIndex;
        string errormsg = string.Empty;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string giftRegistryItemID = uxGrid.DataKeys[rowIndex].Value.ToString();
                int wantQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxWantQuantityLabel" )).Text.Trim() );
                int hasQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxHasQuantityLabel" )).Text.Trim() );
                string productName = ((HyperLink) row.FindControl( "uxNameLink" )).Text.Trim();
                string quantityText = ((TextBox) row.FindControl( "uxQuantityText" )).Text.Trim();

                int quantityInCart = GiftRegistryItem.GetGiftRegistryItemQuantity(StoreContext.ShoppingCart,
                    StoreContext.Culture, StoreContext.WholesaleStatus, giftRegistryItemID );
                int totalItem = wantQuantity - hasQuantity - quantityInCart;

                int quantity;
                if (int.TryParse( quantityText, out quantity ))
                {
                    if (quantity > totalItem && quantity > 0)
                    {
                        errormsg += "<li>" + productName + "</li>";
                    }
                }
            }
        }
        return errormsg;
    }

    private decimal GetStock( string productID, string optionIDText )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );
        if (!String.IsNullOrEmpty( optionIDText ))
        {
            OptionItemValueCollection options = new OptionItemValueCollection( StoreContext.Culture, optionIDText, product.ProductID );
            return product.GetStock( options.GetUseStockOptionItemIDs() );
        }
        else
            return product.GetStock();
    }

    private void CheckCannotBuyItem()
    {
        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string productID = uxGrid.DataKeys[rowIndex]["ProductID"].ToString();
                string optionIDText = ((HiddenField) row.FindControl( "uxOptionHidden" )).Value.ToString();
                decimal stock = GetStock( productID, optionIDText );

                int wantQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxWantQuantityLabel" )).Text.Trim() );
                int hasQuantity = ConvertUtilities.ToInt32( ((Label) row.FindControl( "uxHasQuantityLabel" )).Text.Trim() );
                Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );

                if (wantQuantity <= hasQuantity)
                {
                    ((Label) row.FindControl( "uxCannotBuyLabel" )).Text = "Completed";
                    ((TextBox) row.FindControl( "uxQuantityText" )).Visible = false;
                }
                else if (CatalogUtilities.IsOutOfStock( stock, product.UseInventory ))
                {
                    ((Label) row.FindControl( "uxCannotBuyLabel" )).Text = "Out of stock";
                    ((TextBox) row.FindControl( "uxQuantityText" )).Visible = false;
                }
                else
                {
                    ((Label) row.FindControl( "uxCannotBuyLabel" )).Text = "";
                    ((TextBox) row.FindControl( "uxQuantityText" )).Visible = true;
                }
            }
        }
    }

    private void RegisterJavaScript()
    {
        string script = "var win=null;" +
            "function NewWindow(mypage,myname,w,h,pos,infocus){" +
            "if(pos==\"random\"){myleft=(screen.width)?Math.floor(Math.random()*" +
            "(screen.width-w)):100;mytop=(screen.height)?Math.floor(Math.random()*((screen.height-h)-75)):100;}" +
            "if(pos==\"center\"){myleft=(screen.width)?(screen.width-w)/2:100;mytop=(screen.height)?(screen.height-h)/2:100;}" +
            "else if((pos!='center' && pos!=\"random\") || pos==null){myleft=0;mytop=20}" +
            "settings=\"width=\" + w + \",height=\" + h + \",top=\" + mytop + \",left=\" + myleft + \",scrollbars=yes," +
            "location=yes,directories=no,status=no,menubar=no,toolbar=no,resizable=yes\";win=window.open(mypage,myname,settings);" +
            "win.focus();}";

        ScriptManager.RegisterStartupScript( this, this.GetType(), "PopupWindow", script, true );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterJavaScript();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
            PopulateControls();
    }

    protected void uxAddToCartImageButton_Click( object sender, EventArgs e )
    {
        if (!StoreContext.CheckoutDetails.ContainsGiftRegistry() &&
            StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxMessageLabel.Text = "Error: The shopping cart already has non-gift-registry item(s).";
            return;
        }

        if (StoreContext.CheckoutDetails.ContainsGiftRegistry() &&
            StoreContext.CheckoutDetails.GiftRegistryID != GiftRegistryID)
        {
            uxMessageLabel.Text = "Error: Cannot add item(s) from different gift registry.";
            return;
        }

        bool isContainsFreeShippingInGiftList;
        bool isContainsSameShippingType = CheckAddToCartByFreeShipping( out isContainsFreeShippingInGiftList );
        if (!isContainsSameShippingType)
        {
            uxMessageLabel.Text = "Error: Cannot add free shipping items with not free shipping items.";
            return;
        }

        if (StoreContext.ShoppingCart.CartItems.Count > 0)
        {
            if (!isContainsFreeShippingInGiftList)
            {
                if (StoreContext.ShoppingCart.ContainsFreeShippingCostProduct())
                {
                    uxMessageLabel.Text = "Error: Cannot add not free shipping items to shopping cart because it contains free shipping items already.";
                    return;
                }
            }
            else
            {
                if (!StoreContext.ShoppingCart.ContainsFreeShippingCostProduct())
                {
                    uxMessageLabel.Text = "Error: Cannot add free shipping items to shopping cart because it contains not free shipping items already.";
                    return;
                }
            }
        }

        string errormsg = CheckErrorItem();
        if (!string.IsNullOrEmpty( errormsg ))
        {
            uxMessageLabel.Text = "Error: The following item(s) cannot be added to the cart:<br />" + errormsg;
            return;
        }

        AddToCart();
        StoreContext.CheckoutDetails.SetGiftRegistryID( GiftRegistryID );
        Response.Redirect( "ShoppingCart.aspx" );
    }

    protected string GetUrl( string productID )
    {
        return "ProductPopUp.aspx?ProductID=" + productID;
    }

    protected void uxNameLink_PreRender( object sender, EventArgs e )
    {
        HyperLink link = (HyperLink) sender;
        string strRedirect = link.NavigateUrl;
        link.NavigateUrl = "javascript:NewWindow('" + strRedirect + "','acepopup','700','500','center','front');";
    }

    protected string GetPrice( string giftregistryItemID )
    {
        GiftRegistryItem giftRegistryItem = DataAccessContextDeluxe.GiftRegistryItemRepository.GetOne( giftregistryItemID );

        return StoreContext.Currency.FormatPrice(
                    giftRegistryItem.GetUnitPrice( StoreContext.WholesaleStatus ) );
    }
}
