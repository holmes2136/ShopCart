using System;
using Vevo;

public partial class Admin_MainControls_ExportCategoryList : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            uxExportDataList.SetTabIndex( 3 );
    }
}
