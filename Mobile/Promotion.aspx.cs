using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_Promotion : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void PopulateUserControl()
    {
        UserControl catalogControl = new UserControl();
        catalogControl = LoadControl( "Components/PromotionList.ascx" ) as UserControl;

        uxMobilePromotionListControlPanel.Controls.Add( catalogControl );
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateUserControl();
    }
}
