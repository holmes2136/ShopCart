using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.WebUI;
using Vevo.Domain;

public partial class PromotionGroupList : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private void PopulateControls()
    {
    }

    private void PopulateUserControl()
    {
        UserControl promotionGroupListControl = new UserControl();

        promotionGroupListControl = LoadControl( "~/Layouts/PromotionLists/PromotionListDefault.ascx" ) as UserControl;

        uxPromotionGroupListPanel.Controls.Add( promotionGroupListControl );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "EnableBundlePromo", StoreContext.CurrentStore ))
            Response.Redirect( "~/Default.aspx" );

        if (!IsPostBack)
        {
            PopulateControls();
        }

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }
}
