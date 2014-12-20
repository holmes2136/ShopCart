using System;
using Vevo;

public partial class Admin_MainControls_ProductKitGroupEdit : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxProductKitGroupDetailsAdd.SetEditMode();
    }
}
