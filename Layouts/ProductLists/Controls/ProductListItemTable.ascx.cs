using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Components_ProductListItemTable : BaseProductListItemUserControl
{
    #region Private

    private void uxAddtoWishListButton_BubbleEvent(object sender, EventArgs e)
    {
        ProcessAddToWishListButton(uxAddtoWishListButton, uxAddtoWishListButton.ProductID);
    }

    private void uxAddtoCompareListButton_BubbleEvent(object sender, EventArgs e)
    {
        uxAddtoCompareListButton.AddItemToCompareListCart(uxAddtoCompareListButton.ProductID);
        Response.Redirect("ComparisonList.aspx");
    }

    #endregion

    #region Protect

    protected void Page_Load(object sender, EventArgs e)
    {
        uxAddtoWishListButton.BubbleEvent += new EventHandler(uxAddtoWishListButton_BubbleEvent);
        uxAddtoCompareListButton.BubbleEvent += new EventHandler(uxAddtoCompareListButton_BubbleEvent);
        AddToCartNotificationParent = uxAddToCartNotification;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!DataAccessContext.Configurations.GetBoolValue("TellAFriendEnabled"))
            uxTellFriendLinkButton.Visible = false;
    }
    protected string CheckValidStock (object useInventory, object stock)
    {
        string stockValue = GetLanguageText( "InStock" );
        bool isOutStock = CatalogUtilities.IsOutOfStock(Convert.ToInt32(stock), (bool)useInventory);
        if (ShowRemainingQuantity(useInventory))
        {
            if ((bool)useInventory)
                stockValue = stockValue + "(" + RemainingStock(stock) + ")";
        }
        else
        {
            stockValue = "-";
        }
        if(isOutStock)
            stockValue = GetLanguageText( "OutStock" );

        return stockValue;
    }
    #endregion
}
