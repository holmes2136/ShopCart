using System;
using Vevo;

public partial class Admin_MainControls_BannerEdit : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxDetails.SetEditMode();
    }
}
