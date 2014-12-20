using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MainControls_GoogleSpecMappingEdit : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxGoogleSpecMappingDetails.SetEditMode();
    }
}
