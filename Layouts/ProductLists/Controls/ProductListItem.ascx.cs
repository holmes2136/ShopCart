using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Components_ProductListItem : BaseProductListItemUserControl
{
    #region Private

    private void uxAddtoWishListButton_BubbleEvent( object sender, EventArgs e )
    {
        ProcessAddToWishListButton( uxAddtoWishListButton, uxAddtoWishListButton.ProductID );
    }

    private void uxAddtoCompareListButton_BubbleEvent( object sender, EventArgs e )
    {
        uxAddtoCompareListButton.AddItemToCompareListCart( uxAddtoCompareListButton.ProductID );
        Response.Redirect( "ComparisonList.aspx" );
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxAddtoWishListButton.BubbleEvent += new EventHandler( uxAddtoWishListButton_BubbleEvent );
        uxAddtoCompareListButton.BubbleEvent += new EventHandler( uxAddtoCompareListButton_BubbleEvent );
        OptionGroupDetailsParent = uxOptionGroupPanel;
        AddToCartNotificationParent = uxAddToCartNotification;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "TellAFriendEnabled" ))
            uxTellFriendLinkButton.Visible = false;
    }

    #endregion
}
