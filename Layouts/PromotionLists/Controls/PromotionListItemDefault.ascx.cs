using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Layouts_PromotionLists_Controls_PromotionListItemDefault : 
    BaseProductListItemUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "TellAFriendEnabled" ))
            uxTellFriendLinkButton.Visible = false;
    }

    protected void uxTellFriendLinkButton_Command( object sender, CommandEventArgs e )
    {
        string PromotionGroupID = e.CommandArgument.ToString();
        PromotionGroup group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( 
            StoreContext.Culture, 
            StoreContext.CurrentStore.StoreID, 
            PromotionGroupID, 
            BoolFilter.ShowTrue );
        string url = UrlManager.GetPromotionTellFriendUrl( group.PromotionGroupID, group.UrlName );
        Response.Redirect( url );
    }

    protected void uxAddToCartImageButton_Command( object sender, CommandEventArgs e )
    {
        string promotionGroupID = e.CommandArgument.ToString();
        string urlName = e.CommandName.ToString();

        Response.Redirect( Vevo.UrlManager.GetPromotionUrl( promotionGroupID, urlName ) );
    }

    public string GetImageUrl( object promotionGroupID )
    {
        string imageUrl = "";

        PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne(
            StoreContext.Culture,
            StoreContext.CurrentStore.StoreID,
            (string) promotionGroupID,
            BoolFilter.ShowAll );
        if (String.IsNullOrEmpty( promotionGroup.ImageFile ) || promotionGroup.ImageFile.Equals( "~/" ))
        {
            imageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
        }
        else
        {
            imageUrl = "~/" + promotionGroup.ImageFile;
        }

        return imageUrl;
    }

    protected static bool IsCatalogMode()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "IsCatalogMode" ))
            return true;
        else
            return false;
    }
}
