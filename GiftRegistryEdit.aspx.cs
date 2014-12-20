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
using Vevo.Domain;
using Vevo;
using Vevo.Deluxe.Domain;

public partial class GiftRegistryEdit : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private bool IsValidUserName( string userName )
    {
        return Page.User.Identity.IsAuthenticated &&
            String.Compare( userName, Page.User.Identity.Name, true ) == 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        uxGiftRegistryDetail.SetEditMode();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        string userName = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( Request.QueryString["GiftRegistryID"] ).UserName;
        if (!IsValidUserName( userName ))
        {
            uxGiftRegistryDetail.Visible = false;
            uxErrorLiteral.Visible = true;
        }
    }
}
