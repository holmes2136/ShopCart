using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Components_EBayInternationalShippingCountryDrop : System.Web.UI.UserControl
{
    public string SelectedValue
    {
        get
        {
            return uxEBayInternationalShipToDrop.SelectedValue;
        }
        set
        {
            uxEBayInternationalShipToDrop.SelectedValue = value;
        }
    }

    public void PopulateControls( bool displayNone )
    {
        if (!displayNone)
        {
            uxEBayInternationalShipToDrop.Items.Remove( "None" );
        }
        uxEBayInternationalShipToDrop.SelectedIndex = 0;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
}
