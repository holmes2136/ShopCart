using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_NewArrivalCategoryItem : Vevo.WebUI.Products.BaseProductListItemUserControl
{
    private string RemoveHTML(string strHTML)
    {
        return Regex.Replace(strHTML, "<(.|\n)*?>", "");
    }
    private void uxAddtoWishListButton_BubbleEvent( object sender, EventArgs e )
    {
        ProcessAddToWishListButton( uxAddtoWishListButton, uxAddtoWishListButton.ProductID );
    }

    private void uxAddtoCompareListButton_BubbleEvent( object sender, EventArgs e )
    {
        uxAddtoCompareListButton.AddItemToCompareListCart( uxAddtoCompareListButton.ProductID );
        Response.Redirect( "ComparisonList.aspx" );
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        uxAddtoWishListButton.BubbleEvent += new EventHandler( uxAddtoWishListButton_BubbleEvent );
        uxAddtoCompareListButton.BubbleEvent += new EventHandler( uxAddtoCompareListButton_BubbleEvent );
        if (DataAccessContext.Configurations.GetBoolValue("PriceRequireLogin") && !Page.User.Identity.IsAuthenticated)
        {
            uxRetailPrice.Visible = false;
            uxPricePanel.Visible = false;
        }
        AddToCartNotificationParent = uxAddToCartNotification;
    }
    protected string GetFormattedPrice(object productID)
    {
        Product product = DataAccessContext.ProductRepository.GetOne(StoreContext.Culture, productID.ToString(), StoreContext.CurrentStore.StoreID);

        decimal price = product.GetDisplayedPrice(StoreContext.WholesaleStatus);
        return StoreContext.Currency.FormatPrice(price);
    }
    protected string LimitDisplayCharactor(object description, short characterLimit)
    {
        string shortContent = RemoveHTML(description.ToString());

        if (shortContent.Length > characterLimit)
        {
            string tempString = shortContent.Substring(0, characterLimit).Trim();

            int trimOffset = tempString.LastIndexOf(" ");

            if (trimOffset > 0)
            {
                shortContent = tempString.Substring(0, trimOffset);
            }

            shortContent += " ...";
        }

        return shortContent;
    }
}
