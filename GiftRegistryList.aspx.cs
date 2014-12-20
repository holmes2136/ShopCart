using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.WebAppLib;
using Vevo.Domain;
using Vevo.Deluxe.Domain;

public partial class GiftRegistryList : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private void PopulateControls()
    {
        uxGrid.DataSource = DataAccessContextDeluxe.GiftRegistryRepository.GetAllByUserName(
            Membership.GetUser().UserName, DataAccessContext.StoreRetriever.GetCurrentStoreID() );
        uxGrid.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddGiftRegistryImageButton_OnClick( object sender, EventArgs e )
    {
        Response.Redirect( "GiftRegistryAdd.aspx" );
    }

    protected void uxGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        string giftRegistryID = uxGrid.DataKeys[e.RowIndex].Value.ToString();
        DataAccessContextDeluxe.GiftRegistryRepository.Delete( giftRegistryID );

        uxStatusHidden.Value = "Deleted";
        PopulateControls();
    }

    public bool IsVisible( string giftRegistryID )
    {
        return DataAccessContextDeluxe.GiftRegistryItemRepository.CanBeDeletedByGiftRegistryID( giftRegistryID );
    }

    public string GetDate( object eventDate )
    {
        DateTime date = Convert.ToDateTime( eventDate );
        return date.ToShortDateString();
    }
}
