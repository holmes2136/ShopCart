using System;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;
using System.Collections.Generic;
using System.Web;

public partial class Blog_Themes_ResponsiveGreen_LayoutControls_Header : BaseLayoutControl
{
    private Cart _wishListCart;

    private Cart CurrentWishListCart
    {
        get
        {
            if (_wishListCart == null)
            {
                string wishListID = DataAccessContext.WishListRepository.GetWishListIDFromCustomerID(
                    StoreContext.Customer.CustomerID );
                WishList wishList = DataAccessContext.WishListRepository.GetOne( wishListID );

                _wishListCart = wishList.Cart;
            }
            return _wishListCart;
        }
    }

    private void PopulateControls()
    {
        switch (DataAccessContext.Configurations.GetValue( "HeaderMenuStyle" ))
        {
            case "cascade":
                System.Web.UI.UserControl uxHeaderMenuCategoryRootStyle = (System.Web.UI.UserControl) Page.LoadControl( "~/Components/HeaderMenu3.ascx" );
                uxHeaderMenuPanel.Controls.Add( uxHeaderMenuCategoryRootStyle );
                break;

            case "group":
                System.Web.UI.UserControl uxHeaderMenuCategoryTabStyle = (System.Web.UI.UserControl) Page.LoadControl( "~/Components/CategoryNavTabMenuList.ascx" );
                uxHeaderMenuPanel.Controls.Add( uxHeaderMenuCategoryTabStyle );
                break;

            default:
                System.Web.UI.UserControl uxHeaderMenuNormalStyle = (System.Web.UI.UserControl) Page.LoadControl( "~/Components/HeaderMenu.ascx" );
                uxHeaderMenuPanel.Controls.Add( uxHeaderMenuNormalStyle );
                break;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!Page.IsPostBack)
        {
            PopulateControls();
        }

        if (DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ))
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                uxWishListTotalItemText.Text = "( " + CurrentWishListCart.CartItems.Count.ToString() + " )";
            }
            else
            {
                uxWishListTotalItemText.Text = "( 0 )";
            }

            uxWishlistAnonymousPanel.Visible = true;
        }
        else
        {
            uxWishlistAnonymousPanel.Visible = false;
        }

    }
}
