using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Shared.Utilities;
using System.Text;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Deluxe.WebUI.Base;

public partial class Components_PromotionItem : BaseDeluxeLanguageUserControl
{
    private IList<PromotionGroupSubGroup> subGroupList;
    private PromotionGroup group;

    private void PopulateControl()
    {
        group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );
        subGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupSubGroup( PromotionGroupID, "SortOrder" );
        SetGroup();
    }

    private void SetList()
    {
        IList<PromotionGroupSubGroup> newList = new List<PromotionGroupSubGroup>();
        foreach (PromotionGroupSubGroup subGroup in subGroupList)
        {
            newList.Add( subGroup );
            newList.Add( PromotionGroupSubGroup.Null );
        }

        if (newList.Count <= 0)
            Response.Redirect( "~/PromotionGroupList.aspx" );
        newList.RemoveAt( newList.Count - 1 );
        uxList.DataSource = newList;
        uxList.DataBind();
    }

    private void SetGroup()
    {
        uxNameLabel.Text = group.Name;
        uxPriceLabel.Text = StoreContext.Currency.FormatPrice( group.Price );
        uxDescriptionLabel.Text = group.Description;

        if (IsAuthorizedToViewPrice())
            uxPriceLabel.Visible = true;
        else
            uxPriceLabel.Visible = false;
    }

    protected void uxTellFriendButton_Command( object sender, CommandEventArgs e )
    {
        group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );
        string url = UrlManager.GetPromotionTellFriendUrl( group.PromotionGroupID, group.UrlName );
        Response.Redirect( url );
    }

    public string PromotionGroupID
    {
        get
        {
            if (ViewState["PromotionGroupID"] == null)
                return "0";
            else
                return (string) ViewState["PromotionGroupID"];
        }
        set
        {
            ViewState["PromotionGroupID"] = value;
        }
    }

    public void Reload()
    {
        PopulateControl();

        foreach (DataListItem item in uxList.Items)
        {
            if (item.FindControl( "uxGroup" ) != null)
            {
                Components_PromotionProductGroup productGroup = (Components_PromotionProductGroup) item.FindControl( "uxGroup" );
                productGroup.Reload();
            }
        }
    }

    private bool VerifyStockOption(
    string productID, ArrayList optionItemIDs, int quantity, out int currentStock, string storeID )
    {
        return VerifyStockOption( productID, optionItemIDs, quantity, String.Empty, out currentStock, storeID );
    }

    private bool VerifyStockOption(
        string productID, ArrayList optionItemIDs, int qty, string name, out int currentStock, string storeID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, storeID );

        if (optionItemIDs == null || optionItemIDs.Count == 0)
        {
            if (IsOutOfStock( productID, qty, out currentStock ))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            string[] optionIDsArray = optionItemIDs.ToArray( typeof( string ) ) as string[];
            string combinationID = product.FindProductStock( optionIDsArray ).OptionCombinationID;
            currentStock = product.GetPurchasableStock(
                StoreContext.ShoppingCart.GetNumberOfItems( productID, combinationID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, productID, combinationID ),
                optionIDsArray );
            return !product.IsOutOfStock(
                qty,
                StoreContext.ShoppingCart.GetNumberOfItems( productID, combinationID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, productID, combinationID ),
                optionIDsArray
                );
        }
    }

    private bool IsOutOfStock( string productID, int purchaseQuantity, out int currentStock )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );
        currentStock = product.GetPurchasableStock(
             StoreContext.ShoppingCart.GetNumberOfItems( productID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, productID) );
        return product.IsOutOfStock(
            purchaseQuantity,
            StoreContext.ShoppingCart.GetNumberOfItems( productID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, productID ) );
    }

    private bool CheckOutOfStock( IList<PromotionSelectedItem> items )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        foreach (PromotionSelectedItem item in items)
        {
            Product product = item.Product;
            int currentStock;
            if (item.ProductOptionIDs.Count <= 0)
            {
                if (IsOutOfStock( product.ProductID, item.GetPromotionProduct.Quantity, out currentStock ))
                {
                    //uxMessage.DisplayError( product.Name + " cannot be added to shopping cart because out of stock." );
                    return false;
                }
            }
            else
            {
                IList<ProductOptionGroup> groups = DataAccessContext.ProductRepository.GetProductOptionGroups( StoreContext.Culture, product.ProductID );

                IList<ProductOptionGroup> optionGroupsInputList = new List<ProductOptionGroup>();
                IList<ProductOptionGroup> optionGroupsWithoutInputList = new List<ProductOptionGroup>();
                foreach (ProductOptionGroup group in groups)
                {
                    if (!(group.OptionGroup.Type == OptionGroup.OptionGroupType.InputList))
                    {
                        optionGroupsWithoutInputList.Add( group );
                    }
                    else
                    {
                        optionGroupsInputList.Add( group );
                    }
                }
                if (optionGroupsInputList.Count <= 0)
                {
                    ArrayList optionsUseStock = item.GetUseStockOptionItems();
                    if (!VerifyStockOption(
                        product.ProductID, optionsUseStock, item.GetPromotionProduct.Quantity, out currentStock, storeID ))
                    {
                        //uxMessage.DisplayError( product.Name + " cannot be added to shopping cart because out of stock." );
                        return false;
                    }
                }
                else
                {
                    currentStock = 0;
                    ArrayList optionIDsWithStock = item.GetUseStockOptionItems();
                    if (optionIDsWithStock.Count > 0)
                    {
                        if (!VerifyStockOption(
                            product.ProductID,
                            optionIDsWithStock,
                            item.GetPromotionProduct.Quantity,
                            out currentStock, storeID ))
                        {
                            //uxMessage.DisplayError( product.Name + " cannot be added to shopping cart because out of stock." );
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "EnableBundlePromo", StoreContext.CurrentStore ))
            Response.Redirect( "~/Default.aspx" );

        if (!IsPostBack)
        {
            PopulateControl();
            SetList();
        }
    }

    protected void uxAddToCartButton_Click( object sender, EventArgs e )
    {
        PromotionSelected promotion = new PromotionSelected( StoreContext.Culture, new StoreRetriever().GetCurrentStoreID() );
        promotion.SetPromotionGroupID = PromotionGroupID;
        bool isSuccess = true;

        foreach (DataListItem item in uxList.Items)
        {
            if (item.FindControl( "uxGroup" ) != null)
            {
                Components_PromotionProductGroup group = (Components_PromotionProductGroup) item.FindControl( "uxGroup" );
                if (group.IsSelectedProduct)
                {
                    string productGroup = group.GetSelectedOption;
                    string[] groupInfo = productGroup.Split( ':' );

                    IList<string> options = new List<string>();
                    foreach (string option in groupInfo[1].Split( ',' ))
                    {
                        if (option.Trim() == "") continue;
                        options.Add( option );
                    }
                    Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, groupInfo[0], new StoreRetriever().GetCurrentStoreID() );
                    promotion.AddSelectedPromotionItem( group.PromotionSubGroupID, product, options );
                }
                else
                {
                    isSuccess = false;
                }
            }
        }
        if (!isSuccess)
        {
            return;
        }

        // Check Inventory
        isSuccess = CheckOutOfStock( promotion.PromotionSelectedItems );
        if (!isSuccess)
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=0&PromotionStock=1" );
        // Check Free Shipping
        isSuccess = CartItemPromotion.CheckCanAddItemToCart( StoreContext.ShoppingCart,promotion);
        if (isSuccess)
        {
            PromotionSelected.AddPromotionItem( StoreContext.ShoppingCart, promotion, 1 );

            bool enableNotification = ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", StoreContext.CurrentStore ) );
            if (UrlManager.IsMobileDevice(Request))
            {
                enableNotification = false;
            }
            if (enableNotification)
            {                
                uxAddToCartNotification.Show( promotion, 1 );
            }
            else
            {
                Response.Redirect( "ShoppingCart.aspx" );
            }
        }
        else
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=0&FreeShiping=1" );
        }
    }

    protected static bool IsCatalogMode()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "IsCatalogMode" ))
            return true;
        else
            return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
            return false;

        return true;
    }

}
