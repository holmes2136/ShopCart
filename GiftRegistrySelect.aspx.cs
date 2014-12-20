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
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;

public partial class GiftRegistrySelect : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private void PopulateControls()
    {
        IList<GiftRegistry> giftRegistryList = DataAccessContextDeluxe.GiftRegistryRepository.GetAllByUserName(
            Membership.GetUser().UserName, DataAccessContext.StoreRetriever.GetCurrentStoreID() );
        string radioText, radioValue;
        string eventDate;
        for (int i = 0; i < giftRegistryList.Count; i++)
        {
            eventDate = string.Format( "{0:dd} {0:MMM} {0:yyyy}", giftRegistryList[i].EventDate );
            radioText = giftRegistryList[i].EventName + " (" + eventDate + ")";
            radioValue = giftRegistryList[i].GiftRegistryID;
            uxGiftRegistryRadioList.Items.Add( new ListItem( radioText, radioValue ) );
        }
    }

    private void AddItemToGiftRegistryAndRedirect( string giftRegistryID )
    {
        GiftRegistryItem.ConvertToGiftRegistryItem(StoreContext.ShoppingCart,
            StoreContext.Culture,
            StoreContext.Currency,
            giftRegistryID );

        StoreContext.ClearCheckoutSession();
        Session["GiftRegistryID"] = null;

        Response.Redirect( "GiftRegistryItemList.aspx?GiftRegistryID=" + giftRegistryID );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }

        if (StoreContext.ShoppingCart.GetCartItems().Length == 0)
        {
            uxPanalGiftRegistry.Visible = false;
            uxNoItemLabel.Text = "<br />Cannot add to a gift registry. There is no item in your shopping cart.<br /><br />";
        }
        else if (uxGiftRegistryRadioList.Items.Count == 0)
        {
            uxPanalGiftRegistry.Visible = false;
            uxNoItemLabel.Text = "<br />There is no gift registry belonging to your account.<br /><br />";
        }
        else if (Session["GiftRegistryID"] != null)
        {
            AddItemToGiftRegistryAndRedirect( Session["GiftRegistryID"].ToString() );
        }
        else if (uxGiftRegistryRadioList.Items.Count == 1)
        {
            AddItemToGiftRegistryAndRedirect( uxGiftRegistryRadioList.Items[0].Value );
        }
    }

    protected void uxAddItemImageButton_Click( object sender, EventArgs e )
    {
        AddItemToGiftRegistryAndRedirect( uxGiftRegistryRadioList.SelectedValue );
    }
}
