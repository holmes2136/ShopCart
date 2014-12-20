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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;
using System.IO;

public partial class Admin_Components_StoreConfig_StoreProfileConfig : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private string LanguageID
    {
        get
        {
            return AdminConfig.CurrentContentCultureID;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    public void PopulateControls()
    {
        uxBusinessProfile.CultureID = LanguageID;
        uxBusinessProfile.PopulateControl();
    }

    public void Update()
    {
        if (Page.IsValid)
        {
            uxBusinessProfile.Update();
        }
    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }
}
